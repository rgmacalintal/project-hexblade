import React, { useState, useEffect } from 'react';
import Layout from './Layout';
import { useLocation, Link } from 'react-router-dom';
import { useLanguage } from './LanguageContext';
import './App.css';

export default function Profile({ toggleSidebar, sidebarOpen }) {
    const { t } = useLanguage();
    const location = useLocation();
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedCharacter, setSelectedCharacter] = useState(null);
    const [isCreatingNew, setIsCreatingNew] = useState(false);
    const [isProfilePictureModalOpen, setIsProfilePictureModalOpen] = useState(false);
    const [profilePicture, setProfilePicture] = useState(() => {
        return localStorage.getItem('profilePicture') || '/G1.png';
    });
    const username = localStorage.getItem('username') || 'User';
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

    useEffect(() => {
        // Check if we should open character sheet from menu
        const shouldOpen = location.state?.openCharacterSheet || localStorage.getItem('openCharacterSheet') === 'true';
        if (shouldOpen) {
            setSelectedCharacter(null);
            setIsCreatingNew(true);
            setFormData({
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
            setIsModalOpen(true);
            localStorage.removeItem('openCharacterSheet');
            // Clear location state
            window.history.replaceState({}, document.title);
        }
    }, [location]);

    const openCharacterSheet = (character) => {
        setSelectedCharacter(character);
        setIsCreatingNew(false);
        if (character?.profile) {
            setFormData({
                name: character.profile.name || '',
                nickname: character.profile.nickname || '',
                role: character.profile.role || '',
                age: character.profile.age || '',
                gender: character.profile.gender || '',
                classOccupation: character.profile.classOccupation || '',
                personality: character.profile.personality || '',
                physical: {
                    height: character.profile.physical?.height || '',
                    weight: character.profile.physical?.weight || '',
                    build: character.profile.physical?.build || '',
                    skinTone: character.profile.physical?.skinTone || '',
                    hairColor: character.profile.physical?.hairColor || '',
                    eyeColor: character.profile.physical?.eyeColor || ''
                },
                background: character.profile.background || '',
                goals: character.profile.goals || ['', '', ''],
                relationships: character.profile.relationships || '',
                motivations: character.profile.motivations || '',
                conflicts: character.profile.conflicts || '',
                image: character.image || '/G1.png'
            });
        }
        setIsModalOpen(true);
    };

    const openNewCharacterSheet = () => {
        setSelectedCharacter(null);
        setIsCreatingNew(true);
        setFormData({
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
        setIsModalOpen(true);
    };

    const closeModal = () => {
        setIsModalOpen(false);
        setSelectedCharacter(null);
        setIsCreatingNew(false);
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
        closeModal();
    };

    const openProfilePictureModal = () => {
        setIsProfilePictureModalOpen(true);
    };

    const closeProfilePictureModal = () => {
        setIsProfilePictureModalOpen(false);
    };

    const selectProfilePicture = (imagePath) => {
        setProfilePicture(imagePath);
        localStorage.setItem('profilePicture', imagePath);
        closeProfilePictureModal();
    };

    const profilePictures = ['/G1.png', '/G2.png', '/G3.png'];

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
                        <div 
                            className="avatar-placeholder" 
                            onClick={openProfilePictureModal}
                            style={{ cursor: 'pointer' }}
                            aria-label="Profile picture placeholder"
                        >
                            <img 
                                src={profilePicture} 
                                alt="Profile" 
                                style={{ 
                                    width: '100%', 
                                    height: '100%', 
                                    objectFit: 'cover', 
                                    borderRadius: '50%' 
                                }} 
                            />
                        </div>
                        <div className="profile-info">
                            <h2 className="profile-title">{t('yourProfile')}</h2>
                            <div className="profile-username">{username}</div>
                        </div>
                    </div>

                    <div className="character-sheets">
                        <h3 className="section-title" style={{ textAlign: 'left' }}>{t('characterSheets')}</h3>
                        <div className="character-grid">
                            {characters.slice(0,3).map((c) => (
                                <div key={c.id} className="character-card" onClick={() => openCharacterSheet(c)} role="button" aria-label={`Open character ${c.id}`}>
                                    <div className="profile-character-static">
                                        <img src={c.image} alt={`Character ${c.id}`} className="profile-character-photo" />
                                    </div>
                                </div>
                            ))}
                            <div className="character-card add-character-card" onClick={openNewCharacterSheet} role="button" aria-label="Add character">
                                <span style={{ fontWeight: 600 }}>{t('addCharacter')}</span>
                            </div>
                        </div>
                    </div>

                    {/* Footer */}
                    <footer className="site-footer">
                        <div className="links">
                            <Link to="/about">{t('about')}</Link>
                        </div>
                        <div className="copyright">
                            © {new Date().getFullYear()} Forge Born. All rights reserved.
                        </div>
                    </footer>
                </div>
            </Layout>

            {isModalOpen && (
                <div className="modal-overlay" onClick={closeModal}>
                    <div className="modal-content" onClick={(e) => e.stopPropagation()}>
                        <button className="modal-close" onClick={closeModal} aria-label="Close">×</button>
                            <div className="character-sheet">
                                <div className="sheet-header">
                                    <div className="sheet-portrait">
                                    <img src={formData.image} alt="Character portrait" />
                                    </div>
                                    <div className="sheet-summary">
                                        <div className="row">
                                        <div className="label">{t('name')}:</div>
                                        {(isCreatingNew || !selectedCharacter?.profile) ? (
                                            <input
                                                type="text"
                                                className="value input-field"
                                                value={formData.name}
                                                onChange={(e) => handleChange('name', e.target.value)}
                                                placeholder={t('enterName')}
                                            />
                                        ) : (
                                            <div className="value">{formData.name || '—'}</div>
                                        )}
                                        </div>
                                        <div className="row">
                                        <div className="label">{t('nickname')}:</div>
                                        {(isCreatingNew || !selectedCharacter?.profile) ? (
                                            <input
                                                type="text"
                                                className="value input-field"
                                                value={formData.nickname}
                                                onChange={(e) => handleChange('nickname', e.target.value)}
                                                placeholder={t('enterNickname')}
                                            />
                                        ) : (
                                            <div className="value">{formData.nickname || '—'}</div>
                                        )}
                                        </div>
                                        <div className="row">
                                        <div className="label">{t('roleInStory')}:</div>
                                        {(isCreatingNew || !selectedCharacter?.profile) ? (
                                            <input
                                                type="text"
                                                className="value input-field"
                                                value={formData.role}
                                                onChange={(e) => handleChange('role', e.target.value)}
                                                placeholder={t('enterRole')}
                                            />
                                        ) : (
                                            <div className="value">{formData.role || '—'}</div>
                                        )}
                                        </div>
                                        <div className="row two-col">
                                            <div className="col">
                                            <div className="label">{t('age')}:</div>
                                            {(isCreatingNew || !selectedCharacter?.profile) ? (
                                                <input
                                                    type="text"
                                                    className="value input-field"
                                                    value={formData.age}
                                                    onChange={(e) => handleChange('age', e.target.value)}
                                                    placeholder={t('enterAge')}
                                                />
                                            ) : (
                                                <div className="value">{formData.age || '—'}</div>
                                            )}
                                            </div>
                                            <div className="col">
                                            <div className="label">{t('gender')}:</div>
                                            {(isCreatingNew || !selectedCharacter?.profile) ? (
                                                <input
                                                    type="text"
                                                    className="value input-field"
                                                    value={formData.gender}
                                                    onChange={(e) => handleChange('gender', e.target.value)}
                                                    placeholder={t('enterGender')}
                                                />
                                            ) : (
                                                <div className="value">{formData.gender || '—'}</div>
                                            )}
                                        </div>
                                        </div>
                                        <div className="row">
                                        <div className="label">{t('classOccupation')}:</div>
                                        {(isCreatingNew || !selectedCharacter?.profile) ? (
                                            <input
                                                type="text"
                                                className="value input-field"
                                                value={formData.classOccupation}
                                                onChange={(e) => handleChange('classOccupation', e.target.value)}
                                                placeholder={t('enterClassOccupation')}
                                            />
                                        ) : (
                                            <div className="value">{formData.classOccupation || '—'}</div>
                                        )}
                                        </div>
                                        <div className="row">
                                        <div className="label">{t('personality')}:</div>
                                        {(isCreatingNew || !selectedCharacter?.profile) ? (
                                            <input
                                                type="text"
                                                className="value input-field"
                                                value={formData.personality}
                                                onChange={(e) => handleChange('personality', e.target.value)}
                                                placeholder={t('enterPersonality')}
                                            />
                                        ) : (
                                            <div className="value">{formData.personality || '—'}</div>
                                        )}
                                    </div>
                                    </div>
                                </div>

                                <div className="sheet-body">
                                    <div className="panel">
                                    <div className="panel-title">{t('physicalDescription')}</div>
                                        <div className="grid two">
                                        <div className="grid-item">
                                            <span className="k">{t('height')}:</span>
                                            {(isCreatingNew || !selectedCharacter?.profile) ? (
                                                <input
                                                    type="text"
                                                    className="input-field"
                                                    value={formData.physical.height}
                                                    onChange={(e) => handleChange('physical.height', e.target.value)}
                                                    placeholder={t('enterHeight')}
                                                />
                                            ) : (
                                                <span> {formData.physical.height || '—'}</span>
                                            )}
                                        </div>
                                        <div className="grid-item">
                                            <span className="k">{t('skinTone')}:</span>
                                            {(isCreatingNew || !selectedCharacter?.profile) ? (
                                                <input
                                                    type="text"
                                                    className="input-field"
                                                    value={formData.physical.skinTone}
                                                    onChange={(e) => handleChange('physical.skinTone', e.target.value)}
                                                    placeholder={t('enterSkinTone')}
                                                />
                                            ) : (
                                                <span> {formData.physical.skinTone || '—'}</span>
                                            )}
                                        </div>
                                        <div className="grid-item">
                                            <span className="k">{t('weight')}:</span>
                                            {(isCreatingNew || !selectedCharacter?.profile) ? (
                                                <input
                                                    type="text"
                                                    className="input-field"
                                                    value={formData.physical.weight}
                                                    onChange={(e) => handleChange('physical.weight', e.target.value)}
                                                    placeholder={t('enterWeight')}
                                                />
                                            ) : (
                                                <span> {formData.physical.weight || '—'}</span>
                                            )}
                                        </div>
                                        <div className="grid-item">
                                            <span className="k">{t('hairColor')}:</span>
                                            {(isCreatingNew || !selectedCharacter?.profile) ? (
                                                <input
                                                    type="text"
                                                    className="input-field"
                                                    value={formData.physical.hairColor}
                                                    onChange={(e) => handleChange('physical.hairColor', e.target.value)}
                                                    placeholder={t('enterHairColor')}
                                                />
                                            ) : (
                                                <span> {formData.physical.hairColor || '—'}</span>
                                            )}
                                        </div>
                                        <div className="grid-item">
                                            <span className="k">{t('build')}:</span>
                                            {(isCreatingNew || !selectedCharacter?.profile) ? (
                                                <input
                                                    type="text"
                                                    className="input-field"
                                                    value={formData.physical.build}
                                                    onChange={(e) => handleChange('physical.build', e.target.value)}
                                                    placeholder={t('enterBuild')}
                                                />
                                            ) : (
                                                <span> {formData.physical.build || '—'}</span>
                                            )}
                                        </div>
                                        <div className="grid-item">
                                            <span className="k">{t('eyeColor')}:</span>
                                            {(isCreatingNew || !selectedCharacter?.profile) ? (
                                                <input
                                                    type="text"
                                                    className="input-field"
                                                    value={formData.physical.eyeColor}
                                                    onChange={(e) => handleChange('physical.eyeColor', e.target.value)}
                                                    placeholder={t('enterEyeColor')}
                                                />
                                            ) : (
                                                <span> {formData.physical.eyeColor || '—'}</span>
                                            )}
                                        </div>
                                        </div>
                                    </div>

                                    <div className="panel">
                                    <div className="panel-title">{t('background')}</div>
                                    {(isCreatingNew || !selectedCharacter?.profile) ? (
                                        <textarea
                                            className="panel-text input-field"
                                            value={formData.background}
                                            onChange={(e) => handleChange('background', e.target.value)}
                                            placeholder={t('enterBackgroundStory')}
                                            rows="4"
                                        />
                                    ) : (
                                        <p className="panel-text">{formData.background || '—'}</p>
                                    )}
                                    </div>

                                    <div className="grid panels two">
                                        <div className="panel">
                                        <div className="panel-title">{t('characterGoals')}</div>
                                        {(isCreatingNew || !selectedCharacter?.profile) ? (
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
                                        ) : (
                                            <ul className="bullet-list">
                                                {formData.goals.filter(g => g).map((g, i) => (
                                                    <li key={i}>{g}</li>
                                                ))}
                                            </ul>
                                        )}
                                        </div>
                                        <div className="panel">
                                        <div className="panel-title">{t('relationships')}</div>
                                        {(isCreatingNew || !selectedCharacter?.profile) ? (
                                            <textarea
                                                className="panel-text input-field"
                                                value={formData.relationships}
                                                onChange={(e) => handleChange('relationships', e.target.value)}
                                                placeholder={t('enterRelationships')}
                                                rows="4"
                                            />
                                        ) : (
                                            <p className="panel-text">{formData.relationships || '—'}</p>
                                        )}
                                    </div>
                                    </div>

                                    <div className="grid panels two">
                                        <div className="panel">
                                        <div className="panel-title">{t('motivations')}</div>
                                        {(isCreatingNew || !selectedCharacter?.profile) ? (
                                            <textarea
                                                className="panel-text input-field"
                                                value={formData.motivations}
                                                onChange={(e) => handleChange('motivations', e.target.value)}
                                                placeholder={t('enterMotivations')}
                                                rows="4"
                                            />
                                        ) : (
                                            <p className="panel-text">{formData.motivations || '—'}</p>
                                        )}
                                        </div>
                                        <div className="panel">
                                        <div className="panel-title">{t('conflicts')}</div>
                                        {(isCreatingNew || !selectedCharacter?.profile) ? (
                                            <textarea
                                                className="panel-text input-field"
                                                value={formData.conflicts}
                                                onChange={(e) => handleChange('conflicts', e.target.value)}
                                                placeholder={t('enterConflicts')}
                                                rows="4"
                                            />
                                        ) : (
                                            <p className="panel-text">{formData.conflicts || '—'}</p>
                                        )}
                                    </div>
                                </div>
                            </div>

                            {(isCreatingNew || !selectedCharacter?.profile) && (
                                <div style={{ display: 'flex', gap: '12px', justifyContent: 'flex-end', marginTop: '16px' }}>
                                    <button onClick={closeModal} className="btn-secondary">{t('cancel')}</button>
                                    <button onClick={handleSaveCharacter} className="btn-primary">{t('saveCharacter')}</button>
                                </div>
                            )}
                                    </div>
                                </div>
                            </div>
                        )}

            {/* Profile Picture Selection Modal */}
            {isProfilePictureModalOpen && (
                <div className="modal-overlay" onClick={closeProfilePictureModal}>
                    <div className="modal-content" onClick={(e) => e.stopPropagation()}>
                        <button className="modal-close" onClick={closeProfilePictureModal} aria-label="Close">×</button>
                        <h2 style={{ marginBottom: '30px', color: '#333', textAlign: 'center' }}>Select Profile Picture</h2>
                        <div style={{ 
                            display: 'grid', 
                            gridTemplateColumns: 'repeat(3, 1fr)', 
                            gap: '30px',
                            padding: '20px'
                        }}>
                            {profilePictures.map((imagePath, index) => (
                                <div
                                    key={index}
                                    onClick={() => selectProfilePicture(imagePath)}
                                    style={{
                                        cursor: 'pointer',
                                        position: 'relative',
                                        width: '200px',
                                        height: '200px',
                                        borderRadius: '50%',
                                        overflow: 'hidden',
                                        border: profilePicture === imagePath ? '4px solid #ffa756' : '2px solid rgba(255, 167, 86, 0.3)',
                                        transition: 'all 0.3s ease',
                                        margin: '0 auto'
                                    }}
                                    onMouseEnter={(e) => {
                                        e.currentTarget.style.transform = 'scale(1.1)';
                                        e.currentTarget.style.borderColor = '#ffa756';
                                    }}
                                    onMouseLeave={(e) => {
                                        e.currentTarget.style.transform = 'scale(1)';
                                        e.currentTarget.style.borderColor = profilePicture === imagePath ? '#ffa756' : 'rgba(255, 167, 86, 0.3)';
                                    }}
                                >
                                    <img 
                                        src={imagePath} 
                                        alt={`Profile option ${index + 1}`}
                                        style={{
                                            width: '100%',
                                            height: '100%',
                                            objectFit: 'cover'
                                        }}
                                    />
                                </div>
                            ))}
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}

