import { ref, type Ref } from 'vue'
import { defineStore } from 'pinia'
import OpenAPIClientAxios from 'openapi-client-axios'
import type { Client } from '@/api/client'
import type { IExpense } from '@/types/expense'

console.log(import.meta.env)

const baseURL = import.meta.env.VITE_APP_BASE_URL
console.log('baseURL: ' + baseURL)

const axiosInstance = await new OpenAPIClientAxios({
  definition: baseURL + 'swagger/v1/swagger.json'
})

await axiosInstance.init()
const api: Client = await axiosInstance.getClient<Client>()
api.defaults.baseURL = baseURL

export const useExpenseStore = defineStore('expense', () => {
  const expenses: Ref<IExpense[]> = ref([])

  async function getExpenses() {
    expenses.value = (await api.GetExpenses()).data.map((e) => {
      return {
        id: e.id!,
        description: e.description!,
        amount: e.amount!,
        createdAt: e.createdAt!,
        modifiedAt: e.modifiedAt!
      }
    })
  }

  async function addExpense(expense: IExpense) {
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

  return { expenses, getExpenses, addExpense, updateExpense, deleteExpense }
})
