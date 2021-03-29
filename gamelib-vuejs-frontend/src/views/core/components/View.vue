<template>
  <v-main style="background-color: #eee; height: 100vh">
    <router-view />

    <core-footer />
  </v-main>
</template>

<script>
  export default {
    name: 'CoreView',

    components: {
      CoreFooter: () => import('./Footer'),
    },
  }
</script>
