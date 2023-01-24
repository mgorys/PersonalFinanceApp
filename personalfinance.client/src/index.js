import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import { DataProvider } from './contexts/data.context';
import 'bootstrap/dist/css/bootstrap.min.css';
import { UserProvider } from './contexts/user.context';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <UserProvider>
      <DataProvider>
        <App />
      </DataProvider>
    </UserProvider>
  </React.StrictMode>
);
