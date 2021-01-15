import React, { useState } from "react";
import useMedia from "react-media-hook2";
import { Layout } from "antd";
import AppSider from "./AppSider";
import { Route, Switch } from "react-router-dom";
import NgrokPage from "../pages/ngrok";

const { Header, Content } = Layout;

const AppLayout = () => {
  const [collapse, setCollapse] = useState(false);
  const isMobile = useMedia({
    onChange: (val) => {
      setCollapse(val);
    },
    query: "(max-width:599px)",
  })[0];
  return (
    <Layout style={{ height: "100vh" }}>
      <AppSider
        collapse={collapse}
        onCollapse={(val) => setCollapse(val)}
        isMobile={isMobile}
      />
      <Layout>
        <Header>Header</Header>
        <Content>
          <Switch>
              <Route path="/" exact render={() => <>Home</>} />
              <Route path="/ngrok" component={NgrokPage} />
          </Switch>
        </Content>
      </Layout>
    </Layout>
  );
};

export default AppLayout;
