<template>
  <div class="pagination">
    <button @click="changePage(-1)" :disabled="currentPageProxy === 1">
      Previous
    </button>
    <span>Page {{ currentPageProxy }}</span>
    <button @click="changePage(1)" :disabled="disableNext">Next</button>
  </div>
</template>

<script lang="ts" setup>
// Props
const { currentPage, disableNext } = defineProps({
  currentPage: {
    type: Number,
    default: 1,
  },
  disableNext: {
    type: Boolean,
    default: false,
  },
});

// State
const currentPageProxy = ref(currentPage);

// Methods

const changePage = (page: number) => {
  currentPageProxy.value += page;
  paginate();
};

// Emitters
const emit = defineEmits(["paginate"]);

function paginate() {
  emit("paginate", currentPageProxy.value);
}
</script>

<style lang="scss" scoped>
.pagination {
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 4rem 0;
  gap: 1rem;

  button {
    border-radius: 5px;
    padding: 0.5rem 1rem;
    margin: 0 0.5rem;
    border: 1px solid #06579c;
    background-color: #06579c;
    color: white;
    width: 100px;
    cursor: pointer;

    &:hover:not(:disabled) {
      background-color: lighten(#06579c, 10%);
    }
    &:disabled {
      opacity: 0.5;
      cursor: not-allowed;
    }
  }
}
</style>
