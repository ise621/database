import { Skeleton } from "antd";
import { useSession } from "next-auth/client";
import Layout from "../components/Layout";

const Index = () => {
  const [session, loading] = useSession();

  if (loading) {
    return (
      <Layout>
        <Skeleton />
      </Layout>
    );
  }

  return (
    <Layout>
      {!session && (
        <p>Not signed in.</p>
      )}
      {session && (
        <p>Signed in as {JSON.stringify(session, null, 2)}.</p>
      )}
    </Layout>
  );
};

export default Index;