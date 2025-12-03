import React, { useState, useEffect } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { useLanguage } from './language/UseLanguage';

export default function Layout({ sidebarOpen, toggleSidebar, children }) {
    const { Language, setLanguage, t } = useLanguage();
    const [IsDarkTheme, setIsDarkTheme] = useState(false);
    const [OpenSubmenu, setOpenSubmenu] = useState(null);
    const [characterSheetSidebarOpen, setCharacterSheetSidebarOpen] = useState(false);
    const [languageSidebarOpen, setLanguageSidebarOpen] = useState(false);
    const [themeSidebarOpen, setThemeSidebarOpen] = useState(false);
    const [helpModalOpen, setHelpModalOpen] = useState(false);
    const location = useLocation();
    const navigate = useNavigate();

    // Unused variables
    //const Language = language;
    //const IsDarkTheme = isDarkTheme;
    //const OpenSubmenu = openSubmenu;
    //const OnToggleSubmenu = onToggleSubmenu;
    //const ShouldShowProfileIcon = shouldShowProfileIcon;
    //const HandleProfileClick = handleProfileClick;

    useEffect(() => {
        // Check for saved theme preference or default to cream
        const savedTheme = localStorage.getItem('theme');

        if (!savedTheme) {
            setIsDarkTheme(true);
            document.body.classList.add('dark-theme');
            localStorage.setItem('theme', 'dark');
            return;
        }

        if (savedTheme === 'dark') {
            setIsDarkTheme(true);
            document.body.classList.add('dark-theme');
        } else {
            setIsDarkTheme(false);
            document.body.classList.remove('dark-theme');
        }
    }, []);

    const toggleThemeSidebar = () => {
        setThemeSidebarOpen((prev) => !prev);
    };

    const closeThemeSidebar = () => {
        setThemeSidebarOpen(false);
    };

    const handleThemeSelect = (theme) => {
        if (theme === 'dark') {
            setIsDarkTheme(true);
            document.body.classList.add('dark-theme');
            localStorage.setItem('theme', 'dark');
        } else {
            setIsDarkTheme(false);
            document.body.classList.remove('dark-theme');
            localStorage.setItem('theme', 'cream');
        }
        closeThemeSidebar();
        toggleSidebar();
    };

    const openHelpModal = () => {
        setHelpModalOpen(true);
        toggleSidebar();
    };

    const closeHelpModal = () => {
        setHelpModalOpen(false);
    };

    // Unused function
    const OnToggleSubmenu = (key) => {
        console.log('Toggling submenu:', key);
        setOpenSubmenu((prev) => {
            const newValue = prev === key ? null : key;
            console.log('New submenu state:', newValue);
            return newValue;
        });
    };

    const toggleCharacterSheetSidebar = () => {
        setCharacterSheetSidebarOpen((prev) => !prev);
    };

    const closeCharacterSheetSidebar = () => {
        setCharacterSheetSidebarOpen(false);
    };

    const handleCreateCharacter = () => {
        const username = localStorage.getItem('username');
        if (!username) {
            // Not logged in - redirect to login with flag to open character sheet after login
            localStorage.setItem('redirectAfterLogin', 'createCharacter');
            navigate('/login');
            closeCharacterSheetSidebar();
            toggleSidebar();
        } else {
            // Logged in - open character sheet
            localStorage.setItem('openCharacterSheet', 'true');
            navigate('/profile', { state: { openCharacterSheet: true } });
            closeCharacterSheetSidebar();
            toggleSidebar();
        }
    };

    const toggleLanguageSidebar = () => {
        setLanguageSidebarOpen((prev) => !prev);
    };

    const closeLanguageSidebar = () => {
        setLanguageSidebarOpen(false);
    };

    const handleLanguageSelect = (lang) => {
        setLanguage(lang);
        closeLanguageSidebar();
        toggleSidebar();
    };

    // Unused function
    const ShouldShowProfileIcon = (() => {
        const username = localStorage.getItem('username');
        if (!username) return false; // Only show if logged in
        if (location.pathname === '/') return false;
        if (location.pathname === '/signup') return false;
        if (location.pathname === '/welcome') {
            // Hide on welcome when we came from signup
            //const from = location.state?.from; solve unused variable blank page?
            const from = location.state && location.state.from;
            if (from === 'signup') return false;
        }
        return true;
    })();

    // Unused function
    const HandleProfileClick = (e) => {
        const username = localStorage.getItem('username');
        if (!username) {
            e.preventDefault();
            navigate('/login');
        }
    };

    function handleLogout() {
        const username = localStorage.getItem('username');

        if (!username) {
            alert('Please login first.');
            navigate('/login');
            return;
        }

        localStorage.removeItem('username');
        alert('You have been logged out.');
        navigate('/login');
    }

    return (
        <div className={`login-wrapper ${sidebarOpen ? 'sidebar-open' : ''}`}>
            <header>
                <div className="top-bar">
                    <div className="left-side">
                        {!sidebarOpen && (
                            <button
                                className="menu-btn"
                                onClick={toggleSidebar}
                                aria-label="Open menu"
                            >
                                <div className="menu-icon">☰</div>
                            </button>
                        )}
                    </div>
                </div>

                <div className="center-logo">
                    <span
                        onClick={() => {
                            const username = localStorage.getItem('username');

                            toggleSidebar(); // close menu

                            if (username) {
                                navigate('/welcome');
                            } else {
                                navigate('/');
                            }
                        }}
                        style={{ color: 'inherit', textDecoration: 'none', cursor: 'pointer', textAlign: 'center' }}
                    >
                        <img src="/FORGEBORN_Text.png" alt="Logo" className="logo-img" width="50%" />
                        <img src="/SwordShield.png" alt="Logo" className="logo-img" width="10%" />
                    </span>
                </div>
            </header>

            {/* Sidebar */}
            <nav className={`sidebar ${sidebarOpen ? 'open' : ''}`}>
                <ul>
                    {sidebarOpen && (
                        <li>
                            <button
                                className="menu-btn sidebar-menu-btn"
                                onClick={toggleSidebar}
                                aria-label="Close menu"
                            >
                                <div className="menu-icon">☰</div> {t('closeMenu')}
                            </button>
                        </li>
                    )}
                    <li>
                        <span
                            onClick={() => {
                                const username = localStorage.getItem('username');

                                toggleSidebar(); // close menu

                                if (username) {
                                    navigate('/welcome');
                                } else {
                                    navigate('/');
                                }
                            }}
                            style={{ color: 'inherit', textDecoration: 'none', cursor: 'pointer' }}
                        >
                            {t('homePage')}
                        </span>
                    </li>
                    <li>
                        <Link to="/profile" onClick={toggleSidebar} style={{ color: 'inherit', textDecoration: 'none' }}>
                            {t('profile')}
                        </Link>
                    </li>
                    <li>
                        <span onClick={() => { toggleCharacterSheetSidebar(); }} style={{ cursor: 'pointer' }}>{t('characterSheet')}</span>
                    </li>
                    <li>
                        <span onClick={() => { toggleLanguageSidebar(); }} style={{ cursor: 'pointer' }}>{t('language')}</span>
                    </li>
                    <li>
                        <span onClick={() => { toggleThemeSidebar(); }} style={{ cursor: 'pointer' }}>{t('theme')}</span>
                    </li>
                    <li>
                        <Link to="/about" onClick={toggleSidebar} style={{ color: 'inherit', textDecoration: 'none' }}>
                            {t('about')}
                        </Link>
                    </li>
                    <li onClick={openHelpModal} style={{ cursor: 'pointer' }}>{t('help')}</li>
                    <li onClick={handleLogout} style={{ cursor: 'pointer', color: 'red' }}>{t('logOut')}</li>
                </ul>
            </nav>

            {/* Character Sheet Sidebar */}
            <nav className={`character-sheet-sidebar ${characterSheetSidebarOpen ? 'open' : ''}`}>
                <ul>
                    <li>
                        <button
                            className="menu-btn sidebar-menu-btn"
                            onClick={closeCharacterSheetSidebar}
                            aria-label="Close character sheet menu"
                        >
                            <div className="menu-icon">☰</div> {t('close')}
                        </button>
                    </li>
                    <li onClick={handleCreateCharacter} style={{ cursor: 'pointer' }}>
                        {t('createCharacter')}
                    </li>
                    <li onClick={() => { navigate('/profile'); closeCharacterSheetSidebar(); toggleSidebar(); }} style={{ cursor: 'pointer' }}>
                        {t('allCharacterSheets')}
                    </li>
                </ul>
            </nav>

            {/* Language Sidebar */}
            <nav className={`language-sidebar ${languageSidebarOpen ? 'open' : ''}`}>
                <ul>
                    <li>
                        <button
                            className="menu-btn sidebar-menu-btn"
                            onClick={closeLanguageSidebar}
                            aria-label="Close language menu"
                        >
                            <div className="menu-icon">☰</div> {t('close')}
                        </button>
                    </li>
                    <li onClick={() => handleLanguageSelect('en')} style={{ cursor: 'pointer' }}>
                        {t('english')}
                    </li>
                    <li onClick={() => handleLanguageSelect('fr')} style={{ cursor: 'pointer' }}>
                        {t('french')}
                    </li>
                </ul>
            </nav>

            {/* Theme Sidebar */}
            <nav className={`theme-sidebar ${themeSidebarOpen ? 'open' : ''}`}>
                <ul>
                    <li>
                        <button
                            className="menu-btn sidebar-menu-btn"
                            onClick={closeThemeSidebar}
                            aria-label="Close theme menu"
                        >
                            <div className="menu-icon">☰</div> {t('close')}
                        </button>
                    </li>
                    <li onClick={() => handleThemeSelect('cream')} style={{ cursor: 'pointer' }}>
                        {t('creamTheme')}
                    </li>
                    <li onClick={() => handleThemeSelect('dark')} style={{ cursor: 'pointer' }}>
                        {t('darkTheme')}
                    </li>
                </ul>
            </nav>

            {/* Page content */}
            <main>{children}</main>

            {/* Overlay */}
            {(sidebarOpen || characterSheetSidebarOpen || languageSidebarOpen || themeSidebarOpen) && (
                <div 
                    className="overlay" 
                    onClick={() => {
                        if (sidebarOpen) toggleSidebar();
                        if (characterSheetSidebarOpen) closeCharacterSheetSidebar();
                        if (languageSidebarOpen) closeLanguageSidebar();
                        if (themeSidebarOpen) closeThemeSidebar();
                    }} 
                />
            )}

            {/* Help Modal */}
            {helpModalOpen && (
                <div className="modal-overlay" onClick={closeHelpModal}>
                    <div className="modal-content dark" onClick={(e) => e.stopPropagation()}>
                        <button className="modal-close" onClick={closeHelpModal} aria-label="Close">×</button>
                        <h2 style={{ marginBottom: '20px', color: '#ffffff', textAlign: 'center' }}>{t('help')}</h2>
                        <p style={{ fontSize: '16px', color: '#cccccc', textAlign: 'center', lineHeight: '1.6' }}>
                            {t('helpContactText')}
                        </p>
                    </div>
                </div>
            )}
        </div>
    );
}
