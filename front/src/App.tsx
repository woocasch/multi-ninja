import './App.css';
import multi_ninja from './assets/multi-ninja.png';
import React from "react";
import { NavLink } from "react-router";

export default function App() {
  return (
    <div className="HomePage">
      <h2>
        Welcome to MultiNinja
      </h2>
      <p>
        To start new game click <NavLink to={`/play`}>here</NavLink>
      </p>
      <p>
        <img src={multi_ninja} alt="Multi-Ninja lemur" />
      </p>
    </div>
  );
}
