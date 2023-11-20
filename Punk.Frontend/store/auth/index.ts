import { defineStore } from "pinia";
import { ref } from "vue";
import type { User } from "~/types/User";
import type { ApiResponse } from "~/types/ApiResponse";
import { useApi } from "~/composables/useApi";
import { useUserStore } from "~/store";
import { useToast } from "~/composables/useToast";

export const useAuthStore = defineStore(
  "auth",
  () => {
    const { fetchApi } = useApi();
    const userStore = useUserStore();
    const toast = useToast();

    const isAuthenticated: Ref<boolean> = ref(false);
    const hasCheckedStatus: Ref<boolean> = ref(false);
    const loading: Ref<boolean> = ref(true);

    async function login(formData: FormData) {
      const response: ApiResponse<User> = await fetchApi("/auth/authenticate", {
        method: "POST",
        body: formData,
      });

      if (response.statusCode === 200) {
        userStore.setUser(response.data);
        isAuthenticated.value = true;
        return true;
      }
    }

    async function logout() {
      try {
        const response = await fetchApi("/auth/logout", { method: "POST" });

        if (response.statusCode === 200) {
          userStore.setUser(null);
          isAuthenticated.value = false;
          hasCheckedStatus.value = false;
          return true;
        }

        throw new Error("Error logging out");
      } catch (error) {
        console.error(error);
        toast.showToast("error", "Error logging out", 4000);
        isAuthenticated.value = false;
        hasCheckedStatus.value = false;
      } finally {
      }
    }

    async function checkAuthenticationStatus() {
      setLoading(true);
      try {
        const isValid = await validateToken();

        if (isValid) {
          isAuthenticated.value = true;
          return;
        }

        const refreshed = await refreshToken();

        if (!refreshed) {
          await logout();
          return;
        }

        isAuthenticated.value = true;
      } catch (error) {
        await logout();
        navigateTo("/auth");
      } finally {
        setLoading(false);
      }
    }

    async function validateToken() {
      try {
        const response = await fetchApi("/auth/validate");

        if (response.statusCode === 200) {
          return response.data;
        }

        return false;
      } catch (error) {
        return false;
      }
    }

    async function refreshToken() {
      try {
        const response = await fetchApi("/auth/refresh", {
          method: "POST",
        });

        return response.statusCode === 200;
      } catch (error) {
        return false;
      }
    }

    function setLoading(isLoading: boolean) {
      loading.value = isLoading;
    }

    const isLoading = computed(() => {
      return loading.value;
    });

    return {
      login,
      logout,
      checkAuthenticationStatus,
      isAuthenticated,
      setLoading,
      isLoading,
      validateToken,
      hasCheckedStatus,
    };
  },
  {
    persist: {
      storage: persistedState.localStorage,
    },
  }
);
