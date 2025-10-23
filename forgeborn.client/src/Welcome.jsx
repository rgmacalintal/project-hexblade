import React from 'react';
import { useLocation } from 'react-router-dom';
import Layout from './Layout';
import './Welcome.css';
import './App.css';

export default function Welcome({ toggleSidebar, sidebarOpen }) {
    const location = useLocation();
    const username = location.state?.username || localStorage.getItem('username') || 'User';

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="welcome-page">
                    <h1 className="welcome-text">Welcome {username}!</h1>
                </div>
            </Layout>
        </div>
    );
}
