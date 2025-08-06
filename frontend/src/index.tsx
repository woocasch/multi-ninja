import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import './assets/reset.css';
import './assets/index.css';
import { createBrowserRouter, RouterProvider } from 'react-router';
import MultiplicationComponent from './multiplication/multiplication';
import DivisionComponent from './division/division';

const router = createBrowserRouter([
  {
    path: '/',
    Component: App,  
  },
  {
    path: 'multiplication',
    Component: MultiplicationComponent,
  },
  {
    path: 'division',
    Component: DivisionComponent,
  },
]);

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement,
);
root.render(
  <div className="root_container">
    <div className="menu">
      <a href="/">Start</a>
    </div>
    <RouterProvider router={router} />
    <div className="footer">
      Znalazłeś błąd? Opisz go{' '}
      <a
        href="https://github.com/woocasch/multi-ninja/issues/new"
        target="_blank"
      >
        tutaj
      </a>
      .
    </div>
  </div>,
);
