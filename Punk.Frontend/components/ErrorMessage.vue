<template>
  <div class="error">{{ message }}</div>
</template>

<script setup>
// Props
const props = defineProps({
  message: String,
});
</script>

<style scoped>
.error {
  color: red;
  text-align: center;
  margin: 10px 0;
}
</style>
