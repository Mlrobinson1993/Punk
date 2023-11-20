<template>
  <nav class="navbar">
    <nuxt-link to="/">
      <IconShield alt="Logo" class="logo" />
    </nuxt-link>
    <button class="auth-button" @click="handleLogOut">Log Out</button>
  </nav>
</template>

<script setup>
import { useAuthStore } from "~/store";

// Methods

async function handleLogOut() {
  const success = await useAuthStore().logout();
  if (success) navigateTo("/auth");
}
</script>

<style scoped>
.navbar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 1rem 2rem;
  color: white;
}

.logo {
  height: 50px;
}

.auth-button {
  cursor: pointer;
  background: #f0ad4e;
  border: none;
  border-radius: 5px;
  color: white;
  padding: 0.75rem 1.5rem;
}
</style>
