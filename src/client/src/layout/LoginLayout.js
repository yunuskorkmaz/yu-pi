import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { Button, Col, Form, Input, Row } from "antd";
import { useHistory } from "react-router-dom";
import request from "../utils/request";

import "./loginLayout.css";
import { useMainStore } from "../stores/mainStore";

const LoginLayout = () => {
  const history = useHistory();
  const mainStore = useMainStore();

    useEffect(() => {
        mainStore.isLogin && history.replace('/')
    },[])

  const onFinish = (values) => {
    login(values);
  };

  const login = async (values) => {
    // eslint-disable-next-line no-undef
    try {
      var result = await request.post("login", values, false);
      mainStore.setToken(result.token);
      history.replace("/");
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="loginLayout">
      <Row>
        <Col xs={24}>
          <div className="container">
            <div className="logo">YU-PI App Login</div>
            <div className="loginForm">
              <Form onFinish={onFinish}>
                <Form.Item
                  name="email"
                  rules={[
                    {
                      required: true,
                      message: "Please input your email",
                      type: "email",
                    },
                  ]}
                >
                  <Input placeholder="Email" />
                </Form.Item>
                <Form.Item
                  name="password"
                  rules={[
                    {
                      required: true,
                      message: "Please input your password",
                    },
                    {
                      min: 8,
                      message: "Your password must be mininum 8 char",
                    },
                  ]}
                >
                  <Input type="password" placeholder="password" />
                </Form.Item>
                <Form.Item>
                  <Button type="primary" htmlType="submit">
                    Log in
                  </Button>
                </Form.Item>
              </Form>
            </div>
          </div>
        </Col>
      </Row>
    </div>
  );
};

export default observer(LoginLayout);
