import React, { useState } from 'react';
import Layout from './Layout';
import './Welcome.css';
import './App.css';
import { useLocation, useNavigate } from 'react-router-dom';
import { useEffect } from 'react';

export default function Welcome({ toggleSidebar, sidebarOpen }) {
    const [isCharModalOpen, setIsCharModalOpen] = useState(false);
    const location = useLocation();
    const navigate = useNavigate();
    const username = location.state?.username || localStorage.getItem('username');

    useEffect(() => {
        if (!username) {
            alert('Please login first.');
            navigate('/login');
        }
    }, [username, navigate]);

    const openCharModal = () => setIsCharModalOpen(true);
    const closeCharModal = () => setIsCharModalOpen(false);

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="welcome-page">
                    <h1 className="welcome-text">
                        Welcome {username} to Forgeborn!
                    </h1>
                    <p className="welcome-subtitle">Your journey into the realm of adventure begins here</p>

                    <div className="lobby-actions">
                        <button onClick={() => navigate('/create-lobby')} className="header-btn">Create Lobby</button>
                        <button onClick={() => navigate('/join-lobby')} className="header-btn">Join Lobby</button>
                    </div>
                    
                    <div className="welcome-features">
                        <div className="feature-card" onClick={openCharModal} role="button" aria-label="Open Character Creation choices">
                            <span className="feature-icon">⚔️</span>
                            <h3 className="feature-title">Character Creation</h3>
                            <p className="feature-description">Forge your unique hero with our comprehensive character builder and customization tools.</p>
                        </div>
                        
                        <div className="feature-card">
                            <span className="feature-icon">🎲</span>
                            <h3 className="feature-title">Interactive Dice</h3>
                            <p className="feature-description">Roll the dice and let fate decide your destiny in epic adventures and battles.</p>
                        </div>
                        
                        <div className="feature-card">
                            <span className="feature-icon">📜</span>
                            <h3 className="feature-title">Story Campaigns</h3>
                            <p className="feature-description">Embark on thrilling campaigns with rich narratives and challenging quests.</p>
                        </div>

                      
                        <div className="feature-card">
                            <span className="feature-icon">👥</span>
                            <h3 className="feature-title">Multiplayer</h3>
                            <p className="feature-description">Join forces with friends and create unforgettable memories together.</p>
                        </div>
                    </div>
                </div>
            </Layout>

            {isCharModalOpen && (
                <div className="modal-overlay" onClick={closeCharModal}>
                    <div className="modal-content dark" onClick={(e) => e.stopPropagation()}>
                        <button className="modal-close" onClick={closeCharModal} aria-label="Close">×</button>
                        <div className="choice-header"> create your personal character</div>
                        <div className="choice-grid">
                            <div className="choice-card" role="button" aria-label="Create with Wizard">
                                <div className="choice-icon">🧙</div>
                                <div className="choice-title">Wizard</div>
                                <div className="choice-sub">Guided step-by-step setup</div>
                            </div>
                           
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}
