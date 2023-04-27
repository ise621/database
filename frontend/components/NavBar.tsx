import * as React from "react";
import Link from "next/link";
import { useRouter } from "next/router";
import { Button, Menu } from "antd";
import { UserOutlined } from "@ant-design/icons";
import paths from "../paths";

type NavItemProps = {
  path: string;
  label: string;
};

export type NavBarProps = {
  items: NavItemProps[];
};

const moderatorItems = [
  {
    path: paths.createData,
    label: "Create Data",
  },
  {
    path: paths.uploadFile,
    label: "Upload File",
  },
];

export default function NavBar({ items }: NavBarProps) {
  const router = useRouter();

  return (
    <Menu mode="horizontal" selectedKeys={[router.pathname]} theme="dark">
      {items.map(({ path, label }) => (
        <Menu.Item key={path}>
          <Link href={path}>{label}</Link>
        </Menu.Item>
      ))}
      <Menu.SubMenu key="moderator" icon={<UserOutlined />}>
        <Menu.Item key={paths.login}>
          <Link href={paths.login}>Login</Link>
        </Menu.Item>
        <Menu.Item key={paths.logout}>
          <form action={paths.logout} method="post">
            <Button type="primary" htmlType="submit">
              Logout
            </Button>
          </form>
        </Menu.Item>
        {moderatorItems.map(({ path, label }) => (
          <Menu.Item key={path}>
            <Link href={path}>{label}</Link>
          </Menu.Item>
        ))}
      </Menu.SubMenu>
    </Menu>
  );
}
