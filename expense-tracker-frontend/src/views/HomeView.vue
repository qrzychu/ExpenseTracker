<script setup lang="ts">
import { onMounted } from 'vue'
import { useExpenseStore } from '@/stores/expenses'

const store = useExpenseStore()

function something() {
  store.expenses.push({
    id: 1,
    amount: 100,
    description: 'test',
    addedAt: '2021-10-10'
  })
}
</script>

<template>
  <main>
    <div>
      <ul>
        <li v-for="expense in store.expenses" :key="expense.id">
          {{ expense.id }} - {{ expense.amount }} - {{ expense.description }} -
          {{ expense.addedAt }}
        </li>
      </ul>
    </div>
    <button @click="something">Add</button>
    <button @click="store.setExpenses()">Fetch Expenses</button>
  </main>
</template>
