import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { createBrowserRouter, RouterProvider } from "react-router";
import PlayComponent from './play/play';

const router = createBrowserRouter([
  {
    path: "/",
    Component: App
  },
  {
    path: 'play',
    Component: PlayComponent
  }
]);

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);
root.render(
  <RouterProvider router={router} />,
);
