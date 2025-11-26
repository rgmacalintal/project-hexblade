import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { HubConnectionBuilder } from '@microsoft/signalr';
import Layout from './Layout';

export default function LobbyPage({ toggleSidebar, sidebarOpen }) {
    const { code } = useParams();
    const navigate = useNavigate();

    const username = localStorage.getItem("username");

    const [connection, setConnection] = useState(null);
    const [players, setPlayers] = useState([]);
    const [isHost, setIsHost] = useState(false);
    const [connected, setConnected] = useState(false);

    useEffect(() => {
        if (!username) {
            alert("Please log in first.");
            navigate("/login");
        }
    }, [username, navigate]);

    useEffect(() => {
        let conn = null;

        async function startConnection() {
            let hostStatus = false;
            try {
                const response = await fetch(`/api/Lobbys/IsHost?code=${code}&username=${username}`);
                hostStatus = await response.json();
                setIsHost(hostStatus);
            } catch (err) {
                console.error("Failed checking host status:", err);
            }

            conn = new HubConnectionBuilder()
                .withUrl("/api/lobbyHub")
                .withAutomaticReconnect()
                .build();

            conn.on("JoinFailed", (msg) => {
                alert(msg);
                navigate("/welcome");
            });

            conn.on("PlayerJoined", (player) => {
                console.log(`${player} joined`);
            });

            conn.on("PlayerListUpdated", (list) => {
                setPlayers(list);
            });

            conn.on("LobbyClosed", () => {
                alert("This lobby has closed.");
                navigate("/welcome");
            });

            conn.on("HostReconnected", () => {
                alert("You are reconnected as the host.");
            });

            try {
                await conn.start();
                console.log("Connected to hub.");

                if (hostStatus) {
                    await conn.invoke("ReconnectHost", code, username);
                } else {
                    await conn.invoke("JoinLobby", code, username);
                }

                setConnection(conn);
                setConnected(true);
            } catch (error) {
                console.error("Failed to connect:", error);
                alert("Could not join lobby.");
                navigate("/welcome");
            }
        }

        startConnection();

        return () => {
            if (conn) conn.stop();
        };
    }, [code, username, navigate]);

    function leaveLobby() {
        if (connection) {
            connection.invoke("LeaveLobby", code, username)
                .then(() => {
                    connection.stop();
                    navigate("/welcome");
                })
                .catch((err) => console.error("Error leaving lobby:", err));
        } else {
            navigate("/welcome");
        }
    }

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="lobby-page">
                    <h2>Lobby Code: {code}</h2>
                    <h3>You are {isHost ? "the Host (DM)" : "a Player"}</h3>

                    {!connected && <p>Connecting...</p>}

                    {connected && (
                        <>
                            <h3>Players in Lobby:</h3>
                            <ul>
                                {players.map((p, index) => (
                                    <li key={index}>
                                        {p.username} {p.isHost ? "(Host)" : ""}
                                    </li>
                                ))}
                            </ul>

                            <br />

                            <button className="header-btn" onClick={leaveLobby}>
                                Leave Lobby
                            </button>
                        </>
                    )}
                </div>
            </Layout>
        </div>
    );
}
