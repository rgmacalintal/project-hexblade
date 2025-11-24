import React, { useState } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './Login';
import Signup from './Signup';
import Welcome from './Welcome';
import HomePage from './HomePage';
import Profile from './Profile';
import About from './About';
import CreateLobby from './CreateLobby';
import JoinLobby from './JoinLobby';
import { LanguageProvider } from './LanguageContext';
import './App.css';

function App() {
    const [sidebarOpen, setSidebarOpen] = useState(false);
    const toggleSidebar = () => setSidebarOpen((prev) => !prev);

    return (
        <LanguageProvider>
            <BrowserRouter>
            <Routes>
                <Route
                    path="/"
                    element={<HomePage toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen} />}
                />
                <Route
                    path="/login"
                    element={<Login toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen} />}
                />
                <Route
                    path="/signup"
                    element={<Signup toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen} />}
                />
                <Route
                    path="/welcome"
                    element={<Welcome toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen} />}
                />
                <Route
                    path="/profile"
                    element={<Profile toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen} />}
                />
                <Route
                    path="/about"
                    element={<About toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen} />}
                />
                <Route
                    path="/create-lobby"
                    element={<CreateLobby toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen} />}
                />
                <Route
                    path="/join-lobby"
                    element={<JoinLobby toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen} />}
                />
            </Routes>
        </BrowserRouter>
        </LanguageProvider>
    );
}

export default App;
