import React from 'react';
import Layout from './Layout';
import { Link } from 'react-router-dom';
import { useLanguage } from './language/UseLanguage';
import './App.css';

export default function About({ toggleSidebar, sidebarOpen }) {
    const { t } = useLanguage();
    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="about-page">
                    <div className="about-container">
                        <h1 className="about-title">{t('aboutForgeBorn')}</h1>
                        <div className="about-content">
                            <p className="about-text">
                                {t('aboutText')}
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

