<script setup lang="ts">
import { h, reactive } from 'vue'
import { helpers, required } from '@vuelidate/validators'
import { useExpenseTypeStore } from '@/stores/expensesTypes'
import type { IExpenseType } from '@/types/expense'
import useVuelidate from '@vuelidate/core'

const expenseTypes = useExpenseTypeStore()

const data = reactive({
  name: '',
  description: '',
  message: ''
})

const rules = {
  name: {
    required,
    notEmpty: helpers.withMessage('Name must not be empty', (value: string) => value.trim() !== ''),
    unique: helpers.withMessage(
      'Name must be unique',
      (value: string) =>
        !expenseTypes.expenseTypes.some(
          (expenseType) => expenseType.name.toLowerCase().trim() === value.toLowerCase().trim()
        )
    )
  }
}

const emit = defineEmits<{
  (e: 'expenseTypeCreated', id: number): void
}>()

const $v = useVuelidate(rules, data)
$v.value.$touch()

async function createExpenseType() {
  data.message = ''
  var result = await expenseTypes.addExpenseType(data)
  if (result) {
    emit('expenseTypeCreated', result)
  } else {
    data.message = 'Error creating expense type'
  }
}
</script>

<template>
  <div class="card shadow p-4 text-center">
    <form>
      <div class="container">
        <h3>New expense type</h3>
        <div>
          <label for="name">Name</label>
          <input id="name" type="text" v-model="data.name" placeholder="Name" />
        </div>
        <div>
          <label for="description">Description</label>
          <input
            id="description"
            type="text"
            v-model="data.description"
            placeholder="Description"
          />
        </div>
      </div>

      <div v-if="$v.$invalid" class="border-danger border">
        <p v-for="e in $v.$errors" class="alert-danger text-danger">
          {{ e.$message }}
        </p>
        <p v-if="data.message">{{ data.message }}</p>
      </div>

      <button
        :disabled="$v.$invalid"
        class="btn btn-primary"
        type="submit"
        @click.prevent="createExpenseType"
      >
        Create
      </button>
    </form>
  </div>
</template>

<style scoped>
div {
  margin: 10px;
}
</style>
