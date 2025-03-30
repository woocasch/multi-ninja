import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { createBrowserRouter, RouterProvider } from "react-router";
import GameBoard from './game_board/game_board';

function Root() {
  return <h1>Hello world</h1>;
}

const router = createBrowserRouter([
  {
    path: "/",
    Component: App
  },
  {
    path: 'game_board',
    Component: GameBoard
  }
]);

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
  <RouterProvider router={router} />,
);
