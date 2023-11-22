console.log("processing for: " + process.env.BASE_URL);

export default defineNuxtConfig({
  ssr: false,
  modules: ["@pinia/nuxt", "@pinia-plugin-persistedstate/nuxt", "nuxt-svgo"],
  runtimeConfig: {
    public: {
      apiBase: process.env.BASE_URL,
    },
  },

  // nitro: {
  //   devProxy: {
  //     "/api/": {
  //       target: process.env.BASE_URL,
  //       changeOrigin: true,
  //       prependPath: true,
  //       secure: false,
  //     },
  //   },
  // },
  svgo: {
    defaultImport: "component",
    componentPrefix: "Icon",
  },
});
