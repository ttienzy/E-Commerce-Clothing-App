import { Link, Outlet } from "react-router-dom"


const DashBoard = () => {
    return (
        <div>
            <h1>Welcome to website</h1>
            <Link to='/dashboard/user'>User</Link>
            <Link to='/dashboard/admin'>Admin</Link>

            <Outlet />
        </div>
    )
}


export default DashBoard;