<template>
  <Grid v-if="loading">
    <div class="beers_grid-item" v-for="i in maxResults" :key="i">
      <BeerCardSkeleton />
    </div>
  </Grid>

  <Grid v-else-if="beers.length && !loading">
    <div
      class="beers-grid_item"
      v-for="beer in beers.slice(0, maxResults)"
      :key="beer.id"
    >
      <BeerCard :beer="beer" />
    </div>
  </Grid>

  <div v-else class="beers-empty">
    <p>{{ emptyMessage }}</p>
  </div>

  <LazyPaginationControls
    v-if="showPagination"
    :disableNext="beers.length < maxResults"
    @paginate="handlePagination"
  />
</template>

<script lang="ts" setup>
import type { Beer } from "~/types/Beer";
const route = useRoute();
const currentPage = ref(1);
// Props
const props = defineProps({
  beers: { type: Array as PropType<Beer.Dto[]>, default: () => [] },
  loading: { type: Boolean, default: true },
  maxResults: { type: Number, default: 4 },
  emptyMessage: { type: String, default: "No beers found" },
});

// Computed
const beers = computed(() => props.beers);

const showPagination = computed(
  () =>
    route.name !== "index" && (props.beers.length > 0 || currentPage.value > 1)
);

// Emits
const emit = defineEmits(["paginate"]);

const handlePagination = (page: number) => {
  currentPage.value = page;
  emit("paginate", page);
};
</script>

<style lang="scss" scoped>
.beers-grid_item {
  margin: auto;
}

.beers-empty {
  margin: auto;
  display: block;
  height: 100%;
  position: relative;
}
</style>
