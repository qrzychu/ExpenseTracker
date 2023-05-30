import { defineStore } from 'pinia'
import ApiClient from '@/api/apiClient'
import { ref } from 'vue'
import { useExpenseStore } from '@/stores/expenses'

const api = ApiClient

export const useLoginStore = defineStore('login', () => {
  const username = ref(null as string | null)

  async function login(user: string, password: string) {
    const response = await api.Login(null, { username: user, password })
    if (response.status == 200) {
      username.value = user
    }
  }

  async function logout() {
    if (username) {
      await api.Logout()
      username.value = null
      const expenses = useExpenseStore()
      expenses.reset()
    }
  }

  async function register(user: string, password: string) {
    const response = await api.Register(null, { username: user, password })
    if (response.status == 200) {
      username.value = user
    }
  }

  async function checkLogin() {
    try {
      const response = await api.GetMe()
      if (response.status === 200) {
        username.value = response.data.username!
      }
    } catch (e) {
      username.value = null
    }
  }

  return { login, logout, register, username, checkLogin }
})
