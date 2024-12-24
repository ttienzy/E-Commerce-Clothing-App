import React, { useEffect, useState } from 'react';
import { 
  CreditCard, 
  CheckCircle2, 
  XCircle, 
  Clock, 
  Search 
} from 'lucide-react';

const PaymentCard = ({ payment, onApprove, onReject }) => {
  const formatDate = (dateString) => {
    return new Date(dateString).toLocaleString('vi-VN', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  };

  const getStatusInfo = (status) => {
    switch(status) {
      case 'unpaid':
        return { 
          color: 'bg-yellow-100 text-yellow-800', 
          icon: <Clock className="text-yellow-500" />,
          text: 'Chưa xử lý' 
        };
      case 'paid':
        return { 
          color: 'bg-green-100 text-green-800', 
          icon: <CheckCircle2 className="text-green-500" />,
          text: 'Đã thanh toán' 
        };
      case 'cancelled':
        return { 
          color: 'bg-red-100 text-red-800', 
          icon: <XCircle className="text-red-500" />,
          text: 'Đã hủy' 
        };
      default:
        return { 
          color: 'bg-gray-100 text-gray-800', 
          icon: null,
          text: 'Không xác định' 
        };
    }
  };

  const statusInfo = getStatusInfo(payment.status);

  return (
    <div className="bg-white shadow-md rounded-lg overflow-hidden mb-4 border">
      <div className="flex p-4">
        {/* Hình ảnh sản phẩm */}
        <div className="w-24 h-24 mr-4 flex-shrink-0">
          <img 
            src={payment.urlImage} 
            alt={payment.productName} 
            className="w-full h-full object-cover rounded-md"
          />
        </div>

        {/* Thông tin thanh toán */}
        <div className="flex-grow">
          <div className="flex justify-between items-start">
            <div>
              <h3 className="font-bold text-lg">{payment.productName}</h3>
              <div className="text-sm text-gray-600 mt-1">
                Mã đơn: {payment.paymentId}
              </div>
            </div>
            
            <div className="flex items-center">
              {statusInfo.icon}
              <span className={`ml-2 px-2 py-1 rounded-full text-xs ${statusInfo.color}`}>
                {statusInfo.text}
              </span>
            </div>
          </div>

          <div className="mt-2 text-sm text-gray-600">
            <div className="flex justify-between">
              <span>Số lượng:</span>
              <span className="font-semibold">{payment.quantity}</span>
            </div>
            <div className="flex justify-between mt-1">
              <span>Tổng tiền:</span>
              <span className="font-bold text-blue-600">
                {new Intl.NumberFormat('vi-VN', { 
                  style: 'currency', 
                  currency: 'VND' 
                }).format(payment.totalMoney)}
              </span>
            </div>
            <div className="flex justify-between mt-1">
              <span>Ngày mua:</span>
              <span>{formatDate(payment.createdAt)}</span>
            </div>
          </div>

          {/* Nút hành động */}
          {payment.status === 'unpaid' && (
            <div className="mt-3 flex space-x-2">
              <button 
                onClick={() => onApprove(payment.paymentId)}
                className="flex-1 flex items-center justify-center bg-green-500 text-white py-2 rounded hover:bg-green-600 transition"
              >
                <CheckCircle2 size={16} className="mr-2" /> 
                Xác nhận hóa đơn
              </button>
              <button 
                onClick={() => onReject(payment.paymentId)}
                className="flex-1 flex items-center justify-center bg-red-500 text-white py-2 rounded hover:bg-red-600 transition"
              >
                <XCircle size={16} className="mr-2" /> 
                Hủy đơn hàng
              </button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

const PaymentManagement = ({close}) => {
  const [payments, setPayments] = useState([
    {
      paymentId: "7c58f012-3711-454d-be40-6911167443fe",
      orderId: "65f02d08-2575-4103-8399-a9cca50793fa",
      quantity: 2,
      totalMoney: 780000.0000,
      createdAt: "2024-11-17T09:30:40.937",
      productName: "Đầm sọc caro",
      urlImage: "https://res.cloudinary.com/dteujrbaj/image/upload/v1729514019/dam-soc-caro-780.jpg",
      status: "unpaid"
    }
  ]);

  const fetchStatusOrders = async () => {
    const response = await fetch('https://localhost:7221/api/Orders/ListOrderForAdmin',{
      method : "GET"
    })
    if(!response.ok){
      alert('Fetch error')
      return;
    }
    var datas = await response.json();
    setPayments(datas)
  }
  useEffect(() => {
    try{
      fetchStatusOrders();
    }
    catch(err){
      alert('Fetch to failed')
    }
  },[])
  const [filter, setFilter] = useState('all');

  const handleApprove = async (paymentId) => {
    try{
      console.log(paymentId)
      var response = await fetch(`https://localhost:7221/api/Admin/RemoveOrder?idProduct=${paymentId}&orderInfo=confirmed`,{
        method : 'PUT',
      })
      if(!response.ok){
        alert('bad request')
        return;
      }
      alert('Success')
    }
    catch(err){
      alert('Fetch to failed')
    }
  };

  const handleReject = async (paymentId) => {
    try{
      var response = await fetch(`https://localhost:7221/api/Admin/RemoveOrder?idProduct=${paymentId}$orderInfo=cancelled`,{
        method : 'PUT',
      })
      if(!response.ok){
        alert('bad request')
        return;
      }
      alert('Success')
    }
    catch(err){
      alert('Fetch to failed')
    }
  };

  const filteredPayments = payments.filter(payment => 
    filter === 'all' || payment.status === filter
  );

  return (
    <div className="modal-overlay" style={{
        position: 'fixed',
        top: '0',
        left: 0,
        right: 0,
        bottom: 0,
        backgroundColor: 'rgba(0, 0, 0, 0.5)',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        zIndex: 1000,
    }}
    // onClick={() => close(pre => ({...pre, CheckOrderStatus : false}))}
    >
        <div className="p-6" style={{marginBottom: '42px', maxHeight: '100vh' }}>
      <div className="mx-auto" style={{ maxHeight: '100vh', overflowY: 'auto' }}>
        <div className="bg-white shadow-md rounded-lg mb-6 p-4" style={{width: '100vh',position : 'relative'}}>
          <div className="flex items-center mb-4">  
            <CreditCard className="mr-2 text-blue-500" />
            <h1 className="text-2xl font-bold text-gray-800">Quản Lý Thanh Toán</h1>
            <button className="close-button" onClick={() => close(pre => ({...pre, CheckOrderStatus : false}))} style={{
                position : 'absolute',
                right : '13px',
                top : '-4px'
            }}><b>&times;</b></button>
          </div>

          <div className="flex space-x-2 mb-4">
            {['all', 'unpaid', 'paid', 'cancelled'].map(status => (
              <button
                key={status}
                onClick={() => setFilter(status)}
                className={`px-3 py-1 rounded-full text-sm ${
                  filter === status 
                    ? 'bg-blue-500 text-white' 
                    : 'bg-gray-200 text-gray-700'
                }`}
              >
                {status === 'all' && 'Tất cả'}
                {status === 'unpaid' && 'Chưa thanh toán'}
                {status === 'paid' && 'Đã thanh toán'}
                {status === 'cancelled' && 'Đã hủy'}
              </button>
            ))}
          </div>

          {filteredPayments.length === 0 ? (
            <div className="text-center text-gray-500 py-6">
              Không có đơn thanh toán nào
            </div>
          ) : (
            filteredPayments.map(payment => (
              <PaymentCard 
                key={payment.paymentId}
                payment={payment}
                onApprove={handleApprove}
                onReject={handleReject}
              />
            ))
          )}
        </div>
      </div>
    </div>
    </div>
  );
};

export default PaymentManagement;