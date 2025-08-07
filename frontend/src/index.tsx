import ReactDOM from 'react-dom/client';
import App from './App';
import './assets/reset.css';
import './assets/index.css';
import { createBrowserRouter, RouterProvider } from 'react-router';
import MultiplicationComponent from './multiplication/multiplication';
import DivisionComponent from './division/division';
import AppLayoutComponent from './AppLayout';
import keycloak, * as options from './keycloak';
import { ReactKeycloakProvider } from '@react-keycloak/web';

const router = createBrowserRouter([
  {
    path: '/',
    Component: AppLayoutComponent,
    children: [
      {
        index: true,
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
      {
        path: '*',
        Component: App,
      },
    ]
  }]);

const rootEl = document.getElementById('root');
if (rootEl) {
  const root = ReactDOM.createRoot(rootEl);
  root.render(
    <ReactKeycloakProvider
      authClient={keycloak}
      initOptions={options.keyCloakInitOptions}
      onEvent={options.onEvent}
      onTokens={options.onTokens}>
      <RouterProvider router={router} />
    </ReactKeycloakProvider>
  );
}
