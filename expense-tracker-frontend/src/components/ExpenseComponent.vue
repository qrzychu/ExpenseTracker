<script setup lang="ts">
import type { IExpense } from '@/types/expense'

export interface IExpenseComponentProps {
  expense: IExpense
}

const props = defineProps<IExpenseComponentProps>()
</script>

<template>
  <div>
    <p>{{ expense.id }}</p>
    <p>{{ expense.amount }}</p>
    <p>{{ expense.description }}</p>
    <p>{{ expense.createdAt }}</p>
  </div>
</template>

<style scoped></style>
