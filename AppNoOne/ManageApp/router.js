import { createRouter, createWebHistory } from 'vue-router';
import Home from "./pages/home.vue"

const routes = [
    {
        path: "/",
        name: "manage-home",
        component: Home,
        meta: {
            requiresAuth: true, 
        },
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})


export default router;

