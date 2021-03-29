<template>
  <v-container
    id="regular-tables"
    fluid
    tag="section"
  >
    <v-card class="px-2">
      <v-card-title class="font-weight-light">Adicionar jogo</v-card-title>
      <v-row>
        <v-col cols="5">
          <v-text-field v-model="newGameName" label="Nome">Nome</v-text-field>
        </v-col>
        <v-col cols="5">
          <v-text-field v-model="newGameYear" label="Ano">Ano</v-text-field>
        </v-col>
        <v-col cols="2">
          <v-btn @click="createNewGame()">Adicionar</v-btn>
        </v-col>
      </v-row>
      
      
    </v-card>

    <base-material-card
      icon="mdi-clipboard-text"
      title="Jogos cadastrados"
      class="px-5 py-6"
    >
      <v-simple-table>
        <thead>
          <tr>
            <th class="primary--text">
              Nome
            </th>
            <th class="primary--text">
              Ano
            </th>
          </tr>
        </thead>

        <tbody>
          <tr v-for="game in gamesList" v-bind:key="game.id">
            <td>{{game.name}}</td>
            <td>{{game.year}}</td>
          </tr>
        </tbody>
      </v-simple-table>
    </base-material-card>

  </v-container>
</template>

<script>
export default {
    name: 'AllGames',
    components: {
    },
    data: () => ({
        gamesList: [],
        newGameName: '',
        newGameYear: ''
    }),
    methods: {
        async getGamesList() {
            try {
                let response = await this.$api.games.listGames(this.$store.state.token);
                this.gamesList = response.data;
            } catch (error) {
                return {
                    success: false,
                    message: error.response.data.message
                }
            }
        },
        async createNewGame() {
            try {
                await this.$api.games.createGame(
                    this.newGameName,  parseInt(this.newGameYear), this.$store.state.token);
                this.$toast.open("Jogo criado com sucesso!");
                this.getGamesList();
            } catch (error) {
                return {
                    success: false,
                    message: error.response.data.message
                }
            }
        }
    },
    mounted() {
        this.getGamesList();
    }
};
</script>
