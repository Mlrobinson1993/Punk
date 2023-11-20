<template>
  <div class="heading-container">
    <h2>
      {{ headingStart }} <span> {{ headingRest }} </span>
    </h2>
    <nuxt-link v-if="showSeeMore" :to="seeMoreLink"> See more </nuxt-link>
  </div>
</template>

<script lang="ts" setup>
// Props
const { showSeeMore, sectionName, seeMoreLink } = defineProps({
  sectionName: { type: String, default: "" },
  seeMoreLink: { type: String, default: "" },
  showSeeMore: { type: Boolean, default: false },
});

// Computed
const headingStart = computed(() => sectionName?.split(" ")[0].toLowerCase());
const headingRest = computed(() => sectionName?.split(" ").slice(1).join(" "));
</script>

<style lang="scss" scoped>
.heading-container {
  margin-bottom: 1rem;

  h2 {
    display: inline-block;
    text-transform: capitalize;

    & > span {
      font-weight: 300;
    }
  }
  & > a {
    margin: 0 0 0 1rem;
    text-decoration: none;
    font-size: 0.8rem;
    color: #06579c;

    &:hover {
      text-decoration: underline;
    }
  }
}
</style>
