import { Link, Outlet,useNavigate } from "react-router-dom";
import { useState, useEffect, useContext } from 'react';
import '../../css/DashboardUser.css'
import { jwtDecode } from "jwt-decode";
  

const DashBoardUser = () => {
  const navigate = useNavigate();
  const [username, setUserName] = useState('');
  const [render, setRerender] = useState(Math.random());
  const token = localStorage.getItem('accessToken');
    useEffect(() => {
      
      if (!token) {
        //alert('Token không tồn tại. Vui lòng đăng nhập.');
        navigate('/login');
        return;
      }

    try {
        const decoded = jwtDecode(token);

      const isTokenExpired = decoded.exp < Date.now() / 1000;
      if (isTokenExpired) {
        //alert('Token đã hết hạn. Vui lòng đăng nhập lại.');
        localStorage.removeItem('accessToken');  // Xóa token hết hạn
        navigate('/login');
        return;
      }

      const userRole = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      if (userRole !== 'User') {
        //alert('Bạn không có quyền truy cập trang này.');
        navigate('/login');
        return;
      }
      setUserName(decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"].replace(' ',"").slice(0,10));

      // const intervalId = setInterval(setRerender(Math.random()), 5 * 60 * 1000);

      // // Dọn dẹp interval khi component unmount
      // return () => clearInterval(intervalId);
    } catch (error) {
      //alert('Token không hợp lệ. Vui lòng đăng nhập lại.');
      navigate('/login');
    }
  }, [navigate,token,render]);


    return (
      <div className="dashboard-container">
        <nav className="sidebar">
          <div className="logo-container">
            <Link to="/" className="logo-link">
              <img 
                src="https://img.freepik.com/premium-vector/online-shopping-advertising-flyer-with-modern-design-elements_1326100-154.jpg" 
                alt="CataCo Logo" 
                className="logo"
              />
            </Link>
          </div>
          <h1>Xin chào, {username}</h1>
          <ul className="nav-list">
            <li className="nav-item">
              <Link to="/user" className="nav-link">
                <i className="fa-brands fa-shopify"></i>
                <span>Sản Phẩm</span>
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/user/cart-user/" className="nav-link">
              <i className="fa-solid fa-cart-shopping"></i>
                <span>
                  Giỏ hàng
                  {/* <span style={{
                    color: 'red',
                    backgroundColor: '#eee',
                    padding: '2px 6px',
                    borderRadius: '50%',
                    fontSize: '14px',
                    marginLeft: '4px',
                    fontWeight : '500'
                  }}>{count}
                </span>  */}
                </span>
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/user/order_history" className="nav-link">
                <i className="fa-solid fa-clock-rotate-left"></i>
                <span>Lịch Sử</span>
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/user/product_review" className="nav-link">
                <i className="fa-solid fa-comment-dots"></i>
                <span>Đánh Giá</span>
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/user/account_information" className="nav-link">
                <i className="fa-solid fa-image-portrait"></i>
                <span>Thông tin người dùng</span>
              </Link>
            </li>
            {/* <li className="nav-item">
              <Link to="/user/contact-us" className="nav-link">
                <i className="fa-solid fa-reply"></i>
                <span>Liên hệ NSX</span>
              </Link>
            </li> */}
          </ul>
        </nav>
        <main className="main-content">
          <Outlet />
        </main>
      </div>
    )
}

export default DashBoardUser;