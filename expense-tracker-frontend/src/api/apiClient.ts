import OpenAPIClientAxios from 'openapi-client-axios'
import type { Client } from '@/api/client'

const baseURL = import.meta.env.VITE_APP_BASE_URL

const axiosInstance = new OpenAPIClientAxios({
  definition: baseURL + 'swagger/v1/swagger.json',
  axiosConfigDefaults: {
    withCredentials: true
  }
})

// Probably there is a better way to do this, but I don't want to spend more time on this.
await axiosInstance.init()
const api: Client = await axiosInstance.getClient<Client>()

console.log(api.defaults)

api.defaults.baseURL = baseURL
api.defaults.withCredentials = true

export default api
