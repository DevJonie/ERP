import { Button, Table } from 'antd';
import React, {useEffect, useState} from 'react'
import AddNewProduct from "./AddNewProduct";

import configs from "../configs.json";
import { PlusOutlined } from '@ant-design/icons';

export default function Products() {
  const columns = [
    {
      title: 'Product Name',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: 'Min Price',
      dataIndex: 'minPrice',
      key: 'minPrice',
      render: (price) => <p>{price.amount} {price.currency}</p>
    }
];

const [products, setProducts] = useState([])
const [loading, setLoading] = useState(true)
const [newProductVisibility, setNewProductVisibility] = useState(false)

useEffect(() => {
  fetch(`${configs.baseApiUrl}/Product`)
  .then(res => res.json())
  .then(prods => {
      setProducts(prods)
      setLoading(false)
  })
},[])

function onAddNewProduct(){
  setNewProductVisibility(true)
}

  function onCloseNewProduct(){
    setNewProductVisibility(false)
  }

  function onSubmitNewProduct(data){
    setProducts(prev => [...prev, data])
    setNewProductVisibility(false)
  }

  return (
    <>
    <Button onClick={onAddNewProduct} type='primary' icon={<PlusOutlined />}>New Product</Button>
    <Table columns={columns} dataSource={products} rowKey='id' loading={loading}/>
    <AddNewProduct visibility={newProductVisibility} onClose={onCloseNewProduct} onSubmit={onSubmitNewProduct}></AddNewProduct>
    </>
  )
}
