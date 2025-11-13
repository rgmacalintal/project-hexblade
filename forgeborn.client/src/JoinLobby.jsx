import React, { useEffect, useState } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { useNavigate } from 'react-router-dom';
import Layout from './Layout';

export default function JoinLobby({ toggleSidebar, sidebarOpen }) {
    const [connection, setConnection] = useState(null);
    const [lobbyCode, setLobbyCode] = useState('');
    const [connected, setConnected] = useState(false);
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

        const conn = new HubConnectionBuilder()
            .withUrl('/lobbyHub')
            .withAutomaticReconnect()
            .build();

        try {
            await conn.start();
            console.log('Connected to hub');

            await conn.invoke('JoinLobby', lobbyCode.toUpperCase(), username);

            conn.on('JoinFailed', msg => {
                alert(msg);
                conn.stop();
            });

            conn.on('PlayerJoined', player => {
                alert(`${player} joined the lobby!`);
            });

            conn.on('LobbyClosed', () => {
                alert('Host closed the lobby.');
                navigate('/welcome');
            });

            setConnection(conn);
            setConnected(true);
        } catch (error) {
            console.error('Error joining lobby:', error);
            alert('Could not connect to the lobby.');
        }
    }

    function leaveLobby() {
        if (connection && lobbyCode) {
            connection.invoke('LeaveLobby', lobbyCode, username)
                .then(() => {
                    console.log('Left lobby');
                    connection.stop();
                    navigate('/welcome');
                })
                .catch(err => console.error('Error leaving lobby:', err));
        }
        setConnected(false);
    }

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="join-lobby-page">
                    {!connected ? (
                        <>
                            <h2>Join a Lobby</h2>
                            <input
                                type="text"
                                maxLength="4"
                                placeholder="Enter lobby code"
                                value={lobbyCode}
                                onChange={(e) => setLobbyCode(e.target.value.toUpperCase())}
                                className="input-box"
                            />
                            <button className="header-btn" onClick={handleJoin}>Join Lobby</button>
                        </>
                    ) : (
                        <>
                            <h2>Connected to Lobby {lobbyCode}</h2>
                            <button className="header-btn" onClick={leaveLobby}>Leave Lobby</button>
                        </>
                    )}
                </div>
            </Layout>
        </div>
    );
}
