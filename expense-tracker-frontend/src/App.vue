<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import { useLoginStore } from '@/stores/loginStore'
import { onBeforeMount } from 'vue'
import router from '@/router'

onBeforeMount(async () => {
  await loginStore.checkLogin()
  if (!loginStore.username) {
    await router.push('login')
  }
})

const loginStore = useLoginStore()
</script>

<template>
  <header class="bg-primary">
    <div>
      <nav>
        <RouterLink class="btn m-4" v-if="loginStore.username" to="/">Home</RouterLink>
        <RouterLink class="btn m-4" v-if="!loginStore.username" to="/login">Log in</RouterLink>
        <RouterLink class="btn m-4" v-if="!loginStore.username" to="/register">Register</RouterLink>
        <RouterLink class="btn m-4" v-if="loginStore.username" to="/logout"
          >Log out {{ loginStore.username }}</RouterLink
        >
      </nav>
    </div>
  </header>

  <div class="bg-bod">
    <RouterView />
  </div>
</template>

<style scoped>
header {
  line-height: 1.5;
  max-height: 100vh;
}

nav {
  width: 100%;
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}
</style>
