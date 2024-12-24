import { useRouteError } from "react-router-dom";   
import '../css/ErrorPage.css'

const ErrorPage = () => {

  const error = useRouteError();
  console.log(error);

  return (
    <div className="error-container">
      <div className="error-content">
        <h1 className="error-title">Access Denied</h1>
        <p className="error-message">
          Sorry, you don't have permission to access this page.
        </p>
        
        {error && (
          <div className="error-details">
            Error: {error.status} - {error.message}
          </div>
        )}

        <a href="/" className="home-button">
          Return to Home
        </a>
        
        {error && (
          <p className="status-text">
            Status: {error.status}
          </p>
        )}
      </div>
    </div>
  );
}

export default ErrorPage;