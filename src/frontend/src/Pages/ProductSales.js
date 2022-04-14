import React ,{useState, useEffect} from 'react'
import { Table } from "antd";

import configs from "../configs.json";

export default function ProductSales() {
  const columns = [
    {
      title: 'Product Name',
      dataIndex: 'productName',
      key: 'productName',
    },
    {
      title: 'Quantity',
      dataIndex: 'quantity',
      key: 'quantity',
    },
    {
      title: 'Unit Price',
      dataIndex: 'unitPrice',
      key: 'unitPrice',
      render: (price) => <>{price.amount} {price.currency}</>
    },
    {
      title: 'Cost',
      dataIndex: 'cost',
      key: 'cost',
    },
];

const [sales, setSales] = useState([])
const [loading, setLoading] = useState(true)

  useEffect(() => {
    fetch(`${configs.baseApiUrl}/OrderItem`)
    .then(res => res.json())
    .then(sales => {
      setSales(sales)
      setLoading(false)
    })
  },[])

  return (
    <Table columns={columns} dataSource={sales} rowKey='id'/>
  )
}
