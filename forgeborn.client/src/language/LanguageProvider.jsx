import React, { useState, useEffect, useCallback } from 'react';
import { translations } from './Translations';
import { LanguageContext } from './LanguageContext';

// Contains provider component
export const LanguageProvider = ({ children }) => {
    const [language, setLanguage] = useState(() => {
        return localStorage.getItem('language') || 'en';
    });

    useEffect(() => {
        localStorage.setItem('language', language);
    }, [language]);

    const t = useCallback((key, params = {}) => {
        let text = translations[language][key] || key;
        //if (params && Object.keys(params).length > 0) {
        //    Object.keys(params).forEach(param => {
        //        text = text.replace(`{${param}}`, params[param]);
        //    });
        //}

        // This for loop should be better for text replacement on multiple occurrences
        for (const param in params) {
            text = text.replaceAll(`{${param}}`, params[param]);
        }
        return text;
    }, [language]);

    return (
        <LanguageContext.Provider value={{ language, setLanguage, t }}>
            {children}
        </LanguageContext.Provider>
    );
};
