<template>
  <div class="fullPage_spinner">
    <IconShield class="logo loader" />
  </div>
</template>

<script lang="ts" setup></script>

<style scoped>
.fullPage_spinner {
  height: 100vh;
  width: 100vw;
  position: relative;
}

.loader {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.logo {
  margin-bottom: 1rem;
  height: 100px;
  width: 100px;
}

.spinner {
  height: 150px;
  width: 150px;
  top: 49.5%;
}
</style>
