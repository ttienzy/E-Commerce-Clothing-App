import React, { useEffect, useState } from 'react';
import '../../css/Inventory.css'

const Inventory = () => {
    
    const [categories, setCategories] = useState([]);
    const [category, setCategory] = useState({id:'', name : '', description: ''})
    const [statusC, setStatusC] = useState(false);
    const [discounts, setDiscounts] = useState([]);
    const [discount, setDiscount] = useState({id:'',name:'', discount_percent: 0 })
    const [statusD, setStatusD] = useState(false);
    const [isEditingCategory, setIsEditingCategory] = useState(false);
  const [isEditingDiscount, setIsEditingDiscount] = useState(false);

    const fetchListCategories = async () => {
        try{
            const response = await fetch('https://localhost:7221/api/Category/ListCategory')
            if(!response.ok){
                alert("FetchCategory error")
                return;
            }
            var data = await response.json();
            setCategories(data)
        }
        catch(err){
            alert('Fetch error')
        }
    }
    const fetchListDiscount = async () => {
        try{
            const response = await fetch('https://localhost:7221/api/Discount/ListDiscount')
            if(!response.ok){
                alert("FetchDiscount error")
                return;
            }
            var data = await response.json();
            setDiscounts(data)
        }
        catch(err){
            alert('Fetch error')
        }
    }
    const fetchAddCategory = async () => {
        try {
            const response = await fetch('https://localhost:7221/api/Category/CreateCategory', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    name: category.name,
                    description: category.description
                })
            });
    
            if (!response.ok) {
                const result = await response.json();
                alert('Error message:', result);
                return;
            } 
            alert('Success');
            setStatusC(true);
            setCategory({name: '', description: ''});
        } catch (error) {
            alert('Fetch failed:', error);
        }
    }
    const fetchAddDiscount = async () => {
        try {
            const response = await fetch('https://localhost:7221/api/Discount/CreateDiscount', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    name : discount.name,
                    discount_percent : discount.discount_percent
                })
            });
        
            if (!response.ok) {
                const result = await response.json();
                alert('Error message:', result);
                return;
            } 
            alert('Success');
            setStatusD(true);
            setCategory({name: '', discount_percent: ''});
        } catch (error) {
            alert('Fetch to failed:', error);
        }
    }
    const fetchUpdateCategory = async () => {
        try {
            const response = await fetch(`https://localhost:7221/api/Category/UpdateCategory?categoryId=${category.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    name: category.name,
                    description: category.description
                })
            });
    
            if (!response.ok) {
                const result = await response.json();
                alert('Error message:', result);
                return;
            } 
            alert('Success');
            setStatusC(true);
            setCategory({name: '', description: ''});
        } catch (error) {
            alert('Fetch failed:', error);
        }
    }
    const fetchUpdateDiscount = async () => {
        try {
            const response = await fetch(`https://localhost:7221/api/Discount/UpdateDiscount?id=${discount.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    name : discount.name,
                    discount_percent : discount.discount_percent
                })
            });
        
            if (!response.ok) {
                const result = await response.json();
                alert('Error message:', result);
                return;
            } 
            alert('Success');
            setStatusD(true);
            setCategory({name: '', discount_percent: ''});
        } catch (error) {
            alert('Fetch to failed:', error);
        }
    }
    const fetchDeleteCategory = async (id) => {
        try{
            const response = await fetch(`https://localhost:7221/api/Category/RequirementCategory?id=${id}`,{
                method: 'DELETE'
            })
            if(!response.ok){
                alert("FetchCategory error")
                return;
            }
            alert("delete success");
            setStatusC(true);
        }
        catch(err){
            alert('Fetch error')
        }
    }
    const fetchDeleteDiscount = async (id) => {
        try{
            const response = await fetch(`https://localhost:7221/api/Discount/DeleteDiscount?id=${id}`,{
                method: 'DELETE'
            })
            if(!response.ok){
                alert("FetchDiscount error")
                return;
            }
            alert("delete success");
            setStatusD(true);
        }
        catch(err){
            alert('Fetch error')
        }
    }
    useEffect(() => {
        setStatusC(false)
        fetchListCategories();
    },[statusC]);
    useEffect(() => {
        setStatusD(false)
        fetchListDiscount();
    },[statusD]);

    const handleSubmitCreateCategory = (e) => {
        e.preventDefault();
        fetchAddCategory()
    }
    const handleSubmitCreateDiscount = (e) => {
        e.preventDefault();
        fetchAddDiscount();
    }
    const handleClickDeleteCategory = (id) => {
        fetchDeleteCategory(id);
    }
    const handleClickDeleteDiscount = (id) => {
        fetchDeleteDiscount(id);
    }
    const formatDate = (dateString) => {
        const date = new Date(dateString);
        return new Intl.DateTimeFormat('vi-VN').format(date);
    }
    const handleClickEditCategory = (c) => {
        setIsEditingCategory(true);
        setCategory({ id: c.id, name: c.name, description: c.description });
    };
    
    const handleClickSaveCategory = async () => {
        try {
            await fetchUpdateCategory();
            setIsEditingCategory(false);
            setStatusC(true);
        } catch (error) {
            alert('Error saving category:', error);
        }
    };
    
    const handleClickCancelCategoryEdit = () => {
        setIsEditingCategory(false);
        setCategory({ id: null, name: '', description: '' });
    };
    
    const handleClickEditDiscount = (d) => {
        setIsEditingDiscount(true);
        setDiscount({ id: d.id, name: d.name, discount_percent: d.discount_percent });
    };
    
    const handleClickSaveDiscount = async () => {
        try {
        await fetchUpdateDiscount();
        setIsEditingDiscount(false);
        setStatusD(true);
        } catch (error) {
        alert('Error saving discount:', error);
        }
    };
    
    const handleClickCancelDiscountEdit = () => {
        setIsEditingDiscount(false);
        setDiscount({ id: null, name: '', discount_percent: 0 });
    };
    return (
        <div className="inventory-container">

            {/* Category Section */}
            <div className="section">
                <div className="form-container">
                    <h2 className="form-title">Thêm danh mục mới</h2>
                    <form className="input-form" onSubmit={handleSubmitCreateCategory}>
                        <input 
                            type="text"
                            placeholder="Tên loại"
                            className="input-field"
                            onChange={(e) => setCategory((pre) => ({ ...pre, name : e.target.value}))}
                        />
                        <input 
                            type="text"
                            placeholder="Mô tả"
                            className="input-field"
                            onChange={(e) => setCategory((pre) => ({ ...pre, description : e.target.value}))}
                        />
                        <button type="submit" className="submit-btn">Thêm mới</button>
                    </form>
                </div>

                <div className="list-container">
                    <h3 className="list-title">Danh sách danh mục</h3>
                    <div className="item-list">
                        {categories.map((c) =>
                            isEditingCategory && c.id === category.id ? (
                                <div key={c.id} className="item-card">
                                <div className="item-header">
                                    <input
                                    type="text"
                                    className="item-name"
                                    value={category.name}
                                    onChange={(e) => setCategory((prev) => ({ ...prev, name: e.target.value }))}
                                    />
                                    <div className="button-group">
                                    <button className="action-btn edit-btn" onClick={handleClickSaveCategory}>
                                        Lưu
                                    </button>
                                    <button className="action-btn delete-btn" onClick={handleClickCancelCategoryEdit}>
                                        Hủy
                                    </button>
                                    </div>
                                </div>
                                <input
                                    type="text"
                                    className="item-description"
                                    value={category.description}
                                    onChange={(e) => setCategory((prev) => ({ ...prev, description: e.target.value }))}
                                />
                                </div>
                            ) : (
                                <div key={c.id} className="item-card">
                                <div className="item-header">
                                    <span className="item-name">{c.name}</span>
                                    <div className="button-group">
                                    <button className="action-btn edit-btn" onClick={() => handleClickEditCategory(c)}>
                                        Cập nhật
                                    </button>
                                    <button className="action-btn delete-btn" onClick={() => handleClickDeleteCategory(c.id)}>
                                        Loại bỏ
                                    </button>
                                    </div>
                                </div>
                                <p className="item-description">{c.description}</p>
                                </div>
                            )
                            )}
                    </div>
                </div>
            </div>

            {/* Discount Section */}
            <div className="section">
                <div className="form-container">
                    <h2 className="form-title">Thêm khuyến mãi mới</h2>
                    <form className="input-form" onSubmit={handleSubmitCreateDiscount}>
                        <input 
                            type="text"
                            placeholder="Tên khuyến mãi"
                            className="input-field"
                            onChange={(e) => setDiscount((pre) => ({ ...pre, name : e.target.value}))}
                        />
                        <input 
                            type="number"
                            min="0.1" 
                            max="1" 
                            step="0.1" 
                            placeholder="% giảm giá"
                            className="input-field"
                            onChange={(e) => setDiscount((pre) => ({ ...pre, discount_percent : e.target.value}))}
                        />
                        <button type="submit" className="submit-btn">Thêm mới</button>
                    </form>
                </div>

                <div className="list-container">
                    <h3 className="list-title">Danh sách khuyến mãi</h3>
                    <div className="item-list">
                        {/* {discounts.map(discount => (
                            <div key={discount.id} className="item-card">
                                <div className="item-header">
                                    <span className="item-name">{discount.name} - UpdatedAt:{formatDate(discount.updateAt)}</span>
                                    <div className="button-group">
                                        <button className="action-btn edit-btn">Cập nhật</button>
                                        <button className="action-btn delete-btn" onClick={() => handleClickDeleteDiscount(discount.id)}>Loại bỏ</button>
                                    </div>
                                </div>
                                <p className="item-description">Giảm giá: {discount.discount_percent*100}%</p>
                            </div>
                        ))} */}
                        {discounts.map((d) =>
                        isEditingDiscount && d.id === discount.id ? (
                            <div key={d.id} className="item-card">
                            <div className="item-header">
                                <input
                                type="text"
                                className="item-name"
                                value={discount.name}
                                onChange={(e) => setDiscount((prev) => ({ ...prev, name: e.target.value }))}
                                />
                                <div className="button-group">
                                <button className="action-btn edit-btn" onClick={handleClickSaveDiscount}>
                                    Lưu
                                </button>
                                <button className="action-btn delete-btn" onClick={handleClickCancelDiscountEdit}>
                                    Hủy
                                </button>
                                </div>
                            </div>
                            <input
                                type="number"
                                min="0.1"
                                max="1"
                                step="0.1"
                                className="item-description"
                                value={discount.discount_percent}
                                onChange={(e) => setDiscount((prev) => ({ ...prev, discount_percent: e.target.value }))}
                            />
                            </div>
                        ) : (
                            <div key={d.id} className="item-card">
                            <div className="item-header">
                                <span className="item-name">{d.name} - UpdatedAt:{formatDate(d.updateAt)}</span>
                                <div className="button-group">
                                <button className="action-btn edit-btn" onClick={() => handleClickEditDiscount(d)}>
                                    Cập nhật
                                </button>
                                <button className="action-btn delete-btn" onClick={() => handleClickDeleteDiscount(d.id)}>
                                    Loại bỏ
                                </button>
                                </div>
                            </div>
                            <p className="item-description">Giảm giá: {d.discount_percent * 100}%</p>
                            </div>
                        )
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Inventory;