<template>
  <div class="login-container">
    <div class="login-content">
      <header class="login-header">
        <IconShield class="logo" alt="Brewdog shield" />
        <h1>BeerBase</h1>
        <p>Powered by Punk API</p>
      </header>
      <LoginForm />
    </div>
    <div class="login-illustration">
      <div class="stacked-images">
        <img
          class="stacked-image stacked-image-1"
          src="/punkipa.png"
          alt="a can of punk ipa"
        />
        <img
          class="stacked-image stacked-image-2"
          src="/punkipa.png"
          alt="a can of punk ipa"
        />
        <img
          class="stacked-image stacked-image-3"
          src="/punkipa.png"
          alt="a can of punk ipa"
        />
      </div>
      <h2 class="login-illustration_heading">Simple beer sourcing</h2>
      <p class="login-illustration_description">
        From hoppy IPAs to smooth stouts, our app puts you in control. Save your
        favorites, get detailed information on your picks, and even pair them
        with the perfect meal.
      </p>
    </div>
  </div>
</template>

<script lang="ts" setup></script>
<style lang="scss" scoped>
.login {
  &-container {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    align-items: flex-start;
    background: #06579c;
  }

  &-content,
  &-illustration {
    flex: 1;
    min-width: 300px;
    padding: 0 60px 10% 60px;
    height: 100vh;
    display: flex;
    flex-direction: column;
    justify-content: center;
  }

  &-illustration {
    background: linear-gradient(0, #06579c, 70%, #56acd8);
    color: white;

    &_heading {
      text-align: center;
    }
    &_description {
      margin: 0;
      line-height: 1.8;
      text-align: center;
    }
  }

  &-content {
    border-radius: 0 5px 5px 0;
    background: white;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1), 0 0 20px rgba(0, 0, 0, 0.1);
  }

  &-header {
    text-align: center;
    margin-bottom: 20px;
  }
}

.logo {
  height: 60px;
  width: 60px;
}

.heading {
  position: relative;
  margin-bottom: 20px;
}

.stacked-images {
  position: relative;
  min-height: 300px;
}

.stacked-image {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  max-width: 300px;
  &-1 {
    left: 33%;
    transform: translateX(-50%);
  }

  &-2 {
    left: 67%;
    transform: translateX(-50%);
  }

  &-3 {
    left: 50%;
    transform: translateX(-50%) scale(1.2);
  }
}

@media (max-width: 768px) {
  .login-container {
    flex-direction: column;
    align-items: center;
    height: 100vh;
  }

  .login-content {
    width: 100%;
  }

  .login-illustration {
    display: none;
  }
}
</style>
