import { observer } from "mobx-react-lite";
import React, {  } from "react";
import { Route, Redirect } from "react-router-dom";
import { useMainStore } from "../stores/mainStore";
// import { validateToken } from "../utils";
const PrivateRoute = ({ component: Component, ...rest }) => {
  const {  isLogin  } = useMainStore();
  const renderContent =() => {
    
    if ( isLogin) {
      return <Route {...rest} component={Component} />;
    } else {
      return <Redirect to={{ pathname: "/login" }} />;
    }
  };

  return <>{renderContent()}</>;
};

export default observer(PrivateRoute);
