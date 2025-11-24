import React, { useState, useEffect } from 'react';
import Layout from './Layout';
import { useNavigate, Link } from 'react-router-dom';
import { useLanguage } from './LanguageContext';
import './App.css';

export default function CreateLobby({ toggleSidebar, sidebarOpen }) {
    const { t } = useLanguage();
    const [lobbyCode, setLobbyCode] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        // Generate a random 6-character code (letters and numbers)
        const generateCode = () => {
            const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
            let code = '';
            for (let i = 0; i < 6; i++) {
                code += characters.charAt(Math.floor(Math.random() * characters.length));
            }
            return code;
        };
        setLobbyCode(generateCode());
    }, []);

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="create-lobby-page">
                    <div className="lobby-code-container">
                        <h2 className="lobby-code-title">{t('yourLobbyCode')}</h2>
                        <div className="lobby-code-display">
                            {lobbyCode}
                        </div>
                        <p className="lobby-code-subtitle">{t('shareCode')}</p>
                        <button 
                            className="lobby-back-btn"
                            onClick={() => navigate('/welcome')}
                        >
                            {t('backToWelcome')}
                        </button>
                    </div>

                    {/* Footer */}
                    <footer className="site-footer">
                        <div className="links">
                            <Link to="/about">{t('about')}</Link>
                        </div>
                        <div className="copyright">
                            © {new Date().getFullYear()} Forge Born. All rights reserved.
                        </div>
                    </footer>
                </div>
            </Layout>
        </div>
    );
}

