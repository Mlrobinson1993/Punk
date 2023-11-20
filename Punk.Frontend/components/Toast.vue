<template>
  <div class="toast-container">
    <div
      v-for="toast in toasts"
      :key="toast.id.toString()"
      class="toast"
      :class="`toast-${toast.type}`"
    >
      {{ toast.message }}
      <button class="cross" @click.prevent="removeToast(toast.id)">
        <IconCross class="cross-icon" />
      </button>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { useToast } from "~/composables/useToast";

const { toasts, removeToast } = useToast();
</script>

<style lang="scss" scoped>
.toast-container {
  position: fixed;
  left: 50%;
  transform: translateX(-50%);
  bottom: 2rem;
  min-height: 50px;
  min-width: 300px;
  z-index: 999;
  max-width: 400px;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.toast {
  min-height: 50px;
  width: 100%;
  background-color: #f9f8f8;
  position: relative;
  display: flex;
  align-items: center;
  padding-left: 1rem;
  box-shadow: 0px 3px 6px rgba(0, 0, 0, 0.14);
  &-success {
    border-left: 5px solid #28a745;
    .cross {
      &-icon {
        fill: #28a745;
      }
    }
  }
  &-error {
    border-left: 5px solid red;
    .cross {
      &-icon {
        fill: red;
      }
    }
  }
  &-info {
    border-left: 5px solid #007bff;
    .cross {
      &-icon {
        fill: #007bff;
      }
    }
  }

  .cross {
    border: none;
    background-color: transparent;
    cursor: pointer;
    position: relative;
    height: 35px;
    width: 35px;
    position: absolute;
    right: 1rem;
    top: 50%;
    transform: translateY(-50%);
  }
}
</style>
