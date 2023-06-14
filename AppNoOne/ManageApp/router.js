import { createRouter, createWebHistory } from 'vue-router';
import userList from "./pages/account/List"
import home from "./pages/home"

const routes = [
    {
        path: "/manage",
        name: "manage-home",
        component: home,
        meta: {
            requiresAuth: true, 
        },
    },
    {
        path: "/manage/user/list",
        name: "list-user",
        component: userList,
        meta: {
            requiresAuth: true, 
        },
    },
    {
        path: "/manage/user/role",
        name: "list-role",
        component: userList,
        meta: {
            requiresAuth: true, 
        },
    },
    {
        path: "/manage/user/claim",
        name: "list-claim",
        component: userList,
        meta: {
            requiresAuth: true, 
        },
    },
    {
        path: "/manage/category/list",
        name: "list-cate",
        component: userList,
        meta: {
            requiresAuth: true, 
        },
    },
    {
        path: "/manage/category/product",
        name: "list-product",
        component: userList,
        meta: {
            requiresAuth: true, 
        },
    },
    {
        path: "/manage/category/blog",
        name: "list-blog",
        component: userList,
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

