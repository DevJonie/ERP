import { Form, Modal, Row, Col, Input, InputNumber } from 'antd'
import React, { useState } from 'react'

import configs from "../configs.json";

export default function AddNewProduct({visibility, onClose, onSubmit}) {

    const [productName, setProductName] = useState("")
    const [productPrice, setProductPrice] = useState(1)
    const [loading, setLoading] = useState(false);

    function onProductNameChange(e){
        setProductName(e.target.value)
    }

    function onProductPriceChange(price){
        setProductPrice(price)
    }

    async function onSubmitNewProduct(){
        setLoading(true)
        const newProduct = {
            name: productName,
            minPrice: {
                amount: productPrice,
                currency: "USD"
            },
          };
          
        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(newProduct)
          }
    
        const response  = await fetch(`${configs.baseApiUrl}/Product`, requestOptions)
        const data = await response.json()
        console.log(data)
        setLoading(false);

        onSubmit(data);
    }

  return (
    <Modal 
        visible={visibility} 
        onCancel={onClose} 
        onOk={onSubmitNewProduct}
        okButtonProps={{loading:loading}}>
    <Form layout="vertical">
      <Row gutter={16}>
        <Col span={24}>
          <Form.Item
            name="productName"
            label="Product Name"
            rules={[{ required: true, message: 'Please enter user product\'s name' }]}>
            <Input onChange={onProductNameChange} placeholder="Please enter product name" />
          </Form.Item>
        </Col>
      </Row>
      <Row gutter={16}>
        <Col span={24}>
          <Form.Item
            name="productPrice"
            label="Product Price"
            rules={[{required: true, message: "please enter price"}]}>
            <InputNumber onChange={onProductPriceChange} placeholder="please enter product price" addonAfter="USD"/>
          </Form.Item>
        </Col>
      </Row>
      </Form>
  </Modal>
  )
}
