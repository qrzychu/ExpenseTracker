﻿<script setup lang="ts">
import { ref } from 'vue'
import { useLoginStore } from '@/stores/loginStore'
import { useRouter } from 'vue-router'

const data = ref({
  username: '',
  password: ''
})

const loginStore = useLoginStore()
const router = useRouter()

async function login() {
  await loginStore.login(data.value.username, data.value.password)
  await router.push('/')
}
</script>

<template>
  <div class="container">
    <form v-on:keyup.enter="login">
      <p>Login</p>
      <input type="text" v-model="data.username" placeholder="Username" />

      <input type="password" v-model="data.password" placeholder="Password" />

      <button type="submit" @click.prevent="login">Login</button>
    </form>
  </div>
</template>

<style scoped></style>
