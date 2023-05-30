import type {
  OpenAPIClient,
  Parameters,
  UnknownParamsObject,
  OperationResponse,
  AxiosRequestConfig,
} from 'openapi-client-axios'; 

declare namespace Components {
    namespace Schemas {
        export interface AddExpense {
            description?: string | null;
            amount?: number; // double
            expenseTypeId?: number; // int32
        }
        export interface AddExpenseType {
            name?: string | null;
            description?: string | null;
        }
        export interface ExpenseDto {
            id?: number; // int32
            description?: string | null;
            amount?: number; // double
            expenseTypeId?: number; // int32
            expenseType?: ExpenseType;
            createdAt?: string; // date-time
            modifiedAt?: string; // date-time
            ownerId?: string; // uuid
        }
        export interface ExpenseType {
            id?: number; // int32
            name?: string | null;
            description?: string | null;
            createdAt?: string; // date-time
            creatorId?: string | null; // uuid
            creator?: User;
            isArchived?: boolean;
            normalizedName?: string | null;
        }
        export interface ExpenseTypeDto {
            id?: number; // int32
            name?: string | null;
            description?: string | null;
            createdAt?: string; // date-time
            creatorId?: string | null; // uuid
            isStandard?: boolean;
            isArchived?: boolean;
        }
        export interface LoginRequest {
            username?: string | null;
            password?: string | null;
        }
        export interface RegisterRequest {
            username?: string | null;
            password?: string | null;
        }
        export interface UpdateExpense {
            id?: number; // int32
            description?: string | null;
            amount?: number; // double
            expenseTypeId?: number; // int32
        }
        export interface User {
            id?: string; // uuid
            userName?: string | null;
            normalizedUserName?: string | null;
            email?: string | null;
            normalizedEmail?: string | null;
            emailConfirmed?: boolean;
            passwordHash?: string | null;
            securityStamp?: string | null;
            concurrencyStamp?: string | null;
            phoneNumber?: string | null;
            phoneNumberConfirmed?: boolean;
            twoFactorEnabled?: boolean;
            lockoutEnd?: string | null; // date-time
            lockoutEnabled?: boolean;
            accessFailedCount?: number; // int32
        }
        export interface UserInfoDto {
            id?: string; // uuid
            username?: string | null;
        }
    }
}
declare namespace Paths {
    namespace AddExpense {
        export type RequestBody = Components.Schemas.AddExpense;
        namespace Responses {
            export type $200 = number; // int32
        }
    }
    namespace AddExpenseType {
        export type RequestBody = Components.Schemas.AddExpenseType;
        namespace Responses {
            export interface $200 {
            }
        }
    }
    namespace DeleteExpense {
        namespace Parameters {
            export type Id = number; // int32
        }
        export interface PathParameters {
            id: Parameters.Id /* int32 */;
        }
        namespace Responses {
            export interface $200 {
            }
        }
    }
    namespace DeleteExpenseType {
        namespace Parameters {
            export type Id = number; // int32
        }
        export interface PathParameters {
            id: Parameters.Id /* int32 */;
        }
        namespace Responses {
            export interface $200 {
            }
        }
    }
    namespace GetExpense {
        namespace Parameters {
            export type Id = number; // int32
        }
        export interface PathParameters {
            id: Parameters.Id /* int32 */;
        }
        namespace Responses {
            export type $200 = Components.Schemas.ExpenseDto;
        }
    }
    namespace GetExpenseTypes {
        namespace Responses {
            export type $200 = Components.Schemas.ExpenseTypeDto[];
        }
    }
    namespace GetExpenses {
        namespace Responses {
            export type $200 = Components.Schemas.ExpenseDto[];
        }
    }
    namespace GetMe {
        namespace Responses {
            export type $200 = Components.Schemas.UserInfoDto;
        }
    }
    namespace Login {
        export type RequestBody = Components.Schemas.LoginRequest;
        namespace Responses {
            export interface $200 {
            }
        }
    }
    namespace Logout {
        namespace Responses {
            export interface $200 {
            }
        }
    }
    namespace Register {
        export type RequestBody = Components.Schemas.RegisterRequest;
        namespace Responses {
            export interface $200 {
            }
        }
    }
    namespace UpdateExpense {
        export type RequestBody = Components.Schemas.UpdateExpense;
        namespace Responses {
            export type $200 = number; // int32
        }
    }
}

export interface OperationMethods {
  /**
   * Login
   */
  'Login'(
    parameters?: Parameters<UnknownParamsObject> | null,
    data?: Paths.Login.RequestBody,
    config?: AxiosRequestConfig  
  ): OperationResponse<Paths.Login.Responses.$200>
  /**
   * Logout
   */
  'Logout'(
    parameters?: Parameters<UnknownParamsObject> | null,
    data?: any,
    config?: AxiosRequestConfig  
  ): OperationResponse<Paths.Logout.Responses.$200>
  /**
   * Register
   */
  'Register'(
    parameters?: Parameters<UnknownParamsObject> | null,
    data?: Paths.Register.RequestBody,
    config?: AxiosRequestConfig  
  ): OperationResponse<Paths.Register.Responses.$200>
  /**
   * GetMe
   */
  'GetMe'(
    parameters?: Parameters<UnknownParamsObject> | null,
    data?: any,
    config?: AxiosRequestConfig  
  ): OperationResponse<Paths.GetMe.Responses.$200>
  /**
   * GetExpenses
   */
  'GetExpenses'(
    parameters?: Parameters<UnknownParamsObject> | null,
    data?: any,
    config?: AxiosRequestConfig  
  ): OperationResponse<Paths.GetExpenses.Responses.$200>
  /**
   * UpdateExpense
   */
  'UpdateExpense'(
    parameters?: Parameters<UnknownParamsObject> | null,
    data?: Paths.UpdateExpense.RequestBody,
    config?: AxiosRequestConfig  
  ): OperationResponse<Paths.UpdateExpense.Responses.$200>
  /**
   * AddExpense
   */
  'AddExpense'(
    parameters?: Parameters<UnknownParamsObject> | null,
    data?: Paths.AddExpense.RequestBody,
    config?: AxiosRequestConfig  
  ): OperationResponse<Paths.AddExpense.Responses.$200>
  /**
   * GetExpense
   */
  'GetExpense'(
    parameters?: Parameters<Paths.GetExpense.PathParameters> | null,
    data?: any,
    config?: AxiosRequestConfig  
  ): OperationResponse<Paths.GetExpense.Responses.$200>
  /**
   * DeleteExpense
   */
  'DeleteExpense'(
    parameters?: Parameters<Paths.DeleteExpense.PathParameters> | null,
    data?: any,
    config?: AxiosRequestConfig  
  ): OperationResponse<Paths.DeleteExpense.Responses.$200>
  /**
   * GetExpenseTypes
   */
  'GetExpenseTypes'(
    parameters?: Parameters<UnknownParamsObject> | null,
    data?: any,
    config?: AxiosRequestConfig  
  ): OperationResponse<Paths.GetExpenseTypes.Responses.$200>
  /**
   * AddExpenseType
   */
  'AddExpenseType'(
    parameters?: Parameters<UnknownParamsObject> | null,
    data?: Paths.AddExpenseType.RequestBody,
    config?: AxiosRequestConfig  
  ): OperationResponse<Paths.AddExpenseType.Responses.$200>
  /**
   * DeleteExpenseType
   */
  'DeleteExpenseType'(
    parameters?: Parameters<Paths.DeleteExpenseType.PathParameters> | null,
    data?: any,
    config?: AxiosRequestConfig  
  ): OperationResponse<Paths.DeleteExpenseType.Responses.$200>
}

export interface PathsDictionary {
  ['/Account/Login']: {
    /**
     * Login
     */
    'post'(
      parameters?: Parameters<UnknownParamsObject> | null,
      data?: Paths.Login.RequestBody,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.Login.Responses.$200>
  }
  ['/Account/Logout']: {
    /**
     * Logout
     */
    'post'(
      parameters?: Parameters<UnknownParamsObject> | null,
      data?: any,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.Logout.Responses.$200>
  }
  ['/Account/Register']: {
    /**
     * Register
     */
    'post'(
      parameters?: Parameters<UnknownParamsObject> | null,
      data?: Paths.Register.RequestBody,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.Register.Responses.$200>
  }
  ['/Account/GetMe']: {
    /**
     * GetMe
     */
    'get'(
      parameters?: Parameters<UnknownParamsObject> | null,
      data?: any,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.GetMe.Responses.$200>
  }
  ['/Expenses']: {
    /**
     * GetExpenses
     */
    'get'(
      parameters?: Parameters<UnknownParamsObject> | null,
      data?: any,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.GetExpenses.Responses.$200>
    /**
     * AddExpense
     */
    'post'(
      parameters?: Parameters<UnknownParamsObject> | null,
      data?: Paths.AddExpense.RequestBody,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.AddExpense.Responses.$200>
    /**
     * UpdateExpense
     */
    'put'(
      parameters?: Parameters<UnknownParamsObject> | null,
      data?: Paths.UpdateExpense.RequestBody,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.UpdateExpense.Responses.$200>
  }
  ['/Expenses/{id}']: {
    /**
     * GetExpense
     */
    'get'(
      parameters?: Parameters<Paths.GetExpense.PathParameters> | null,
      data?: any,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.GetExpense.Responses.$200>
    /**
     * DeleteExpense
     */
    'delete'(
      parameters?: Parameters<Paths.DeleteExpense.PathParameters> | null,
      data?: any,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.DeleteExpense.Responses.$200>
  }
  ['/ExpenseTypes']: {
    /**
     * GetExpenseTypes
     */
    'get'(
      parameters?: Parameters<UnknownParamsObject> | null,
      data?: any,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.GetExpenseTypes.Responses.$200>
    /**
     * AddExpenseType
     */
    'post'(
      parameters?: Parameters<UnknownParamsObject> | null,
      data?: Paths.AddExpenseType.RequestBody,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.AddExpenseType.Responses.$200>
  }
  ['/ExpenseTypes/{id}']: {
    /**
     * DeleteExpenseType
     */
    'delete'(
      parameters?: Parameters<Paths.DeleteExpenseType.PathParameters> | null,
      data?: any,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.DeleteExpenseType.Responses.$200>
  }
}

export type Client = OpenAPIClient<OperationMethods, PathsDictionary>
