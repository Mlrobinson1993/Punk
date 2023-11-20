<template>
  <div class="input-group">
    <button v-if="!isLoading" type="submit" class="login-button">
      {{ isSignUpActive ? "Sign Up" : "Log In" }}
    </button>
    <button v-else type="submit" class="login-button" disabled>
      Loading...
    </button>
  </div>
</template>

<script lang="ts" setup>
// Props
const props = defineProps({
  isLoading: Boolean,
  isSignUpActive: Boolean,
});
</script>

<style scoped>
.login-button {
  width: 100%;
  padding: 10px;
  border: none;
  background-color: #f0ad4e;
  color: white;
  border-radius: 5px;
  cursor: pointer;

  &:disabled {
    background-color: #ccc;
    cursor: default;
  }
}
</style>
