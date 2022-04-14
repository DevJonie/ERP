import { useState } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';

import { Layout, Breadcrumb } from 'antd';

import './App.css';
import 'antd/dist/antd.css';

import SalesOrders from './Pages/SalesOrders';
import ProductSales from './Pages/ProductSales';
import Products from './Pages/Products';
import SideBar from './SideBar';


const { Header, Content, Footer } = Layout;

function App() {
  const [collapsed, setState] = useState(false);

  function onCollapse(e){
    setState(!collapsed);
  };

  return (
    <BrowserRouter>
    <Layout style={{ minHeight: '100vh' }}>
      <Header className="site-layout-background" style={{ padding: 0, float: 'center'}}>
        <div className="logo" />
      </Header>
      <Layout>
        <SideBar collapsed={collapsed} onCollapse={onCollapse}/>
        <Layout className="site-layout">
            <Content style={{ margin: '0 16px' }}>
              <Breadcrumb style={{ margin: '16px 0' }}>
                <Breadcrumb.Item>User</Breadcrumb.Item>
                <Breadcrumb.Item>Bill</Breadcrumb.Item>
              </Breadcrumb>
            <div className="site-layout-background" style={{ padding: 24, minHeight: 360 }}>
                <Routes>
                  <Route path='/' element={<SalesOrders/>} exact/>
                  <Route path='/product-sales' element={<ProductSales/>} />
                  <Route path='/products' element={<Products/>} />
                  <Route path='*' element={<SalesOrders/>} />
                </Routes>
            </div>
            </Content>
          <Footer style={{ textAlign: 'center' }}>ERP ©2022 Created by John</Footer>
        </Layout>
      </Layout>
    </Layout>
    </BrowserRouter>
  );
}

export default App;
