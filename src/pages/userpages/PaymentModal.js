import { useContext, useState } from 'react';
import '../../css/PaymentModel.css';
import { jwtDecode } from "jwt-decode";
import { ContextTrade } from './ProductCategories';
import { useNavigate } from 'react-router-dom';

const PaymentModal = ({ product, onClose }) => {
    const [quantity, setQuantity] = useState(1);
    const decoded = jwtDecode(localStorage.getItem('accessToken'));
    const setTrade = useContext(ContextTrade);
    const navigate = useNavigate();
    const calculateDiscountedPrice = (originalPrice, discount) => {
        return Math.floor(originalPrice - (originalPrice * discount / 100));
    };
    
    const formatPrice = (price) => {
        return new Intl.NumberFormat('vi-VN').format(price) + 'đ';
    };

    const calculateTotal = () => {
        const price = product.discountPercent > 0 
            ? calculateDiscountedPrice(product.price, product.discountPercent)
            : product.price;
        return price * quantity;
    };

    const handleQuantityChange = (newQuantity) => {
        if (newQuantity >= 1 && newQuantity <= product.quantity) {
            setQuantity(newQuantity);
        }
    };

    const handlePaymentLater = async () => {
        // Xử lý thanh toán ở đây
        try{
            const response = await fetch('https://localhost:7221/api/Provider/PurchaseProduct',{
                method : 'POST',
                headers : {
                    'Content-Type': 'application/json-patch+json',
                },
                body : JSON.stringify({
                    productId : product.id,
                    userId : localStorage.getItem('client_id'),
                    price : product.price,
                    quatity : quantity
                }) 
            })
            if(!response.ok){
                alert('Có lỗi xảy ra khi thực hiện');
                return;
            }
            else{
                var result = await response.json()
                setTrade(pre => pre + 1)
                //alert("success");
                window.location.reload()
                
            }

            console.log(result.url)
        }
        catch(err){
            console.log(err.message);
            alert('Có lỗi xảy ra khi thực hiện thanh toán');
        }
    };
    const handlePayment = async () => {
        try{
            const response = await fetch('https://localhost:7221/api/Home',{
                method : 'POST',
                headers : {
                    'Content-Type': 'application/json-patch+json',
                    'Authorization': `Bearer ${localStorage.getItem('accessToken')}`
                },
                body : JSON.stringify({
                    productId : product.id,
                    userId :  decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
                    amount : product.price,
                    quantity : quantity
                }) 
            })
            if(!response.ok){
                throw new Error('Payment initiation failed');
            }
            else{
                var result = await response.json()
                
                
                // Chuyển hướng đến trang thanh toán
                localStorage.setItem('returnUrl', '/user/');
                window.location.href = result.url;
            }

            console.log(result.url)
        }
        catch(err){
            console.log(err.message);
            alert('Có lỗi xảy ra khi thực hiện thanh toán');
        }
    }
    //---------------------

    return (
        <div className="modal-overlay" onClick={onClose}>
            <div className="modal-content" onClick={e => e.stopPropagation()}>
                <div className="modal-header">
                    <h3>Xác nhận đơn hàng</h3>
                    <button className="close-button" onClick={onClose}>&times;</button>
                </div>
                <div className="modal-body">
                    <div className="product-brief">
                        <img src={product.imageProducts} alt={product.name} />
                        <div className="product-info">
                            <h4>{product.name}</h4>
                            <p className="price">
                                {product.discountPercent > 0 ? (
                                    <>
                                        <span className="original-price" style={{fontSize:'12px'}}>{formatPrice(product.price)}</span>
                                        <span className="final-price">
                                            {formatPrice(calculateDiscountedPrice(product.price, product.discountPercent))}
                                        </span>
                                    </>
                                ) : (
                                    <span   className="final-price" 
                                            >{formatPrice(product.price)}</span>
                                )} {product.discountPercent > 0 ? (
                                    <span style={{
                                                backgroundColor : '#feeeea',
                                                color:'#ee4d2d',
                                                fontWeight : '400',
                                                marginLeft : '12px',
                                                padding: '0 4px'
                                            }}>{product.discountPercent*100}%</span>
                                ) : ""}
                            </p>
                            <p>Mô tả :</p>
                            <textarea style={{
                                
                                resize:'auto',
                                border : '0.1px solid lightblue',
                                borderRadius : '4px',
                                padding : '0 4px',
                                outline : "none"
                            }}>...</textarea>
                            <p style={{fontSize:'14px'}}>Ship : <img src='https://deo.shopeemobile.com/shopee/shopee-pcmall-live-sg/productdetailspage/d9e992985b18d96aab90.png' alt='png' style={{display:'inline-block',width:'24px',height:'24px'}}/> Miễn phí giao hàng</p>
                            <p style={{fontSize:'14px'}}>Ship to : {localStorage.getItem('address')}</p>
                        </div>
                    </div>
                    
                    <div className="quantity-selector">
                        <span>Số lượng:</span>
                        <div className="quantity-controls">
                            <button onClick={() => handleQuantityChange(quantity - 1)} style={{borderRadius : '50%'}}>-</button>
                            <input 
                                type="number" 
                                value={quantity} 
                                onChange={(e) => handleQuantityChange(parseInt(e.target.value))}
                                style={{borderRadius : '4px'}}
                                min="1"
                                max={product.quantity}
                            />
                            <button onClick={() => handleQuantityChange(quantity + 1)} style={{borderRadius : '50%'}}>+</button>
                        </div>
                    </div>

                    <div className="total-section">
                        <span>Tổng tiền:</span>
                        <span className="total-price">{formatPrice(calculateTotal())}</span>
                    </div>
                </div>

                <div className="modal-footer">
                    <button className="cancel-button" onClick={onClose}><i class="fa-solid fa-angles-left"></i></button>
                    <button className="payment-button" onClick={handlePaymentLater}>
                        Mua
                    </button>
                    <button className="payment-button" onClick={handlePayment}>
                        Thanh toán Online
                    </button>
                </div>

            </div>
        </div>
    );
};

export default PaymentModal;