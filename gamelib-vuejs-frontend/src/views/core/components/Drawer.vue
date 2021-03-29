<template>
  <v-navigation-drawer
    id="core-drawer"
    :dark="true"
    :expand-on-hover="expandOnHover"
    :right="$vuetify.rtl"
    src="../../../assets/images/0615d8acca316bfc1860ee5a0b489cc0.jpg"
    mobile-breakpoint="960"
    app
    width="260"
    v-bind="$attrs"
  >
    <template v-slot:img="props">
      <v-img
        :gradient="`to bottom, ${barColor}`"
        v-bind="props"
      />
    </template>

    <v-divider class="mb-1" />

    <v-list dense nav >
      <v-list-item>
        <v-list-item-avatar
          class="align-self-center"
          color="white"
          contain
        >
          <v-img
            src="../../../assets/images/1001372063.jpg"
            max-height="90"
            style="transform: scale(1.6)"
          />
        </v-list-item-avatar>

        <v-list-item-content height="40px">
          <v-list-item-title class="display-1" v-text="nickname"/>
        </v-list-item-content>
      </v-list-item>
    </v-list>

    <v-divider class="mb-2" />

    <v-list
      expand
      nav
    >
      <div />

      <template v-for="(item, i) in computedItems">
        <base-item-group
          v-if="item.children"
          :key="`group-${i}`"
          :item="item"
        >
        </base-item-group>

        <base-item
          v-else
          :key="`item-${i}`"
          :item="item"
        />
      </template>
      <div />
    </v-list>
  </v-navigation-drawer>
</template>

<script>
  // Utilities
  import {
    mapState,
  } from 'vuex'

  export default {
    name: 'CoreDrawer',
    props: {
      expandOnHover: {
        type: Boolean,
        default: false,
      },
    },
    data: () => ({
      items: [
        {
          icon: 'mdi-controller-classic',
          title: 'Meus Jogos',
          to: '/MeusJogos',
        },
        {
          icon: 'mdi-clipboard-text',
          title: 'Todos os Jogos',
          to: '/TodosOsJogos',
        }
      ],
      barColor: 'rgba(0, 0, 0, .8), rgba(0, 0, 0, .8)'
    }),

    computed: {
      ...mapState(['userId', 'nickname']),
      computedItems () {
        return this.items.map(this.mapItem)
      },
      profile () {
        return {
          avatar: true,
          title: 'avatar',
        }
      },
    },

    methods: {
      mapItem (item) {
        return {
          ...item,
          children: item.children ? item.children.map(this.mapItem) : undefined,
          title: item.title,
        }
      },
    },
  }
</script>

<style lang="sass">
  @import '~vuetify/src/styles/tools/_rtl.sass'

  #core-drawer
    .v-list-group__header.v-list-item--active:before
      opacity: .24

    .v-list-item
      &__icon--text,
      &__icon:first-child
        justify-content: center
        text-align: center
        width: 20px

        +ltr()
          margin-right: 24px
          margin-left: 12px !important

        +rtl()
          margin-left: 24px
          margin-right: 12px !important

    .v-list--dense
      .v-list-item
        &__icon--text,
        &__icon:first-child
          margin-top: 10px

    .v-list-group--sub-group
      .v-list-item
        +ltr()
          padding-left: 8px

        +rtl()
          padding-right: 8px

      .v-list-group__header
        +ltr()
          padding-right: 0

        +rtl()
          padding-right: 0

        .v-list-item__icon--text
          margin-top: 19px
          order: 0

        .v-list-group__header__prepend-icon
          order: 2

          +ltr()
            margin-right: 8px

          +rtl()
            margin-left: 8px
</style>
