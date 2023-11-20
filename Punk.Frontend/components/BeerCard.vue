<template>
  <div class="beer-card" @click="showModal = true">
    <img
      v-if="beer.imageUrl"
      :src="beer.imageUrl"
      alt="Beer image"
      class="beer-image"
    />
    <div v-else class="beer-placeholder">No Image Available</div>
    <div class="beer-bottom">
      <div class="beer-card-header">
        <h3 class="beer-name">{{ beer.name }}</h3>
        <button
          class="beer-favourite"
          @click.stop="toggleIsFavourite(!isFavourite)"
        >
          <IconHeartFilled
            v-if="isFavourite"
            alt="Favourite"
            class="beer-favourite_icon"
          />
          <IconHeartLined
            v-else
            alt="UnFavourite"
            class="beer-favourite_icon"
          />
        </button>
      </div>
      <p class="beer-tagline">{{ beer.tagline }}</p>
      <div class="beer-stats">
        <span class="beer-abv">ABV: {{ beer.abv }}%</span>
        <span class="beer-ibu">IBU: {{ beer.ibu }}</span>
      </div>
    </div>
  </div>
  <LazyBeerModal v-if="showModal" :beer="beer" @close="showModal = false" />
</template>

<script lang="ts" setup>
import { useUserStore } from "~/store";
import type { Beer } from "~/types/Beer";
import { useToast } from "@/composables/useToast";
// Composables
const { fetchApi } = useApi();
const userStore = useUserStore();
const { showToast } = useToast();

// Props
const { beer } = defineProps({
  beer: {
    type: Object as PropType<Beer.Dto>,
    required: true,
  },
});

// State
const isFavourite = ref(false);
const isLoading = ref(false);
const showModal = ref(false);

// Methods
async function toggleIsFavourite(isFav: boolean) {
  if (isLoading.value) return;
  setIsFavourite(isFav);
  await tellApi(isFav);
  tellStore(isFav);
}

function setIsFavourite(isFav: boolean) {
  isFavourite.value = isFav;
}

function tellStore(isFav: boolean) {
  if (isFav) {
    userStore.addUserFavouriteBeer(beer as Beer.Dto);
  } else {
    userStore.removeUserFavouriteBeer(beer.id);
  }
}

async function tellApi(isFav: boolean) {
  try {
    isLoading.value = true;
    await fetchApi(`/beer/favourite`, {
      method: "PUT",
      body: {
        beerId: beer.id,
        isFavourite: isFav,
        userId: userStore.getUserId(),
      },
    });
  } catch (err: any) {
    showToast("error", err?.data?.message ?? "Something went wrong", 4000);
  } finally {
    isLoading.value = false;
  }
}

// Lifecycle Hooks
onMounted(() => {
  setIsFavourite(
    userStore.userFavouriteBeers.findIndex((b) => b.id === beer.id) > -1
  );
});
</script>

<style lang="scss" scoped>
.beer {
  &-card {
    border: 1px solid #eaeaea;
    padding: 1rem;
    border-radius: 0.5rem;
    width: 300px;
    height: 370px;
    cursor: pointer;
    &-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }

    &:hover {
      .beer-card-header {
        text-decoration: underline;
      }
    }
  }

  &-bottom {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 40%;
  }

  &-image {
    width: 100%;
    height: auto;
    border-radius: 0.25rem;
  }

  &-placeholder {
    width: 100%;
    height: 200px;
    background-color: #eaeaea;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 0.25rem;
  }

  &-name {
    font-size: 1.25rem;
    margin: 1rem 0 0.5rem 0;
    white-space: nowrap;
    text-overflow: ellipsis;
    width: 80%;
    overflow: hidden;
  }

  &-tagline {
    color: #777;
    margin: 0.5rem 0;
    font-size: 0.8em;
  }

  &-stats {
    display: flex;
    justify-content: space-between;
    margin-top: 0.5rem;
  }

  &-abv,
  &-ibu {
    font-weight: bold;
    color: #333;
  }

  &-description {
    margin-top: 0.5rem;
  }

  &-food-pairing {
    list-style: none;
    padding: 0;
    margin-top: 0.5rem;
  }

  &-brewers-tips {
    margin-top: 0.5rem;
    font-size: 0.9rem;
    background-color: #f0f0f0;
    padding: 0.5rem;
    border-left: 4px solid #fa0;
  }

  &-favourite {
    height: 30px;
    width: 40px;
    border: none;
    background: transparent;
    cursor: pointer;

    &_icon {
      fill: #f0ad4e;
    }
  }
}
</style>
