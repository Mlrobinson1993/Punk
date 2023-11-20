<template>
  <div>
    <NavBar />

    <div class="content-container">
      <img src="/not-found.svg" />
      <h1>Uh oh!</h1>
      <p>Looks like the page you're looking for doesn't exist.</p>
      <nuxt-link to="/">Go Home</nuxt-link>
    </div>
  </div>
</template>

<script lang="ts" setup>
definePageMeta({
  title: "Server Down",
  description: "The server is down. Please try again later.",
  layout: "default",
});
</script>

<style lang="scss" scoped>
.content-container {
  height: 100vh;
  width: 100vw;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  padding: 2rem;
  text-align: center;
  background: #f2f2f2;

  img {
    width: 100%;
    max-width: 500px;
    margin-bottom: 2rem;
  }
}
</style>
