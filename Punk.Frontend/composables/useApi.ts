import { useRuntimeConfig } from "#app";
import type { ApiResponse } from "~/types/ApiResponse";

export function useApi() {
  const config = useRuntimeConfig();
  const apiBaseUrl = config.public.apiBase;

  async function fetchApi<T>(
    endpoint: string,
    options = <any>{}
  ): Promise<ApiResponse<T>> {
    try {
      const url = `${apiBaseUrl}/api${endpoint}`;

      const response: ApiResponse<T> = await $fetch(url, {
        ...options,
        // credentials: "include",
      });

      return response;
    } catch (err: any) {
      throw err;
    }
  }

  return { fetchApi };
}
