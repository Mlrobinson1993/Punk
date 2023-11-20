<template>
  <form class="login-form" @submit.prevent="handleSubmit">
    <Input
      v-if="state.signUpActive"
      :modelValue="state.loginData.name"
      @updated="updateModelValue('name', $event)"
      type="text"
      placeholder="Name"
      icon="name"
    />
    <Input
      :modelValue="state.loginData.email"
      @updated="updateModelValue('email', $event)"
      type="text"
      placeholder="Email"
      icon="username"
    />
    <Input
      :modelValue="state.loginData.password"
      @updated="updateModelValue('password', $event)"
      type="password"
      placeholder="Password"
      icon="password"
    />

    <LoginButton
      :isLoading="state.isLoading"
      :isSignUpActive="state.signUpActive"
    />
    <ToggleFormLink
      :isSignUpActive="state.signUpActive"
      @toggle="handleFormToggle"
    />
    <ErrorMessage v-if="state.error" :message="state.error" />
  </form>
</template>

<script setup lang="ts">
import { useLoginForm } from "@/composables/useLoginForm";
import type { AuthState } from "~/types/AuthState";

// State

const state: AuthState = reactive({
  signUpActive: true,
  loginData: { name: "", email: "", password: "" },
  error: "",
  isLoading: false,
});

// Composables

const { handleSubmit, handleFormToggle } = useLoginForm(state);

// Methods

function updateModelValue(key: keyof AuthState["loginData"], value: string) {
  state.loginData[key] = value;
}
</script>

<style lang="scss" scoped></style>
