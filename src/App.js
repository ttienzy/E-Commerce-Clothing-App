import { BrowserRouter, Route, Routes } from "react-router-dom";
import HomePage from "./home/HomePage";
import Login from "./home/Login";
import Register from "./home/Register";
import ForgotPassword from "./home/ForgotPassword";
import SentOTP from "./home/SentOTP";
import ResetPassword from "./home/ResetPassword";
import AdminPage from './pages/adminpages/AdminPage';
import Category from './pages/adminpages/Category';
import Inventory from './pages/adminpages/Inventory';
import Revenue from './pages/adminpages/Revenue';
import ProductManagement from "./pages/adminpages/ProductManagement";
import Role from './pages/adminpages/Role'
import AccountInfo from './pages/userpages/AccountInfo';
import OrderHistory from './pages/userpages/OrderHistory';
import ProductReviews from './pages/userpages/ProductReviews';
import ProductCategoties from "./pages/userpages/ProductCategories";
import ErrorPage from './errors/ErrorPage';
import PaymentCallback from "./pages/userpages/PaymentCallback";
import ContactUs from "./pages/userpages/ContactUs";
import CartInfo from "./pages/userpages/CartInfo";
import DashBoardUser from "./pages/userpages/DashBoardUser";
function App() {
  return (
      <BrowserRouter>
      <Routes>
        <Route path="/" element={<HomePage />}>
          <Route index element={<Login />} />
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route path="forgot-password" element={<ForgotPassword />} />
          <Route path="sentotp" element={<SentOTP />} />
          <Route path="resetpassword" element={<ResetPassword />}/>
        </Route>
        <Route path="/user" element={<DashBoardUser />} errorElement={<ErrorPage />}>
          <Route index element={<ProductCategoties />}/>   
          <Route path="/user/cart-user/" element={<CartInfo />} />
          <Route path="/user/order_history/" element={<OrderHistory />}/>
          <Route path="/user/product_review/" element={<ProductReviews />}/>
          <Route path="/user/account_information/" element={<AccountInfo />}/>
          <Route path="/user/contact-us/" element={<ContactUs />}/>
        </Route>
        <Route path="/payment_callback/" element={<PaymentCallback />} />
        <Route path="/admin/" element={<AdminPage />} errorElement={<ErrorPage />}>
          <Route index element={<Revenue />}/>
          <Route path="/admin/product_category" element={<Category />}/>
          <Route path="/admin/product_inventory" element={<Inventory />}/>
          <Route path="/admin/role" element={<Role />}/>
          <Route path="/admin/product_manage" element={<ProductManagement />}/>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
