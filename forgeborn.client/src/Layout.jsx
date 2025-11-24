import React, { useState, useEffect } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';

export default function Layout({ sidebarOpen, toggleSidebar, children }) {
    const [isDarkTheme, setIsDarkTheme] = useState(false);
    const [openSubmenu, setOpenSubmenu] = useState(null);
    const location = useLocation();
    const navigate = useNavigate();

    useEffect(() => {
        // Check for saved theme preference or default to light
        const savedTheme = localStorage.getItem('theme');
        if (savedTheme === 'dark') {
            setIsDarkTheme(true);
            document.body.classList.add('dark-theme');
        } else {
            setIsDarkTheme(false);
            document.body.classList.remove('dark-theme');
        }
    }, []);

    const toggleTheme = (e) => {
        e.preventDefault();
        e.stopPropagation();
        
        const newTheme = !isDarkTheme;
        setIsDarkTheme(newTheme);
        
        if (newTheme) {
            document.body.classList.add('dark-theme');
            localStorage.setItem('theme', 'dark');
        } else {
            document.body.classList.remove('dark-theme');
            localStorage.setItem('theme', 'light');
        }
        
        console.log('Theme toggled to:', newTheme ? 'dark' : 'light');
    };

    const onToggleSubmenu = (key) => {
        console.log('Toggling submenu:', key);
        setOpenSubmenu((prev) => {
            const newValue = prev === key ? null : key;
            console.log('New submenu state:', newValue);
            return newValue;
        });
    };

    const shouldShowProfileIcon = (() => {
        if (location.pathname === '/') return false;
        if (location.pathname === '/signup') return false;
        if (location.pathname === '/welcome') {
            // Hide on welcome when we came from signup
            const from = location.state && location.state.from;
            if (from === 'signup') return false;
        }
        return true;
    })();

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
                    {shouldShowProfileIcon && (
                        <div className="right-side">
                            <Link to="/profile" className="profile-icon-btn" aria-label="Open profile">
                                <svg viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
                                    <circle cx="12" cy="8" r="4" strokeWidth="2" />
                                    <path d="M4 20c0-4 4-6 8-6s8 2 8 6" strokeWidth="2" strokeLinecap="round" />
                                </svg>
                            </Link>
                        </div>
                    )}
                </div>

                <div className="center-logo">
                    <img src="/FORGEBORN.png" alt="Logo" className="logo-img" />
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
                                <div className="menu-icon">☰</div> Close Menu
                            </button>
                        </li>
                    )}
                    <li>
                        <Link to="/" onClick={toggleSidebar} style={{ color: 'inherit', textDecoration: 'none' }}>
                            Home Page
                        </Link>
                    </li>
                    <li className={`has-submenu ${openSubmenu === 'character' ? 'open' : ''}`}>
                        <span onClick={() => onToggleSubmenu('character')} style={{ cursor: 'pointer' }}>Character Sheet</span>
                        <ul className="submenu" onClick={(e) => e.stopPropagation()}>
                            <li>Create Character</li>
                            <li>Load Character</li>
                            <li>Character Stats</li>
                            <li>Equipment</li>
                        </ul>
                    </li>
                    <li>
                        <Link to="/profile" onClick={toggleSidebar} style={{ color: 'inherit', textDecoration: 'none' }}>
                            Profile
                        </Link>
                    </li>
                    <li className="has-submenu">
                        <span>Language</span>
                        <ul className="submenu">
                            <li>English</li>
                            <li>French</li>
                        </ul>
                    </li>
                    <li className="has-submenu">
                        <span>Theme</span>
                        <ul className="submenu">
                            <li onClick={toggleTheme} className="theme-toggle" style={{cursor: 'pointer'}}>
                                {isDarkTheme ? '🌞 Switch to Light' : '🌙 Switch to Dark'}
                            </li>
                            <li>Light Mode</li>
                            <li>Dark Mode</li>
                            <li>Auto Theme</li>
                        </ul>
                    </li>
                    <li>Help</li>
                    <li onClick={handleLogout} style={{ cursor: 'pointer', color: 'red' }}>Log Out</li>
                    <li>About</li>
                </ul>
            </nav>

            {/* Page content */}
            <main>{children}</main>

            {/* Overlay */}
            {sidebarOpen && <div className="overlay" onClick={toggleSidebar} />}
        </div>
    );
}
