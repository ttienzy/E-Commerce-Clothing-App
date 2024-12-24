import { useState } from "react";   
import { useNavigate } from "react-router-dom";
import '../css/ResetPassword.css'
const ResetPassword = () => {
    const nagivate = useNavigate();
    const [password, setPassword] = useState('')

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch(`https://localhost:7221/api/Authentication/ResetPassword`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body : JSON.stringify(password)
            });
        
            if (response.ok) {
            nagivate('/login');
            } else {
                const errorData = await response.json();
                alert('Error registering user:', errorData);
            }
        } catch (error) {
            console.error('Network error:', error);
        }
        nagivate('/login');
    }
    return(
        <div className="auth-page">
            <div className="auth-container reset-container">
                <h2 className="auth-title">Nhập mật khẩu mới</h2>
                <div className="reset-description">
                Vui lòng nhập mật khẩu mới cho tài khoản của bạn
                </div>

                <form className="auth-form" onSubmit={handleSubmit}>
                    <div className="form-group">
                        <div className="password-requirements">
                            <p className="requirement-title">Mật khẩu cần có:</p>
                            <ul className="requirement-list">
                                <li>Ít nhất 8 ký tự</li>
                                <li>Chữ hoa và chữ thường</li>
                                <li>Ít nhất 1 số</li>
                                <li>Ít nhất 1 ký tự đặc biệt</li>
                            </ul>
                        </div>
                        <div className="password-input-container">
                            <input
                                type="password"
                                className="form-input password-input"
                                placeholder="Nhập mật khẩu mới"
                                onChange={(e) => setPassword(e.target.value)}
                            />
                        </div>
                    </div>
                    <div className="form-group">
                        <div className="password-input-container">
                        <input
                            type="password"
                            className="form-input password-input"
                            placeholder="Xác nhận mật khẩu mới"
                            // onChange={(e) => setConfirmPassword(e.target.value)}
                        />
                        </div>
                    </div>

                    <button type="submit" className="auth-button">
                        Xác nhận
                    </button>
                </form>
            </div>
        </div>
    )
}


export default ResetPassword;