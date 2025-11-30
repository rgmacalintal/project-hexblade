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

            console.log("Creating SignalR connection...");

            const conn = new HubConnectionBuilder()
                .withUrl("/api/lobbyHub")
                .withAutomaticReconnect()
                .build();

            connectionRef.current = conn;

            conn.on("PlayerListUpdated", setPlayers);

            conn.on("JoinFailed", msg => {
                alert(msg);
                navigate("/welcome");
            });

            conn.on("HostReconnected", () => {
                console.log("DM reconnected.");
            });

            conn.on("Kicked", (msg) => {
                alert(msg);
                navigate("/welcome");
            });

            conn.on("HostOffline", (username) => {
                console.log(`${username} (DM) went offline.`);
            });

            conn.on("PlayerOffline", (username) => {
                console.log(`${username} went offline.`);
            });

            let hostStatus = false;
            try {
                const response = await fetch(`/api/Lobbys/IsHost?code=${codeRef.current}&username=${usernameRef.current}`);
                hostStatus = await response.json();
                setIsHost(hostStatus);
            } catch (err) {
                console.error("Failed checking host status:", err);
            }

            await conn.start();
            console.log("SignalR connected.");

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
                console.log("Stopping SignalR connection…");
                connectionRef.current.stop();
            }
        };
    }, [navigate]);

    function handleSelectPlayer(player) {
        setSelectedPlayer(player);
        console.log("Selected player:", player);
    }

    function kickSelectedPlayer() {
        if (!connectionRef.current || !selectedPlayer) return;

        if (selectedPlayer.username === usernameRef.current) {
            alert("You cannot remove yourself as the Dungeon Master.");
            return;
        }

        if (window.confirm(`Remove ${selectedPlayer.username} from the lobby?`)) {
            connectionRef.current.invoke("KickPlayer", codeRef.current, selectedPlayer.username)
                .catch(err => console.error("Kick failed:", err));
        }
    }

    function leaveLobby() {
        if (connectionRef.current) {
            connectionRef.current.invoke("LeaveLobby", codeRef.current, usernameRef.current)
                .then(() => {
                    connectionRef.current.stop();
                    navigate("/welcome");
                })
                .catch(err => console.error("Error leaving lobby:", err));
        } else {
            navigate("/welcome");
        }
    }

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="lobby-page">
                    <h2>Lobby Code: {code}</h2>
                    <h3>You are {isHost ? "the Dungeon Master" : "a D&D Player"}</h3>

                    {!connected && <p>Connecting...</p>}

                    {connected && (
                        <>
                            <h3>Players in Lobby:</h3>
                            <ul>
                                {players.map((p, index) => (
                                    <li key={index} onClick={() => handleSelectPlayer(p)} style={{ cursor: "pointer"} }>
                                        {p.username} {p.isHost ? "(DM)" : ""}
                                    </li>
                                ))}
                            </ul>

                            <br />

                            <button className="header-btn" onClick={leaveLobby}>
                                Leave Lobby
                            </button>
                        </>
                    )}

                    {selectedPlayer && (
                        <div className="selected-player-box">
                            <h4>Selected: {selectedPlayer.username}</h4>
                        </div>
                    )}

                    {isHost && selectedPlayer && !selectedPlayer.isHost && (
                        <button
                            className="header-btn"
                            onClick={kickSelectedPlayer}
                            style={{ backgroundColor: "red", marginTop: "10px" }}
                        >
                            Kick {selectedPlayer.username}
                        </button>
                    )}
                </div>
            </Layout>
        </div>
    );
}
