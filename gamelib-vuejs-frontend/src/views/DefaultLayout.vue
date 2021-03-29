<template>
    <span>
        <core-app-bar />
        <core-drawer />
        <core-view />
    </span>
</template>

<script>
// @ is an alias to /src

export default {
    name: 'DefaultLayout',
    components: {
        CoreAppBar: () => import('./core/components/AppBar'),
        CoreDrawer: () => import('./core/components/Drawer'),
        CoreView: () => import('./core/components/View'),
    },
};
</script>
