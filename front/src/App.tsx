import './App.css';
import React from "react";
import { NavLink } from "react-router";

export default function App() {
  return (
    <div className="HomePage">
      <h2>
        Welcome to MultiNinja
      </h2>
      <p>
        To start new game click <NavLink to={`/game_board`}>here</NavLink>
      </p>
    </div>
  );
}
