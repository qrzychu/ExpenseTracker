import ApiClient from '@/api/apiClient'
import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { IExpenseType } from '@/types/expense'

const api = ApiClient

export const useExpenseTypeStore = defineStore('expenseType', () => {
  const expenseTypes = ref([] as IExpenseType[])

  async function getExpenseTypes() {
    const response = await api.GetExpenseTypes()
    if ((response.status = 200)) {
      expenseTypes.value = response.data.map((e) => {
        return {
          id: e.id!,
          name: e.name!,
          description: e.description!,
          isArchived: e.isArchived!,
          isStandard: e.isStandard!
        }
      })
    }
  }

  async function addExpenseType(expenseType: { name: string; description: string }) {
    const response = await api.AddExpenseType(null, expenseType)

    if (response.status == 201) {
      await getExpenseTypes()
      return response.data as number
    }

    return null
  }

  async function deleteExpenseType(expenseType: IExpenseType) {
    await api.DeleteExpenseType({ id: expenseType.id! })
    await getExpenseTypes()
  }

  function reset() {
    expenseTypes.value = []
  }

  return { expenseTypes, getExpenseTypes, addExpenseType, deleteExpenseType, reset }
})
