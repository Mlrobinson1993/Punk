import { useRuntimeConfig } from "#app";
import { useAuthStore } from "~/store";
import type { ApiResponse } from "~/types/ApiResponse";

export function useApi() {
  const config = useRuntimeConfig();
  const apiBaseUrl = config.public.apiBase;

  const { token } = useAuthStore();
  async function fetchApi<T>(
    endpoint: string,
    options = <any>{}
  ): Promise<ApiResponse<T>> {
    try {
      const url = `${apiBaseUrl}/api${endpoint}`;

      const response: ApiResponse<T> = await $fetch(url, {
        ...options,
        headers: token ? { Authorization: `Bearer ${token}` } : {},
      });

      return response;
    } catch (err: any) {
      throw err;
    }
  }

  return { fetchApi };
}
