import React from 'react';
import { Link } from 'react-router-dom';
import Layout from './Layout';

export default function Login({ toggleSidebar, sidebarOpen }) {
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
