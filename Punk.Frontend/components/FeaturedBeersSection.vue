<template>
  <section class="content-container">
    <BeerSectionHeader
      sectionName="Featured beers"
      seeMoreLink="/beers/featured"
      :showSeeMore="showSeeMore"
    />

    <BeerList
      :beers="beers"
      :loading="loading"
      :maxResults="maxResults"
      emptyMessage="No featured beers found"
      @paginate="handlePagination"
    />
  </section>
</template>

<script lang="ts" setup>
import { useApi } from "@/composables/useApi";
import type { ApiResponse } from "~/types/ApiResponse";
import type { Beer } from "~/types/Beer";
import { useRoute } from "vue-router";
import { useToast } from "~/composables/useToast";

// Composables
const { showToast } = useToast();

// Props
const { maxResults } = defineProps({
  maxResults: { type: Number, default: 3 },
});

// State
const currentPage = ref(1);
const beers: Ref<Beer.Dto[]> = ref([]);
const loading = ref(true);
const route = useRoute();

// Computed Properties
const showSeeMore = computed(
  () => beers.value.length > 0 && route.path === "/"
);

// Methods
const fetchBeers = async () => {
  const { fetchApi } = useApi();
  loading.value = true;

  try {
    const response: ApiResponse<Beer.Dto[]> = await fetchApi(
      `/beer?page=${currentPage.value}&pageSize=${maxResults}&ids=null`
    );
    if (response.statusCode === 200) {
      beers.value = response.data;
    }
  } catch (err) {
    showToast("error", err?.data?.message ?? "Something went wrong", 4000);
  } finally {
    loading.value = false;
  }
};

const handlePagination = async (page: number) => {
  currentPage.value = page;
  await fetchBeers();
};

onMounted(fetchBeers);
</script>

<style lang="scss" scoped></style>
