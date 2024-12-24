import { Link, Outlet, useNavigate, } from "react-router-dom";
import '../../css/AdminPage.css'
import { useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";

const AdminPage = () => {

    const navigate = useNavigate();
    //const [username, setUserName] = useState('');
    const [render, setRerender] = useState(Math.random());
        const token = localStorage.getItem('adminToken');
        useEffect(() => {
        
        if (!token) {
            alert('Token không tồn tại. Vui lòng đăng nhập.');
            navigate('/login');
            return;
        }

        try {
            const decoded = jwtDecode(token);

            const userRole = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            if (userRole !== 'Admin') {
                alert('Bạn không có quyền truy cập trang này.');
                navigate('/login');
                return;
            }
        const isTokenExpired = decoded.exp < Date.now() / 1000;
        if (isTokenExpired) {
            alert('Token đã hết hạn. Vui lòng đăng nhập lại.');
            navigate('/login');
            return;
        }


        } catch (error) {
        alert('Token không hợp lệ. Vui lòng đăng nhập lại.');
        navigate('/login');
        }
    }, [navigate,token,render]);



    return(
        <div className="dashboard-container">
            <nav className="sidebar">
                <div className="logo-container">
                    <Link to="/" className="logo-link">
                        <img 
                        src="https://img.freepik.com/premium-vector/online-shopping-concept-vector-illustration-white-background_906149-104854.jpg"
                        alt="CataCo Logo" 
                        className="logo"
                        />
                    </Link>
                </div>
                <h1>This is AdminPage</h1>
                <ul className="nav-list">
                    <li className="nav-item">
                        <Link to="/admin" className="nav-link">
                            <i className="fa-solid fa-chart-line"></i>
                            <span>Doanh Thu</span>
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to="/admin/product_category" className="nav-link">
                            <i className="fa-solid fa-tags"></i>
                            <span>Phân Loại sản phẩm</span>
                        </Link>
                    </li>
                    <li className="nav-item">
                        <Link to="/admin/product_inventory" className="nav-link">
                            <i className="fa-solid fa-warehouse"></i>
                            <span>Hàng Tồn Kho</span>
                        </Link>
                    </li>
                    {/* <li className="nav-item">
                        <Link to="/admin/role" className="nav-link">
                            <i className="fa-solid fa-user-shield"></i>
                            <span>Quản lí quyền</span>
                        </Link>
                    </li> */}
                    <li className="nav-item">
                        <Link to="/admin/product_manage" className="nav-link">
                            <i className="fa-solid fa-box"></i>
                            <span>Thông tin sản phẩm</span>
                        </Link>
                    </li>
                </ul>
            </nav>
            <main className="main-content">
                <Outlet />
            </main>
        </div>
    )
}


export default AdminPage;