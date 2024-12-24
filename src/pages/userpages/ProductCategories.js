import { createContext, useEffect, useState } from "react";
import '../../css/ProductCategoties.css'
import PaymentModal from './PaymentModal';
import { useNavigate } from "react-router-dom";

const ContextTrade = createContext();
const ProductCategoties = () => {

    
    const [trade, setTrade] = useState(0);
    const navigate = useNavigate();
    const [products, setProduct] = useState([]);
    const [categories, setCategory] = useState([]);
    const [showProduct, setSHowProducts] = useState([])
    const [selectedProduct, setSelectedProduct] = useState(null);
    const [count, setCount] = useState(0);
    useEffect(() => {
        const fetchCategory = async () => {
            try{
                const response = await fetch(`https://localhost:7221/api/Category/ListCategory`,{
                    method : 'GET'
                });
                if(!response.ok){
                    throw new Error('Network response was not ok');
                }
                var results = await response.json();
                setCategory(results)
            }
            catch(err){
                console.error(err.message)
            }
        }
        fetchCategory();   
        },[]);

    useEffect(() => {
        const fetchData = async () => {
            try{
                const response = await fetch(`https://localhost:7221/api/Product/ListProductInclude`,
                    {
                        method: 'GET'
                    }
                );
                if(!response.ok){
                    throw new Error('Network response was not ok');
                }
                var results = await response.json();
                setProduct(results);
                setSHowProducts(results)
            }
            catch(err){
                console.error(err.message);
            }
        }
        fetchData();
    },[]);
    useEffect(() => {
        const fetchData = async () => {
            try{
                const response = await fetch(`https://localhost:7221/api/Cart/GetCount?id=${localStorage.getItem('client_id')}`,
                    {
                        method: 'GET'
                    }
                );
                if(!response.ok){
                    throw new Error('Network response was not ok');
                }
                var results = await response.json();
                setCount(results)
            }
            catch(err){
                console.error(err.message);
            }
        }
        fetchData();
    },[count]);
    useEffect(() => {
        const fetchData = async () => {
            try{
                const response = await fetch(`https://localhost:7221/api/Provider/GetCountUnPaid?idUser=${localStorage.getItem('client_id')}`,
                    {
                        method: 'GET'
                    }
                );
                if(!response.ok){
                    throw new Error('Network response was not ok');
                }
                var results = await response.json();
                setTrade(results)
            }
            catch(err){
                console.error(err.message);
            }
        }
        fetchData();
    },[trade]);
    const handleAddToCart = async (productId) => {
        console.log(productId)
        console.log(localStorage.getItem('client_id'))
        try{
            const response = await fetch('https://localhost:7221/api/Cart',{
                method: "POST",
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    userId: localStorage.getItem('client_id'),
                    productId: productId
                })
            })
            if(!response.ok){
                alert('Fetch to faild')
            }
        }
        catch(err){
            alert('Fetch to faild///',err)
        }
        setCount(pre => pre+1)
    }

    const handleFilterByCategory = (e) => {
        var results = products.filter((item) => item.categoryId === e.target.value)
        setSHowProducts(results)
    }
    const handleSortByPrice = (e) => {
        if(e.target.value === 'asc'){
            
            var templateAsending = showProduct.slice().sort((a,b) => a.price - b.price);
            setSHowProducts(templateAsending);
        }
        if(e.target.value === 'desc'){
            var templateDesending = showProduct.slice().sort((a,b) => -a.price + b.price);
            setSHowProducts(templateDesending);
        }
    }
    const handleSearchByName = (e) => {
        const search = e.target.value;
        if (!search) {
            setSHowProducts(products);
        } else {
            const normalizedSearch = removeVietnameseTones(search.toLowerCase());
            const filterProduct = products.filter((item) => {
                const normalizedName = removeVietnameseTones(item.name.toLowerCase());
                return normalizedName.includes(normalizedSearch);
            });
            setSHowProducts(filterProduct);
        }
    }
    const removeVietnameseTones = (str) => {
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        // Một số bộ ký tự có dấu khác
        str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
        str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
        str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
        str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
        str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
        str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
        str = str.replace(/Đ/g, "D");
        return str;
    }
    const handleNavigateToCart = () => {
        navigate('/user/cart-user/')
    }
    const formatPrice = (price) => {
        return new Intl.NumberFormat('vi-VN').format(price) + 'đ';
    };
    const calculateDiscountedPrice = (originalPrice, discount) => {
        return Math.floor(originalPrice - (originalPrice * discount / 100));
    }
    /**---------------------------------------PaymentModel--------------------------- */
    
    return (
        
        <div className="products-container">
            {/* <h1 className="page-title">Order Product is here !</h1> */}
            
            <div className="filters-section">
                {/* Product Type Filter */}
                <div className="filter-group">
                    <h3 className="filter-title">Lọc theo loại</h3>
                    <select className="filter-select" defaultValue='0' onChange={handleFilterByCategory}>
                        <option value="0" hidden  disabled >Tất cả sản phẩm</option>
                        { categories.map((item) => (
                            <option key={item.id} value={item.id}>{item.name}</option>
                        )) }
                    </select>
                </div>

                {/* Price Filter */}
                <div className="filter-group">
                    <h3 className="filter-title">Lọc theo giá</h3>
                    <select className="filter-select" defaultValue="" onChange={handleSortByPrice}>
                        <option value="" disabled hidden>Mặc định</option>
                        <option value="asc">Giá tăng dần</option>
                        <option value="desc">Giá giảm dần</option>
                    </select>
                </div>

                {/* Search */}
                <div className="filter-group">
                    <h3 className="filter-title">Tìm kiếm</h3>
                    <input 
                        type="search" 
                        className="search-input"
                        placeholder="Nhập tên sản phẩm..."
                        onChange={handleSearchByName}
                    />
                </div>
                {/* Cart */}
                <div className="filter-group">
                    <h3 className="filter-title">Giỏ hàng</h3>
                    <div>
                    <i  onClick={handleNavigateToCart}
                        className="fa-solid fa-cart-shopping"
                        style={{
                            width : '45px'
                        }}
                    ></i>
                    <i  className="fa-solid fa-truck-fast"
                        onClick={() => navigate('/user/order_history/')}
                    ></i>
                    {/* <i className="fa-solid fa-bell"></i> */}
                    </div>
                    <span className="number-of-cart" >{count}</span>
                    <span className="number-trade-order" >{trade}</span>
                </div>
            </div>

            <div className="products-grid">
                {showProduct?.length > 0 ? (
                    showProduct.map(item => (
                    <div key={item.id} className="product-card">
                        <div className="product-image">
                            <img src={item.imageProducts} alt={item.name} />
                            {item.discountPercent > 0 && (
                            <div className="discount-badge">
                                -{item.discountPercent*100}%
                            </div>
                            )}
                        </div>
                        <div className="product-info">
                            <h3 className="product-name">{item.name}</h3>
                            <p className="product-quantity">Số lượng: {item.quantity}</p>
                            <div className="price-container">
                            {item.discountPercent > 0 ? (
                                <>
                                <span className="original-price">{formatPrice(item.price)}</span>
                                <span className="discounted-price">
                                    {formatPrice(calculateDiscountedPrice(item.price, item.discountPercent))}
                                </span>
                                </>
                            ) : (
                                <span className="regular-price">{formatPrice(item.price)}</span>
                            )}
                            </div>
                            <button className="add-to-cart-btn" onClick={() => setSelectedProduct(item)}>Chi tiết</button>
                            <button className="add-to-cart-btn a-t-c" onClick={() => handleAddToCart(item.id)}>Thêm vào giỏ hàng</button>
                        </div>
                    </div>
                ))
                ) : (
                <p className="no-products">No products available.</p>
                )}
            </div>
            {selectedProduct && ( 
                <ContextTrade.Provider value={setTrade}>
                    <PaymentModal 
                        product={selectedProduct}
                        onClose={() => setSelectedProduct(null)}
                    />
                </ContextTrade.Provider>
            )}
        </div>
    )
}

export default ProductCategoties;
export {ContextTrade};