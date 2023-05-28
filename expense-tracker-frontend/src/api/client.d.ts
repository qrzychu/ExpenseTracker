import type {
  OpenAPIClient,
  Parameters,
  UnknownParamsObject,
  OperationResponse,
  AxiosRequestConfig,
} from 'openapi-client-axios'; 

declare namespace Components {
    namespace Schemas {
        export interface Expense {
            id?: number; // int32
            description?: string | null;
            amount?: number; // double
            addedAt?: string; // date-time
        }
    }
}
declare namespace Paths {
    namespace GetExpenses {
        namespace Responses {
            export type $200 = Components.Schemas.Expense[];
        }
    }
    namespace _ {
        namespace Get {
            namespace Responses {
                export type $200 = string;
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
}

export interface PathsDictionary {
  ['/']: {
  }
  ['/expenses']: {
    /**
     * GetExpenses
     */
    'get'(
      parameters?: Parameters<UnknownParamsObject> | null,
      data?: any,
      config?: AxiosRequestConfig  
    ): OperationResponse<Paths.GetExpenses.Responses.$200>
  }
}

export type Client = OpenAPIClient<OperationMethods, PathsDictionary>
