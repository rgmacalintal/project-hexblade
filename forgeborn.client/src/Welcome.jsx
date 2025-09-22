import React from 'react';
import Layout from './Layout';
import './Welcome.css';
import './App.css';

export default function Welcome({ toggleSidebar, sidebarOpen }) {
    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="welcome-page">
                    <h1 className="welcome-text">Welcome user987654</h1>
                </div>
            </Layout>
        </div>
    );
}
