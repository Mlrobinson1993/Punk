export default defineNuxtConfig({
  ssr: false,
  modules: ["@pinia/nuxt", "@pinia-plugin-persistedstate/nuxt", "nuxt-svgo"],
  runtimeConfig: {
    public: {
      // apiBase: "https://punk-api.azurewebsites.net", // Comment me out to test live
      apiBase: "http://localhost:7218", // Uncomment me to test locally
    },
  },

  svgo: {
    defaultImport: "component",
    componentPrefix: "Icon",
  },
});
