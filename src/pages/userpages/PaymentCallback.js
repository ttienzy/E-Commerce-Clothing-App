import { useLocation,Link } from "react-router-dom"
import '../../css/PaymentCallback.css'

const PaymentCallback = () => {

    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);

    const order_id = queryParams.get('order_id')
    const amount = queryParams.get('amount')
    const order_info = queryParams.get('order_info')

    return(
        <div className="success-card">
            <div className="success-icon">
                <i className="fas fa-check"></i>
            </div>
            <h1 className="success-title">Thanh toán thành công</h1>
            <div className="order-details">
                <div className="order-item">
                    <i className="fas fa-receipt"></i>
                    <span className="order-label">Order ID:</span>
                    <strong className="order-value">{order_id}</strong>
                </div>
                <div className="order-item">
                    <i className="fas fa-money-bill-wave"></i>
                    <span className="order-label">Amount:</span>
                    <strong className="order-value">{amount}</strong>
                </div>
                <div className="order-item">
                    <i className="fas fa-info-circle"></i>
                    <span className="order-label">Order Info:</span>
                    <strong className="order-value">{order_info}</strong>
                </div>
            </div>
            <Link to="/user/" className="back-link">
                <i className="fas fa-arrow-left"></i>
                Trở về trang chủ
            </Link>
        </div>
    )
}

export default PaymentCallback;