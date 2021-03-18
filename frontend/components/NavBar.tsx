import * as React from "react";
import Link from "next/link";
import { useRouter } from "next/router";
import { Menu, Button } from "antd";
import { signIn, signOut, useSession } from "next-auth/client";

type NavItemProps = {
  path: string;
  label: string;
};

export type NavBarProps = {
  items: NavItemProps[];
};

export const NavBar: React.FunctionComponent<NavBarProps> = ({ items }) => {
  const router = useRouter();
  const [session /*, loading */] = useSession()

  return (
    <Menu mode="horizontal" selectedKeys={[router.pathname]} theme="dark">
      {items.map(({ path, label }) => (
        <Menu.Item key={path}>
          <Link href={path}>{label}</Link>
        </Menu.Item>
      ))}
      {session && (
        <Menu.Item>
          <Button type="link" onClick={() => signOut()}>
            Logout
          </Button>
        </Menu.Item>
      )}
      {!session && (
        <Menu.Item>
          {/* TODO Instead of the provider id `metabase` use a global constant. It must match the one set in `[...nextauth].ts` */}
          <Button type="link" onClick={() => signIn("metabase")}>
            Login
          </Button>
        </Menu.Item>
      )}
    </Menu>
  );
};

export default NavBar;
