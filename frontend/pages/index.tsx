import { Skeleton, List, message } from "antd";
import { useSession } from "next-auth/client";
import Layout from "../components/Layout";
import { useDataFormatsQuery } from "../queries/dataFormats.graphql";
import { useCurrentUserQuery } from "../queries/currentUser.graphql";
import { useEffect } from "react";

const Index = () => {
  const [session, loading] = useSession();
  const dataFormatsQuery = useDataFormatsQuery();
  const currentUserDataQuery = useCurrentUserQuery();

  useEffect(() => {
    if (dataFormatsQuery.error) {
      message.error(dataFormatsQuery.error);
    }
    if (currentUserDataQuery.error) {
      message.error(currentUserDataQuery.error);
    }
  }, [dataFormatsQuery.error, currentUserDataQuery.error]);

  if (loading) {
    return (
      <Layout>
        <Skeleton />
      </Layout>
    );
  }

  return (
    <Layout>
      {!session && <p>Not signed in.</p>}
      {session && <p>Signed in as {JSON.stringify(session, null, 2)}.</p>}
      {currentUserDataQuery.data?.currentUser && (
        <>
          <p>
            Personal user data in metabase is you UUID{" "}
            {currentUserDataQuery.data.currentUser.uuid} and your email address{" "}
            {currentUserDataQuery.data.currentUser.email}.
          </p>
        </>
      )}
      {dataFormatsQuery.data?.dataFormats && (
        <>
          <p>Available data formats in metabase are</p>
          <List>
            {dataFormatsQuery.data.dataFormats.nodes.map((df) => (
              <List.Item>{df.name}</List.Item>
            ))}
          </List>
        </>
      )}
    </Layout>
  );
};

export default Index;
