import React, { useState } from 'react';

const CacelledPayment = (idPayment) => {
    const [isDialogOpen, setIsDialogOpen] = useState(false);
    const [selectedOrderId, setSelectedOrderId] = useState(null);
    const [cancelReason, setCancelReason] = useState('');
    const cancelReasons = [
        "Đặt nhầm sản phẩm",
        "Thay đổi địa chỉ giao hàng",
        "Muốn thay đổi phương thức thanh toán",
        "Không còn nhu cầu mua nữa",
        "Tìm thấy sản phẩm giá tốt hơn",
        "Lý do khác"
    ];
    const handleCancelClick = (orderId) => {
        setSelectedOrderId(orderId);
        setIsDialogOpen(true);
      };
      const handleCancelSubmit = () => {
        if (cancelReason) {
          console.log(`Hủy đơn hàng ${selectedOrderId} với lý do: ${cancelReason}`);
          // Thực hiện API call để hủy đơn hàng ở đây
          setIsDialogOpen(false);
          setCancelReason('');
          setSelectedOrderId(null);
        }
      };
    
    return (
        <>
            {/* Dialog Box */}
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
        </>
    )
}