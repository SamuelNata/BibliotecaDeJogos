<template>
  <v-container
    id="regular-tables"
    fluid
    tag="section"
  >

    <v-card class="px-2">
      <v-card-title class="font-weight-light">Adquirir jogo</v-card-title>
      <v-row>
        <v-col cols="9">
          <v-autocomplete label="Jogo" v-model="gameIdToAquire" :items="gameOptions">
          </v-autocomplete>
        </v-col>
        <v-col cols="2">
          <v-btn @click="acquireGame()">Adquirir</v-btn>
        </v-col>
      </v-row>
      
      
    </v-card>

    <base-material-card
      icon="mdi-controller-classic"
      title="Meus Jogos"
      class="px-5 py-6"
    >
      <v-simple-table>
        <thead>
          <tr>
            <th class="primary--text">
              Jogo
            </th>
            <th class="primary--text">
              Status
            </th>
            <th class="primary--text">
              Devolução marcada para
            </th>
            <th class="primary--text">
              Ações
            </th>
          </tr>
        </thead>

        <tbody>
          <template v-if="myGamesList.length">
            <tr v-for="game in myGamesList" v-bind:key="game.id">
              <td>{{game.gameName}}</td>
              <td>
                <span 
                  :class="game.currentBorrowingId==null? 'green--text' : 'orange--text'"
                  :title="game.borrowerName? `Emprestado a ${game.borrowerName} em ${game.expectedDevolutionDate}` : ''"
                >
                  {{game.currentBorrowingId==null? "Em casa" : `Emprestado a ${game.borrowerName}`}}
                </span>
              </td>
              <td>
                {{game.expectedDevolutionDate==null? '-' : game.expectedDevolutionDate}}
              </td>
              <td>
                <v-btn v-if="game.currentBorrowingId==null" class="mx-2" fab dark small color="blue" title="Emprestar" 
                  @click="borrow.ownedGameId = game.ownedGameRelationId; borrow.dialog = true; borrow.gameName = game.gameName;">
                  <v-icon dark>
                    mdi-account-arrow-right
                  </v-icon>
                </v-btn>

                <v-btn v-if="game.currentBorrowingId!=null" class="mx-2" fab dark small color="green" title="Receber de volta"
                  @click="receiveBorrowedGame(game.currentBorrowingId)">
                  <v-icon dark>
                    mdi-account-arrow-left
                  </v-icon>
                </v-btn>
              </td>
            </tr>
          </template>
          <template v-else>
            <tr>
              <td colspan="100">Você não possui nenhum jogo</td>
            </tr>
          </template>
        </tbody>
      </v-simple-table>
    </base-material-card>

    <v-dialog
      v-model="borrow.dialog"
      width="650"
    >
      <v-card>
        <v-card-title class="headline grey lighten-2">
          Emprestando {{borrow.gameName}}
        </v-card-title>

        <div class="px-3 py-3">
          <v-row>
            <v-col col="6">
              <v-autocomplete label="Quem vai pegar emprestado" v-model="borrow.friendId" :items="friendOptions">
              </v-autocomplete>
              <v-text-field v-model="borrow.expectedDevolutionDate" label="Devolução esperada" disabled></v-text-field>
            </v-col>
            <v-col col="6">
              <v-date-picker v-model="borrow.expectedDevolutionDate" label="Previsão de devolução"></v-date-picker>
            </v-col>
          </v-row>
        </div>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="borrow.dialog = false" >
            Cancelar
          </v-btn>
          <v-btn color="primary" text @click="borrowGame()">
            Confirmar!
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

  </v-container>
</template>

<script>
export default {
    name: 'MyGames',
    components: {
    },
    data: () => ({
        usersList: [],
        gamesList: [],
        myGamesList: [],
        gameIdToAquire: null,
        borrow: {
          dialog: false,
          gameName: '',
          ownedGameId: null,
          friendId: null,
          expectedDevolutionDate: null
        }
    }),
    computed: {
      gameOptions() {
        return this.gamesList.map((el) => ({
          text: `${el.name} (${el.year})`,
          value: el.id
        }));
      },
      friendOptions() {
        return this.usersList.map((el) => ({
          text: el.nickname,
          value: el.id
        }));
      }
    },
    methods: {
        async getUsersList() {
            try {
                let response = await this.$api.users.listUsers(this.$store.state.token);
                this.usersList = response.data;
            } catch (error) {
                this.$toast.open({
                  message: error.response.data.message,
                  type: 'error'
                });
            }
        },
        async getGamesList() {
            try {
                let response = await this.$api.games.listGames(this.$store.state.token);
                this.gamesList = response.data;
            } catch (error) {
                this.$toast.open({
                  message: error.response.data.message,
                  type: 'error'
                });
            }
        },
        async getMyGamesList() {
            try {
                let response = await this.$api.users.listMyGames(this.$store.state.token);
                this.myGamesList = response.data;
            } catch (error) {
                this.$toast.open({
                  message: error.response.data.message,
                  type: 'error'
                });
            }
        },
        async acquireGame(){
          try {
            let response = await this.$api.users.acquireGame(this.gameIdToAquire, this.$store.state.token);
            this.$toast.open({ message: response.data.message });
            this.getMyGamesList();
          } catch(error) {
            this.$toast.open({
              message: error.response.data.message,
              type: 'error'
            });
          }
        },
        async borrowGame(){
          try {
            let response = await this.$api.users.borrowGameForFriend(
              this.borrow.ownedGameId,
              this.borrow.expectedDevolutionDate,
              this.borrow.friendId,
              this.$store.state.token
            );
            this.$toast.open({ message: response.data.message });
            this.getMyGamesList();
          } catch(error) {
            this.$toast.open({
              message: error.response.data.message,
              type: 'error'
            });
          }
          this.borrow.dialog = false;
        },
        async receiveBorrowedGame(borrowId){
          try {
            let response = await this.$api.users.receiveBorrowedGameBack(borrowId, this.$store.state.token);
            this.$toast.open({ message: response.data.message });
            this.getMyGamesList();
          } catch(error) {
            this.$toast.open({
              message: error.response.data.message,
              type: 'error'
            });
          }
        }
    },
    mounted() {
        this.getUsersList();
        this.getGamesList();
        this.getMyGamesList();
    }
};
</script>
