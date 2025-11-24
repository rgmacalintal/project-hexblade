import React from 'react';
import Layout from './Layout';
import { useLanguage } from './LanguageContext';
import './Welcome.css';
import './App.css';
import { Link } from 'react-router-dom';

export default function HomePage({ toggleSidebar, sidebarOpen }) {
    const { t } = useLanguage();
    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                {/* Header with Login/Signup buttons */}
                <div className="homepage-header">
                    <div className="header-buttons">
                        <Link to="/login" className="header-btn login-btn">{t('loginBtn')}</Link>
                        <Link to="/signup" className="header-btn signup-btn">{t('signUpBtn')}</Link>
                    </div>
                </div>
                
                <div className="homepage">
                    {/* Hero Section */}
                    <section className="hero-section">
                        <div className="hero-content">
                            <h1 className="hero-title">{t('heroTitle')}</h1>
                            <p className="hero-subtitle">{t('heroSubtitle')}</p>
                        </div>
                        <div className="hero-image">
                            <div className="character-container">
                                <img src="/G1.png" alt="Game Image 1" className="character-image active" />
                                <img src="/G2.png" alt="Game Image 2" className="character-image" />
                                <img src="/G3.png" alt="Game Image 3" className="character-image" />
                            </div>
                        </div>
                    </section>

                    {/* Features Section */}
                    <section className="features-section">
                        <h2 className="section-title">A Complete Tabletop for D&D and More</h2>
                        <p className="section-subtitle">Forge Born® is the most complete solution for digital play. Access character sheets, tokens, rulebooks, dice, and more - with powerful tools to automate the tedious stuff.</p>
                        
                        <div className="features-grid">
                            <div className="feature-item">
                                <div className="feature-icon">🎭</div>
                                <h3>Drag & Drop Monsters, Characters, & NPCs</h3>
                            </div>
                           
                            <div className="feature-item">
                                <div className="feature-icon">🎲</div>
                                <h3>Roll 3D Dice</h3>
                            </div>
                            <div className="feature-item">
                                <div className="feature-icon">📹</div>
                                <h3>Integrated Video & Voice</h3>
                            </div>
                            <div className="feature-item">
                                <div className="feature-icon">📋</div>
                                <h3>Interactive Character Sheets</h3>
                            </div>
                        </div>
                    </section>

                    {/* How It Works Section */}
                    <section className="how-it-works">
                        <h2 className="section-title">How to Get Started</h2>
                        <div className="steps-container">
                            <div className="step">
                                <div className="step-number">1</div>
                                <div className="step-icon">👤</div>
                                <h3>Sign Up</h3>
                                <p>Create your free account. Everything else is right in your browser - nothing to download or install.</p>
                            </div>
                            <div className="step">
                                <div className="step-number">2</div>
                                <div className="step-icon">🎮</div>
                                <h3>Choose a Game</h3>
                                <p>Build your own from scratch, buy a ready-to-play adventure in our Marketplace, or join someone's game.</p>
                            </div>
                            <div className="step">
                                <div className="step-number">3</div>
                                <div className="step-icon">👤</div>
                                <h3>Game Wizard</h3>
                                <p>
                                    Use the game wizard to guide you through the game, discover new features, and get started on your adventure with ease.
                                </p>
                            </div>
                            <div className="step">
                                <div className="step-number">4</div>
                                <div className="step-icon">👥</div>
                                <h3>Invite Friends</h3>
                                <p>Share a link with your existing group or find a new party with the Join a Game feature.</p>
                            </div>
                            <div className="step">
                                <div className="step-number">5</div>
                                <div className="step-icon">⚔️</div>
                                <h3>Play</h3>
                                <p>Start gaming! We've got you covered from basic rolls to advanced calculations, turn trackers to simple markers.</p>
                            </div>
                        </div>
                    </section>

                    {/* Customizable Section */}
                    <section className="customizable-section">
                        <h2 className="section-title">Customizable</h2>
                        <div className="customizable-grid">
                            <div className="customizable-item">
                                <div className="customizable-icon">🎨</div>
                                <h3>Rules</h3>
                                <p>Upload your own or choose from our Marketplace full of talented artists.</p>
                            </div>
                            <div className="customizable-item">
                                <div className="customizable-icon">📄</div>
                                <h3>Character Sheets</h3>
                                <p>Hundreds of sheets to automatically track and calculate character information, or build your own.</p>
                            </div>
                            <div className="customizable-item">
                                <div className="customizable-icon">⚙️</div>
                                <h3>Programming Scripts</h3>
                                <p>Automate tedious game mechanics: get hundreds of options you can add with one click.</p>
                            </div>
                        </div>
                    </section>

                    {/* Community Section */}
                    <section className="community-section">
                        <h2 className="section-title">Community</h2>
                        <p className="section-subtitle">Find people to round out your party, or start with a fresh pack of players. You can even find a game starting right away. All possible because of our huge and amazing community. Once you find them, it's easy to play and connect with built-in video and voice chat, text chat, and integrated rolling.</p>
                        <div className="community-stats">
                            <div className="stat">
                                <div className="stat-number">2k+</div>
                                <div className="stat-label">Players</div>
                            </div>
                            <div className="stat">
                                <div className="stat-number">Many</div>
                                <div className="stat-label">Game Systems</div>
                            </div>
                            <div className="stat">
                                <div className="stat-number">Tons</div>
                                <div className="stat-label">Of Games Played</div>
                            </div>
                        </div>
                    </section>

                    {/* CTA Section */}
                    <section className="cta-section">
                        <h2 className="cta-title">Play on Forgeborn® for Free</h2>
                        <p className="cta-subtitle">Join over 15 million players and DMs playing dungeons and dragons online</p>
                        <Link to="/signup" className="cta-button primary large">Create Free Account</Link>
                        <p className="cta-login">Already have an account? <Link to="/login">Login</Link></p>
                    </section>

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

