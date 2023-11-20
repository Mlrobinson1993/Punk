<template>
  <FavouriteBeersSection :maxResults="15" />
</template>

<script setup>
definePageMeta({
  title: "Beer App",
  description: "A beer app built with Nuxt.js",
  requiresAuth: true,
});
</script>

<style lang="scss" scoped>
.content-container {
  margin: 0 auto;
  max-width: 1280px;
  width: 90%;
  margin-bottom: 2rem;
  min-height: 200px;
}
</style>
