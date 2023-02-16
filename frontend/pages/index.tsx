import Layout from "../components/Layout";
import { Typography } from "antd";
import Link from "next/link";
import paths from "../paths";

function Page() {
  return (
    <Layout>
      <div style={{ maxWidth: 768 }}>
        <Typography.Paragraph>
          <Link href={paths.home}>solarbuildingenvelopes.com</Link> is an example of a database in the network <Typography.Link href="https://www.buildingenvelopedata.org">Building Envelope Data</Typography.Link>. It provides access to some data of the <Typography.Link href="https://www.ise.fraunhofer.de/en/rd-infrastructure/accredited-labs/testlab-solar-facades.html">TestLab Solar Façades</Typography.Link> which is an accredited laboratory at <Typography.Link href="https://www.ise.fraunhofer.de">Fraunhofer Institute for Solar Energy Systems (ISE)</Typography.Link>. The database is a live instance of the <Typography.Link href="https://github.com/building-envelope-data/database">reference implementation</Typography.Link>.
      </Typography.Paragraph>
      </div>
    </Layout>
  );
}

export default Page;
