import React from 'react'
import { Menu, Layout } from "antd";
import {
  FundOutlined,
  BarChartOutlined,
  OrderedListOutlined
} from '@ant-design/icons';
import { Link } from 'react-router-dom';

const { Sider } = Layout;

export default function SideBar({collapsed, onCollapse}) {
  return (
    <Sider collapsible collapsed={collapsed} onCollapse={onCollapse}>
    <Menu theme="dark" defaultSelectedKeys={['1']} mode="inline">
      <Menu.Item key="1" icon={<BarChartOutlined />}>
        <Link to='/'>
          Sales Orders
        </Link>
      </Menu.Item>
      <Menu.Item key="2" icon={<FundOutlined />}>
      <Link to='/product-sales'>
          Product Sales
        </Link>
      </Menu.Item>
      <Menu.Item key="3" icon={<OrderedListOutlined />}>
      <Link to='/products'>
          Product
        </Link>
      </Menu.Item>
    </Menu>
  </Sider>
  )
}
