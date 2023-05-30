<script setup lang="ts">
import { useExpenseStore } from '@/stores/expenses'
import ExpenseComponent from '@/components/ExpenseComponent.vue'
import { onBeforeMount, onMounted, Ref, ref } from 'vue'
import { useLoginStore } from '@/stores/loginStore'
import { useRouter } from 'vue-router'
import type { IExpenseType } from '@/types/expense'
import NewExpenseComponent from '@/components/NewExpenseComponent.vue'

const store = useExpenseStore()
const loginStore = useLoginStore()
const router = useRouter()

interface IData {
  addingExpenseType: boolean
  newExpenseType: IExpenseType | null
}

const data: ref<IData> = ref({
  addingExpenseType: false,
  newExpenseType: null
})

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
