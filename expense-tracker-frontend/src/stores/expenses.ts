import { ref, type Ref } from 'vue'
import { defineStore } from 'pinia'
import type { IExpense, IExpenseType } from '@/types/expense'
import ApiClient from '@/api/apiClient'

const api = ApiClient

export const useExpenseStore = defineStore('expense', () => {
  const expenses: Ref<IExpense[]> = ref([])

  async function getExpenses() {
    expenses.value = (await api.GetExpenses()).data.map((e) => {
      return {
        id: e.id!,
        description: e.description!,
        amount: e.amount!,
        createdAt: e.createdAt!,
        modifiedAt: e.modifiedAt!,
        expenseType: e.expenseType! as IExpenseType
      }
    })
  }

  async function addExpense(expense: {
    description: string
    amount: number
    expenseTypeId: number
  }) {
    await api.AddExpense(null, expense)
    await getExpenses()
  }

  async function updateExpense(expense: IExpense) {
    await api.UpdateExpense(null, expense)
    await getExpenses()
  }

  async function deleteExpense(expense: IExpense) {
    await api.DeleteExpense({ id: expense.id! })
    await getExpenses()
  }

  function reset() {
    expenses.value = []
  }

  return { expenses, getExpenses, addExpense, updateExpense, deleteExpense, reset }
})
