import React from 'react';
import { Link } from 'react-router-dom';
import Layout from './Layout';

export default function Signup({ toggleSidebar, sidebarOpen }) {
    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="signup-page">
                    <div className="login-header">
                        <h2 className="login-heading">Create Account</h2>
                        <p className="subheading">Sign up to get started.</p>
                    </div>

                    <form className="login-form">
                        <label>Name</label>
                        <input type="text" placeholder="Your Name" />

                        <label>Email</label>
                        <input type="email" placeholder="tracy100@gmail.com" />

                        <label>Password</label>
                        <input type="password" placeholder="********" />

                        <label>Re-enter Password</label>
                        <input type="password" placeholder="********" />

                        <button type="submit" className="login-btn">
                            Sign Up
                        </button>

                        <p className="footer-links">
                            <Link to="/">Already have an account? Login</Link>
                        </p>
                    </form>
                </div>
            </Layout>
        </div>
    );
}
