import { ref, type Ref } from 'vue'
import { defineStore } from 'pinia'
import OpenAPIClientAxios from 'openapi-client-axios'
import type { Client } from '@/api/client'
import type { IExpense } from '@/types/expense'

// import { openapi } from '/src/api/swagger.json' assert { type: 'json' }

const baseURL = import.meta.env.VITE_APP_BASE_URL

const axiosInstance = new OpenAPIClientAxios({
  definition: baseURL + 'swagger/v1/swagger.json',
  axiosConfigDefaults: {
    withCredentials: true
  }
})

await axiosInstance.init()
const api: Client = await axiosInstance.getClient<Client>()

console.log(api.defaults)

api.defaults.baseURL = baseURL
api.defaults.withCredentials = true

export const useLoginStore = defineStore('login', () => {
  async function login(username: string, password: string) {
    await api.Login(null, { username, password })
  }

  async function logout() {
    await api.Logout()
  }

  async function register(username: string, password: string) {
    await api.Register(null, { username, password })
  }

  return { login, logout, register }
})

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
