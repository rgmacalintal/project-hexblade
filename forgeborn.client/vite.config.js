import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import { env } from 'process';
import { fileURLToPath, URL } from 'node:url';

// https://vitejs.dev/config/

const target = env.ASPNETCORE_URLS || 'http://localhost:8080';

export default defineConfig({
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        port: 3000,
        proxy: {
            '/api/': {
                target,
                changeOrigin: true,
            },
            '/openapi/': {
                target,
                changeOrigin: true,
            }
        }
    },
    plugins: [react()]
});
