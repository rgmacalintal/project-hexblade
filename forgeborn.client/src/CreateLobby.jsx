import React, { useEffect } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { useNavigate } from 'react-router-dom';
import Layout from './Layout';

export default function CreateLobby({ toggleSidebar, sidebarOpen }) {
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

        conn.on("LobbyCreated", async (code) => {
            console.log('Lobby created:', code);
            localStorage.setItem("currentLobbyCode", code);
            navigate(`/lobby/${code}`);
        });

        try {
            await conn.start();
            await conn.invoke("CreateLobby", username);
        } catch (err) {
            console.error(err);
            alert("Could not create lobby.");
        }
    }

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="create-lobby-page">
                    <div className="lobby-code-container">
                        <h2 className="lobby-code-title">Become a Dungeon Master</h2>

                        <p className="lobby-code-subtitle">
                            Generate a code to your lobby to share.
                        </p>

                        <button
                            className="header-btn signup-btn"
                            onClick={handleCreateLobby}
                        >
                            Create Lobby
                        </button>

                        <button
                            className="lobby-back-btn"
                            onClick={() => navigate('/welcome')}
                        >
                            Back
                        </button>
                    </div>
                </div>
            </Layout>
        </div>
    );
}
