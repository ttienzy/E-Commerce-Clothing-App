import React, { useEffect, useState } from "react";
import { PieChart, Pie, Cell, Tooltip } from "recharts";
import { AreaChart, Area, XAxis, YAxis, CartesianGrid, ResponsiveContainer } from "recharts";
import '../../css/Revenue.css'
import PaymentManagement from "./CellTotals/PaymentManagement";
import { useNavigate } from "react-router-dom";

const Revenue = () => {
    const navigate = useNavigate();
    const [areaData, setAreaData] = useState([]);
    const [donutData, setDonutData] = useState([]);
    const [totalCell, setTotalCell] = useState({
        "totalRevenue": 0,
        "totalOrders": 0,
        "totalUsers": 0,
        "totalCategories": 0,
        "totalDiscounts": 0,    
        "mostPurchasedProduct": 0,
        "totalInventory": 0,
        "onlineOrders": 0,
        "offlineOrders": 0,
        "totalOrderStatus":0
    });
    const donutColor = ["#ff0000", "#ffff00", "#00ff00", "#00ffff", "#0040ff", "#8000ff", "#ff00bf" ,"#ff0040" ]
    const [status, setStatus] = useState({
        CheckOrderStatus : false
    })

    const donutTotal = donutData.reduce((sum, item) => sum + item.orderCount, 0);

    const formatPrice = (price) => {
        return new Intl.NumberFormat('vi-VN').format(price) + 'đ';
    };
    // Các ô tổng số liệu
    const stats = [
        { label: "Tổng Doanh Thu", value: formatPrice(totalCell.totalRevenue) },
        { label: "Tổng Orders", value: totalCell.totalOrders },
        { label: "Tổng Người Đăng Ký", value: totalCell.totalUsers },
        { label: "Tổng Categories", value: totalCell.totalCategories },
        { label: "Tổng Discounts", value: totalCell.totalDiscounts },
        { label: "Số đơn cần duyệt", value: totalCell.totalOrderStatus },
        { label: "Tổng Tồn Kho", value: totalCell.totalInventory },
        { label: "Danh Sách Mua Online", value: totalCell.onlineOrders },
        { label: "Danh Sách Mua Offline", value: totalCell.offlineOrders },
    ];
    const RevenueInfo = async () => {
        try{
        const response = await fetch('https://localhost:7221/api/Orders/RevenueInfo',{
            method: 'GET'
        })
        if(!response.ok){
            alert("An error occurred while fetching data : Revenue Infomation")
        }
        const results = await response.json()
        console.log(results);
        setAreaData(results);
        }
        catch(err){
        alert('Fetch to failed');
        }
    }
    const ToTalCellInfo = async () => {
        try{
        const response = await fetch('https://localhost:7221/api/Admin',{
            method: 'GET'
        })
        if(!response.ok){
            alert("An error occurred while fetching data : ToTalCellInfo Infomation")
        }
        const results = await response.json()
        console.log(results);
        setTotalCell({
            "totalRevenue": results.totalRevenue,
            "totalOrders": results.totalOrders,
            "totalUsers": results.totalUsers,
            "totalCategories": results.totalCategories,
            "totalDiscounts": results.totalDiscounts,
            "mostPurchasedProduct": results.mostPurchasedProduct,
            "totalInventory": results.totalInventory,
            "onlineOrders": results.onlineOrders,
            "offlineOrders": results.offlineOrders,
            "totalOrderStatus": results.totalOrderStatus
        })
        }
        catch(err){
        alert('Fetch to failed');
        }
    }
    const SoftBestSellingOrder = async () => {
        try{
        const response = await fetch('https://localhost:7221/api/Admin/GetBestSelling',{
            method: 'GET'
        })
        if(!response.ok){
            alert("An error occurred while fetching data : SoftBestSellingOrder Infomation")
        }
        const results = await response.json()
        console.log(results);
        setDonutData(results);
        }
        catch(err){
        alert('Fetch to failed');
        }
    }
    useEffect(() => {
        RevenueInfo();
        ToTalCellInfo();
        SoftBestSellingOrder();
    },[]);
    const handleClickTotalCell = (key) => {
        switch (key) {
            case 0:
                console.log('Total Revenue');
                // Xử lý logic cho users
                break;
            case 1:
                console.log('Total orders'); 
                // Xử lý logic cho revenue
                break;
            case 2:
                console.log('Total register user '); 
                // Xử lý logic cho revenue
                break;
            case 3:
                console.log('Categories'); 
                navigate('/admin/product_inventory/')
                break;
            case 4:
                console.log('Discounts '); 
                navigate('/admin/product_inventory/')
                break;
            case 5:
                console.log('Check order status'); 
                setStatus(pre => ({...pre, CheckOrderStatus : true}))
                break;
            case 6:
                console.log('Inventories'); 
                navigate('/admin/product_manage/')
                break;
            case 7:
                console.log('Online'); 
                setStatus(pre => ({...pre, CheckOrderStatus : true}))
                break;
            case 8:
                console.log('Offline'); 
                setStatus(pre => ({...pre, CheckOrderStatus : true}))
                break;
            default:
                break;
            }
    }
return (
    <div className="min-h-screen">
    {/* Section Title */}
    {/* <h1 className="text-2xl font-bold mb-6">Dashboard Tổng Quan</h1> */}
    
    {/* Main Layout */}
    <div className="grid grid-cols-3 gap-6">
        {/* Các ô tổng số liệu */}
        <div className="col-span-2 grid grid-cols-3 gap-4">
        {stats.map((stat, index) => (
            <div key={index} className="bg-white p-4 rounded-lg shadow-md" style={{ cursor : 'pointer'}} onClick={() => handleClickTotalCell(index)}>
            <h3 className="text-gray-600 text-sm font-medium">{stat.label}</h3>
            <p className="text-xl font-semibold mt-2">{stat.value}</p>
            </div>
        ))}
        </div>

        {/* Donut Chart */}
        <div className="bg-white p-6 rounded-lg shadow-md">
        <h3 className="text-lg font-semibold mb-4">Top sản phẩm bán chạy</h3>
        <div className="flex justify-center items-center">
            <PieChart width={200} height={200}>
            <Pie
                data={donutData}
                dataKey="orderCount"
                nameKey="productName"
                cx="50%"
                cy="50%"
                innerRadius={60}
                outerRadius={80}
                paddingAngle={3}
            >
                {donutData.map((entry, index) => (
                <Cell key={`cell-${entry}`} fill={donutColor[index]} />
                ))}
            </Pie>
            <Tooltip />
            </PieChart>
            <div className="absolute text-center">
            <p className="text-2xl font-bold">{Math.floor(donutTotal*100/totalCell.totalOrders)}%</p>
            <p className="text-gray-500 text-sm">Hóa đơn</p>
            </div>
        </div>
        {/* Legend */}
        <ul className="mt-4 space-y-2">
            {donutData.map((item, index) => (
            <li key={index} className="flex items-center justify-between text-sm">
                <div className="flex items-center">
                <span
                    className="inline-block w-3 h-3 rounded-full"
                    style={{ backgroundColor: donutColor[index] }}
                ></span>
                <span className="ml-2">{item.productName}</span>
                </div>
                <span>{item.orderCount}</span>
            </li>
            ))}
        </ul>
        </div>
    </div>

    {/* Biểu đồ miền */}
    <div className="bg-white mt-6 p-6 rounded-lg shadow-md">
        <h3 className="text-lg font-semibold mb-4">Tổng Quan Doanh Thu</h3>
        <ResponsiveContainer width="100%" height={300}>
        <AreaChart data={areaData}>
            <defs>
            <linearGradient id="colorRevenue" x1="0" y1="0" x2="0" y2="1">
                <stop offset="5%" stopColor="#8884d8" stopOpacity={0.8} />
                <stop offset="95%" stopColor="#8884d8" stopOpacity={0} />
            </linearGradient>
            <linearGradient id="colorProfit" x1="0" y1="0" x2="0" y2="1">
                <stop offset="5%" stopColor="#82ca9d" stopOpacity={0.8} />
                <stop offset="95%" stopColor="#82ca9d" stopOpacity={0} />
            </linearGradient>
            </defs>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="month" />
            <YAxis 
            tick={{ fontSize: 12 }}
            />
            <Tooltip />
            <Area
            type="monotone"
            dataKey="revenue"
            stroke="#8884d8"
            fillOpacity={1}
            fill="url(#colorRevenue)"
            />
            {/* <Area
            type="monotone"
            dataKey="profit"
            stroke="#82ca9d"
            fillOpacity={1}
            fill="url(#colorProfit)"
            /> */}
        </AreaChart>
        </ResponsiveContainer>
    </div>
    {status.CheckOrderStatus && <PaymentManagement close={setStatus}/>}
    </div>
);
};

export default Revenue;
