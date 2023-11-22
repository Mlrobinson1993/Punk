import { defineStore } from "pinia";
import { ref } from "vue";
import type { User } from "~/types/User";
import type { ApiResponse } from "~/types/ApiResponse";
import { useApi } from "~/composables/useApi";
import { useUserStore } from "~/store";
import { useToast } from "~/composables/useToast";
import type { RefreshToken } from "~/types/RefreshToken";

export const useAuthStore = defineStore(
  "auth",
  () => {
    const { fetchApi } = useApi();
    const userStore = useUserStore();
    const toast = useToast();

    const isAuthenticated: Ref<boolean> = ref(false);
    const hasCheckedStatus: Ref<boolean> = ref(false);
    const loading: Ref<boolean> = ref(true);
    const token: Ref<string | null> = ref(null);
    const refresh: Ref<string | null> = ref(null);

    async function login(formData: FormData) {
      const response = await fetchApi<User>("/auth/authenticate", {
        method: "POST",
        body: formData,
      });

      if (response.statusCode === 200) {
        const { id, name, username, favouriteBeers } = response.data;
        token.value = response.data.token;
        refresh.value = response.data.refreshToken;
        userStore.setUser({ id, name, username, favouriteBeers } as User);
        isAuthenticated.value = true;
        return true;
      }
    }

    async function logout() {
      try {
        const response = await fetchApi("/auth/logout", { method: "POST" });

        if (response.statusCode === 200) {
          userStore.setUser(null);
          token.value = null;
          refresh.value = null;
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
        navigateTo("/auth");
      }
    }

    async function checkAuthenticationStatus() {
      setLoading(true);
      try {
        const isValid = await validateToken();
        console.log(isValid);

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
        const response = await fetchApi<RefreshToken>("/auth/refresh", {
          method: "POST",
          body: { refreshToken: refresh.value, expiredToken: token.value },
        });

        if (response.statusCode === 200) {
          token.value = response.data.token;
          refresh.value = response.data.refreshToken;
        }

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
      token,
      refresh,
    };
  },
  {
    persist: {
      storage: persistedState.localStorage,
    },
  }
);
