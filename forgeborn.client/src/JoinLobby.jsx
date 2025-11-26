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
                    <h2>Join Lobby</h2>
                    <input
                        type="text"
                        maxLength="4"
                        placeholder="Enter lobby code"
                        value={lobbyCode}
                        onChange={(e) => setLobbyCode(e.target.value.toUpperCase())}
                        className="input-box"
                    />
                    <button className="header-btn" onClick={handleJoin}>Join Lobby</button>
                </div>
            </Layout>
        </div>
    );
}
