import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import Layout from './Layout';

export default function Signup({ toggleSidebar, sidebarOpen }) {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const navigate = useNavigate();

    async function handleSignup(e) {
        e.preventDefault();

        if (password !== confirmPassword) {
            alert("Passwords do not match.");
            return;
        }

        try {
            const response = await fetch('/api/auth/register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ username, email, password })
            });

            if (response.ok) {
                const data = await response.json();
                console.log('Registration success:', data);
                alert(data.message || "Registration successful.");
                navigate('/');
            } else if (response.status === 409) {
                const conflictMsg = await response.text();
                alert(conflictMsg);
            } else {
                const errorText = await response.text();
                alert(errorText || "Registration failed.");
            }
        } catch (error) {
            console.error("Registration failed:", error);
            alert(`Registration failed: ${error}`);
        }
    }

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="signup-page">
                    <div className="login-header">
                        <h2 className="login-heading">Create Account</h2>
                        <p className="subheading">Sign up to get started.</p>
                    </div>

                    <form className="login-form" onSubmit={handleSignup}>
                        <label>Username</label>
                        <input
                            type="text"
                            placeholder="Username"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            required
                        />

                        <label>Email</label>
                        <input
                            type="email"
                            placeholder="test@test.com"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />

                        <label>Password</label>
                        <input
                            type="password"
                            placeholder="********"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />

                        <label>Confirm Password</label>
                        <input
                            type="password"
                            placeholder="********"
                            value={confirmPassword}
                            onChange={(e) => setConfirmPassword(e.target.value)}
                            required
                        />

                        <Link to="/welcome" state={{ from: 'signup' }} className="login-btn" style={{ display: 'inline-block', textAlign: 'center', textDecoration: 'none', color: 'white' }}>
                            Sign Up
                        </Link>

                        <p className="footer-links">
                            <Link to="/">Already have an account? Login</Link>
                        </p>
                    </form>
                </div>
            </Layout>
        </div>
    );
}
