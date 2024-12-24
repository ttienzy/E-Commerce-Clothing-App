import React, { useEffect, useState, useCallback } from 'react';
import { useNavigate } from "react-router-dom";
import '../../css/ProductReview.css'

const ProductReview = () => {
    const [orderPreview, setOrderPreview] = useState([]);
    const [reviews, setReviews] = useState({});
    const [submittedOrders, setSubmittedOrders] = useState({});
    const [productComments, setProductComments] = useState({}); // Change to object to store comments per product
    const [visibleComments, setVisibleComments] = useState({}); // Track which products have visible comments
    const client_id = localStorage.getItem('client_id');
    const navigate = useNavigate();
    const dateFormatter = new Intl.DateTimeFormat('vi-VN', {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit',
        hour12: false
    });

    const fetchOrders = useCallback(async () => {
        try {
            const response = await fetch(`https://localhost:7221/api/Orders/OrderPreview?client_id=${client_id}`, {
                method: 'GET'
            });
            if (!response.ok) {
                console.error('fetch failed');
                return;
            }
            const results = await response.json();
            setOrderPreview(results);

            const initialReviews = {};
            results.forEach(order => {
                initialReviews[order.orderId] = {
                    rating: 0,
                    hover: 0,
                    review: '',
                };
            });
            setReviews(initialReviews);
        } catch (error) {
            console.error('Error fetching orders:', error);
        }
    }, [client_id]);

    useEffect(() => {
        fetchOrders();
    }, [fetchOrders]);

    const handleComments = async (productName) => {
        try {
            // Toggle visibility
            setVisibleComments(prev => ({
                ...prev,
                [productName]: !prev[productName]
            }));

            // If we already have the comments, no need to fetch again
            if (productComments[productName]) return;

            const response = await fetch(`https://localhost:7221/api/Product/NumberReview?nameProduct=${productName}`, {
                method: 'GET',
            });
            if (!response.ok) {
                alert('fetch failed');
                return;
            }
            
            const results = await response.json();
            setProductComments(prev => ({
                ...prev,
                [productName]: results
            }));
        } catch (error) {
            alert('Error fetching comments:', error);
        }
    };

    const handleSubmitReview = async (orderId) => {
        if (submittedOrders[orderId]) return;

        try {
            const orderReview = reviews[orderId];
            if (!orderReview) return;

            setSubmittedOrders(prev => ({
                ...prev,
                [orderId]: true
            }));

            const response = await fetch(
                `https://localhost:7221/api/Orders/OrderReview?order_id=${orderId}&des=${encodeURIComponent(orderReview.review)}`,
                {
                    method: 'PUT',
                    headers: {
                        'Accept': '*/*',
                    },
                }
            );

            if (!response.ok) {
                setSubmittedOrders(prev => ({
                    ...prev,
                    [orderId]: false
                }));
                throw new Error('Failed to update order review');
            }
            //navigate('/user/product_review/')
        } catch (error) {
            console.error('Error submitting review:', error);
            setSubmittedOrders(prev => ({
                ...prev,
                [orderId]: false
            }));
        }
    };

    const updateReview = useCallback((orderId, field, value) => {
        setReviews(prev => ({
            ...prev,
            [orderId]: {
                ...prev[orderId],
                [field]: value
            }
        }));
    }, []);

    const renderStar = useCallback((orderId, index) => {
        const { rating, hover } = reviews[orderId] || { rating: 0, hover: 0 };
        return (
            <svg
                key={index}
                className={`star ${index <= (hover || rating) ? 'active' : ''}`}
                onClick={() => updateReview(orderId, 'rating', index)}
                onMouseEnter={() => updateReview(orderId, 'hover', index)}
                onMouseLeave={() => updateReview(orderId, 'hover', rating)}
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="currentColor"
                stroke="currentColor"
            >
                <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z" />
            </svg>
        );
    }, [reviews, updateReview]);

    function formatDate(dateString) {
        const parts = dateFormatter.formatToParts(new Date(dateString));
        const values = {};
        parts.forEach(part => {
            values[part.type] = part.value;
        });
        return `${values.day}/${values.month}/${values.year}`;
    }

    return (
        <div className="review-card">
            <h1 className="card-title">Đánh giá sản phẩm đã mua</h1>      
            {orderPreview.map((item) => {
                const orderReview = reviews[item.orderId] || { rating: 0, review: '' };
                const isSubmitted = submittedOrders[item.orderId];
                const productCommentsData = productComments[item.productName] || [];
                const isCommentsVisible = visibleComments[item.productName];

                return (
                    <div key={item.orderId} style={{
                        padding : '12px',
                        marginBottom : '48px',
                        borderRadius : '12px',
                        boxShadow : '0 2px 4px rgba(0,0,0,0.52)',
                        backgroundColor : 'white'
                    }}>
                        <div className="product-info">
                            <div className="info-row">
                                <span className="info-label">Mã sản phẩm:</span>
                                <span>{item.orderId}</span>
                            </div>
                            <div className="info-row">
                                <span className="info-label">Tên sản phẩm:</span>
                                <span>{item.productName}</span>
                            </div>
                            <div className="info-row">
                                <span className="info-label">Số lượng:</span>
                                <span>{item.quantity}</span>
                            </div>
                            <div className="info-row">
                                <span className="info-label">Đơn giá:</span>
                                <span>{item.totalMoney.toLocaleString('vi-VN')}đ</span>
                            </div>
                            <div className="info-row">
                                <span className="info-label">Ngày mua:</span>
                                <span>{new Date(item.createdAt).toLocaleDateString('vi-VN')}</span>
                            </div>
                        </div>
                        <div>
                            <button 
                                onClick={() => handleComments(item.productName)}
                                className="text-gray-900 bg-white border border-gray-300 focus:outline-none hover:bg-gray-100 focus:ring-4 focus:ring-gray-100 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:hover:border-gray-600 dark:focus:ring-gray-700"
                            >
                                {isCommentsVisible ? 'Ẩn đánh giá' : 'Xem đánh giá'} <i className="fa-solid fa-angle-down"></i>
                            </button>
                        </div>
                        
                        {isCommentsVisible && (
                            <div className="mx-auto space-y-6 p-4">
                                <h2 className="text-xl font-semibold text-gray-900 mb-6">
                                    Bình luận ({productCommentsData.length})
                                </h2>
                                
                                {productCommentsData.map((comment, index) => (
                                    <div key={index} className="flex items-start space-x-4 bg-white rounded-lg shadow-sm p-4 hover:shadow-md transition duration-200">
                                        <div className="flex-shrink-0">
                                            <img
                                                src="https://cdn.pixabay.com/photo/2016/03/31/20/37/client-1295901_1280.png"
                                                alt={comment.userName}
                                                className="h-10 w-10 rounded-full object-cover bg-gray-100"
                                            />
                                        </div>
                                        <div className="flex-1 min-w-0">
                                            <div className="flex items-center space-x-2">
                                                <h3 className="text-sm font-medium text-gray-900 truncate">
                                                    {comment.userName}
                                                </h3>
                                                <span className="text-xs text-gray-500">
                                                    {formatDate(comment.dateCreated)}
                                                </span>
                                            </div>
                                            <p className="mt-1 text-sm text-gray-700 whitespace-pre-line">
                                                {comment.description}
                                            </p>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        )}

                        {!isSubmitted ? (
                            <>
                                <div className="rating-section">
                                    <label className="rating-label">Đánh giá của bạn:</label>
                                    <div className="stars">
                                        {[1, 2, 3, 4, 5].map((index) => renderStar(item.orderId, index))}
                                    </div>
                                </div>

                                <div className="review-section">
                                    <label className="rating-label">Nhận xét của bạn:</label>
                                    <textarea
                                        className="review-textarea"
                                        value={orderReview.review}
                                        onChange={(e) => updateReview(item.orderId, 'review', e.target.value)}
                                        placeholder="Chia sẻ trải nghiệm của bạn về sản phẩm..."
                                    />
                                </div>

                                <button 
                                    className="submit-btn"
                                    onClick={() => handleSubmitReview(item.orderId)}
                                    disabled={!orderReview.rating || !orderReview.review.trim()}
                                >
                                    Gửi đánh giá
                                </button>
                            </>
                        ) : (
                            <div className="success-message">
                                Cảm ơn bạn đã gửi đánh giá!
                            </div>
                        )}
                    </div>
                );
            })}
        </div>
    );
};

export default ProductReview;