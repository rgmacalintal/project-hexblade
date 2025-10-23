import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
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
            //alert(`Welcome, ${data.username}!`);
            //navigate('/welcome');
            console.log('Login success:', data);
            localStorage.setItem('username', data.username);
            alert(`Welcome, ${data.username}!`);
            navigate('/welcome', { state: { username: data.username } });
        } else {
            const error = await response.text();
            console.log('Login unsuccessful:', error);
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

                <form className="login-form" onSubmit={handleLogin}>
                    <label>Username</label>
                    <input type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} required />

                    <label>Password</label>
                    <input type="password" placeholder="********" value={password} onChange={(e) => setPassword(e.target.value)} required />

                    <button type="submit" className="login-btn">
                        Log in
                    </button>

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
