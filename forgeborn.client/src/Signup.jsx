import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import Layout from './Layout';
import { useLanguage } from './language/UseLanguage';

export default function Signup({ toggleSidebar, sidebarOpen }) {
    const { t } = useLanguage();
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const navigate = useNavigate();

    async function handleSignup(e) {
        e.preventDefault();

        if (password !== confirmPassword) {
            alert('Passwords do not match.');
            return;
        }

        try {
            const response = await fetch('/api/auth/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ username, email, password }),
            });

            if (response.ok) {
                const data = await response.json();
                localStorage.setItem('username', data.username || username);
                navigate('/welcome', { state: { username: data.username || username } });
            } else {
                const error = await response.json();
                alert(error.message || 'Signup failed. Please try again.');
            }
        } catch (error) {
            console.error('Signup error:', error);
            alert('An error occurred during signup. Please try again.');
        }
    }

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="signup-page">
                    <div className="login-header">
                        <h2 className="login-heading">{t('createAccount')}</h2>
                        <p className="subheading">{t('signUp')}</p>
                    </div>

                    <form className="login-form" onSubmit={handleSignup}>
                        <label>{t('username')}</label>
                        <input
                            type="text"
                            placeholder={t('username')}
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            required
                        />

                        <label>{t('email')}</label>
                        <input
                            type="email"
                            placeholder={t('emailAddress')}
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />

                        <label>{t('password')}</label>
                        <input
                            type="password"
                            placeholder="********"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                        />

                        <label>{t('confirmPassword')}</label>
                        <input
                            type="password"
                            placeholder="********"
                            value={confirmPassword}
                            onChange={(e) => setConfirmPassword(e.target.value)}
                            required
                        />

                        <button type="submit" className="login-btn">
                            {t('signUpBtn')}
                        </button>

                        <p className="footer-links">
                            <Link to="/login">{t('alreadyHaveAccountLogin')}</Link>
                        </p>
                    </form>

                    {/* Footer */}
                    <footer className="site-footer">
                        <div className="links">
                            <Link to="/about">{t('about')}</Link>
                        </div>
                        <div className="copyright">
                            © {new Date().getFullYear()} Forgeborn. All rights reserved.
                        </div>
                    </footer>
                </div>
            </Layout>
        </div>
    );
}
