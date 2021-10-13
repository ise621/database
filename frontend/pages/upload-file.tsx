import { Form, Input, Button, Row, Col, Card, Typography, Upload } from "antd";
import { UploadOutlined } from "@ant-design/icons";
import Layout from "../components/Layout";
import { RcFile } from "antd/lib/upload/interface";

const layout = {
  labelCol: { span: 8 },
  wrapperCol: { span: 16 },
};

function Page() {
  const [form] = Form.useForm();

  const constructFileUploadAction = (_file: RcFile) =>
    `/api/upload-file?accessToken=${encodeURIComponent(
      form.getFieldValue("accessToken")
    )}&getHttpsResourceUuid=${encodeURIComponent(
      form.getFieldValue("getHttpsResourceUuid")
    )}`;

  return (
    <Layout>
      <Row justify="center">
        <Col>
          <Card title="Upload File">
            <Typography.Paragraph style={{ maxWidth: 768 }}>
              For an existing GET HTTPS resource entry in the database, upload a
              file with the content for that resource.
            </Typography.Paragraph>
            <Form {...layout} form={form} name="basic">
              <Form.Item
                label="Access Token"
                name="accessToken"
                rules={[
                  {
                    required: true,
                  },
                ]}
              >
                <Input />
              </Form.Item>

              <Form.Item
                label="GET HTTPS Resource UUID"
                name="getHttpsResourceUuid"
                rules={[
                  {
                    required: true,
                  },
                ]}
              >
                <Input />
              </Form.Item>

              <Form.Item name="file" label="File">
                <Upload
                  action={constructFileUploadAction}
                  withCredentials
                  listType="text"
                >
                  <Button icon={<UploadOutlined />}>
                    Select File to Upload It
                  </Button>
                </Upload>
              </Form.Item>
            </Form>
          </Card>
        </Col>
      </Row>
    </Layout>
  );
}

export default Page;
