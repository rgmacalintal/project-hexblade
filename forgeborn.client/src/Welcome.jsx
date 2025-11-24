import React, { useState, useEffect } from 'react';
import Layout from './Layout';
import { useLanguage } from './language/UseLanguage';
import './Welcome.css';
import './App.css';
import { useLocation, useNavigate, Link } from 'react-router-dom';

export default function Welcome({ toggleSidebar, sidebarOpen }) {
    const { t } = useLanguage();
    const [isCharModalOpen, setIsCharModalOpen] = useState(false);
    const [isCharacterSheetOpen, setIsCharacterSheetOpen] = useState(false);
    const [formData, setFormData] = useState({
        name: '',
        nickname: '',
        role: '',
        age: '',
        gender: '',
        classOccupation: '',
        personality: '',
        physical: {
            height: '',
            weight: '',
            build: '',
            skinTone: '',
            hairColor: '',
            eyeColor: ''
        },
        background: '',
        goals: ['', '', ''],
        relationships: '',
        motivations: '',
        conflicts: '',
        image: '/G1.png'
    });
    const location = useLocation();
    const navigate = useNavigate();
    const username = location.state?.username || localStorage.getItem('username');

     useEffect(() => {
         if (!username) {
             alert('Please login first.');
             navigate('/login');
         }
     }, [username, navigate]);

    const openCharModal = () => setIsCharModalOpen(true);
    const closeCharModal = () => setIsCharModalOpen(false);
    
    const openCharacterSheet = () => {
        setIsCharModalOpen(false);
        setIsCharacterSheetOpen(true);
    };

    const closeCharacterSheet = () => {
        setIsCharacterSheetOpen(false);
    };

    const handleChange = (field, value) => {
        if (field.includes('.')) {
            const [parent, child] = field.split('.');
            setFormData(prev => ({
                ...prev,
                [parent]: {
                    ...prev[parent],
                    [child]: value
                }
            }));
        } else if (field === 'goals') {
            setFormData(prev => ({
                ...prev,
                goals: value
            }));
        } else {
            setFormData(prev => ({
                ...prev,
                [field]: value
            }));
        }
    };

    const handleGoalChange = (index, value) => {
        const newGoals = [...formData.goals];
        newGoals[index] = value;
        setFormData(prev => ({
            ...prev,
            goals: newGoals
        }));
    };

    const handleSaveCharacter = () => {
        console.log('Saving character:', formData);
        closeCharacterSheet();
    };

    return (
        <div className="fullscreen-wrapper">
            <Layout toggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen}>
                <div className="welcome-page">
                    <h1 className="welcome-text">
                        {t('welcome', { username: username || 'User' })}
                    </h1>
                    <p className="welcome-subtitle">Your journey into the realm of adventure begins here</p>

                    <div className="lobby-actions">
                        <button onClick={() => navigate('/create-lobby')} className="header-btn">Create Lobby</button>
                        <button onClick={() => navigate('/join-lobby')} className="header-btn">Join Lobby</button>
                    </div>
                    
                    <div className="welcome-features">
                        <div className="feature-card" onClick={openCharModal} role="button" aria-label="Open Character Creation choices">
                            <span className="feature-icon">⚔️</span>
                            <h3 className="feature-title">{t('characterCreation')}</h3>
                            <p className="feature-description">{t('characterCreationDesc')}</p>
                        </div>
                        
                        <div className="feature-card">
                            <span className="feature-icon">🎲</span>
                            <h3 className="feature-title">{t('interactiveDice')}</h3>
                            <p className="feature-description">{t('interactiveDiceDesc')}</p>
                        </div>
                        
                        <div className="feature-card">
                            <span className="feature-icon">📜</span>
                            <h3 className="feature-title">{t('storyCampaigns')}</h3>
                            <p className="feature-description">{t('storyCampaignsDesc')}</p>
                        </div>

                      
                        <div className="feature-card">
                            <span className="feature-icon">👥</span>
                            <h3 className="feature-title">{t('multiplayer')}</h3>
                            <p className="feature-description">{t('multiplayerDesc')}</p>
                        </div>
                    </div>

                    {/* Footer */}
                    <footer className="site-footer">
                        <div className="links">
                            <Link to="/about">About</Link>
                        </div>
                        <div className="copyright">
                            © {new Date().getFullYear()} Forge Born. All rights reserved.
                        </div>
                    </footer>
                </div>
            </Layout>

            {isCharModalOpen && (
                <div className="modal-overlay" onClick={closeCharModal}>
                    <div className="modal-content dark" onClick={(e) => e.stopPropagation()}>
                        <button className="modal-close" onClick={closeCharModal} aria-label="Close">×</button>
                        <div className="choice-header">{t('createPersonalCharacter')}</div>
                        <div className="choice-grid">
                            <div className="choice-card" onClick={openCharacterSheet} role="button" aria-label="Create with Character Sheet">
                                <div className="choice-icon">📋</div>
                                <div className="choice-title">{t('characterSheetTitle')}</div>
                                <div className="choice-sub">{t('fillCharacterDetails')}</div>
                            </div>
                           
                        </div>
                    </div>
                </div>
            )}

            {isCharacterSheetOpen && (
                <div className="modal-overlay" onClick={closeCharacterSheet}>
                    <div className="modal-content" onClick={(e) => e.stopPropagation()}>
                        <button className="modal-close" onClick={closeCharacterSheet} aria-label="Close">×</button>
                        <div className="character-sheet">
                            <div className="sheet-header">
                                <div className="sheet-portrait">
                                    <img src={formData.image} alt="Character portrait" />
                                </div>
                                <div className="sheet-summary">
                                    <div className="row">
                                        <div className="label">{t('name')}:</div>
                                        <input
                                            type="text"
                                            className="value input-field"
                                            value={formData.name}
                                            onChange={(e) => handleChange('name', e.target.value)}
                                            placeholder={t('enterName')}
                                        />
                                    </div>
                                    <div className="row">
                                        <div className="label">{t('nickname')}:</div>
                                        <input
                                            type="text"
                                            className="value input-field"
                                            value={formData.nickname}
                                            onChange={(e) => handleChange('nickname', e.target.value)}
                                            placeholder={t('enterNickname')}
                                        />
                                    </div>
                                    <div className="row">
                                        <div className="label">{t('roleInStory')}:</div>
                                        <input
                                            type="text"
                                            className="value input-field"
                                            value={formData.role}
                                            onChange={(e) => handleChange('role', e.target.value)}
                                            placeholder={t('enterRole')}
                                        />
                                    </div>
                                    <div className="row two-col">
                                        <div className="col">
                                            <div className="label">{t('age')}:</div>
                                            <input
                                                type="text"
                                                className="value input-field"
                                                value={formData.age}
                                                onChange={(e) => handleChange('age', e.target.value)}
                                                placeholder={t('enterAge')}
                                            />
                                        </div>
                                        <div className="col">
                                            <div className="label">{t('gender')}:</div>
                                            <input
                                                type="text"
                                                className="value input-field"
                                                value={formData.gender}
                                                onChange={(e) => handleChange('gender', e.target.value)}
                                                placeholder={t('enterGender')}
                                            />
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="label">{t('classOccupation')}:</div>
                                        <input
                                            type="text"
                                            className="value input-field"
                                            value={formData.classOccupation}
                                            onChange={(e) => handleChange('classOccupation', e.target.value)}
                                            placeholder={t('enterClassOccupation')}
                                        />
                                    </div>
                                    <div className="row">
                                        <div className="label">{t('personality')}:</div>
                                        <input
                                            type="text"
                                            className="value input-field"
                                            value={formData.personality}
                                            onChange={(e) => handleChange('personality', e.target.value)}
                                            placeholder={t('enterPersonality')}
                                        />
                                    </div>
                                </div>
                            </div>

                            <div className="sheet-body">
                                <div className="panel">
                                    <div className="panel-title">{t('physicalDescription')}</div>
                                    <div className="grid two">
                                        <div className="grid-item">
                                            <span className="k">{t('height')}:</span>
                                            <input
                                                type="text"
                                                className="input-field"
                                                value={formData.physical.height}
                                                onChange={(e) => handleChange('physical.height', e.target.value)}
                                                placeholder={t('enterHeight')}
                                            />
                                        </div>
                                        <div className="grid-item">
                                            <span className="k">{t('skinTone')}:</span>
                                            <input
                                                type="text"
                                                className="input-field"
                                                value={formData.physical.skinTone}
                                                onChange={(e) => handleChange('physical.skinTone', e.target.value)}
                                                placeholder={t('enterSkinTone')}
                                            />
                                        </div>
                                        <div className="grid-item">
                                            <span className="k">{t('weight')}:</span>
                                            <input
                                                type="text"
                                                className="input-field"
                                                value={formData.physical.weight}
                                                onChange={(e) => handleChange('physical.weight', e.target.value)}
                                                placeholder={t('enterWeight')}
                                            />
                                        </div>
                                        <div className="grid-item">
                                            <span className="k">{t('hairColor')}:</span>
                                            <input
                                                type="text"
                                                className="input-field"
                                                value={formData.physical.hairColor}
                                                onChange={(e) => handleChange('physical.hairColor', e.target.value)}
                                                placeholder={t('enterHairColor')}
                                            />
                                        </div>
                                        <div className="grid-item">
                                            <span className="k">{t('build')}:</span>
                                            <input
                                                type="text"
                                                className="input-field"
                                                value={formData.physical.build}
                                                onChange={(e) => handleChange('physical.build', e.target.value)}
                                                placeholder={t('enterBuild')}
                                            />
                                        </div>
                                        <div className="grid-item">
                                            <span className="k">{t('eyeColor')}:</span>
                                            <input
                                                type="text"
                                                className="input-field"
                                                value={formData.physical.eyeColor}
                                                onChange={(e) => handleChange('physical.eyeColor', e.target.value)}
                                                placeholder={t('enterEyeColor')}
                                            />
                                        </div>
                                    </div>
                                </div>

                                <div className="panel">
                                    <div className="panel-title">{t('background')}</div>
                                    <textarea
                                        className="panel-text input-field"
                                        value={formData.background}
                                        onChange={(e) => handleChange('background', e.target.value)}
                                        placeholder={t('enterBackgroundStory')}
                                        rows="4"
                                    />
                                </div>

                                <div className="grid panels two">
                                    <div className="panel">
                                        <div className="panel-title">{t('characterGoals')}</div>
                                        <div>
                                            {formData.goals.map((goal, i) => (
                                                <input
                                                    key={i}
                                                    type="text"
                                                    className="input-field"
                                                    value={goal}
                                                    onChange={(e) => handleGoalChange(i, e.target.value)}
                                                    placeholder={`${t('characterGoals')} ${i + 1}`}
                                                    style={{ marginBottom: '8px', width: '100%' }}
                                                />
                                            ))}
                                        </div>
                                    </div>
                                    <div className="panel">
                                        <div className="panel-title">{t('relationships')}</div>
                                        <textarea
                                            className="panel-text input-field"
                                            value={formData.relationships}
                                            onChange={(e) => handleChange('relationships', e.target.value)}
                                            placeholder={t('enterRelationships')}
                                            rows="4"
                                        />
                                    </div>
                                </div>

                                <div className="grid panels two">
                                    <div className="panel">
                                        <div className="panel-title">{t('motivations')}</div>
                                        <textarea
                                            className="panel-text input-field"
                                            value={formData.motivations}
                                            onChange={(e) => handleChange('motivations', e.target.value)}
                                            placeholder={t('enterMotivations')}
                                            rows="4"
                                        />
                                    </div>
                                    <div className="panel">
                                        <div className="panel-title">{t('conflicts')}</div>
                                        <textarea
                                            className="panel-text input-field"
                                            value={formData.conflicts}
                                            onChange={(e) => handleChange('conflicts', e.target.value)}
                                            placeholder={t('enterConflicts')}
                                            rows="4"
                                        />
                                    </div>
                                </div>
                            </div>

                            <div style={{ display: 'flex', gap: '12px', justifyContent: 'flex-end', marginTop: '16px' }}>
                                <button onClick={closeCharacterSheet} className="btn-secondary">{t('cancel')}</button>
                                <button onClick={handleSaveCharacter} className="btn-primary">{t('saveCharacter')}</button>
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}
