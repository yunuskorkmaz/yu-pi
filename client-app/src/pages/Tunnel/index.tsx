import { Modal, Table, Form, Input, Select, Card, Button, Tag } from 'antd';
import { ColumnsType } from 'antd/lib/table';
import { observer } from 'mobx-react-lite';
import React, { useEffect, useState } from 'react';
import { Tunnel, TunnelStatus } from '../../models/Tunnel';
import { TunnelStore } from '../../store/tunnelStore';

const tunnelStore = new TunnelStore();

const TunnelPage = observer(() => {
    const [store] = useState<TunnelStore>(tunnelStore);
    const [modal, setModal] = useState(false)
    const [form] = Form.useForm()

    const openEdit = (record: any) => {
        form.setFieldsValue(record)
        setModal(true)
    }
    const openAdd = () => {
        form.resetFields();
        setModal(true);
    }

    const columns: ColumnsType<any> = [
        {
            title: 'Name',
            dataIndex: 'name',
            key: 'name'
        },
        {
            title: 'Protokol',
            dataIndex: 'protokol',
            key: 'protokol'
        },
        {
            title: 'port',
            dataIndex: 'port',
            key: 'port'
        },
        {
            title: 'Public Url',
            dataIndex: 'publicUrl',
            key: 'publicUrl'
        },
        {
            title: 'status',
            dataIndex: 'status',
            key: 'status',
            render: (value: TunnelStatus, record: Tunnel) => {
                let color = '#fff';
                switch (value) {
                    case TunnelStatus.Preparing:
                        color = 'blue';
                        break;
                    case TunnelStatus.Active:
                        color = 'green';
                        break;
                    case TunnelStatus.Passive:
                        color = 'red';
                        break;
                }
                return <>
                    <Tag color={color}>{TunnelStatus[value]}</Tag>
                </>
            }
        }, {
            title: 'actions',
            dataIndex: '',
            key: 'x',
            render: (value: any, record: any) => {
                return <>
                    <Button size="small" onClick={() => openEdit(record)} >Edit</Button>
                    <Button size="small" onClick={() =>  store.closeTunnel(record)} >Delete</Button>
                </>
            }
        }

    ]

    const onFinish = (values: any) => {
        store.addTunnel(values);
        setModal(false);
    }

    return (
        <>
            <div style={{ padding: '20px' }}>
                <Card extra={
                    <>
                        <Button size="small" onClick={() => openAdd()}>Ekle</Button>
                        {/* <Button size="small" onClick={() => store.closeAll()}>Ekle</Button> */}
                    </>}>
                    <Table rowKey={record =>  '_' + Math.random().toString(36).substr(2, 9)} columns={columns} dataSource={[...store.tunnels]} pagination={false} bordered={false} size={"small"} />
                </Card>

            </div>

            <Modal title="Tunnel Ekle" visible={modal} onCancel={() => setModal(false)} onOk={() => form.submit()} >
                <Form name="test" onFinish={onFinish} form={form}>
                    <Form.Item
                        label="Name"
                        name="name"
                    >
                        <Input />
                    </Form.Item>
                    <Form.Item
                        label="Protokol"
                        name="protokol"
                    >
                        <Select>
                            <Select.Option value={"http"}>http</Select.Option>
                            <Select.Option value={"tcp"}>tcp</Select.Option>
                        </Select>
                    </Form.Item>
                    <Form.Item
                        label="Port"
                        name="port"
                    >
                        <Input />
                    </Form.Item>
                </Form>
            </Modal>
        </>
    )
})

export default TunnelPage;