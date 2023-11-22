export default defineNuxtConfig({
  ssr: false,
  modules: ["@pinia/nuxt", "@pinia-plugin-persistedstate/nuxt", "nuxt-svgo"],
  runtimeConfig: {
    public: {
      apiBase: "https://punk-api.azurewebsites.net",
    },
  },

  svgo: {
    defaultImport: "component",
    componentPrefix: "Icon",
  },
});
