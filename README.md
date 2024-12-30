- Sources : https://github.com/ttienzy/E-Commerce-Clothing-App
- Hướng dẫn : 
 BE(ASP.NET Core) : 
	+ Clone code từ branch main
	+ Chạy ở NET 8.0
	+ Cấu hình lại trong file appSetting.json
		+ConnectionStrings : <Chuỗi kết nối đến MSSQL>
		+Jwt : khóa bí mật dùng để xác minh token
		+Cloudinary : Lấy Url trong trang khi đăng nhập (lưu tệp trên cloud)
		+smtp : cấu hình tham số phục vụ sendEmail
		+"Vnpay": {
  "vnp_TmnCode": "3GRT5TZS",
  "HashSecret": "Q6R1RT1LVGLVASU79A0VP0XYB1KAHC76",
  "BaseUrl": "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html",
  "vnp_Command": "pay",
  "vnp_CurrCode": "VND",
  "vnp_Version": "2.1.0",
  "vnp_Locale": "vn"
}, -> tham số sử dugnj dịch vụ vnPay
"PaymentCallBack": {
  "vnp_ReturnUrl": "https://localhost:7221/api/Home/PaymentCallback"
},
"TimeZoneId": "SE Asia Standard Time" 
	+ Sau khi cấu hình thành công, sử dụng codefirst để generate ra database
	+Run dự án
	 	+dotnet run với vsc
FE(ReactJs) : 
	+Clone từ nhánh master
	+Project đã bỏ đi thư viện, tải lại với "npm i",
