<script setup lang="ts">
import { useExpenseStore } from '@/stores/expenses'
import ExpenseComponent from '@/components/ExpenseComponent.vue'
import { onMounted } from 'vue'
import NewExpenseComponent from '@/components/NewExpenseComponent.vue'

const store = useExpenseStore()

onMounted(async () => {
  await store.getExpenses()
})
</script>

<template>
  <main class="container">
    <div class="row">
      <NewExpenseComponent class="col m-4" />
      <div class="col">
        <h2 class="text-center">Your expenses</h2>
        <p>
          Total:
          {{
            store.expenses
              .map((x) => x.amount)
              .reduce((previousValue, currentValue) => previousValue + currentValue, 0)
          }}
          $
        </p>
        <ul>
          <li v-for="expense in store.expenses" :key="expense.id">
            <ExpenseComponent :expense="expense" />
          </li>
        </ul>
      </div>
    </div>
  </main>
</template>

<style scoped>
ul {
  list-style: none;
  padding: 0;
  margin: 0;
}
</style>
```
