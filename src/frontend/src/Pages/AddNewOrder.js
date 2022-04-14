import React, {useEffect, useState} from 'react'
import { PlusOutlined, DeleteFilled } from "@ant-design/icons";
import { Drawer, Form, Button, Col, Row, Input, 
  InputNumber, Select, Space, Table, Popconfirm } from 'antd';

  import configs from "../configs.json";

const { Option } = Select;

export default function AddNewOrder({visibility, onClose, onSubmit}) {

  const orderItemsColumn = [
    {
      title: 'Product',
      dataIndex: 'productId',
      key: 'productId',
      render: (prodId) => {
        const prodName = products.find(p => p.id === prodId).name
        return <>{prodName}</>
      }
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
      render: price => <>{price.amount}{price.currency}</>,
    },
    {
      title: 'Cost',
      key: 'unitPrice',
      render: (_, record) => {
        const cost = record.unitPrice.amount * record.quantity;
        return <>{cost}</>
      },
    },
    {
      title: 'Remove',
      key: 'removeaction',
      render: (_, record) =>
        <Popconfirm title="Sure to delete?" onConfirm={() => handleDelete(record.productId)}>
          <Button type="link" icon={<DeleteFilled/>} size='small' />
        </Popconfirm>,
    }
  ];

    const [customer, setCustomer] = useState({})
    const [orderItems, setOrderItems] = useState([])
    const [selectedProduct, setSelectedProduct] = useState({})
    const [quantity, setQuantity] = useState(1)

    const [products, setProducts] = useState([])
    const [loading, setLoading] = useState(true)
    const [addOrderDisabled, setAddOrderDisabled] = useState(true);
    const [submitDisabled, setSubmitDisabled] = useState(true);

    useEffect(() => {
      fetch(`${configs.baseApiUrl}/Product`)
      .then(res => res.json())
      .then(prods => {
          setProducts(prods)
          setLoading(false)
      })
    },[])

    function onOrderItemChanged(prodId){
      const prod = products.find(p => p.id === prodId)
      setSelectedProduct(prod)
      setAddOrderDisabled(false)
    }

    function onAddOrderItem(e){
      const allOrderItems = [...orderItems];
      const existingItem = allOrderItems
          .find(o => o.productId === selectedProduct.id)
      
      if(existingItem){
        existingItem.quantity += quantity;
        setOrderItems(allOrderItems)
      }
      else{
        const newOrderItem = {
          productId: selectedProduct.id,
          quantity: quantity,
          unitPrice: {
            amount: selectedProduct.minPrice.amount,
            currency: selectedProduct.minPrice.currency
          }
        }
        setOrderItems(prev => [...prev, newOrderItem])
      }
      setSubmitDisabled(false)
    }

    function handleDelete(id){
      console.log(id)
      const allOrderItems = [...orderItems];
      const existingItem = allOrderItems
          .filter(o => !(o.productId === id))
      setOrderItems(existingItem)

      if(existingItem.length <= 0){
        setSubmitDisabled(true)
      }

    }

    function onQuantityChanged(quatity){
      setQuantity(quatity)
    }

    function onCustomerAddressChange(e){
      setCustomer(prev => { 
        return {...prev, customerAddress: e.target.value};
      })
    }

    function onCustomerNameChange(e){
      setCustomer(prev => {
        return {...prev, customerName: e.target.value}
      })
    }

    async function onSubmitSalessOrder(){
        setLoading(true)
      const salesOrder = {
        customerName: customer.customerName,
        customerAddress: customer.customerAddress,
        orderItems: orderItems
      };
      
      const requestOptions = {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(salesOrder)
      }

      const response  = await fetch(`${configs.baseApiUrl}/SalesOrder`, requestOptions)
      const data = await response.json()
      console.log(data)
      setLoading(false);

      onSubmit(data);
    }

  return (
    <Drawer
      title="Create a new account"
      width={'60%'}
      onClose={onClose}
      visible={visibility}
      bodyStyle={{ paddingBottom: 80 }}
      extra={
        <Space>
          <Button onClick={onClose}>Cancel</Button>
          <Button 
            onClick={onSubmitSalessOrder} 
            disabled={submitDisabled}
            loading = {loading}
            type="primary">Submit</Button>
        </Space>
      }>
    <Form layout="vertical">
      <Row gutter={16}>
        <Col span={24}>
          <Form.Item
            name="customerName"
            label="Customer Name"
            rules={[{ required: true, message: 'Please enter user Customer\'s name' }]}
          >
            <Input onChange={onCustomerNameChange} placeholder="Please enter customer's name" />
          </Form.Item>
        </Col>
      </Row>
      <Row gutter={16}>
        <Col span={24}>
          <Form.Item
            name="customerAddress"
            label="Customer's Address"
            rules={[
              {
                required: true,
                message: "please enter customer's address",
              },
            ]}
          >
            <Input.TextArea onChange={onCustomerAddressChange} rows={4} placeholder="please enter customer's address" />
          </Form.Item>
        </Col>
      </Row>
      </Form>

      <Form layout="vertical" hideRequiredMark>
      <Row gutter={16}>
        <Col span={24}>
          <RenderOrderItems/>
        </Col>
      </Row>
      <Row gutter={16}>
        <Col span={12}>
          <Form.Item
            name="productId"
            label="Product"
            rules={[
              {
                required: true,
                message: "please select a product",
              },
            ]}
          >
            <Select onSelect={onOrderItemChanged} placeholder="Please select a product">
              {
                products.map(prod => {
                  return <Option key={prod.id} value={prod.id}>{prod.name} : {prod.minPrice.amount}{prod.minPrice.currency}</Option>
                })
              }
            </Select>
          </Form.Item>
        </Col>
        <Col span={6}>
          <Form.Item
            name="quatity"
            label="Quatity"
            initialValue={1}
            rules={[
              {
                required: true,
              },
            ]}
          >
            <InputNumber onChange={onQuantityChanged} min={1} />
          </Form.Item>
        </Col>
        <Col span={6} style={{display:'flex', alignItems:'center'}}>
          <Button onClick={onAddOrderItem} disabled={addOrderDisabled} type="primary" icon={<PlusOutlined />} size='medium' />
        </Col>
      </Row>
      </Form>
  </Drawer>
  )

  function RenderOrderItems(){
    return <Table 
      dataSource={orderItems} 
      columns={orderItemsColumn} 
      pagination={false} bordered 
      rowKey='productId'
      footer={(d) => {
        const costArr = d.map(d => d.quantity * d.unitPrice.amount);
        const TotalCost = costArr.reduce((cur, prev) => cur + prev, 0);
        return<strong>Total Cost: {TotalCost}</strong>
      }}/>
  }
}
