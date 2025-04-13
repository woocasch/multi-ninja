import './App.css';
import multi_ninja from './assets/multi-ninja.png';
import React from 'react';
import { NavLink } from 'react-router';

export default function App() {
  return (
    <div className="HomePage">
      <h2>Witaj na szkoleniu Multi-Ninja</h2>
      <p>
        <NavLink to={`/multiplication`}>Ćwicz mnożenie</NavLink>
      </p>
      <p>
        <NavLink to={`/flawless-victory`}>Test screen</NavLink>
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
