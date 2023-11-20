import { useAuthStore } from "@/store";
import { useApi } from "@/composables/useApi";
import type { AuthState } from "~/types/AuthState";

export function useLoginForm(state: AuthState) {
  const { fetchApi } = useApi();

  const handleSubmit = async () => {
    state.isLoading = true;
    state.signUpActive ? await handleRegister() : await handleLogin();
  };

  const handleLogin = async () => {
    try {
      const authStore = useAuthStore();
      const { email, password } = state.loginData;
      const formData = new FormData();
      formData.append("email", email);
      formData.append("password", password);

      const success = await authStore.login(formData);

      if (success) {
        navigateTo("/");
      }
    } catch (err: any) {
      state.error = err?.data?.message || "Unable to login. Please try again.";
    } finally {
      state.isLoading = false;
      resetError();
    }
  };

  const handleRegister = async () => {
    try {
      const { email, password, name } = state.loginData;
      const formData = new FormData();
      formData.append("email", email);
      formData.append("password", password);
      formData.append("name", name);

      const response = await fetchApi("/user", {
        method: "POST",
        body: formData,
      });

      if (response) {
        await handleLogin();
        return;
      }
    } catch (err: any) {
      state.error =
        err?.data?.message || "Unable to register. Please try again.";
    } finally {
      state.isLoading = false;
      resetError();
    }
  };

  const resetError = () => {
    setTimeout(() => {
      state.error = "";
    }, 3000);
  };

  const handleFormToggle = () => {
    state.signUpActive = !state.signUpActive;
  };

  return { handleSubmit, handleFormToggle };
}
