<script setup lang="ts">
import { useExpenseStore } from '@/stores/expenses'
import ExpenseComponent from '@/components/ExpenseComponent.vue'

const store = useExpenseStore()

async function something() {
  await store.addExpense({
    amount: 100,
    description: 'test'
  })
}
</script>

<template>
  <main>
    <div>
      <ul>
        <li v-for="expense in store.expenses" :key="expense.id">
          <ExpenseComponent :expense="expense" />
        </li>
      </ul>
    </div>
    <button class="btn btn-primary" @click="something">Add</button>
    <button @click="store.getExpenses()">Fetch Expenses</button>
  </main>
</template>
