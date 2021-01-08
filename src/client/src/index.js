import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import { MainStoreProvider } from './stores/mainStore';

ReactDOM.render(
  <React.StrictMode>
    <MainStoreProvider>
        <App />
    </MainStoreProvider>
  </React.StrictMode>,
  document.getElementById('root')
);