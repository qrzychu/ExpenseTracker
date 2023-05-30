export interface IExpense {
  id?: number
  description: string
  amount: number
  createdAt?: string
  modifiedAt?: string
  expenseType: IExpenseType
}

export interface IExpenseType {
  id: number
  name: string
  description: string
  isArchived: boolean
  isStandard: boolean
}
