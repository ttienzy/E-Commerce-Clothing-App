import { jwtDecode } from "jwt-decode";
import { useState } from "react";

const UserPage = () => {
    const [token, setToken] = useState('');

    const handleClick = () => {

        var decoded = jwtDecode(token)
        console.log(typeof(decoded["exp"]))

        var expDate = new Date(decoded["exp"]*1000)

        var currentDate = new Date(); // Lấy thời gian hiện tại dưới dạng đối tượng Date
        console.log(currentDate.toLocaleString());  
        console.log(expDate.toLocaleString());
        
    }
    return(
        <>
            <input 
                type="text"
                onChange={(e) => setToken(e.target.value)}
            />
            <button onClick={handleClick}>Decode</button>
        </>
    )
}


export default UserPage;