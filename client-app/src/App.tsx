import React from 'react';
import { HashRouter, Route, Switch } from 'react-router-dom';
import AppLayout from './layout';

import './App.css'


import routes from "./routes";
function App() {
  return (
    <HashRouter>
      <Switch>
        {routes.map((item) => (
          <Route
            exact
            path={item.path}
            component={() => <AppLayout />}
            key={item.path}
          />
        ))}
      </Switch>
    </HashRouter>
  );
}

export default App;
