<script setup lang="ts">
import type { IExpense } from '@/types/expense'
import moment from 'moment'

export interface IExpenseComponentProps {
  expense: IExpense
}

const props = defineProps<IExpenseComponentProps>()
</script>

<template>
  <div class="card shadow m-4 grow">
    <span class="card-header">Description: {{ expense.description }}</span>
    <div class="container p-4">
      <span class="d-block">Amount: {{ expense.amount }} $</span>
      <span class="small d-bloc"
        >Created at {{ moment(expense.createdAt).format('YYYY-MM-DD HH:mm') }}</span
      >
    </div>
  </div>
</template>

<style scoped>
div {
  transition: all 0.2s;
}

div.grow:hover {
  transform: scale(1.3);
}
</style>
