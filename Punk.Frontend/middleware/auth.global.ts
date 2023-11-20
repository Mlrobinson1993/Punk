import { useAuthStore } from "~/store/auth";

export default defineNuxtRouteMiddleware(async (to) => {
  const authStore = useAuthStore();

  if (!authStore.hasCheckedStatus && to.meta.requiresAuth) {
    await authStore.checkAuthenticationStatus();
    authStore.hasCheckedStatus = true;
  }

  if (!authStore.isAuthenticated && to.meta.requiresAuth) {
    return await navigateTo("/auth");
  }

  if (authStore.isAuthenticated && to.path === "/auth") {
    return await navigateTo("/");
  }
  return Promise.resolve();
});
