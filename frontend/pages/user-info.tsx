import Layout from "../components/Layout";
import { useCurrentUserInfoQuery } from "../queries/currentUser.graphql";
import { Skeleton, Result, Descriptions, Typography } from "antd";
import { PageHeader } from "@ant-design/pro-layout";
import { useEffect } from "react";
import { messageApolloError } from "../lib/apollo";

function Page() {
  const { loading, error, data } = useCurrentUserInfoQuery();
  const currentUserInfo = data?.currentUserInfo;

  useEffect(() => {
    if (error) {
      messageApolloError(error);
    }
  }, [error]);

  if (loading) {
    return <Skeleton active avatar title />;
  }

  if (!currentUserInfo) {
    return (
      <Result
        status="500"
        title="500"
        subTitle="Sorry, something went wrong."
      />
    );
  }

  return (
    <Layout>
      <PageHeader
        title={currentUserInfo.name}
        subTitle={currentUserInfo.sub}
        backIcon={false}
      >
        <Descriptions size="small" column={1}>
          <Descriptions.Item label="Email">
            {currentUserInfo.email} (Verified: {currentUserInfo.emailVerified})
          </Descriptions.Item>
          <Descriptions.Item label="Phone Number">
            {currentUserInfo.phoneNumber} (Verified:{" "}
            {currentUserInfo.phoneNumberVerified})
          </Descriptions.Item>
          <Descriptions.Item label="Address">
            {currentUserInfo.address?.formatted}
          </Descriptions.Item>
          <Descriptions.Item label="Website">
            <Typography.Link href={currentUserInfo.website}>
              {currentUserInfo.website}
            </Typography.Link>
          </Descriptions.Item>
          <Descriptions.Item label="Roles">
            {currentUserInfo.roles}
          </Descriptions.Item>
        </Descriptions>
      </PageHeader>
    </Layout>
  );
}

export default Page;
