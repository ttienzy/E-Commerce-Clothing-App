import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { jwtDecode} from 'jwt-decode';
import '../css/Login.css'

const Login = () => {

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorSubmit, setErrorSubmit] = useState('')
    const navigate = useNavigate();


    const handleSubmit = async (e) => {
        e.preventDefault();
        
        try {
            const response = await fetch('https://localhost:7221/api/Authentication/LogInUser', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ email, password }),
                mode: 'cors',
                credentials: 'include',
            });
    
            if (!response.ok) {
                const errorData = await response.json();
                setErrorSubmit(errorData.errorMessage)
                return;
            }
            const data = await response.json();
            if(email === 'tiennd.23ce@vku.udn.vn'){
                localStorage.setItem('adminToken', data.accessToken);
                localStorage.setItem('admin_id', data.client_id);
            }else{
                localStorage.setItem('accessToken', data.accessToken);
                localStorage.setItem('client_id', data.client_id);
            }
            
            const decoded = jwtDecode(data.accessToken);
            navigate(`/${decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"].toLowerCase()}/`);
        } catch (err) {
            console.log('Fetch to failed',err);
        }
    }
    

    return (
        <div className="auth-section">
            <div className="auth-container">
                <h2 className="auth-title">Đăng nhập</h2>
                <form className="auth-form" onSubmit={handleSubmit}>
                    <div className="form-group">
                        <input 
                            type="email"
                            className="form-input"
                            placeholder="Email"
                            onChange={(e) => setEmail(e.target.value)}
                            onClick={() => setErrorSubmit('')}
                        />
                    </div>
                    <div className="form-group">
                        <input 
                            type="password"
                            className="form-input"
                            placeholder="Mật khẩu"
                            onChange={(e) => setPassword(e.target.value)}
                            onClick={() => setErrorSubmit('')}
                        />
                    </div>
                    <div className="form-group">
                        <span className='error-form'>{errorSubmit}</span>
                    </div>
                    <button type="submit" className="auth-button">Đăng nhập</button>
                </form>
                <div className="auth-links">
                    <a href="/register" className="auth-link">
                        <i className="fas fa-user-plus"></i>
                        Đăng ký
                    </a>
                    <a href="/forgot-password" className="auth-link">
                        <i className="fas fa-key"></i>
                        Quên mật khẩu?
                    </a>
                </div>
            </div>
        </div>
    )
}

export default Login;