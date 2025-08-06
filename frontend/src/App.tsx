import './App.css';
import multi_ninja from './assets/multi-ninja.png';
import React from 'react';
import { NavLink } from 'react-router';
import Keycloak from 'keycloak-js';

let initOptions = {
  url: 'http://localhost:10006',
  realm: 'multi-ninja',
  clientId: 'multi-ninja-frontend'
};

let kc = new Keycloak(initOptions);

kc.init({
  onLoad: 'login-required',
  checkLoginIframe: true,
  pkceMethod: 'S256'
}).then((auth) =>{
  if (!auth) {
    window.location.reload();
  }
  else{
    console.info('Authenticated');
    console.log('auth', auth);
    console.log('Keycloak', kc);
    console.log('Access Token', kc.token);
    console.log('Token info', JSON.stringify(kc.tokenParsed));

    // httpclient.defaults.headers.common['Authorization'] = `Bearer ${kc.token}`;
    kc.onTokenExpired = () => {
      console.log('Token expired');
    }
  }
}, () => {
  console.error('Authentication failed');
})

export default function App() {
  return (
    <div className="HomePage">
      <h2>Witaj na szkoleniu Multi-Ninja</h2>
      <p>
        <NavLink to={`/multiplication`}>Ćwicz mnożenie</NavLink>
      </p>
      <p>
        <NavLink to={`/division`}>Ćwicz dzielenie</NavLink>
      </p>
      <p>
        <img
          className="ninja-lemur"
          src={multi_ninja}
          alt="Multi-Ninja lemur"
        />
      </p>
    </div>
  );
}
