<template>
  <div v-if="isSignUpActive" class="toggle">
    Already have an account?
    <a @click.prevent="toggle" href="#">Log in</a>
  </div>

  <div v-else class="toggle">
    No account?
    <a @click.prevent="toggle" href="#">Sign up</a>
  </div>
</template>

<script lang="ts" setup>
// Props
const { isSignUpActive } = defineProps({
  isSignUpActive: Boolean,
});

// Emitters
const emit = defineEmits(["toggle"]);

const toggle = () => {
  emit("toggle");
};
</script>

<style lang="scss" scoped>
.toggle {
  text-align: center;
  margin-top: 20px;
  & > a {
    color: #06579c;
    text-decoration: none;

    &:hover {
      text-decoration: underline;
    }
  }
}
</style>
