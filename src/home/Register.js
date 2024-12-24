import { Link,useNavigate } from "react-router-dom";
import Province from "../address/Provinces";
import { createContext,useState } from "react";
import '../css/Register.css'

const UserAddress = createContext();
const Register = () => {
    const nagivate = useNavigate();
    const [address, setAddress] = useState({ province: '', district: '', ward: ''});
    const [otherInfo, setOtherInfo] = useState({ userName:'', phoneNumber:'', email:'', password:''});
    const [errors, setError] = useState([])

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch('https://localhost:7221/api/Authentication/RegisterUser', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                userName: otherInfo.userName,
                phoneNumber: otherInfo.phoneNumber,
                email: otherInfo.email,
                password: otherInfo.password,
                province: address.province,
                district: address.district,
                ward: address.ward
                })
            });
        
            if (!response.ok) {
                const errorResponse = await response.json();
                var ArrTemplate = [];
                for(const [, messages] of  Object.entries(errorResponse.errors))
                {
                    ArrTemplate.push(messages);
                }
                //console.log(ArrTemplate)
                setError(ArrTemplate)
                return;
            } 
            alert('success register')
            nagivate('/login')
        } catch (error) {
            console.error('Network error:', error);
        }
    }
    return(
        <div className="register-container">
            <h2 className="auth-title">Đăng ký</h2>
            <form className="auth-form" onSubmit={handleSubmit}>
                <div className="form-group">
                    <input 
                        type="text" 
                        className="form-input"
                        placeholder="Họ và tên" 
                        onChange={(e) => setOtherInfo((preList) => ({ ...preList, userName : e.target.value }))}
                        onClick={() => setError([])}
                        />
                    </div>
                <div className="form-group">
                    <input
                        type="email" 
                        className="form-input"
                        placeholder="Email" 
                        onChange={(e) => setOtherInfo((preList) => ({ ...preList, email : e.target.value }))}
                        onClick={() => setError([])}
                    />
                </div>
                <div className="form-group">
                    <input 
                        type="tel" 
                        className="form-input"
                        placeholder="Nhập số điện thoại" 
                        onChange={(e) => setOtherInfo((preList) => ({ ...preList, phoneNumber : e.target.value }))}
                        onClick={() => setError([])}
                    />
                </div>
                <div className="form-group">
                    <input 
                        type="password" 
                        className="form-input"
                        placeholder="Mật khẩu" 
                        onChange={(e) => setOtherInfo((preList) => ({ ...preList, password : e.target.value }))}
                        onClick={() => setError([])}
                    />
                </div>
                <div className="address-section">
                    <UserAddress.Provider value={setAddress}>
                        <Province />
                    </UserAddress.Provider>
                </div>
                <div className="form-group">
                    { (errors.length > 0) && errors.map(error => (
                        <span key={error} className="error-response">{error}</span>
                    )) }
                </div>
                <button type="submit" className="auth-button">Đăng ký</button>
            </form>
            <Link to="/login" className="back-link">
                <i className="fas fa-arrow-left"></i> Đăng nhập
            </Link>
            
        </div>
    )
}

export default Register;
export {UserAddress};