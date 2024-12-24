import React from 'react';
import { useState,useEffect } from 'react';

import '../../css/OrderHistory.css'
const OrderHistory = () => {

    const [orderInvoice, setOrderInvoice] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [totalPage, setTotalPage] = useState(10);
    const [history, setHistory] = useState(true);
    const client_id = localStorage.getItem('client_id')
    const PAGE_SIZE = 4;
    const dateFormatter = new Intl.DateTimeFormat('vi-VN', {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
        hour12 : false
    });
    const [orders,setOrders] = useState([]);
    const [isDialogOpen, setIsDialogOpen] = useState(false);
    const [selectedOrderPaymentId, setSelectedOrderPaymentId] = useState(null);
    const [cancelReason, setCancelReason] = useState('');
    const cancelReasons = [
        "Đặt nhầm sản phẩm",
        "Thay đổi địa chỉ giao hàng",
        "Muốn thay đổi phương thức thanh toán",
        "Không còn nhu cầu mua nữa",
        "Tìm thấy sản phẩm giá tốt hơn",
        "Lý do khác"
      ];
    
      const handleCancelClick = (paymentId) => {
        setSelectedOrderPaymentId(paymentId);
        setIsDialogOpen(true);
      };
    
      const handleCancelSubmit = async () => {
        try{
            const response = await fetch(`https://localhost:7221/api/Home/CacelledPayment?idPayment=${selectedOrderPaymentId}`,{
                method : 'PUT'
            })
            if(!response.ok){
                alert('Fetch to failed--------')
                return;
            }
            if (cancelReason) {
                console.log(`Hủy đơn hàng ${selectedOrderPaymentId} với lý do: ${cancelReason}`);
                // Thực hiện API call để hủy đơn hàng ở đây
                setIsDialogOpen(false);
                setCancelReason('');
                setSelectedOrderPaymentId(null);
              }
        }
        catch(err){
            alert('Fetch to failed')
        }
      };
    useEffect(() => {
        const fetchOrderInvoice = async () => {
            const response = await fetch(`https://localhost:7221/api/Orders/OrderHistory?client_id=${client_id}&pageNumber=${pageNumber}&pageSize=${PAGE_SIZE}`,{
                method : 'GET'
            })
            if(!response.ok){
                console.error("Fetch invalid")
            }
            var data = await response.json();
            console.log(data)
            setTotalPage(data.totalPages)
            setOrderInvoice(data.listResponse)
        }
        fetchOrderInvoice();
    },[pageNumber,client_id]);
    useEffect(() => {
        const fetchOrderInvoiceTrade = async () => {
            const response = await fetch(`https://localhost:7221/api/Orders/ListOrderHistoryTradeAsync?client_id=${client_id}`,{
                method : 'GET'
            })
            if(!response.ok){
                console.error("Fetch invalid")
            }
            var data = await response.json();
            console.log(data)
            setOrders(data)
        }
        fetchOrderInvoiceTrade();
    },[client_id]);
    const formatMoney = (amount) => {
        return new Intl.NumberFormat('vi-VN', {
          style: 'currency',
          currency: 'VND'
        }).format(amount);
      };
      const formatDateTime = (dateTimeStr) => {
        const date = new Date(dateTimeStr);
        return date.toLocaleString('vi-VN');
      };
    
    const getStatusText = (status) => {
        switch (status) {
            case 'paid':
                return 'Đã thanh toán';
            case 'received':
                return 'Đã nhận hàng';
            case 'cancelled':
                return 'Đã hủy đơn';
            case 'unpaid':
                return 'Chờ xác nhận';
            case 'confirmed':
              return 'Đã xác nhận'
            default:
                return status;
        }
    };  
    const handlePrevPage = () => {
        if(pageNumber === 1){
            setPageNumber(1)
            return;
        }
        setPageNumber(count => count - 1)
    }
    const handleNextPage = () =>{
        setPageNumber(count => count + 1)
    }
    function formatDate(dateString) {
        const parts = dateFormatter.formatToParts(new Date(dateString));
        const values = {};
    
        parts.forEach(part => {
            values[part.type] = part.value;
        });
    
        // Tùy chỉnh format theo ý muốn
        return `${values.day}/${values.month}/${values.year} ${values.hour}:${values.minute}:${values.second}`;
    }
    const handleHistoryButton = () => {
        if(history){
            setHistory(false)
            return
        }
        setHistory(true)
    }

    return (
        <div>
            <div className="p-6 mx-auto">
      <h1 className="text-2xl font-bold mb-6">Danh sách đơn hàng đã mua</h1>
      <div className="grid gap-6">
        {orders.map((order) => (
          <div 
            key={order.orderId}
            className="bg-white rounded-lg shadow-md p-4 grid grid-cols-1 md:grid-cols-4 gap-4 items-center"
          >
            <div className="md:col-span-1">
              <img
                src={order.urlImage}
                alt={order.productName}
                className="w-full h-32 object-cover rounded-lg"
              />
            </div>
            
            <div className="md:col-span-3 space-y-2">
              <div className="flex justify-between items-start">
                <h2 className="text-lg font-semibold">{order.productName}</h2>
                <span className={`px-3 py-1 rounded-full text-sm ${
                  (order.status === 'unpaid' || order.status === 'confirmed') 
                    ? 'bg-red-100 text-red-600' 
                    : 'bg-green-100 text-green-600'
                }`}>
                  {(order.status === 'unpaid' || order.status === 'confirmed') ? 'Chưa thanh toán' : 'Đã thanh toán'}
                </span>
                
              </div>
              
              <div className="grid grid-cols-2 gap-4">
                <div>
                  <p className="text-gray-600">Mã đơn hàng:</p>
                  <p className="font-medium">{order.orderId}</p>
                </div>
                <div>
                  <p className="text-gray-600">Ngày đặt mua:</p>
                  <p className="font-medium">{formatDateTime(order.createdAt)}</p>
                </div>
                <div>
                  <p className="text-gray-600">Số lượng:</p>
                  <p className="font-medium">{order.quantity}</p>
                </div>
                <div>
                  <p className="text-gray-600">Tổng tiền:</p>
                  <p className="font-medium text-lg text-red-600">
                    {formatMoney(order.totalMoney)}
                  </p>
                </div>
                {order.status === 'paid'
                ? (<button className="text-gray-900 bg-white border border-gray-300 focus:outline-none hover:bg-gray-100 focus:ring-4 focus:ring-gray-100 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:hover:border-gray-600 dark:focus:ring-gray-700"><i className="fa-regular fa-comment"></i> Liên hệ với chúng tôi</button>)
                :(<button disabled>.</button>)}
                { order.status === 'confirmed' 
                ? (<button className="text-white bg-gradient-to-r from-green-400 via-green-500 to-green-600 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-green-300 dark:focus:ring-green-800 shadow-lg shadow-green-500/50 dark:shadow-lg dark:shadow-green-800/80 font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2">Đã xác nhận đơn hàng</button>)
                :(<button type="button" className="text-white bg-gradient-to-r from-red-400 via-red-500 to-red-600 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-red-300 dark:focus:ring-red-800 font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2" onClick={() => handleCancelClick(order.paymentId)}>Hủy đơn hàng này <i className="fa-solid fa-angles-right"></i></button>)}
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
    <button type="button" className="text-white bg-gradient-to-r from-blue-500 via-blue-600 to-blue-700 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-blue-300 dark:focus:ring-blue-800 font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2" onClick={handleHistoryButton}>Lịch sử đặt hàng <i className="fa-solid fa-angles-down"></i></button>
            <div className="order-history">
            {( history === false) ? ( //orderInvoice.length === 0 && 
                <div className="no-orders">
                    <p>Danh sánh đơn hàng đã mua</p>
                </div>  
                ) : (
                <>
                    <table>
                        <thead>
                            <tr>
                            <th>Hình ảnh</th>
                            <th>Sản phẩm</th>
                            <th>Số lượng</th>
                            <th>Giá</th>
                            <th>Ngày đặt</th>
                            <th>Trạng thái</th>
                            </tr>
                        </thead>
                    <tbody>
                        {orderInvoice.map((order) => (
                            <tr key={order.orderId}>
                                <td className="image-cell">
                                    <img src={order.urlImage} alt={order.productName} />
                                </td>
                                <td className="product-cell">
                                    <div className="order-id">#{order.orderId.toUpperCase()}</div>
                                    <div className="product-name">{order.productName}</div>
                                </td>
                                <td className="quantity-cell">{order.quantity}</td>
                                <td className="price-cell">{order.totalMoney}</td>
                                <td className="date-cell">{formatDate(order.createdAt)}</td>
                                <td className="status-cell">
                                    <span className={`status status-${order.status}`}>
                                        {getStatusText(order.status)}
                                    </span>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                    </table>

                    <div className="pagination">
                    <button 
                        className="prev-btn"
                        onClick={handlePrevPage}
                        disabled={pageNumber === 1}
                    >
                        Previous
                    </button>
                    <span className="page-info">
                        Trang {pageNumber} / {totalPage}
                    </span>
                    <button 
                        className="next-btn"
                        onClick={handleNextPage}
                        disabled={pageNumber === totalPage}
                    >
                        Next
                    </button>
                    </div>
                </>
            )}
            </div>
            {isDialogOpen && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4">
          <div className="bg-white rounded-lg max-w-md w-full p-6">
            <h3 className="text-lg font-semibold mb-4">Chọn lý do hủy đơn</h3>
            
            <div className="space-y-3 mb-6">
              {cancelReasons.map((reason) => (
                <label key={reason} className="flex items-center space-x-3">
                  <input
                    type="radio"
                    name="cancelReason"
                    value={reason}
                    checked={cancelReason === reason}
                    onChange={(e) => setCancelReason(e.target.value)}
                    className="form-radio h-4 w-4 text-red-500"
                  />
                  <span>{reason}</span>
                </label>
              ))}
            </div>

            {cancelReason === 'Lý do khác' && (
              <textarea
                placeholder="Vui lòng nhập lý do..."
                className="w-full p-2 border rounded-lg mb-4 focus:outline-none focus:ring-2 focus:ring-red-500"
                rows="3"
                onChange={(e) => setCancelReason(e.target.value)}
              />
            )}
            
            <div className="flex justify-end space-x-3">
              <button
                onClick={() => {
                  setIsDialogOpen(false);
                  setCancelReason('');
                }}
                className="px-4 py-2 border border-gray-300 rounded-lg hover:bg-gray-50"
              >
                Đóng
              </button>
              <button
                onClick={handleCancelSubmit}
                disabled={!cancelReason}
                className={`px-4 py-2 rounded-lg text-white ${
                  cancelReason 
                    ? 'bg-red-500 hover:bg-red-600' 
                    : 'bg-gray-300 cursor-not-allowed'
                }`}
              >
                Xác nhận hủy
              </button>
            </div>
          </div>
        </div>
      )}
        </div>
    )
}

export default OrderHistory;