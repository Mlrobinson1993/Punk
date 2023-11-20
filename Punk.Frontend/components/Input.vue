<template>
  <div class="input-group">
    <input
      :type="type"
      :placeholder="placeholder"
      :data-test-id="type"
      required
      v-model="modelProxy"
    />

    <component :is="icon" class="icon" v-if="icon" />
  </div>
</template>

<script lang="ts" setup>
// Props
const props = defineProps({
  modelValue: String,
  type: String,
  placeholder: String,
  icon: String,
});

// State
const modelProxy = ref(props.modelValue);

// Computed
const icon = computed(() => {
  if (props.icon == "name") return "IconUser";
  if (props.icon == "username") return "IconEmail";
  if (props.icon == "password") return "IconPadlock";
});

// Emitters
const emit = defineEmits(["updated"]);

function updated() {
  emit("updated", modelProxy.value);
}

// Watchers
watch(modelProxy, updated);
</script>

<style scoped>
.input-group {
  position: relative;
  margin-bottom: 20px;

  & > input {
    width: 100%;
    padding: 10px 35px 10px 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
  }
}

.icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
  height: 20px;
  width: 20px;
  fill: #868585;
}
</style>
