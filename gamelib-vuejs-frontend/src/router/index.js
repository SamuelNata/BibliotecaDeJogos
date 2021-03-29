import Vue from 'vue';
import VueRouter from 'vue-router';
import DefaultLayout from '../views/DefaultLayout.vue';
import Home from '../views/Home.vue';

Vue.use(VueRouter);

const routes = [
    {
        path: '/Login',
        name: 'Login',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () =>
            import(/* webpackChunkName: "login" */ '../views/Login.vue'),
    },
    {
        path: '/',
        component: DefaultLayout,
        children: [
            {
                path: '',
                alias: '',
                component: Home,
                name: 'Home',
                meta: {description: 'Home'}
            }
        ]
    }
];

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes,
});

export default router;
