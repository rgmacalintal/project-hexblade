import React, { useState } from 'react';
import Layout from './Layout';
import { useNavigate, Link } from 'react-router-dom';
import { useLanguage } from './LanguageContext';
import './App.css';

export default function JoinLobby({ toggleSidebar, sidebarOpen }) {
    const { t } = useLanguage();
    const [lobbyCode, setLobbyCode] = useState('');
    const navigate = useNavigate();

    const handleJoin = () => {
        if (lobbyCode.trim().length === 0) {
            alert('Please enter a lobby code');
            return;
        }
        // Here you would typically validate and join the lobby
        console.log('Joining lobby with code:', lobbyCode);
        alert(`Joining lobby: ${lobbyCode}`);
        // Navigate to lobby/game page or show success message
    };

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="join-lobby-page">
                    <div className="join-lobby-container">
                        <h2 className="join-lobby-title">{t('joinLobbyTitle')}</h2>
                        <p className="join-lobby-subtitle">{t('enterLobbyCode')}</p>
                        
                        <div className="join-lobby-input-container">
                            <input
                                type="text"
                                className="join-lobby-input"
                                placeholder={t('enter6DigitCode')}
                                value={lobbyCode}
                                onChange={(e) => setLobbyCode(e.target.value.toUpperCase())}
                                maxLength={6}
                            />
                        </div>

                        <button 
                            className="join-lobby-btn"
                            onClick={handleJoin}
                        >
                            {t('join')}
                        </button>

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

