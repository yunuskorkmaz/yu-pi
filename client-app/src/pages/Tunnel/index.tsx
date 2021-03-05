import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Modal, Table, Form, Input, Select, Card, Button } from 'antd';
import { ColumnsType } from 'antd/lib/table';
import React, { useEffect, useState } from 'react';



const TunnelPage = () => {
    const [hubConnection, setHubConnection] = useState<HubConnection>();
    const [data, setData] = useState<Array<any>>([]);
    const [refresh, setRefresh] = useState(false);
    const [modal, setModal] = useState(false)
    const [form] = Form.useForm()

    useEffect(() => {
        setupSignalRConnection('tunnelHub');
    }, [])

    const setupSignalRConnection = async (hubName: string) => {
        const connection = new HubConnectionBuilder()
            .withUrl(process.env.REACT_APP_REST_URL + hubName)
            .withAutomaticReconnect()
            .build();

        connection.on('onConnected', (data: Array<any>) => {
            console.log('connected', data)
            setData(data)
        })

        connection.on('tunnelAdded', tunnel => {
            setRefresh(false);
            console.log("added", tunnel)
            setData([...data, tunnel])
        })

        try {
            await connection.start();
            setHubConnection(connection);
        } catch (error) {
            console.log(error);
        }
    }

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
            key: 'status'
        }, {
            title: 'actions',
            dataIndex: '',
            key: 'x',
            render: (value: any, record: any) => {
                return <>
                    <Button size="small" onClick={() => openEdit(record)} >Edit</Button>
                </>
            }
        }

    ]

    const onFinish = (values: any) => {
        console.log(values);
        if (hubConnection) {
            hubConnection.send('AddTunnel', values);
            setRefresh(true);
        }
    }

    return (
        <>
            <div style={{ padding: '20px' }}>
                <Card extra={<Button size="small" onClick={() => openAdd()}>Ekle</Button>}> 
                    <Table columns={columns} dataSource={data} rowKey="id" loading={refresh} pagination={false} bordered={false} size={"small"} />
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
}

export default TunnelPage;