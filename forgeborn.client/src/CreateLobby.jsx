import React, { useEffect, useState } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { useNavigate } from 'react-router-dom';
import Layout from './Layout';

export default function CreateLobby({ toggleSidebar, sidebarOpen }) {
    const [connection, setConnection] = useState(null);
    const username = localStorage.getItem('username');
    const navigate = useNavigate();

    useEffect(() => {
        if (!username) {
            alert('Please login first.');
            navigate('/login');
        }
    }, [username, navigate]);

    async function handleCreateLobby() {
        const conn = new HubConnectionBuilder()
            .withUrl('/api/lobbyHub')
            .withAutomaticReconnect()
            .build();

        conn.on("JoinFailed", (msg) => alert(msg));

        conn.on("LobbyCreated", (code) => {
            console.log('Lobby created:', code);
            navigate(`/lobby/${code}`);
        });

        try {
            await conn.start();
            await conn.invoke("CreateLobby", username);
            setConnection(true);
        } catch (err) {
            console.error(err);
            alert("Could not create lobby.");
        }
    }

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="create-lobby-page">
                    <h2>Create Lobby</h2>
                    <button className="header-btn" onClick={handleCreateLobby}>Create Lobby</button>
                </div>
            </Layout>
        </div>
    );
}
