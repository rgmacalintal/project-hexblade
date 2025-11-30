import React from 'react';
import Layout from './Layout';
import { Link } from 'react-router-dom';
import { useLanguage } from './language/UseLanguage';
import './App.css';

export default function ForgotPassword({ toggleSidebar, sidebarOpen }) {
    const { t } = useLanguage();

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="about-page">
                    <div className="about-container">
                        <h1 className="about-title">{t('forgotPassword')}</h1>

                        <div className="about-content" style={{ textAlign: 'center' }}>
                            <p className="about-text" style={{ marginBottom: '20px' }}>
                                {t('featureUnavailable') || 'This feature is currently unavailable.'}
                            </p>

                            <p className="about-text" style={{ opacity: 0.8 }}>
                                {t('contactAdmin') || 'Please contact the administrator if you need assistance.'}
                            </p>

                            <p style={{ marginTop: '20px' }}>
                                <Link to="/login" style={{ color: '#ffa756', fontWeight: '600' }}>
                                    {t('backToLogin') || 'Back to Login'}
                                </Link>
                            </p>
                        </div>
                    </div>

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
