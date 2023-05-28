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
        }
        export interface Expense {
            id?: number; // int32
            description?: string | null;
            amount?: number; // double
            createdAt?: string; // date-time
            modifiedAt?: string; // date-time
        }
        export interface UpdateExpense {
            id?: number; // int32
            description?: string | null;
            amount?: number; // double
        }
    }
}
declare namespace Paths {
    namespace AddExpense {
        export type RequestBody = Components.Schemas.AddExpense;
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
    namespace GetExpense {
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
    namespace GetExpenses {
        namespace Responses {
            export type $200 = Components.Schemas.Expense[];
        }
    }
    namespace UpdateExpense {
        export type RequestBody = Components.Schemas.UpdateExpense;
        namespace Responses {
            export interface $200 {
            }
        }
    }
}

export interface OperationMethods {
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
}

export interface PathsDictionary {
  ['/expenses']: {
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
  ['/expenses/{id}']: {
    /**
     * GetExpense
     */
    'get'(
      parameters?: Parameters<Paths.GetExpense.PathParameters> | null,
      data?: any,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.GetExpense.Responses.$200>
  }
  ['/delete/{id}']: {
    /**
     * DeleteExpense
     */
    'delete'(
      parameters?: Parameters<Paths.DeleteExpense.PathParameters> | null,
      data?: any,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.DeleteExpense.Responses.$200>
  }
}

export type Client = OpenAPIClient<OperationMethods, PathsDictionary>
