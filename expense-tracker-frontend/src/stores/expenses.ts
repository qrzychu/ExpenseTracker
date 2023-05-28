import { ref, computed, type Ref } from 'vue'
import { defineStore } from 'pinia'
import OpenAPIClientAxios from 'openapi-client-axios'
import type { Client } from '@/api/client'
import { type Components } from '@/api/client'

// @ts-ignore
import Expense = Components.Schemas.Expense

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
  const expenses: Ref<Expense[]> = ref([])

  async function setExpenses() {
    console.log('setExpenses')
    let a = await api.GetExpenses()
    console.log(a)
    expenses.value = a.data
  }

  return { expenses, setExpenses }
})
