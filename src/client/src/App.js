// import { useObserver } from 'mobx-react-lite';
import { observer } from "mobx-react-lite";
import React from "react";
import { HashRouter, Route, Switch } from "react-router-dom";
import AppLayout from "./layout";
import routes from "./routes";

import "./App.css";

function App() {
  var paths = [];
  routes.map(
    (route) =>
      route.path != "/" &&
      typeof route.path == "string" &&
      paths.push(route.path.replace("/", ""))
  );

  return (
      <HashRouter>
        <Switch>
          <Route path={`/(|${paths.join("|")})`} component={AppLayout} />
          <Route path="/login" component={() => <>login</>} />
          <Route component={() => <>404</>} />
        </Switch>
      </HashRouter>
  );
}

export default observer(App);
