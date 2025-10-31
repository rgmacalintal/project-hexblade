import React from 'react';
import { Link } from 'react-router-dom';
import Layout from './Layout';

export default function Login({ toggleSidebar, sidebarOpen }) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    async function handleLogin(e) {
        e.preventDefault();

        const response = await fetch('/api/auth/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, password })
        });

        if (response.ok) {
            const data = await response.json();
            console.log('Login success:', data);
            localStorage.setItem('username', data.username);
            alert(`Welcome, ${data.username}!`);
            navigate('/welcome', { state: { username: data.username } });
        } else {
            const error = await response.text();
            console.log('Login failed:', error);
            alert(`Login failed: ${error}`);
        }
    }

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="login-header">
                    <h2 className="login-heading">Login</h2>
                    <p className="subheading">Sign in to continue.</p>
                </div>

                <form className="login-form">
                    <label>Username</label>
                    <input type="text" placeholder="Tracy Chesu" />

                    <label>Password</label>
                    <input type="password" placeholder="********" />

                    <Link
                        to="/welcome"
                        state={{ from: 'login' }}
                        className="login-btn"
                        style={{ display: 'inline-block', textAlign: 'center', textDecoration: 'none', color: 'white' }}
                    >
                        Log in
                    </Link>

                    <p className="footer-links">
                        <Link to="/forgot-password">Forgot Password?</Link>
                        <br />
                        <Link to="/signup">Don't have an Account? Create Account!</Link>
                    </p>
                </form>
            </Layout>
        </div>
    );
}
