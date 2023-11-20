<template>
  <section class="content-container">
    <BeerSectionHeader
      sectionName="Favourite beers"
      seeMoreLink="/beers/favourite"
      :showSeeMore="showSeeMore"
    />

    <BeerList
      :beers="userFavouriteBeers"
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
const { maxResults } = defineProps({
  maxResults: { type: Number, default: 4 },
});

//State
const currentPage = ref(1);
const loading = ref(true);
const userStore = useUserStore();
const beers = ref([]);
const { userFavouriteBeers } = storeToRefs(userStore);
const route = useRoute();

// Computed Properties
const showSeeMore = computed(
  () => route.path === "/" && beers.value.length > 0
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
    await userStore.fetchUserFavouriteBeers(currentPage.value, maxResults);
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
