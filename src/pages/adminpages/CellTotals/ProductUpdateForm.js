import React, { useContext, useState } from 'react';
import { UseDisCountData } from '../ProductManagement';

const ProductUpdateForm = ({ product, onClose, onUpdate }) => {
    const [formData, setFormData] = useState({
    id: product.id,
    name: product.name || '',
    price: product.price || 0,
    quantity: product.quantity || 0,
    discountPercent: product.discountPercent || 0,
    imageProducts: product.imageProducts || '',
    categoryId: product.categoryId
    });
    const listDiscount = useContext(UseDisCountData)
    const [discount, setDiscount] = useState(null);
    
    const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
        ...prev,
        [name]: name === 'discount_percent' ? parseFloat(value) / 100 : value
    }));
    };
    console.log(discount)
    

    const handleSubmit = async (e) => {
    e.preventDefault();
    try {
        const response = await fetch(`https://localhost:7221/api/Product/UpdateProduct?idProduct=${formData.id}`,
        {
            method: 'PUT',
            headers: {
            'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                name : formData.name,
                quantity : formData.quantity,
                price : formData.price,
                discountId : discount
            }),
        }
        );

        if (!response.ok) {
        alert('Cập nhật thất bại');
        return;
        }

        alert('Cập nhật thành công');
        onUpdate();
        onClose();
    } catch (error) {
        alert('Lỗi khi cập nhật: ' + error.message);
    }
    };

    return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
        <div className="bg-white rounded-lg p-6 max-w-5xl w-full mx-4 max-h-[90vh] overflow-y-auto">
        <div className="flex justify-between items-center mb-6">
            <h2 className="text-2xl font-bold text-gray-800">Cập nhật sản phẩm</h2>
            <button 
            onClick={onClose}
            className="text-gray-500 hover:text-gray-700"
            >
            ✕
            </button>
        </div>

        <div className="flex gap-6">
            {/* Phần hình ảnh bên trái */}
            <div className="w-1/3">
            <div className="sticky top-0">
                <div className="aspect-square w-full rounded-lg overflow-hidden border">
                <img 
                    src={formData.imageProducts} 
                    alt={formData.name}
                    className="w-full h-full object-cover"
                />
                </div>
                {/* <div className="mt-4">
                <p className="text-sm text-gray-600 break-all"></p>
                </div> */}
            </div>
            </div>

            {/* Form bên phải */}
            <div className="w-2/3">
            <form onSubmit={handleSubmit} className="space-y-6">
                <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">
                    Tên sản phẩm
                </label>
                <input
                    type="text"
                    name="name"
                    value={formData.name}
                    onChange={handleChange}
                    className="w-full border rounded-md px-3 py-2 focus:ring-blue-500 focus:border-blue-500"
                    required
                />
                </div>

                <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">
                    Giá (VNĐ)
                </label>
                <input
                    type="number"
                    name="price"
                    value={formData.price}
                    onChange={handleChange}
                    className="w-full border rounded-md px-3 py-2 focus:ring-blue-500 focus:border-blue-500"
                    required
                    min="0"
                />
                </div>

                <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">
                    Số lượng
                </label>
                <input
                    type="number"
                    name="quantity"
                    value={formData.quantity}
                    onChange={handleChange}
                    className="w-full border rounded-md px-3 py-2 focus:ring-blue-500 focus:border-blue-500"
                    required
                    min="0"
                />
                </div>

                <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">
                    Phần trăm giảm giá
                </label>
                <select
                    onChange={(e) => setDiscount(e.target.value)}
                    className="w-full border rounded-md px-3 py-2 focus:ring-blue-500 focus:border-blue-500"
                >
                    {listDiscount.map((option) => (
                    <option key={option.id} value={option.id}>
                        {option.discount_percent * 100}%
                    </option>
                    ))}
                </select>
                </div>

                <div className="flex justify-end space-x-4 pt-4">
                <button
                    type="button"
                    onClick={onClose}
                    className="px-4 py-2 border rounded-md hover:bg-gray-50"
                >
                    Hủy
                </button>
                <button
                    type="submit"
                    className="px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600"
                >
                    Lưu thay đổi
                </button>
                </div>
            </form>
            </div>
        </div>
        </div>
    </div>
    );
};

export default ProductUpdateForm;