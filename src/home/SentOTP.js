import { Link,useNavigate } from 'react-router-dom';
import { useState,useEffect } from 'react';
import '../css/SentOTP.css'

const SentOTP = () => {
    const nagivate = useNavigate();
    const [otp, setOTP] = useState('');
    const [timer, setTimer] = useState(120);
    const [error, setError] = useState("");
    
    useEffect(() => {
        const interval = setInterval(() => {
            setTimer((pre) => pre - 1)
        },1000)

        if(timer === 0){
            nagivate('/login/')
        }

        return () => clearInterval(interval);
        }, [timer,nagivate]);
        const formatTime = (time) => {
        const minutes = Math.floor(time / 60);
        const seconds = time % 60;
        return `${minutes}:${seconds < 10 ? `0${seconds}` : seconds}`;
    };
    const handleSubmit = async (e) => {
        e.preventDefault();
        
        try {
            const response = await fetch(`https://localhost:7221/api/Authentication/SentOTP`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body : JSON.stringify(otp)
            });
        
            if (!response.ok) {
                setError('Sai mã OTP')
                return;
            } 
            nagivate('/resetpassword');
        } catch (error) {
            console.error('Network error:', error);
        }
    }

    return (
        <div className="auth-page">
            <div className="auth-container otp-container">
                <h2 className="auth-title">Nhập mã xác nhận</h2>
                <div className="otp-description">
                Vui lòng nhập mã OTP đã được gửi đến email của bạn
                </div>

                <form className="auth-form" onSubmit={handleSubmit}>
                    <div className="form-group">
                        <div className="otp-input-container">
                            <input
                                type="text"
                                className="form-input otp-input"
                                placeholder="Enter OTP"
                                maxLength="6"
                                onChange={(e) => setOTP(e.target.value)}
                                onClick={() => setError('')}
                            />
                            <span className='error-otp'>{error}</span>
                            <div className="otp-timer">
                                Mã còn hiệu lực: <span>{formatTime(timer)}</span>
                            </div>
                        </div>
                    </div>

                    <button type="submit" className="auth-button">
                        Xác nhận
                    </button>
                </form>

                <div className="otp-actions">
                <span className="otp-hint">Không nhận được mã?</span>
                <button className="resend-button">Gửi lại mã</button>
                </div>

                <Link to="/forgot-password" className="back-link">
                <i className="fas fa-arrow-left"></i>
                <span>Quay lại</span>
                </Link>
            </div>
        </div>
    );
};
export default SentOTP;