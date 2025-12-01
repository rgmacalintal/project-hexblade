import React, { useEffect, useState } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { useNavigate } from 'react-router-dom';
import Layout from './Layout';

export default function JoinLobby({ toggleSidebar, sidebarOpen }) {
    const [lobbyCode, setLobbyCode] = useState('');
    const username = localStorage.getItem('username');
    const navigate = useNavigate();

    useEffect(() => {
        if (!username) {
            alert('Please login first.');
            navigate('/login');
        }
    }, [username, navigate]);

    async function handleJoin() {
        if (!lobbyCode.trim()) {
            alert('Please enter a valid lobby code.');
            return;
        }

        const code = lobbyCode.toUpperCase();

        let isHost = false;
        try {
            const response = await fetch(`/api/Lobbys/IsHost?code=${code}&username=${username}`);
            isHost = await response.json();
        } catch (err) {
            console.error("Failed checking host:", err);
        }

        const conn = new HubConnectionBuilder()
            .withUrl('/api/lobbyHub')
            .withAutomaticReconnect()
            .build();

        conn.on('JoinFailed', msg => {
            alert(msg);
            conn.stop();
        });

        conn.on('LobbyClosed', () => {
            alert('This lobby is closed.');
            navigate('/welcome');
        });

        try {
            await conn.start();

            if (isHost) {
                console.log("Reconnecting as host...");
                await conn.invoke('ReconnectHost', code, username);
            } else {
                console.log("Joining as player...");
                await conn.invoke('JoinLobby', code, username);
            }

            await conn.stop();
            localStorage.setItem("currentLobbyCode", code);
            navigate(`/lobby/${code}`);
        } catch (error) {
            console.error('Error joining lobby:', error);
            alert('Could not join lobby.');
        }
    }

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="join-lobby-page">
                    <div className="join-lobby-container">
                        <h2 className="join-lobby-title">Join or Return</h2>
                        <p className="join-lobby-subtitle">Enter the code to the lobby.</p>

                        <div className="join-lobby-input-container">
                            <input
                                type="text"
                                className="join-lobby-input"
                                placeholder=""
                                value={lobbyCode}
                                onChange={(e) => setLobbyCode(e.target.value.toUpperCase())}
                                maxLength={6}
                            />
                        </div>

                        <button
                            className="join-lobby-btn"
                            onClick={handleJoin}
                        >
                            Join Lobby
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
