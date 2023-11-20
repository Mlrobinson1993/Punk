<template>
  <form class="search-bar" @submit.prevent="performSearch">
    <input type="text" placeholder="E.g. Hazy Jane" v-model="searchQuery" />
    <button @click="performSearch">Search</button>
  </form>
</template>

<script lang="ts" setup>
// State
const searchQuery = ref("");

// Methods
const performSearch = () => {
  if (searchQuery.value) {
    navigateTo(`/beers/search?name=${searchQuery.value}`);
  }
};
</script>

<style lang="scss" scoped>
$color-button-primary: #06579c;
.search-bar {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  input[type="text"] {
    flex-basis: 75%;
    padding: 10px 35px 10px 10px;
    border: 1px solid #ccc;
    border-radius: 5px;
  }

  button {
    padding: 10px;
    border: 1px solid $color-button-primary;
    color: white;
    flex-basis: 25%;
    text-align: center;
    background-color: $color-button-primary;
    border-radius: 0.25rem;
    cursor: pointer;
  }
}

@media (max-width: 768px) {
  .search-bar {
    flex-direction: column;

    input[type="text"] {
      flex-basis: 100%;
      width: 100%;
    }
    button {
      flex-basis: 100%;
      width: 100%;
    }
  }
}
</style>
