<template>
  <div class="ingredients">
    <div v-if="malts.length" class="ingredients-malts">
      <IconMalt class="ingredients-malts_image" alt="Malt icon" />
      <ul class="ingredients-malts_list">
        <li v-for="malt in malts" :key="malt.name">
          {{ malt.name }}, {{ parseAmount(malt.amount) }}
        </li>
      </ul>
    </div>

    <div v-if="hops.length" class="ingredients-hops">
      <IconHop class="ingredients-hops_image" alt="Hop icon" />
      <ul class="ingredients-hops_list">
        <li v-for="hop in hops" :key="hop.name">
          <span class="ingredients-hops_name">{{ hop.name }}</span>
          <span class="ingredients-hops_amount"
            >{{ hop.amount.value }} {{ parseUnit(hop.amount.unit) }}</span
          >
          <span class="ingredients-hops_attribute">
            {{ parseAttribute(hop.attribute) }}
          </span>
        </li>
      </ul>
    </div>
  </div>
</template>

<script lang="ts" setup>
import type { Beer } from "~/types/Beer";

// Props
const { ingredients } = defineProps({
  ingredients: {
    type: Object as PropType<Beer.Ingredients>,
    required: true,
  },
});

// Computed
const malts = computed(() => ingredients?.malt ?? []);
const hops = computed(() => ingredients?.hops ?? []);

// Methods
const parseUnit = (unit: string): string => {
  switch (unit) {
    case "kilograms":
      return "kg";
    case "grams":
      return "g";
    case "litres":
      return "l";
    case "millilitres":
      return "ml";
    default:
      return unit;
  }
};

const parseAttribute = (attribute?: string) =>
  attribute ? `(${attribute})` : "";

const parseAmount = (amount: Beer.Volume): string =>
  `${amount.value} ${parseUnit(amount.unit)}`;
</script>

<style lang="scss" scoped>
.ingredients {
  font-size: 1em;
  display: flex;
  gap: 1rem;
  flex-direction: column;
  margin: 1rem 0;

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

  &-malts {
    border-left: 0.25rem solid #f0ad4e;
  }

  &-hops {
    border-left: 0.25rem solid #06579c;
  }

  &-malts,
  &-hops {
    flex-basis: 50%;
    padding: 1rem 0;
    display: flex;
    align-items: center;
    background-color: #f0f0f0;

    &_name,
    &_amount {
      margin-right: 0.25rem;
    }

    ul,
    p {
      max-width: 80%;
    }

    &_image {
      height: 50px;
      width: 50px;
    }
  }
}
</style>
