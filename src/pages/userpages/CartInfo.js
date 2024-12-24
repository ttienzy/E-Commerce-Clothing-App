import React, { useState, useEffect } from 'react';
import '../../css/Cart.css';
import { jwtDecode } from "jwt-decode";


const CartInfo = () => {
  const decoded = jwtDecode(localStorage.getItem('accessToken'));
  const [cartItems, setCartItems] = useState([]);
  const [selectedQuantities, setSelectedQuantities] = useState({});
  const [discountCode, setDiscountCode] = useState('');
  const [discountError, setDiscountError] = useState('');

  useEffect(() => {
    const initialQuantities = cartItems.reduce((acc, item) => {
      acc[item.id] = 1;
      return acc;
    }, {});
    setSelectedQuantities(initialQuantities);
  }, []);
  useEffect(() => {
    const fetchCart = async () => {
        try {
            const response = await fetch(`https://localhost:7221/api/Cart?userId=${localStorage.getItem('client_id')}`, {
                method: 'GET'
            });
            
            if (!response.ok) {
                alert("Fetch failed------");
            }
            
            const data = await response.json();
            setCartItems(data); // Sửa lại cách thêm data
        } catch (err) {
            alert("Fetch failed");
            console.error(err);
        }
    };

    fetchCart();
}, []);
  const formatPrice = (price) => {
    return new Intl.NumberFormat('vi-VN', {
      style: 'currency',
      currency: 'VND'
    }).format(price);
  };

  const calculateItemPrice = (item) => {
    const basePrice = item.price * (selectedQuantities[item.id] || 1);
    if (item.discountCode) {
      const discountPercent = item.discountCode || 0;
      return basePrice * (1 - discountPercent / 100);
    }
    return basePrice;
  };

  const calculateTotal = () => {
    return cartItems.reduce((total, item) => {
      return total + calculateItemPrice(item);
    }, 0);
  };

  const handleQuantityChange = (itemId, newQuantity) => {
    const item = cartItems.find(item => item.id === itemId);
    if (!item) return;
    const validQuantity = Math.min(Math.max(1, newQuantity), item.quantity);
    setSelectedQuantities(prev => ({
      ...prev,
      [itemId]: validQuantity
    }));
  };

  const handleRemoveItem = async (itemId) => {

    try {
      const response = await fetch(`https://localhost:7221/api/Cart?cartId=${itemId}`, {
          method: 'DELETE'
      });
      
      if (!response.ok) {
          alert("Fetch failed------");
      }
      
      setCartItems(prev => prev.filter(item => item.id !== itemId));
      setSelectedQuantities(prev => {
        const newQuantities = { ...prev };
        delete newQuantities[itemId];
        return newQuantities;
      });
  } catch (err) {
      alert("Fetch failed");
      console.error(err);
  }
  };
    const handlePayment = async (quantity) => {
      console.log(quantity)
      console.log(cartItems.reduce((sum, item) => sum + item.price, 0))
      // Xử lý thanh toán ở đây
      try{
          const response = await fetch('https://localhost:7221/api/Home',{
              method : 'POST',
              headers : {
                  'Content-Type': 'application/json-patch+json',
                  'Authorization': `Bearer ${localStorage.getItem('accessToken')}`
              },
              body : JSON.stringify({
                  productId : cartItems[0].productId,
                  userId :  decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
                  amount : cartItems[0].price,
                  quantity : quantity
              }) 
          })
          if(!response.ok){
              alert("Fetch to faild--")
              return;
          }
          var result = await response.json()
                
                
                // Chuyển hướng đến trang thanh toán
                localStorage.setItem('returnUrl', '/user/');
                window.location.href = result.url;
      }
      catch(err){
          console.log(err.message);
          alert('Có lỗi xảy ra khi thực hiện thanh toán');
      }
  };
  console.log(cartItems)
  return (
    <div className="cart-root-container">
      <h1>Giỏ hàng của bạn</h1>
      
      <div className="cart-header-grid">
        <div className="cart-product-info-header">Thông tin sản phẩm</div>
        <div className="cart-product-price-header">Đơn giá</div>
        <div className="cart-product-quantity-header">Số lượng</div>
        <div className="cart-product-total-header">Thành tiền</div>
        <div className="cart-product-action-header">Thao tác</div>
      </div>

      <div className="cart-items-list">
        {cartItems.map((item) => (
          <div className="cart-item-row" key={item.id}>
            <div className="cart-product-info-cell">
              <img src={item.imageProducts} alt={item.name} className="cart-product-image" />
              <div className="cart-product-details">
                <h3 className="cart-product-name">{item.name}</h3>
                <p className="cart-product-id">Mã SP: {item.productId}</p>
                <p className="cart-product-stock">Còn lại: {item.quantity} sản phẩm</p>
                {item.discountCode && (
                  <p className="cart-discount-badge">
                    Giảm {item.discountCode}%
                  </p>
                )}
              </div>
            </div>

            <div className="cart-product-price-cell">
              {formatPrice(item.price)}
            </div>

            <div className="cart-product-quantity-cell">
              <div className="cart-quantity-control">
                <button 
                  className="cart-quantity-btn"
                  onClick={() => handleQuantityChange(item.id, (selectedQuantities[item.id] || 1) - 1)}
                >
                  -
                </button>
                <input 
                  type="number"
                  className="cart-quantity-input"
                  min="1" 
                  max={item.quantity}
                  value={selectedQuantities[item.id] || 1}
                  onChange={(e) => handleQuantityChange(item.id, parseInt(e.target.value) || 1)}
                />
                <button 
                  className="cart-quantity-btn"
                  onClick={() => handleQuantityChange(item.id, (selectedQuantities[item.id] || 1) + 1)}
                >
                  +
                </button>
              </div>
            </div>

            <div className="cart-product-total-cell">
              {formatPrice(calculateItemPrice(item))}
            </div>

            <div className="cart-product-action-cell">
              <button 
                className="cart-delete-btn"
                onClick={() => handleRemoveItem(item.id)}
              >
                <i class="fa-solid fa-trash-can"></i>
              </button>
            </div>
          </div>
        ))}
      </div>

      {cartItems.length > 0 ? (
        <div className="cart-footer-section">
          <div className="cart-summary-box">
            <div className="cart-summary-row">
              <span>Tổng sản phẩm:</span>
              <span>
                {cartItems.length} sản phẩm
              </span>
            </div>
            <div className="cart-summary-row cart-total-row">
              <span>Tổng tiền:</span>
              <span>{formatPrice(calculateTotal())}</span>
            </div>
          </div>
          <button className="cart-checkout-btn" onClick={() => handlePayment(Object.values(selectedQuantities).reduce((a, b) => a + b, cartItems.length))}>Tiến hành thanh toán</button>
        </div>
      ) : (
        <div className="cart-empty-state">
          <p>Giỏ hàng của bạn đang trống</p>
        </div>
      )}
    </div>
  );
};

export default CartInfo;