import React, { useEffect, useState } from 'react'

import { Drawer, Table, Button, Col, Row, Space } from 'antd';
import { PlusOutlined } from '@ant-design/icons';



import "./SalesOrderDetails.css";

export default function SalesOrderDetails({visibility, onClose, salesOrder = null}) {
    const columns = [
        {
          title: 'Product',
          dataIndex: 'productName',
          key: 'productName'
        },
        {
          title: 'Quantity',
          dataIndex: 'quantity',
          key: 'quantity',
        },
        {
          title: 'Price',
          dataIndex: 'unitPrice',
          key: 'unitPrice',
          render: unitPrice =>
                <>{unitPrice.amount} {unitPrice.currency}</>
        },
        {
          title: 'Cost',
          key: 'cost',
          dataIndex: 'cost'
        }
      ];
    
    return (
      <Drawer
      title="Sales Order Details"
      onClose={onClose}
      visible={visibility}
      bodyStyle={{padding: '0 80px', display: 'flex', alignItems: 'center',  flexDirection:'column' }}
      placement = 'top'
      className="drawer-container"
    >
        <div style={{textAlign:'right'}}>
          <div style={{display:'flex', justifyContent:'space-between'}}>
            
            <div><strong>Company Name</strong></div>
            <div><strong>SO-000{salesOrder.id}</strong></div>
          </div>
          <hr/>
          <div><strong>{salesOrder.customerName}</strong></div>
          <div><strong>{salesOrder.customerAddress}</strong></div>
          <hr/>
          <Table columns={columns} dataSource={salesOrder.orderItems} rowKey='id' pagination={false} bordered={true}/>
          <hr/>
          <div><strong>Total Cost: {salesOrder.totalCost}</strong></div>
          </div>
    </Drawer>
    )
}
