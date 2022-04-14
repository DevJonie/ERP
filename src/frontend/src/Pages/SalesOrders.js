import React, { useState, useEffect } from 'react'
import { Button, Table, Tag } from 'antd';
import { PlusOutlined } from '@ant-design/icons';

import SalesOrderDetails from "./SalesOrderDetails";
import AddNewOrder from './AddNewOrder';

import configs from "../configs.json";

export default function SalesOrders() {
    const columns = [
        {
          title: 'Customer Name',
          dataIndex: 'customerName',
          key: 'customerName',
        },
        {
          title: 'Address',
          dataIndex: 'customerAddress',
          key: 'customerAddress',
        },
        {
          title: 'Status',
          dataIndex: 'status',
          key: 'status',
          render: status => <Tag color={'geekblue'}>{status}</Tag>,
        },
        {
          title: 'Created Date',
          key: 'createdDate',
          dataIndex: 'createdDate',
        },
        {
            title: 'Total Cost',
            key: 'totalCost',
            dataIndex: 'totalCost',
            render: (cost) => (<span>{cost}</span>)
        }
    ];

    const [salesOrders, setSalesOrders] = useState([])
    const [loading, setLoading] = useState(true)
    const [drawerVisibilty, setDrawerVisibilty] = useState(false)
    const [selectedSalesOrder, setSelectedSalesOrder] = useState({});
    const [newOrderVisibility, setNewOrderVisibility] = useState(false);
    const [selectedRow, setSelecteRow] = useState(0);

    useEffect(() => {
        fetch(`${configs.baseApiUrl}/SalesOrder`)
        .then(res => res.json())
        .then(orders => {
            setSalesOrders(orders)
            setLoading(false)
        }).catch(err => console.log(err))
    },[])

    function onRow(record, rowIndex){
        return {
            onClick: (e) => {
                setDrawerVisibilty(true)
                setSelectedSalesOrder(record)
            }
        }
    }

    function onCloseDrawer(e){
        setDrawerVisibilty(false)
    }

    function onAddNewOrder(){
        setNewOrderVisibility(true)
    }

    function onCloseNewOrder(){
        setNewOrderVisibility(false)
    }

    function onSubmitNewOrder(data){
        setSalesOrders(prev => [...prev, data])
        setNewOrderVisibility(false)
    }

    return (
        <>
            <Button onClick={onAddNewOrder} type="primary" style={{ marginBottom: 16}} icon={<PlusOutlined />}>New</Button>
            <Table columns={columns} dataSource={salesOrders} rowKey="id" loading={loading} rowClassName='table-row' onRow={onRow} />
            <SalesOrderDetails salesOrder={selectedSalesOrder} onClose={onCloseDrawer} visibility={drawerVisibilty} />
            <AddNewOrder visibility={newOrderVisibility} onClose={onCloseNewOrder} onSubmit={onSubmitNewOrder}></AddNewOrder>
        </>
    )
}
