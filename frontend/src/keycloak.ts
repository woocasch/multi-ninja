import Keycloak from 'keycloak-js'
import * as kcCore from '@react-keycloak/core';
import axios from 'axios';

// Setup Keycloak instance as needed
// Pass initialization options as required or leave blank to load from 'keycloak.json'
const keycloak = new Keycloak({
    realm: 'multi-ninja',
    clientId: 'multi-ninja-frontend',
    url: 'http://localhost:10006'
});

export const keyCloakInitOptions: kcCore.AuthClientInitOptions = {
    onLoad: 'check-sso',
};

export function onEvent(eventType: kcCore.AuthClientEvent, error?: kcCore.AuthClientError) {
    // DO NOTHING
}

export function onTokens(token?: kcCore.AuthClientTokens) {
    if (!token) {
        return;
    }

    axios.defaults.headers.common = {
        'Authorization': `Bearer ${token.token}`
    };

    const responsePromise = axios.post('http://localhost:5078/api/auth/ensureAccountCreated');
    responsePromise
        .then(response => console.log(response));
}

export default keycloak