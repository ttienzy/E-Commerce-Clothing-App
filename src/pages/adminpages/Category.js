import React, { useEffect, useState } from 'react';
import '../../css/Category.css';

const Category = () => {
  const [formData, setFormData] = useState({
    telNo: '',
    nameProduct: '',
    quantity: '',
    unitPrice: '',
    formFile: null,
    categoryId: '', // Thêm trường category
    discountId: '', // Thêm trường mã giảm giá
    
  });

  const [fileName, setFileName] = useState('No file chosen');
  const [previewImage, setPreviewImage] = useState(null); // State cho preview ảnh
  const [category, setCategory] = useState([])
  const [discount, setDiscount] = useState([])
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState(null);
  // Get category
  useEffect(() => {
    try{
      const fetchCategory = async () => {
        const response = await fetch('https://localhost:7221/api/Category/ListCategory',{
          method : 'GET'
        })
        if(!response.ok){
          alert('Fetch error')
        }
        var results = await response.json()
        setCategory(results)
      }
      fetchCategory();
    }
    catch(err){
      alert(`Fetch failed ${err.message}`)
    }
  },[])

  // Get discount
  useEffect(() => {
    try{
      const fetchCategory = async () => {
        const response = await fetch('https://localhost:7221/api/Discount/ListDiscount',{
          method : 'GET'
        })
        if(!response.ok){
          alert('Fetch error')
        }
        var results = await response.json()
        setDiscount(results)
      }
      fetchCategory();
    }
    catch(err){
      alert(`Fetch failed ${err.message}`)
    }
  },[])

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({...prev, [name]: value}));
  };

  const handleFileChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      setFormData(prev => ({
        ...prev,
        formFile: file
      }));
      setFileName(file.name);
      
      // Tạo preview cho ảnh
      const reader = new FileReader();
      reader.onloadend = () => {
        setPreviewImage(reader.result);
      };
      reader.readAsDataURL(file);
    } else {
      setFileName('No file chosen');
      setPreviewImage(null);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);
    setError(null);

    try {
      // Tạo đối tượng FormData
      const submitFormData = new FormData();
      
      // Thêm các trường vào FormData
      submitFormData.append('telNo', formData.telNo);
      submitFormData.append('nameProduct', formData.nameProduct);
      submitFormData.append('quantity', formData.quantity);
      submitFormData.append('unitPrice', formData.unitPrice);
      submitFormData.append('categoryId', formData.categoryId);
      submitFormData.append('discountId', formData.discountId);
      
      // Thêm file nếu có
      if (formData.formFile) {
        submitFormData.append('formFile', formData.formFile);
      }

      // Gọi API
      const response = await fetch('https://localhost:7221/api/Provider/TransactionProvider', {
        method: 'POST',
        body: submitFormData,
        // Không cần set Content-Type header, browser sẽ tự thêm với boundary
      });

      if (!response.ok) {
        throw new Error(`Error: ${response.status}`);
      }

      const result = await response.json();
      //console.log('Success:', result);

      //handleReset();
      alert('Thêm sản phẩm thành công!');

    } catch (error) {
      console.error('Error:', error);
      setError('Có lỗi xảy ra khi thêm sản phẩm. Vui lòng thử lại!');
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleReset = () => {
    setFormData({
      telNo: '',
      nameProduct: '',
      quantity: '',
      unitPrice: '',
      formFile: null,
      category: '',
    });
    setFileName('No file chosen');
    setPreviewImage(null);
    setError(null);
  };

  return (
    <div className="min-h-screen p-6">
      <div className="max-w-4xl mx-auto bg-white rounded-lg shadow-md">
        <div className="p-6 border-b border-gray-200">
          <h2 className="text-2xl font-semibold text-gray-800">Thêm Sản Phẩm Mới</h2>
        </div>

        <div className="p-6">
          <form onSubmit={handleSubmit} className="grid grid-cols-1 md:grid-cols-2 gap-6">
            <div className="space-y-6">
              {/* Cột trái - Thông tin sản phẩm */}
              <div className="form-group">
                <label className="block text-sm font-medium text-gray-700">Số điện thoại</label>
                <input
                  type="text"
                  name="telNo"
                  value={formData.telNo}
                  onChange={handleInputChange}
                  className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  placeholder="Nhập số điện thoại"
                />
              </div>

              <div className="form-group">
                <label className="block text-sm font-medium text-gray-700">Tên sản phẩm</label>
                <input
                  type="text"
                  name="nameProduct"
                  value={formData.nameProduct}
                  onChange={handleInputChange}
                  className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  placeholder="Nhập tên sản phẩm"
                />
              </div>

              <div className="form-group">
                <label className="block text-sm font-medium text-gray-700">Danh mục</label>
                <select
                  name="categoryId"
                  value={formData.category}
                  onChange={handleInputChange}
                  className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                >
                  <option value="">Chọn danh mục</option>
                  {category.map((item) => (
                    <option key={item.id} value={item.id}>{item.name}</option>
                  ))}
                </select>
              </div>

              <div className="form-group">
                <label className="block text-sm font-medium text-gray-700">Số lượng</label>
                <input
                  type="number"
                  name="quantity"
                  value={formData.quantity}
                  onChange={handleInputChange}
                  className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  placeholder="Nhập số lượng"
                />
              </div>

              <div className="form-group">
                <label className="block text-sm font-medium text-gray-700">Đơn giá</label>
                <input
                  type="number"
                  step="0.01"
                  name="unitPrice"
                  value={formData.unitPrice}
                  onChange={handleInputChange}
                  className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  placeholder="Nhập đơn giá"
                />
              </div>
            </div>

            <div className="space-y-6">
              {/* Cột phải - Ảnh và giảm giá */}
              <div className="form-group">
                <label className="block text-sm font-medium text-gray-700">Hình ảnh sản phẩm</label>
                <div className="mt-1 flex items-center">
                  <button
                    type="button"
                    onClick={() => document.getElementById('formFile').click()}
                    className="inline-flex items-center px-4 py-2 border border-gray-300 rounded-md shadow-sm text-sm font-medium text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
                  >
                    Chọn ảnh
                  </button>
                  <span className="ml-3 text-sm text-gray-500">{fileName}</span>
                </div>
                <input
                  type="file"
                  id="formFile"
                  name="formFile"
                  onChange={handleFileChange}
                  className="hidden"
                  accept="image/*"
                />
                {previewImage && (
                  <div className="mt-4">
                    <img
                      src={previewImage}
                      alt="Preview"
                      className="max-w-full h-auto max-h-48 rounded-lg"
                    />
                  </div>
                )}
              </div>

              <div className="form-group">
                <label className="block text-sm font-medium text-gray-700">Mã giảm giá</label>
                <select
                  name="discountId"
                  value={formData.discountId}
                  onChange={handleInputChange}
                  className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                >
                  <option value="">Áp mã giảm giá</option>
                  {discount.map((item) => (
                    <option key={item.id} value={item.id}>{item.name} - {item.discount_percent*100}%</option>
                  ))}
                </select>
              </div>

              
            </div>

            <div className="col-span-full flex justify-end space-x-4 mt-6">
              <button
                type="button"
                onClick={handleReset}
                className="px-4 py-2 border border-gray-300 rounded-md shadow-sm text-sm font-medium text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
              >
                Làm mới
              </button>
              <button
                type="submit"
                className="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 disabled:bg-blue-300"
                disabled={isSubmitting}
              >
                {isSubmitting ? 'Đang xử lý...' : 'Thêm sản phẩm'}
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default Category;