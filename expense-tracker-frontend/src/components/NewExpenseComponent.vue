<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue'
import type { IExpense, IExpenseType } from '@/types/expense'
import { useExpenseTypeStore } from '@/stores/expensesTypes'
import { useExpenseStore } from '@/stores/expenses'
import useVuelidate from '@vuelidate/core'
import { helpers, minLength, minValue, required } from '@vuelidate/validators'
import NewExpenseTypeComponent from '@/components/NewExpenseTypeComponent.vue'

const expenseTypes = useExpenseTypeStore()
const expenses = useExpenseStore()

onMounted(async () => {
  await expenseTypes.getExpenseTypes()
})

interface INewExpense {
  description: string
  amount: number
  expenseType: IExpenseType | null | 'new'
}

const data = reactive<INewExpense>({
  description: '',
  amount: 0,
  expenseType: null
})

const rules = {
  description: {
    required,
    minLength: minLength(3)
  },
  amount: {
    minValue: helpers.withMessage(
      'Amount must be non zero and positive',
      (value: number) => value > 0
    )
  },
  expenseType: {
    anObject: helpers.withMessage(
      'Expense type must be selected',
      (value: number | null | 'new') => value !== null && value !== 'new'
    )
  }
}

const $v = useVuelidate(rules, data)
$v.value.$touch()
function addExpense() {
  expenses.addExpense({
    description: data.description,
    amount: data.amount,
    expenseTypeId: (data.expenseType as IExpenseType)!.id
  })
}

function expenseTypeCreated(id: number) {
  console.log('created expense type', id)
  data.expenseType = expenseTypes.expenseTypes.find((x) => x.id === id)
}
</script>

<template>
  <form class="ui form">
    <div class="container justify-content-center card p-4 shadow">
      <div class="m-4 row">
        <label for="description" class="col">Description</label>
        <input
          id="description"
          class="col"
          type="text"
          v-model="data.description"
          placeholder="Description"
        />
        <div class="row" v-if="$v.description.$dirty && $v.description.$invalid">
          <p class="text-danger">{{ $v.description.$errors[0].$message }}</p>
        </div>
      </div>
      <div class="m-4 row">
        <label class="col" for="amount">Amount</label>
        <input
          class="col"
          id="amount"
          type="number"
          v-model.number="data.amount"
          placeholder="Amount"
        />
        <div v-if="$v.amount.$dirty && $v.amount.$invalid">
          <p class="text-danger">{{ $v.amount.$errors[0].$message }}</p>
        </div>
      </div>

      <div class="m-4 row">
        <label class="col" for="expenseType">Expense Type</label>
        <select class="col" id="expenseType" v-model="data.expenseType">
          <option
            v-for="expenseType in expenseTypes.expenseTypes"
            :key="expenseType.id"
            :value="expenseType"
          >
            {{ expenseType.name }}
          </option>
          <option :key="-1" value="new">New</option>
        </select>

        <div class="row" v-if="data.expenseType === 'new'"></div>
        <div clas="row" v-if="$v.expenseType.$invalid && $v.expenseType.$dirty">
          <p class="text-danger">{{ $v.expenseType.$errors[0].$message }}</p>
        </div>

        <NewExpenseTypeComponent
          v-if="data.expenseType === 'new'"
          v-on:expenseTypeCreated="expenseTypeCreated"
        />
      </div>
      <button
        :disabled="$v.$invalid"
        class="btn btn-info m-4 row"
        type="submit"
        @click.prevent="addExpense"
      >
        Add
      </button>
    </div>
  </form>
</template>

<style scoped></style>
