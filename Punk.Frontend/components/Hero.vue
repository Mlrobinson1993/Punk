<template>
  <header class="hero">
    <span class="hero-image" aria-alt="Illustration of a brewery" />
    <div class="hero-content content-container">
      <div class="hero-content_inner">
        <div class="hero-content_text">
          <h1>Discover</h1>
          <h2>some amazing beers</h2>
          <p>Find great beers, and foot pairings to match.</p>
        </div>
        <div class="hero-content_form">
          <SearchBar />
        </div>
      </div>
    </div>
  </header>
</template>

<script setup></script>

<style lang="scss" scoped>
.hero {
  position: relative;
  min-height: 50vh;
  &-image {
    z-index: 1;
    width: 100%;
    height: 100%;
    background-image: url("/brewery.webp");
    background-size: cover;
    position: absolute;
    display: inline-block;
    &::after {
      content: "";
      height: 100%;
      width: 100%;
      display: inline-block;
      background: rgb(255, 255, 255);
      background: -moz-linear-gradient(
        360deg,
        rgba(255, 255, 255, 1) 0%,
        rgba(0, 0, 50, 0.2) 100%
      );
      background: -webkit-linear-gradient(
        360deg,
        rgba(255, 255, 255, 1) 0%,
        rgba(0, 0, 50, 0.2) 100%
      );
      background: linear-gradient(
        360deg,
        rgba(255, 255, 255, 1) 0%,
        rgba(0, 0, 50, 0.2) 100%
      );
      filter: progid:DXImageTransform.Microsoft.gradient(startColorstr="#ffffff",endColorstr="#000000",GradientType=1);
    }
  }

  &-content {
    position: absolute;
    z-index: 2;
    display: flex;
    left: 50%;
    top: 50%;
    transform: translate(-50%, -50%);
    justify-content: center;
    align-items: center;
    flex-direction: column;
    height: 100%;
    margin: auto;

    @media (max-width: 900px) {
      width: 80%;
    }

    @media (max-width: 768px) {
      padding: 0 1rem;
    }

    @media (max-width: 480px) {
      padding: 0 0.5rem;
      width: 95%;
    }

    &_inner {
      border-radius: 5px;
      background: white;
      padding: 2rem 3rem;
      box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16), 0 3px 6px rgba(0, 0, 0, 0.23);
      min-width: 700px;

      @media (max-width: 800px) {
        min-width: 0;
        padding: 1.5rem 2rem;
      }

      @media (max-width: 480px) {
        padding: 1rem;
        box-shadow: none;
      }
    }

    h1 {
      font-weight: 700;
      margin: 0;
      font-size: 2em;
    }

    h1,
    h2 {
      font-size: 2.5em;
      line-height: 1.2;

      @media (max-width: 768px) {
        font-size: 1.75em;
      }

      @media (max-width: 480px) {
        font-size: 1.5em;
      }
    }

    h2,
    p {
      font-weight: 300;
    }

    h2 {
      margin-top: 0;
      margin-bottom: 1.5rem;
    }

    p {
      margin-bottom: 1.5rem;
      @media (max-width: 768px) {
        font-size: 0.9em;
      }

      @media (max-width: 480px) {
        font-size: 0.8em;
      }
    }
  }
}
</style>
