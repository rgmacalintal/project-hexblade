import React, { useEffect, useState, useRef } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { HubConnectionBuilder } from '@microsoft/signalr';
import Layout from './Layout';

export default function LobbyPage({ toggleSidebar, sidebarOpen }) {
    const { code } = useParams();
    const navigate = useNavigate();
    const username = localStorage.getItem("username");

    const [players, setPlayers] = useState([]);
    const [isHost, setIsHost] = useState(false);
    const [connected, setConnected] = useState(false);
    const [selectedPlayer, setSelectedPlayer] = useState(null);

    const connectionRef = useRef(null);
    const reconnectHandled = useRef(false);
    const codeRef = useRef(code);
    const usernameRef = useRef(username);

    useEffect(() => {
        if (!username) {
            alert("Please log in first.");
            navigate("/login");
        }
    }, [username, navigate]);

    useEffect(() => {
        codeRef.current = code;
        usernameRef.current = username;
    }, [code, username]);

    useEffect(() => {
        async function start() {
            if (connectionRef.current) return;

            const conn = new HubConnectionBuilder()
                .withUrl("/api/lobbyHub")
                .withAutomaticReconnect()
                .build();

            connectionRef.current = conn;

            conn.on("PlayerListUpdated", setPlayers);
            conn.on("JoinFailed", msg => { alert(msg); navigate("/welcome"); });
            conn.on("Kicked", msg => { alert(msg); navigate("/welcome"); });

            let hostStatus = false;
            try {
                const res = await fetch(`/api/Lobbys/IsHost?code=${codeRef.current}&username=${usernameRef.current}`);
                hostStatus = await res.json();
                setIsHost(hostStatus);
            } catch (err) {
                console.error("Host check failed:", err);
            }

            await conn.start();

            if (hostStatus && !reconnectHandled.current) {
                reconnectHandled.current = true;
                await conn.invoke("ReconnectHost", codeRef.current, usernameRef.current);
            }

            if (!hostStatus) {
                const alreadyJoined = await conn.invoke("IsPlayerInLobby", codeRef.current, usernameRef.current);
                if (!alreadyJoined) {
                    await conn.invoke("JoinLobby", codeRef.current, usernameRef.current);
                }
            }

            setConnected(true);
        }

        start();

        return () => {
            if (connectionRef.current) {
                connectionRef.current.stop();
            }
        };
    }, [navigate]);

    function handleSelectPlayer(player) {
        setSelectedPlayer(player);
    }

    function leaveLobby() {
        if (connectionRef.current) {
            connectionRef.current.invoke("LeaveLobby", codeRef.current, usernameRef.current)
                .then(() => {
                    connectionRef.current.stop();
                    navigate("/welcome");
                })
                .catch(err => console.error(err));
        } else {
            navigate("/welcome");
        }
    }

    function kickPlayer() {
        if (!selectedPlayer) return;

        if (selectedPlayer.username === usernameRef.current) {
            alert("You cannot kick yourself as DM.");
            return;
        }

        if (window.confirm(`Kick ${selectedPlayer.username}?`)) {
            connectionRef.current.invoke("KickPlayer", codeRef.current, selectedPlayer.username)
                .catch(err => console.error(err));
        }
    }

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="join-lobby-page">
                    <div className="join-lobby-container">

                        {/* Title */}
                        <h2 className="join-lobby-title">Lobby Code: {code}</h2>
                        <p className="join-lobby-subtitle">
                            You are {isHost ? "the Dungeon Master" : "a Player"}
                        </p>

                        {!connected && (
                            <p style={{ marginTop: "20px", color: "#666" }}>Connecting...</p>
                        )}

                        {connected && (
                            <>
                                {/* Players List */}
                                <h3 className="join-lobby-subtitle" style={{ marginTop: "30px" }}>
                                    Players in Lobby
                                </h3>

                                <ul style={{ listStyle: "none", padding: 0, marginTop: "10px" }}>
                                    {players.map((p, index) => (
                                        <li
                                            key={index}
                                            onClick={() => handleSelectPlayer(p)}
                                            style={{
                                                padding: "12px",
                                                marginBottom: "8px",
                                                borderRadius: "10px",
                                                cursor: "pointer",
                                                background:
                                                    selectedPlayer?.username === p.username
                                                        ? "rgba(255, 167, 86, 0.25)"
                                                        : "rgba(0,0,0,0.05)",
                                                border: "1px solid rgba(255, 167, 86, 0.3)",
                                                transition: "0.2s"
                                            }}
                                        >
                                            {p.username} {p.isHost ? "(DM)" : ""}
                                        </li>
                                    ))}
                                </ul>

                                {/* DM Kick & Actions */}
                                {isHost && selectedPlayer && !selectedPlayer.isHost && (
                                    <button
                                        className="join-lobby-btn"
                                        onClick={kickPlayer}
                                        style={{
                                            backgroundColor: "red",
                                            borderColor: "rgba(255,0,0,0.5)",
                                            marginTop: "15px"
                                        }}
                                    >
                                        Kick {selectedPlayer.username}
                                    </button>
                                )}

                                {/* Leave Lobby */}
                                <button
                                    className="lobby-back-btn"
                                    style={{ marginTop: "25px" }}
                                    onClick={leaveLobby}
                                >
                                    Leave Lobby
                                </button>
                            </>
                        )}

                    </div>
                </div>
            </Layout>
        </div>
    );
}
