import * as React from "react";
import Link from "next/link";
import { useRouter } from "next/router";
import { Menu } from "antd";
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
        {moderatorItems.map(({ path, label }) => (
          <Menu.Item key={path}>
            <Link href={path}>{label}</Link>
          </Menu.Item>
        ))}
      </Menu.SubMenu>
    </Menu>
  );
}
