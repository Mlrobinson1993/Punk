import { useAuthStore } from "~/store";

export default defineNuxtPlugin((nuxtApp) => {
  const authStore = useAuthStore();

  nuxtApp.hook("app:mounted", async () => {
    await authStore.checkAuthenticationStatus();
  });
});
