<template>
  <div class="card-skeleton">
    <div class="card-skeleton_image"></div>
    <div class="card-skeleton_header">
      <div class="card-skeleton_name"></div>
      <div class="card-skeleton_favourite"></div>
    </div>
    <div class="card-skeleton_tagline"></div>
    <div class="card-skeleton_stats">
      <div class="card-skeleton_abv"></div>
      <div class="card-skeleton_ibu"></div>
    </div>
  </div>
</template>

<script setup></script>

<style lang="scss" scoped>
.card-skeleton {
  width: 300px;
  padding: 1rem;
  border-radius: 0.5rem;
  border: 1px solid #eaeaea;

  &_image {
    width: 100%;
    height: 200px;
    background-color: #eaeaea;
    border-radius: 0.25rem;
  }
  &_header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-top: 1rem;
    &_name {
      width: 70%;
      height: 20px;
      background-color: #eaeaea;
      border-radius: 4px;
    }
    &_favourite {
      width: 40px;
      height: 30px;
      background-color: #eaeaea;
      border-radius: 4px;
    }
  }
  &_tagline {
    width: 80%;
    height: 15px;
    background-color: #eaeaea;
    border-radius: 4px;
    margin-top: 1rem;
  }
  &_stats {
    display: flex;
    justify-content: space-between;
    margin-top: 0.5rem;
    div {
      width: 30%;
      height: 15px;
      background-color: #eaeaea;
      border-radius: 4px;
    }
  }
}

@keyframes shimmer {
  0% {
    background-position: -468px 0;
  }
  100% {
    background-position: 468px 0;
  }
}

.shimmer {
  animation-duration: 1.2s;
  animation-fill-mode: forwards;
  animation-iteration-count: infinite;
  animation-name: shimmer;
  animation-timing-function: linear;
  background: #f6f7f8;
  background: linear-gradient(to right, #eeeeee 8%, #dddddd 18%, #eeeeee 33%);
  background-size: 800px 104px;
  position: relative;
}

.card-skeleton {
  div {
    &.skeleton_image,
    &.skeleton_name,
    &.skeleton_favourite,
    &.skeleton_tagline,
    div {
      @extend .shimmer;
    }
  }
}
</style>
