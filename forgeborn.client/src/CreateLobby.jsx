import React, { useEffect, useState } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { useNavigate } from 'react-router-dom';
import Layout from './Layout';

export default function CreateLobby({ toggleSidebar, sidebarOpen }) {
    const [connection, setConnection] = useState(null);
    const [lobbyCode, setLobbyCode] = useState('');
    const username = localStorage.getItem('username');
    const navigate = useNavigate();

    useEffect(() => {
        if (!username) {
            alert('Please login first.');
            navigate('/login');
        }
    }, [username, navigate]);

    useEffect(() => {
        const conn = new HubConnectionBuilder()
            .withUrl('/api/lobbyHub')
            .withAutomaticReconnect()
            .build();

        conn.start()
            .then(() => {
                console.log('Connected to hub');
                conn.invoke('CreateLobby', username);
            })
            .catch(err => console.error('Connection failed:', err));

        conn.on('LobbyCreated', code => {
            console.log('Lobby created:', code);
            setLobbyCode(code);
        });

        conn.on('PlayerJoined', player => {
            alert(`${player} joined your lobby!`);
        });

        conn.on('LobbyClosed', () => {
            alert('Lobby closed by host.');
            navigate('/welcome');
        });

        setConnection(conn);

        return () => {
            if (conn) {
                conn.stop();
                console.log('Disconnected from hub');
            }
        };
    }, [username, navigate]);

    function leaveLobby() {
        if (connection) {
            connection.invoke('LeaveLobby', lobbyCode, username)
                .then(() => {
                    console.log('Left lobby');
                    connection.stop();
                    navigate('/welcome');
                })
                .catch(err => console.error('Error leaving lobby:', err));
        }
    }

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="create-lobby-page">
                    <h2>Lobby Code: {lobbyCode || 'Creating lobby...'}</h2>
                    <button className="header-btn" onClick={leaveLobby}>Leave Lobby</button>
                </div>
            </Layout>
        </div>
    );
}
