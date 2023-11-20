<template>
  <div class="modal-overlay" @click.self="closeModal">
    <div class="modal">
      <div class="beer">
        <div class="beer-image_container">
          <img
            v-if="beer.imageUrl"
            :src="beer.imageUrl"
            alt="Beer image"
            class="beer-image"
          />
          <div v-else class="beer-placeholder">No Image Available</div>
        </div>
        <div class="beer-text">
          <h3 class="beer-text_heading">{{ beer.name }}</h3>
          <p class="beer-tagline">"{{ beer.tagline }}"</p>

          <div class="beer-meta">
            <ul class="beer-food_pairing">
              <h4>Pairs well with</h4>
              <li v-for="food in beer.foodPairing" :key="food">{{ food }}</li>
            </ul>

            <div class="beer-brewers_tips">
              <h4>Brewers Tips</h4>
              <p>
                {{ beer.brewersTips }}
              </p>
            </div>
          </div>
        </div>
      </div>
      <div class="beer-description">
        <h4>Description</h4>
        <p>
          {{ beer.description }}
        </p>

        <div v-if="yeast" class="beer-yeast">
          <h4>Yeast</h4>
          <div>{{ yeast }}</div>
        </div>
      </div>
      <IngredientsList v-if="showIngredients" :ingredients="beer.ingredients" />
      <button class="close-button" @click="closeModal">Close</button>
    </div>
  </div>
</template>

<script lang="ts" setup>
import type { Beer } from "~/types/Beer";

// Props and State
const { beer } = defineProps({
  beer: {
    type: Object as PropType<Beer.Dto>,
    required: true,
  },
});

// Computed
const showIngredients = computed(() => beer.ingredients !== undefined);
const yeast = computed(() => beer.ingredients?.yeast ?? "");

// Emits
const emits = defineEmits(["close"]);

const closeModal = () => {
  emits("close");
};
</script>

<style lang="scss" scoped>
.modal {
  background: white;
  padding: 20px;
  border-radius: 10px;
  max-width: 600px;
  width: 90%;
  &-overlay {
    position: fixed;
    beer: 0;
    left: 0;
    right: 0;
    bottom: 0;
    height: 100vh;
    width: 100vw;
    z-index: 9;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
  }
}

.beer {
  display: flex;
  gap: 1rem;
  justify-content: space-between;
  margin-bottom: 1rem;

  &-image {
    &-container {
      flex-basis: 40%;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
    }
  }

  &-text {
    flex-basis: 60%;

    &_heading {
      margin: 0;
    }
  }

  h4 {
    margin: 0 0 0.25rem 0;
  }

  ul {
    list-style: none;
    padding: 0;
    margin: 0;
    margin-bottom: 0.25rem;
    font-size: 0.8em;
  }

  li {
    display: inline;
  }

  li:not(:last-of-type):after {
    content: ", ";
  }

  &-tagline {
    font-style: italic;
    font-size: 0.8em;
    color: #777;
    margin: 0.5rem 0;
  }

  &-image,
  &-placeholder {
    height: 250px;
    width: 250px;
    border-radius: 0.25rem;
  }

  &-placeholder {
    background-color: #eaeaea;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  &-meta {
    margin: 1rem 0;

    ul,
    div {
      margin: 0.5rem 0;
    }
  }
  &-brewers_tips,
  &-description {
    font-size: 0.8em;

    h4 {
      margin: 0 0 0.25rem 0;
    }
    p {
      margin: 0;
    }
  }

  &-description {
    border-left: 0.25rem solid #06579c;
    padding: 1rem;
    background-color: #f0f0f0;
  }

  &-yeast {
    margin-top: 0.5rem;
  }
}

.close-button {
  width: 100%;
  cursor: pointer;
  background: #f0ad4e;
  border: none;
  border-radius: 5px;
  color: white;
  padding: 0.75rem 1.5rem;
}

@media (max-width: 768px) {
  .modal {
    width: 95%;
    padding: 1.5rem;
  }

  .beer {
    flex-direction: column;
    align-items: center;
    gap: 0;

    &-text {
      flex-basis: 100%;
    }

    &-image,
    &-placeholder {
      display: none;
    }

    &-brewers-tips,
    &-food-pairing {
      padding: 0.5rem;
    }
  }

  .close-button {
    padding: 0.5rem 1rem;
  }
}

@media (max-width: 500px) {
  .modal {
    width: 100vw;
  }
}
</style>
