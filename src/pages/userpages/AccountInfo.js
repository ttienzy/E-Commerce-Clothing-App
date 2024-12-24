import { useEffect, useState } from 'react'
import '../../css/AccountInfoUser.css'

const AccountInfo = () => {

  const [user, setUser] = useState({
    create_At : '2024-10-05T22:09:29.4',
    modified_At : '2024-10-05T22:09:29.4',
    province : '1',
    district : '2',
    ward : '3',
    orders : '7',
    userName : '',
    email : '',
    phoneNumber : ''
  });
  const [formData, setFormData] = useState({
    province: '',
    district: '',
    ward: ''
  });
  const [info, setInfo] = useState({
    userName : '',
    email : '',
    phoneNumber : ''
  })
  const [showInfo, setShowInfo] = useState(false);
  const [showEditForm, setShowEditForm] = useState(false);
  const [reload, setReLoad] = useState(false);
  const client_id = localStorage.getItem('client_id');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };


  useEffect(() => {
    const fetchUser = async () => {
      const response = await fetch(`https://localhost:7221/api/Authentication/ListUser?client_id=${client_id}`,{
        method: 'GET'
      })
      if(!response.ok){
        console.error("Fetch Failed")
      }
      var results = await response.json();
      localStorage.setItem('address',`${results.address.province}, ${results.address.district}, ${results.address.ward}`)
      setUser(preList => ({
        ...preList,
        create_At : results.create_At,
        modified_At : results.modified_At,
        province : results.address.province,
        district : results.address.district,
        ward : results.address.ward,
        userName : results.userName,
        email : results.email,
        phoneNumber : results.phoneNumber
      }))
      setFormData({
        province: results.address.province,
        district: results.address.district,
        ward: results.address.ward,
      })
      setInfo({
        userName : results.userName,
        email : results.email,
        phoneNumber : results.phoneNumber
      })
    }
    fetchUser();
  },[client_id,reload]);
  
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch('https://localhost:7221/api/UserAddress/UpdateUserAddress', {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json-patch+json'
        },
        body: JSON.stringify({
          province: formData.province,
          district: formData.district,
          ward: formData.ward,
          userId: client_id
        })
      });
  
      if (response.ok) {
        alert('Cập nhật địa chỉ thành công!');
        setReLoad(true);
        setShowEditForm(false);
      } else {
        alert('Có lỗi xảy ra khi cập nhật địa chỉ!');
      }
    } catch (error) {
      console.error('Error updating address:', error);
      alert('Có lỗi xảy ra khi cập nhật địa chỉ!');
    }
  };
  const handleSubmitInfo = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch(`https://localhost:7221/api/Authentication/SetInfo?UserId=${client_id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json-patch+json'
        },
        body: JSON.stringify({
          userName: info.userName,
          email: info.email,
          phoneNumber: info.phoneNumber,
        })
      });
  
      if (response.ok) {
        alert('Cập nhật thông tin thành công!');
        setReLoad(true);
        setShowInfo(false);
      } else {
        alert('Có lỗi xảy ra khi cập nhật thông tin !');
      }
    } catch (error) {
      console.error('Error updating infomation:', error);
      alert('Có lỗi xảy ra khi cập nhật thông tin!');
    }
  };


  return (
    <div className="profile-container">
    {/* Basic Information Card */}
      <div className="profile-card">
        <div className="card-header">
          <i className="icon user-icon">👤</i>
          <h2>{user.userName}</h2>
          <a className='log-out' href='http://localhost:3000/login'><i class="fa-solid fa-arrow-right-from-bracket"></i> Đăng xuất</a>
        </div>
        <div className="card-content">
          <div className="info-row">
            <i className="icon">✉️</i>
            <span>{user.email}</span>
          </div>
          <div className="info-row">
            <i className="icon">📞</i>
            <span>{user.phoneNumber}</span>
          </div>
          <div className="info-row">
            <i className="icon">🕒</i>
            <span>Ngày tạo: {new Date(user.create_At).toLocaleDateString('vi-VN')}</span>
          </div>
          <div className="info-row">
            <i className="icon">🕒</i>
            <span>Cập nhật gần nhất: {new Date(user.modified_At).toLocaleDateString('vi-VN')}</span>
          </div>
        </div>
      </div>

    {/* Address Card */}
      <div className="profile-card">
        <div className="card-header">
          <i className="icon">📍</i>
          <h2>Địa chỉ</h2>
          <button 
            className="edit-button"
            onClick={() => setShowEditForm(true)}
            style={{
              position : 'absolute',
              top : '407px',
              right : '67px'
            }}
          >
            ✏️ Chỉnh sửa
          </button>
          <button 
            className="edit-button-info"
            onClick={() => setShowInfo(true)}
            style={{
              position : 'absolute',
              top : '61px',
              right : '194px',
              backgroundColor : '#007cff',
              padding : '8px 16px',
              borderRadius : '4px',
              color : 'white'
            }}
          >
            ✏️ Chỉnh sửa
          </button>
        </div>
        <div className="card-content">
          <div className="address-row">
            <span className="label">Tỉnh/Thành phố:</span>
            <span>{user.province}</span>
          </div>
          <div className="address-row">
            <span className="label">Quận/Huyện:</span>
            <span>{user.district}</span>
          </div>
          <div className="address-row">
            <span className="label">Phường/Xã:</span>
            <span>{user.ward}</span>
          </div>
        </div>
      </div>

    {/* Role Card */}
      <div className="profile-card">
        <div className="card-header">
          <i className="icon">🔑</i>
          <h2>Vai trò người dùng</h2>
        </div>
        <div className="card-content">
          <span className="role-badge">User</span>
        </div>
      </div>
      {/* Thêm modal form này */}
      {showEditForm && (
        <div className="modal-overlay">
          <div className="modal">
            <div className="modal-header">
              <h3>Cập nhật địa chỉ</h3>
              <button 
                className="close-button"
                onClick={() => setShowEditForm(false)}
              >
                ×
              </button>
            </div>
            <form onSubmit={handleSubmit} className="edit-form">
              <div className="form-group">
                <label>Tỉnh/Thành phố:</label>
                <input
                  type="text"
                  name="province"
                  value={formData.province}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="form-group">
                <label>Quận/Huyện:</label>
                <input
                  type="text"
                  name="district"
                  value={formData.district}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="form-group">
                <label>Phường/Xã:</label>
                <input
                  type="text"
                  name="ward"
                  value={formData.ward}
                  onChange={handleChange}
                  required
                />
              </div>
              <div className="form-buttons">
                <button type="button" onClick={() => setShowEditForm(false)}>
                  Hủy
                </button>
                <button type="submit" className="submit-button">
                  Lưu thay đổi
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
      {/* model update user */}
      {showInfo && (
        <div className="modal-overlay">
          <div className="modal">
            <div className="modal-header">
              <h3>Cập nhật thông tin</h3>
              <button 
                className="close-button"
                onClick={() => setShowInfo(false)}
              >
                ×
              </button>
            </div>
            <form onSubmit={handleSubmitInfo} className="edit-form">
              <div className="form-group">
                <label>Tên người dùng:</label>
                <input
                  type="text"
                  value={info.userName}
                  onChange={(e) => setInfo(pre => ({...pre, userName : e.target.value}))}
                  required
                />
              </div>
              <div className="form-group">
                <label>Email:</label>
                <input
                  type="text"
                  value={info.email}
                  onChange={(e) => setInfo(pre => ({...pre, email : e.target.value}))}
                  required
                />
              </div>
              <div className="form-group">
                <label> Số điện thoại:</label>
                <input
                  type="text"
                  value={info.phoneNumber}
                  onChange={(e) => setInfo(pre => ({...pre, phoneNumber : e.target.value}))}
                  required
                />
              </div>
              <div className="form-buttons">
                <button type="button" onClick={() => setShowInfo(false)}>
                  Hủy
                </button>
                <button type="submit" className="submit-button">
                  Lưu thay đổi
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    
    </div>
  )
}


export default AccountInfo;