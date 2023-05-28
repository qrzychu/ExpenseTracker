import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { PiniaLogger } from 'pinia-logger'

import App from './App.vue'
import router from './router'

const app = createApp(App)

const pinia = createPinia()

pinia.use(
  PiniaLogger({
    expanded: true
  })
)

app.use(pinia)
app.use(router)

app.mount('#app')
