<template>
  <div class="grid">
    <slot></slot>
  </div>
</template>

<script></script>
<style lang="scss" scoped>
.grid {
  display: grid;
  grid-gap: 1rem;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
}

@media (min-width: 600px) {
  .grid {
    grid-template-columns: repeat(2, minmax(300px, 1fr));
  }
}

@media (min-width: 900px) {
  .grid {
    grid-template-columns: repeat(3, minmax(300px, 1fr));
  }
}

@media (min-width: 1200px) {
  .grid {
    grid-template-columns: repeat(4, minmax(300px, 1fr));
  }
}
</style>
