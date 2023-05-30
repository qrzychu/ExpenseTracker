<script setup lang="ts">
import { useLoginStore } from '@/stores/loginStore'
import { onMounted } from 'vue'

const loginStore = useLoginStore()
onMounted(async () => {
  await loginStore.logout()
})
</script>

<template>
  <p>You have been logged out!</p>
</template>

<style scoped></style>
