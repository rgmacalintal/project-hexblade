import React from 'react';
import { Link } from 'react-router-dom';

export default function Layout({ sidebarOpen, toggleSidebar, children }) {
    return (
        <div className={`login-wrapper ${sidebarOpen ? 'sidebar-open' : ''}`}>
            <header>
                <div className="top-bar">
                    <div className="left-side">
                        <button className="language-btn">Language</button>
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
                    <div className="right-side"></div>
                </div>

                <div className="center-logo">
                    <img src="/forgelogo.png" alt="Logo" className="logo-img" />
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
                    <li>Character Sheet</li>
                    <li>Profile</li>
                    <li>Wizard</li>
                    <li>Help</li>
                    <li>LogOut</li>
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
