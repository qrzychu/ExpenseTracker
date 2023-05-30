import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import { useLoginStore } from '@/stores/loginStore'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      beforeEnter: (to, from, next) => {
        const loginStore = useLoginStore()

        if (!loginStore.username) {
          next({ name: 'login' })
        } else {
          next()
        }
      }
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('@/views/LoginView.vue')
    },
    {
      path: '/logout',
      name: 'logout',
      component: () => import('@/views/LogoutView.vue')
    },

    {
      path: '/register',
      name: 'register',
      component: () => import('@/views/RegisterView.vue')
    }
  ]
})

export default router
