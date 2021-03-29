<template>
  <v-app-bar
    id="app-bar"
    absolute
    app
    color="transparent"
    flat
    height="75"
  >
    <v-toolbar-title
      class="hidden-sm-and-down font-weight-light"
      v-text="`Jogoteca - Sua biblioteca de jogos`"
    />

    <v-spacer />

    <div class="mx-3" />

    <v-btn
      class="ml-2"
      min-width="0"
      text
      @click="logout()"
    >
      Sair
      <v-icon>mdi-logout</v-icon>
    </v-btn>
  </v-app-bar>
</template>

<script>
  export default {
    name: 'CoreAppBar',
    components: { },
    props: {
      value: {
        type: Boolean,
        default: false,
      },
    },
    data: () => ({ }),
    computed: { },
    methods: { 
      logout(){
        this.$store.dispatch('logout');
        this.$router.push({ name: 'Login' });
      }
    },
  }
</script>
