import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';

const ForgotPassword = () => {
    const nagivate = useNavigate();
    const [Email, setEmail] = useState('');
    const [error, setError] = useState('')

    const handleSubmit = async (e) => {
        e.preventDefault();
        
        try {
            const response = await fetch(`https://localhost:7221/api/Authentication/ForgotPassword?Email=${Email}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            });
            if (!response.ok) {
              const erronResponse = await response.json();
              setError(erronResponse.errorMessage || erronResponse.errors.Email[0]);
              return;
            } 
            nagivate('/sentotp');
        } catch (error) {
            alert('Fetch error:', error);
        }
    }

    return (
        <div className="auth-page">
      <div className="auth-container forgot-container">
        <h2 className="auth-title">Quên mật khẩu</h2>
        <p className="forgot-description">
          Vui lòng nhập email của bạn để nhận hướng dẫn đặt lại mật khẩu
        </p>

        <form className="auth-form" onSubmit={handleSubmit}>
          <div className="form-group">
            <label className="form-label">Email</label>
            <input
              type="email"
              className="form-input"
              placeholder="Nhập email của bạn"
              onChange={(e) => setEmail(e.target.value)}
              onClick={() => setError('')}
            />
            <span className='error-email' style={{fontSize:'small', color : 'red', marginLeft : '6px'}}>{error}</span>
          </div>

          <button type="submit" className="auth-button">
            Gửi yêu cầu
          </button>
        </form>

        <Link to="/login" className="back-link">
          <i className="fas fa-arrow-left"></i>
          <span>Quay lại đăng nhập</span>
        </Link>
      </div>
    </div>
    );
};
export default ForgotPassword;