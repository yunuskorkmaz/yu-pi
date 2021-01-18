import React, { useEffect, useState } from "react";
import { Col, PageHeader, Row, Table } from "antd";
import request from "../../utils/request";

const NgrokPage = () => {

  const [data, setData] = useState([]);

  useEffect(() => {
    getData()
  },[])

  const getData = async () => {
    try {
      const result = await request.get('ngrok/getAll');
      setData(result);
    } catch (error) {
      console.log(error)
    }
  }

  const columns = [
    {
      title: "URL",
      dataIndex: "url",
      key: "url",
    }
  ];
  return (
    <>
      <PageHeader
        title="Tunnels"
      />
      <div style={{ margin: "20px" }}>
        <Row justify="center">
          <Col xs={24}>
            <Table size="middle" columns={columns} dataSource={data} pagination={false} rowKey={"url"} />
          </Col>
        </Row>
      </div>
    </>
  );
};

export default NgrokPage;
