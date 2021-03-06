import Vue from 'vue';
import VueRouter from 'vue-router';
import DefaultLayout from '../views/DefaultLayout.vue';
import MyGames from '../views/MyGames.vue';
import AllGames from '../views/AllGames.vue';
import store from '../store/index';

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
                path: 'MeusJogos',
                alias: '',
                component: MyGames,
                name: 'Meus Jogos',
                meta: {description: 'Meus Jogos', requiresAuth: true }
            },
            {
                path: 'TodosOsJogos',
                alias: '',
                component: AllGames,
                name: 'Todos os Jogos',
                meta: {description: 'Todos os Jogos', requiresAuth: true }
            }
        ]
    }
];

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes,
});

router.beforeEach((to, from, next) => {
    if(to.matched.some(record => record.meta.requiresAuth)) {
        if (store.state.token == null) {
            return next({
                path: '/Login',
                params: { nextUrl: to.fullPath }
            })
        }
    }
    return next();
})

export default router;
