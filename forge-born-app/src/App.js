import React, { useState } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './Login';
import Signup from './Signup';
import Welcome from './Welcome';
import './App.css';

function App() {
    const [sidebarOpen, setSidebarOpen] = useState(false);
    const toggleSidebar = () => setSidebarOpen((prev) => !prev);

    return (
        <BrowserRouter>
            <Routes>
                <Route
                    path="/"
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
            </Routes>
        </BrowserRouter>
    );
}

export default App;
