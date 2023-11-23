<template>
  <section class="content-container">
    <BeerSectionHeader
      sectionName="Favourite beers"
      seeMoreLink="/beers/favourite"
      :showSeeMore="showSeeMore"
    />

    <BeerList
      :beers="userFavouriteBeers.slice(0, resultsToShow)"
      :loading="loading"
      :maxResults="maxResults"
      emptyMessage="No favourite beers found"
      @paginate="handlePagination"
    />
  </section>
</template>

<script lang="ts" setup>
import { useUserStore } from "~/store";
import { computed, ref, onMounted } from "vue";
import { useRoute } from "vue-router";
import { useToast } from "~/composables/useToast";

// Composables
const { showToast } = useToast();

// Props
const props = defineProps({
  maxResults: { type: Number, default: 4 },
  showResults: { type: Number, default: -1, required: false },
});

//State
const currentPage = ref(1);
const loading = ref(true);
const userStore = useUserStore();
const { userFavouriteBeers } = storeToRefs(userStore);
const route = useRoute();

const resultsToShow = computed(() =>
  props.showResults && props.showResults > 0
    ? props.showResults
    : props.maxResults
);

// Computed Properties
const showSeeMore = computed(
  () => route.path === "/" && userFavouriteBeers.value.length > 0
);

// Methods
const handlePagination = async (page: number) => {
  currentPage.value = page;
  loading.value = true;
  await fetchBeers();
  loading.value = false;
};

const fetchBeers = async () => {
  try {
    await userStore.fetchUserFavouriteBeers(
      currentPage.value,
      resultsToShow.value
    );
  } catch (e) {
    showToast("error", err?.data?.message ?? "Something went wrong", 4000);
  } finally {
    loading.value = false;
  }
};

// Lifecycle Hooks

onMounted(fetchBeers);
</script>

<style lang="scss" scoped></style>
