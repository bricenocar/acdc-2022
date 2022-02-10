import React, { createContext } from 'react';
import Header from './Components/Header/Header'
import Content from './Components/Content/Content'
import { AuthProvider } from './Providers/Auth';
import { initializeIcons, Label } from '@fluentui/react';

import '@fluentui/react/dist/css/fabric.css';
import './App.css';

export const AuthContext = createContext({});

export const App: React.FunctionComponent = () => {
  // Get auth provider
  const useAuth = AuthProvider();

  // Init icons
  initializeIcons();

  return (
    <AuthContext.Provider value={useAuth}>
      <div className="ms-Grid" dir="ltr">
        <div className="ms-Grid-row">
          <div className="main-element ms-Grid-col ms-sm12 ms-xl12">
            <div className="ms-Grid-row">
              <div className="ms-Grid-col ms-sm12 ms-xl12">
                <Header />
              </div>
            </div>
            <div className="ms-Grid-row">
              <div className="ms-Grid-col ms-sm12 ms-xl12">
                <Content />
              </div>
            </div>
          </div>
        </div>
      </div>
    </AuthContext.Provider>
  );
};
