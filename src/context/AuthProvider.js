import React,{createContext, useState, useEffect} from 'react'
import { useNavigate } from 'react-router-dom';

const AuthContext = createContext();


function AuthProvider({children}) {
    const accessToken = localStorage.getItem('accessToken');
    const refreshToken =  localStorage.getItem('refreshToken');
    const [IsAuthen, setIsAuthen] = useState(false);
    const navigate = useNavigate(); 

    const handleRequest = async () => {
        if(!accessToken || !refreshToken){
            setIsAuthen(false)
          navigate('/login')
          return;
        }
        try{
          const response = await fetch('https://localhost:7020/api/Token/refresh',{
            method : 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body : JSON.stringify({accessToken, refreshToken})
          })
          if(!response.ok){
            setIsAuthen(false);
            navigate('/login')
            return;
          }
          var data = await response.json()
          localStorage.setItem('accessToken',data.token)
          setIsAuthen(true);
        }
        catch(err){
          alert('Fetch to failed')
        }
    }
    useEffect( () => {
        handleRequest();
    },[navigate])
    return (
        <AuthContext.Provider value={{IsAuthen, setIsAuthen}}>
            {children}
        </AuthContext.Provider>
    )
}

export default AuthProvider