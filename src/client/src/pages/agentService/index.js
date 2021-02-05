/* eslint-disable react/display-name */
/* eslint-disable no-undef */
import React, { useEffect, useState } from "react";
import { PageHeader, Row, Col, Table, Button } from "antd";
import * as signalR from "@microsoft/signalr";

const AgentService = () => {
  const [data, setData] = useState([]);
  const [hubConnection,sethubConnection] = useState(null);
  useEffect(() => {
   var _hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(process.env.REACT_APP_API + "hubs/agentService")
      .configureLogging(signalR.LogLevel.Information)
      .withAutomaticReconnect()
      .build();

    _hubConnection
      .start()
      .then(() => { })
      .catch(console.log);

    _hubConnection.on("data", (message) => {
      console.log(message)
      setData(JSON.parse(message));
    });
    sethubConnection(_hubConnection);
  }, []);

  const columns = [
    {
      title: "Name",
      dataIndex: "name",
      key: "name",
    },
    {
      title: "Path",
      dataIndex: "path",
      key: "path",
    },
    {
      title: "Status",
      dataIndex: "status",
      key: "status",
    },
    {
      title: "Action",
      dataIndex: "",
      key: "x",
      render: (record) => <>
          <Button onClick={() => handleStatus(record.path,true) } >Start</Button>
          <Button onClick={() => handleStatus(record.path,false)}>Stop</Button>
      </>
    },
  ];

  const handleStatus = (path,status) => {
    hubConnection.invoke('ChangeStatus',path,status )
  }

  return (
    <>
      <PageHeader title="Agent Services" />
      <div style={{ margin: "20px" }}>
        <Row justify="center">
          <Col xs={24}>
            <Table
              size="middle"
              columns={columns}
              dataSource={data}
              pagination={false}
              rowKey={"path"}
            />
          </Col>
        </Row>
      </div>
    </>
  );
};

export default AgentService;
