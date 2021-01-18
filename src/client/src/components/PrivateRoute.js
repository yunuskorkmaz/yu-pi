import { observer } from "mobx-react-lite";
import React, { useMemo } from "react";
import { Route, Redirect } from "react-router-dom";
import { useMainStore } from "../stores/mainStore";
const PrivateRoute = ({ component: Component, ...rest }) => {
  const { isLogin } = useMainStore();
  const renderContent = useMemo(() => {
    if (isLogin) {
      return <Route {...rest} component={Component} />;
    } else {
      return <Redirect to={{ pathname: "/login" }} />;
    }
  }, [isLogin]);

  return <>{renderContent}</>;
};

export default observer(PrivateRoute);
