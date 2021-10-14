import * as React from "react";
import Link from "next/link";
import { useRouter } from "next/router";
import { Menu } from "antd";

type NavItemProps = {
  path: string;
  label: string;
};

export type NavBarProps = {
  items: NavItemProps[];
};

export default function NavBar({ items }: NavBarProps) {
  const router = useRouter();

  return (
    <Menu mode="horizontal" selectedKeys={[router.pathname]} theme="dark">
      {items.map(({ path, label }) => (
        <Menu.Item key={path}>
          <Link href={path}>{label}</Link>
        </Menu.Item>
      ))}
    </Menu>
  );
}
