<template>
  <div>
    <LoginContent v-if="!isLoading" />
    <FullPageSpinner v-else />
  </div>
</template>

<script lang="ts" setup>
import { useAuthStore } from "~/store";

definePageMeta({
  layout: "auth",
  requiresAuth: false,
});

let isLoading = ref(true);

onMounted(() => {
  const authStore = useAuthStore();
  authStore.setLoading(false);
  isLoading.value = authStore.isLoading;
});
</script>

<style lang="scss" scoped></style>
