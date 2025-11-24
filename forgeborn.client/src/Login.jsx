import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import Layout from './Layout';
import { useLanguage } from './language/UseLanguage';

export default function Login({ toggleSidebar, sidebarOpen }) {
    const { t } = useLanguage();
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    async function handleLogin(e) {
        e.preventDefault();

        try {
            const response = await fetch('/api/auth/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ username, password }),
            });

            if (response.ok) {
                const data = await response.json();
                localStorage.setItem('username', data.username || username);
                
                // Check if user wanted to create character after login
                const redirectAfterLogin = localStorage.getItem('redirectAfterLogin');
                if (redirectAfterLogin === 'createCharacter') {
                    localStorage.removeItem('redirectAfterLogin');
                    localStorage.setItem('openCharacterSheet', 'true');
                    navigate('/profile', { state: { openCharacterSheet: true } });
                } else {
                    navigate('/welcome', { state: { username: data.username || username } });
                }
            } else {
                const error = await response.json();
                alert(error.message || 'Login failed. Please check your credentials.');
            }
        } catch (error) {
            console.error('Login error:', error);
            alert('An error occurred during login. Please try again.');
        }
    }

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="login-header">
                    <h2 className="login-heading">{t('login')}</h2>
                    <p className="subheading">{t('signIn')}</p>
                </div>

                <form className="login-form" onSubmit={handleLogin}>
                    <label>{t('username')}</label>
                    <input
                        type="text"
                        placeholder={t('username')}
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
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

                    <button type="submit" className="login-btn">
                        {t('login')}
                    </button>

                    <p className="footer-links">
                        <Link to="/forgot-password">{t('forgotPassword')}</Link>
                        <br />
                        <Link to="/signup">{t('dontHaveAccount')}</Link>
                    </p>
                </form>

                {/* Footer */}
                <footer className="site-footer">
                    <div className="links">
                        <Link to="/about">{t('about')}</Link>
                    </div>
                    <div className="copyright">
                        © {new Date().getFullYear()} Forge Born. All rights reserved.
                    </div>
                </footer>
            </Layout>
        </div>
    );
}
