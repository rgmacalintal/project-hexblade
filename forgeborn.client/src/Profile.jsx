import React, { useState } from 'react';
import Layout from './Layout';
import './App.css';
import { useLocation, useNavigate } from 'react-router-dom';
import { useEffect } from 'react';

export default function Profile({ toggleSidebar, sidebarOpen }) {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedCharacter, setSelectedCharacter] = useState(null);
    const location = useLocation();
    const navigate = useNavigate();
    const username = location.state?.username || localStorage.getItem('username');

    useEffect(() => {
        if (!username) {
            alert('Please login first.');
            navigate('/login');
        }
    }, [username, navigate]);

    const openCharacterSheet = (character) => {
        setSelectedCharacter(character);
        setIsModalOpen(true);
    };

    const closeModal = () => {
        setIsModalOpen(false);
        setSelectedCharacter(null);
    };

    const characters = [
        {
            id: 1,
            image: '/G1.png',
            profile: {
                name: 'OLIVIA WILSON',
                nickname: 'OLIVIA',
                role: 'MAIN FEMALE CHARACTER',
                age: '25',
                gender: 'FEMALE',
                classOccupation: 'ARTIST',
                personality: 'CHATTY',
                physical: {
                    height: '170 cm',
                    weight: '55 kg',
                    build: 'Skinny',
                    skinTone: 'Pale',
                    hairColor: 'Red',
                    eyeColor: 'Green'
                },
                background: 'She thrives as an artist with an insatiable passion for bringing vibrant colors and life to her canvas. Her signature red hair has always been her trademark, lighting up any room with her infectious smile. Drawn to color both in her art and in life, she eagerly welcomes people into her world, often losing herself in its embrace.',
                goals: [
                    'OLIVIA WANTS RECOGNITION AND SUCCESS AS AN ARTIST',
                    'SHE IS DRIVEN BY HER PASSION FOR ART AND A DESIRE FOR IMPACT',
                    'A LONG-TERM GOAL IS TO HAVE A SOLO ART EXHIBITION SHOWCASING HER UNIQUE STYLE'
                ],
                relationships: 'OLIVIA VALUES FAMILY, SHARES HER PASSION WITH FRIENDS, AND SEEKS UNDERSTANDING IN ROMANTIC RELATIONSHIPS',
                motivations: 'HER DESIRE TO CONNECT WITH OTHERS AND SHARE THE JOY SHE FINDS IN ART',
                conflicts: 'BATTLES SELF-DOUBT ABOUT HER ART, FINDING STRENGTH IN EXTERNAL FEEDBACK'
            }
        },
        { id: 2, image: '/G2.png', profile: null },
        { id: 3, image: '/G3.png', profile: null }
    ];

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="profile-page">
                    <div className="profile-header">
                        <div className="avatar-placeholder" aria-label="Profile picture placeholder" />
                        <div className="profile-info">
                            <h2 className="profile-title">Your Profile</h2>
                            <div className="profile-fields">
                                <label>Character Name</label>
                                <br></br>
                                <br></br>
                                <label>Account Email</label>
                              </div>
                        </div>
                    </div>

                    <div className="character-sheets">
                        <h3 className="section-title" style={{ textAlign: 'left' }}>Character Sheets</h3>
                        <div className="character-grid">
                            {characters.slice(0,3).map((c) => (
                                <div key={c.id} className="character-card" onClick={() => openCharacterSheet(c)} role="button" aria-label={`Open character ${c.id}`}>
                                    <div className="profile-character-static">
                                        <img src={c.image} alt={`Character ${c.id}`} className="profile-character-photo" />
                                    </div>
                                </div>
                            ))}
                            <div className="character-card add-character-card" role="button" aria-label="Add character">
                                <span style={{ fontWeight: 600 }}>+ Add Character</span>
                            </div>
                        </div>
                    </div>
                </div>
            </Layout>

            {isModalOpen && (
                <div className="modal-overlay" onClick={closeModal}>
                    <div className="modal-content" onClick={(e) => e.stopPropagation()}>
                        <button className="modal-close" onClick={closeModal} aria-label="Close">×</button>
                        {selectedCharacter && (
                            <div className="character-sheet">
                                <div className="sheet-header">
                                    <div className="sheet-portrait">
                                        <img src={selectedCharacter.image} alt="Character portrait" />
                                    </div>
                                    <div className="sheet-summary">
                                        <div className="row">
                                            <div className="label">Name:</div>
                                            <div className="value">{selectedCharacter.profile?.name || 'Unknown'}</div>
                                        </div>
                                        <div className="row">
                                            <div className="label">Nickname:</div>
                                            <div className="value">{selectedCharacter.profile?.nickname || '—'}</div>
                                        </div>
                                        <div className="row">
                                            <div className="label">Role in story:</div>
                                            <div className="value">{selectedCharacter.profile?.role || '—'}</div>
                                        </div>
                                        <div className="row two-col">
                                            <div className="col">
                                                <div className="label">Age:</div>
                                                <div className="value">{selectedCharacter.profile?.age || '—'}</div>
                                            </div>
                                            <div className="col">
                                                <div className="label">Gender:</div>
                                                <div className="value">{selectedCharacter.profile?.gender || '—'}</div>
                                            </div>
                                        </div>
                                        <div className="row">
                                            <div className="label">Class / Occupation:</div>
                                            <div className="value">{selectedCharacter.profile?.classOccupation || '—'}</div>
                                        </div>
                                        <div className="row">
                                            <div className="label">Personality:</div>
                                            <div className="value">{selectedCharacter.profile?.personality || '—'}</div>
                                        </div>
                                    </div>
                                </div>

                                <div className="sheet-body">
                                    <div className="panel">
                                        <div className="panel-title">Physical Description</div>
                                        <div className="grid two">
                                            <div className="grid-item"><span className="k">Height:</span> {selectedCharacter.profile?.physical?.height || '—'}</div>
                                            <div className="grid-item"><span className="k">Skin tone:</span> {selectedCharacter.profile?.physical?.skinTone || '—'}</div>
                                            <div className="grid-item"><span className="k">Weight:</span> {selectedCharacter.profile?.physical?.weight || '—'}</div>
                                            <div className="grid-item"><span className="k">Hair color:</span> {selectedCharacter.profile?.physical?.hairColor || '—'}</div>
                                            <div className="grid-item"><span className="k">Build:</span> {selectedCharacter.profile?.physical?.build || '—'}</div>
                                            <div className="grid-item"><span className="k">Eye color:</span> {selectedCharacter.profile?.physical?.eyeColor || '—'}</div>
                                        </div>
                                    </div>

                                    <div className="panel">
                                        <div className="panel-title">Background</div>
                                        <p className="panel-text">{selectedCharacter.profile?.background || '—'}</p>
                                    </div>

                                    <div className="grid panels two">
                                        <div className="panel">
                                            <div className="panel-title">Character Goals</div>
                                            <ul className="bullet-list">
                                                {(selectedCharacter.profile?.goals || []).map((g, i) => (
                                                    <li key={i}>{g}</li>
                                                ))}
                                            </ul>
                                        </div>
                                        <div className="panel">
                                            <div className="panel-title">Relationships</div>
                                            <p className="panel-text">{selectedCharacter.profile?.relationships || '—'}</p>
                                        </div>
                                    </div>

                                    <div className="grid panels two">
                                        <div className="panel">
                                            <div className="panel-title">Motivations</div>
                                            <p className="panel-text">{selectedCharacter.profile?.motivations || '—'}</p>
                                        </div>
                                        <div className="panel">
                                            <div className="panel-title">Conflicts</div>
                                            <p className="panel-text">{selectedCharacter.profile?.conflicts || '—'}</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        )}
                    </div>
                </div>
            )}
        </div>
    );
}

