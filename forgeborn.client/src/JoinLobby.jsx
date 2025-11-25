import React, { useEffect, useState } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { useNavigate } from 'react-router-dom';
import Layout from './Layout';

export default function JoinLobby({ toggleSidebar, sidebarOpen }) {
    const [connection, setConnection] = useState(null);
    const [lobbyCode, setLobbyCode] = useState('');
    const [connected, setConnected] = useState(false);
    const [players, setPlayers] = useState([]);
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
        setLobbyCode(code);

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
            setConnected(false);
        });

        conn.on('PlayerJoined', player => {
            alert(`${player} joined the lobby!`);
        });

        conn.on("PlayerListUpdated", (list) => {
            setPlayers(list);
        });

        conn.on('LobbyClosed', () => {
            alert('Host closed the lobby.');
            navigate('/welcome');
        });

        conn.on('HostReconnected', () => {
            alert("You have reconnected as the host!");
        });

        try {
            await conn.start();
            console.log('Connected to hub');

            if (isHost) {
                console.log("Reconnecting as host...");
                await conn.invoke('ReconnectHost', code, username);
            } else {
                console.log("Joining as player...");
                await conn.invoke('JoinLobby', code, username);
            }

            if (conn.state === "Connected") {
                setConnection(conn);
                setConnected(true);
            }
        } catch (error) {
            console.error('Error joining lobby:', error);
            alert('Could not connect to the lobby.');
        }
    }

    function leaveLobby() {
        if (connection) {
            connection.invoke('LeaveLobby', lobbyCode.toUpperCase(), username)
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

                            <h3>Players in Lobby:</h3>
                            <ul>
                                {players.map((p, index) => (
                                    <li key={index}>
                                        {p.username} {p.isHost ? "(Host)" : ""}
                                    </li>
                                ))}
                            </ul>
                            <button className="header-btn" onClick={leaveLobby}>Leave Lobby</button>
                        </>
                    )}
                </div>
            </Layout>
        </div>
    );
}
