declare global {
  namespace NodeJS {
    interface ProcessEnv {
      NEXT_PUBLIC_DATABASE_URL: string;
      NEXT_PUBLIC_METABASE_URL: string;
      NEXTAUTH_URL: string;
      NEXT_WEBPACK_USEPOLLING: string;
      NODE_ENV: "test" | "development" | "production";
      AUTH_CLIENT_ID: string;
      AUTH_CLIENT_SECRET: string;
      AUTH_SECRET: string;
      AUTH_JWT_SECRET: string;
    }
  }
}
