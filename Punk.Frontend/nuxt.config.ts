export default defineNuxtConfig({
  ssr: false,
  modules: ["@pinia/nuxt", "@pinia-plugin-persistedstate/nuxt", "nuxt-svgo"],
  runtimeConfig: {
    public: {
      apiBase: process.env.BASE_URL,
    },
  },
  devServer: {
    https: {
      key: "localhost-key.pem",
      cert: "localhost.pem",
    },
  },
  svgo: {
    defaultImport: "component",
    componentPrefix: "Icon",
  },
});
