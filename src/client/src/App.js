// import { useObserver } from 'mobx-react-lite';
import { observer } from "mobx-react-lite";
import React from "react";
import { HashRouter, Route, Switch } from "react-router-dom";
import AppLayout from "./layout";
import PrivateRoute from "./components/PrivateRoute";
import LoginLayout from "./layout/LoginLayout";

import "./App.css";
import { MainStoreProvider } from "./stores/mainStore";
import NotFound from "./layout/NotFound";
import routes from "./routes";

function App() {
  return (
    <HashRouter>
      <MainStoreProvider>
        <Switch>
          {routes.map((item) => (
            <PrivateRoute
              exact
              path={item.path}
              component={AppLayout}
              key={item.path}
            />
          ))}
          <Route exact path="/login" component={LoginLayout} />
          <Route path="/*" component={NotFound} />
        </Switch>
      </MainStoreProvider>
    </HashRouter>
  );
}

export default observer(App);
