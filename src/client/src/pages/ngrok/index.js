import React from "react";
import { Col, PageHeader, Row, Table } from "antd";

const NgrokPage = () => {
  const columns = [
    {
      title: "Name",
      dataIndex: "name",
      key: "name",
    },
    {
      title: "Age",
      dataIndex: "age",
      key: "age",
    },
    {
      title: "Address",
      dataIndex: "address",
      key: "address",
    },
    {
      title: "Tags",
      key: "tags",
      dataIndex: "tags",
    },
  ];
  const data = [
    {
      key: "1",
      name: "John Brown",
      age: 32,
      address: "New York No. 1 Lake Park",
      tags: ["nice", "developer"],
    },
    {
      key: "2",
      name: "Jim Green",
      age: 42,
      address: "London No. 1 Lake Park",
      tags: ["loser"],
    },
    {
      key: "3",
      name: "Joe Black",
      age: 32,
      address: "Sidney No. 1 Lake Park",
      tags: ["cool", "teacher"],
    },
  ];
  return (
    <>
      <PageHeader
        title="Tunnels"
      />
      <div style={{ margin: "20px" }}>
        <Row justify="center">
          <Col xs={22}>
            <Table size="middle" columns={columns} dataSource={data} pagination={false} />
          </Col>
        </Row>
      </div>
    </>
  );
};

export default NgrokPage;
