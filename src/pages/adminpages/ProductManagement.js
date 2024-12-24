import React, { createContext, useEffect, useState } from 'react';
import ProductUpdateForm from './CellTotals/ProductUpdateForm';

const UseDisCountData = createContext();
const ProductManagement = () => {
  const [products, setProduct] = useState([]);
  const [category, setCategory] = useState([]);
  //const [isChecked, setIsChecked] = useState(true);
  
  const [selectedValueCategory, setSelectedValueCategory] = useState(
    '6f97800f-ec33-4e58-aaa0-0f76286d3466'
  );
  const prices = [
    { name: 'Từ 100.000 vnđ - 500.000 vnđ', min: 100000, max: 500000 },
    { name: 'Từ 500.000 vnđ - 1.000.000 vnđ', min: 500000, max: 1000000 },
    { name: 'Từ 1.000.000 vnđ - 2.000.000 vnđ', min: 1000000, max: 2000000 },
    { name: 'Trên 2.000.000 vnđ', min: 2000000, max: 50000000 },
  ];
  const [selectedValuePrice, setSelectedValuePrice] = useState({ min: 0, max: 0 });
  const [showAll, setShowAll] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [showUpdateForm, setShowUpdateForm] = useState(false);
  const [listDiscount, setListDiscount] = useState([]);
  const handleUpdate = (product) => {
    setSelectedProduct(product);
    setShowUpdateForm(true);
  };
  
  const handleUpdateSuccess = () => {
    fetchAll(); // Fetch lại dữ liệu sau khi cập nhật
  };
  const fetchAll = async () => {
    const response = await fetch(
      `https://localhost:7221/api/Product/GetProductByQueryAsync?categoryId=${selectedValueCategory}&min=${selectedValuePrice.min}&max=${selectedValuePrice.max}`
    );
    if (!response.ok) {
      alert('Lỗi xuất hiện khi fetch data', response);
      return;
    }
    const results = await response.json();
    setProduct(results);
  };

  const fetchCategory = async () => {
    const response = await fetch('https://localhost:7221/api/Category/ListCategory');
    if (!response.ok) {
      alert('Lỗi xuất hiện khi fetch data', response);
      return;
    }
    const results = await response.json();
    setCategory(results);
  };
  const fetchDiscount = async () => {
    const response = await fetch('https://localhost:7221/api/Discount/ListDiscount', {
      method: 'GET',
    });
    if (!response.ok) {
      alert('Fetch error');
      return;
    }
    var results = await response.json();
    setListDiscount(results)
  };

  useEffect(() => {
    fetchAll();
  }, [selectedValueCategory, selectedValuePrice, showAll]);

  useEffect(() => {
    fetchCategory();
    fetchDiscount();
  }, []);

  // const handleUpdate = (id) => {
  //   console.log('Update product:', id);
  // };

  const handleDelete = (id) => {
    console.log('Delete product:', id);
    const fetchData = async () => {
      const response = await fetch(
        `https://localhost:7221/api/Product/DeleteProduct?id=${id}`,
        {
          method: 'DELETE',
        }
      );
      if (!response.ok) {
        alert('Lỗi xuất hiện khi fetch data', response);
        return;
      }
      alert('Delete success');
      setShowAll(true);
    };
    fetchData();
  };

  const formatPrice = (price) => {
    return new Intl.NumberFormat('vi-VN', {
      style: 'currency',
      currency: 'VND',
    }).format(price);
  };

  const handleSelectCategory = (value) => {
    setSelectedValueCategory(value);
  };

  const handleSelectPrice = (min, max) => { 
    setSelectedValuePrice({ min : min, max:max });
  }
  
  // const handleSelectPriceAll = () => { 
  //   if(isChecked){
  //     setSelectedValuePrice({min : 0, max : 0})
  //     setIsChecked(false)
  //   }
  //   else{
  //     setIsChecked(true)
  //   }
  // };
  console.log("demo-test")
  return (
    <div className="container mx-auto bg-gray-50">
      {/* <h1 className="text-3xl font-bold text-center mb-6 text-gray-800">Quản lý sản phẩm</h1> */}
      <div className="grid grid-cols-1 md:grid-cols-4 gap-6">
        {/* Sidebar */}
        <div className="bg-white shadow rounded-lg p-4 md:col-span-1">
          <h3 className="text-lg font-semibold mb-4 text-gray-700">Lọc sản phẩm</h3>
          <div className="space-y-4">
            <div>
              <h4 className="font-medium text-gray-600">Danh mục</h4>
              {category.map((option) => (
                <label
                  key={option.id}
                  className="flex items-center space-x-2 cursor-pointer mt-2"
                >
                  <input
                    type="checkbox"
                    value={option.name}
                    checked={selectedValueCategory === option.id}
                    onChange={() => handleSelectCategory(option.id)}
                    className="form-checkbox h-5 w-5 text-blue-600"
                  />
                  <span className="text-gray-700">{option.name}</span>
                </label>
              ))}
            </div>
            <div>
              <h4 className="font-medium text-gray-600">Giá</h4>
              {/* <label
                  key='all'
                  className="flex items-center space-x-2 cursor-pointer mt-2"
                >
                  <input
                    type="checkbox"
                    value='all'
                    checked={isChecked}  
                    onChange={handleSelectPriceAll}
                    className="form-checkbox h-5 w-5 text-blue-600"
                  />
                  <span className="text-gray-700">Tất cả</span> 
                </label> */}
              {prices.map((option) => (
                <label
                  key={option.name}
                  className="flex items-center space-x-2 cursor-pointer mt-2"
                >
                  <input
                    type="checkbox"
                    value={option.name}
                    checked={selectedValuePrice.min === option.min}  
                    onChange={() => handleSelectPrice(option.min, option.max)}
                    className="form-checkbox h-5 w-5 text-blue-600"
                  />
                  <span className="text-gray-700">{option.name}</span>
                </label>
              ))}
            </div>
          </div>
        </div>
        {/* Product List */}
        <div className="md:col-span-3">
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {products.map((product) => (
              <div
                key={product.id}
                className="bg-white p-4 rounded-lg shadow hover:shadow-lg transition duration-300"
              >
                <img
                  src={product.imageProducts}
                  alt={product.name}
                  className="w-full h-40 object-cover rounded-t-lg mb-4"
                />
                <div style={{position:'relative'}}>
                  <h3 className="text-lg font-semibold text-gray-800" style={{height:'56px'}}>{product.name}</h3>
                  <div className="mt-2">
                    {product.discountPercent === 0 ? (
                      <span className="text-lg font-bold text-red-600">
                        {formatPrice(product.price)}
                      </span>
                    ): (
                      <>
                      <span className="text-sm text-gray-500 line-through" >
                        {formatPrice(product.price)} <i class="fa-solid fa-arrow-right-long"></i>
                      </span>
                      <b style={{color : '#DC2626'}}>{formatPrice(product.price * (1 - product.discountPercent))}</b>
                      <span 
                    style={{
                      position : 'absolute',
                      bottom : '265px',
                      left : '144px',
                      backgroundColor : '#ff2626',
                      padding : '1px 3px',
                      borderRadius : '5px',
                      fontSize : '12px',
                      color : 'white'
                      }}
                      >{product.discountPercent*100}%</span>
                      </>
                    )}
                  </div>
                  <p className="text-sm text-gray-600 mt-1">Còn lại: {product.quantity} sản phẩm</p>
                </div>
                <div className="flex justify-between mt-4">
                  <button
                    onClick={() => handleUpdate(product)}
                    className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
                  >
                    Cập nhật
                  </button>
                  
                  <button
                    onClick={() => handleDelete(product.id)}
                    className="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600"
                  >
                    Xóa
                  </button>
                </div>
              </div>
            ))}
            {showUpdateForm  && (
                    <UseDisCountData.Provider value={listDiscount}>
                    <ProductUpdateForm
                      product={selectedProduct}
                      onClose={() => setShowUpdateForm(false)}
                      onUpdate={handleUpdateSuccess}
                    />
                    </UseDisCountData.Provider>
                  )}
          </div>
        </div>
      </div>
    </div>
  );
};



export default ProductManagement;
export {UseDisCountData};