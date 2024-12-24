import { Outlet } from "react-router-dom";
import '../css/HomePage.css'

const HomePage = () => {

    return (
        <div className="homepage-container">
            <div className="content-section">
                <div className="brand-content">
                    <h1 className="brand-title">Welcome to Our Store</h1>
                    <p className="brand-subtitle">
                        Discover amazing products and shop with confidence. 
                        Join our community of satisfied customers today.
                    </p>
                    <img 
                        src="https://img.graphicsurf.com/2019/12/online-shopping-vector-illustration3.jpg" 
                        alt="Fashion Collection" 
                        className="hero-image" 
                    />
                </div>
            </div>
            <div className="auth-section">
                <Outlet /> {/* This will render Login/Register/Reset Password */}
            </div>
        </div>
    );

}

export default HomePage;