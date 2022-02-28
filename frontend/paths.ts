export default {
  home: "/",
  legalNotice: "/legal-notice",
  dataProtectionInformation: "/data-protection-information",
  data: "/data",
  calorimetricData: "/data/calorimetric",
  hygrothermalData: "/data/hygrothermal",
  opticalData: "/data/optical",
  photovoltaicData: "/data/photovoltaic",
  createData: "/data/create",
  uploadFile: "/upload-file",
  metabase: {
    component(uuid: string) {
      return `${
        process.env.NEXT_PUBLIC_METABASE_URL
      }/components/${encodeURIComponent(uuid)}`;
    },
    database(uuid: string) {
      return `/databases/${encodeURIComponent(uuid)}`;
    },
    dataFormat(uuid: string) {
      return `${
        process.env.NEXT_PUBLIC_METABASE_URL
      }/data-formats/${encodeURIComponent(uuid)}`;
    },
    institution(uuid: string) {
      return `${
        process.env.NEXT_PUBLIC_METABASE_URL
      }/institutions/${encodeURIComponent(uuid)}`;
    },
    method(uuid: string) {
      return `${
        process.env.NEXT_PUBLIC_METABASE_URL
      }/methods/${encodeURIComponent(uuid)}`;
    },
  },
};
