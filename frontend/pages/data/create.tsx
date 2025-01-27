import { Row, Col, Card, Typography } from "antd";
import Layout from "../../components/Layout";

function Page() {
  return (
    <Layout>
      <Row justify="center">
        <Col>
          <Card title="Create Data">
            <Typography.Paragraph style={{ maxWidth: 768 }}>
              Create data by sending GraphQL mutations to the{" "}
              <Typography.Link
                href={`${process.env.NEXT_PUBLIC_DATABASE_URL}/graphql/`}
              >
                GraphQL endpoint
              </Typography.Link>
              .
            </Typography.Paragraph>
            {/* <Form {...layout} form={form} name="basic">
              <Form.Item
                label="..."
                name="..."
                rules={[
                  {
                    required: true,
                  },
                ]}
              >
                <Input />
              </Form.Item>
            </Form> */}
          </Card>
        </Col>
      </Row>
    </Layout>
  );
}

export default Page;
