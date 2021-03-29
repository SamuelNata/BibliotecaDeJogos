<template>
    <div class="limiter">
        <div
            class="container-login100"
            :style="`background-image: url('${require('../assets/images/pc_vs_console_qual-e-melhor.jpg')}');`"
        >
            <div class="wrap-login100 p-t-140 p-b-30">
                <div class="login100-form validate-form">
                    <span class="login100-form-title p-t-20 p-b-25">
                        Jogoteca
                    </span>

                    <div class="login100-form-avatar">
                        <img
                            src="../assets/images/1001372063.jpg"
                            style="transform: scale(1.5)"
                            alt="AVATAR"
                        >
                    </div>

                    <div class="p-t-20 p-b-45 text-center text-white h-100">
                        ~ Sua biblioteca de jogos ~
                    </div>

                    <div v-if="errorMessage" class="p-b-10 text-center red--text darken-1 h-100">
                        <i class="fa fa-warning"></i>
                        {{errorMessage}}
                    </div>

                    <div v-if="successMessage" class="p-b-10 text-center green--text darken-1 h-100">
                        <i class="fa fa-check"></i>
                        {{successMessage}}
                    </div>

                    <div v-if="action == 'sign in'" class="wrap-input100 validate-input m-b-10">
                        <input class="input100" type="text" v-model="name" placeholder="Nome">
                        <span class="focus-input100"></span>
                        <span class="symbol-input100">
                            <i class="fa fa-tag"></i>
                        </span>
                    </div>

                    <div class="wrap-input100 validate-input m-b-10">
                        <input class="input100" type="text" v-model="username" placeholder="Usuário">
                        <span class="focus-input100"></span>
                        <span class="symbol-input100">
                            <i class="fa fa-user"></i>
                        </span>
                    </div>

                    <div class="wrap-input100 validate-input m-b-10">
                        <input class="input100" type="password" v-model="password" placeholder="Senha">
                        <span class="focus-input100"></span>
                        <span class="symbol-input100">
                            <i class="fa fa-lock"></i>
                        </span>
                    </div>

                    <div  class="container-login100-form-btn p-t-10">
                        <button class="login100-form-btn" v-on:click="screemMainAction()">
                            <breeding-rhombus-spinner
                                v-if="loading"
                                :animation-duration="1000"
                                :size="30"
                                color="#ffffff"
                            />
                            <template v-else-if="action == 'login'">
                                Entrar
                            </template>
                            <template v-else-if="action == 'sign in'">
                                Cadastrar
                            </template>
                        </button>
                    </div>

                    <div class="text-center w-full p-t-25 p-b-130">
                        <a v-if="action == 'login'" v-on:click="action = 'sign in'" class="txt1">
                            Não tem conta? Crie agora!
                        </a>
                        <a v-if="action == 'sign in'" v-on:click="action = 'login'" class="txt1">
                            Já tenho uma conta
                        </a>
                    </div>

                    <div class="text-center w-full">
						<a class="white--text" href="https://colorlib.com/wp/template/login-form-v12/">
							Esta página é uma customização de Login form v12			
						</a>
					</div>
                </div>
            </div>
        </div>
    </div>
</template>

<style>
    @import '../assets/login/css/util.css';
    @import '../assets/login/css/main.css';
    @import '../assets/fonts/font-awesome-4.7.0/css/font-awesome.min.css';
</style>

<script>
// @ is an alias to /src
import { BreedingRhombusSpinner } from 'epic-spinners'
import axios from 'axios';

export default {
    name: 'Login',
    components: {
        BreedingRhombusSpinner
    },
    data: () => ({
        name: '',
        username: '',
        password: '',
        loading: false,
        errorMessage: null,
        successMessage: null,
        action: "login"
    }),
    methods: {
        screemMainAction(){
            if(this.action == 'login') {
                return this.login();
            }
            if(this.action == 'sign in') {
                return this.signIn();
            }
        },
        async login() {
            this.loading = true;
            let result = await this.$store.dispatch('login', { username: this.username, password: this.password });
            this.loading = false;
            if(!result.success){
                this.errorMessage = result.message;
            }
            else
            {
                this.$router.push({ name: 'Home' })
            }
        },
        async signIn() {
            if(!this.name || !this.username || !this.password) {
                this.errorMessage = "Preencha todos os campos";
                return;
            }
            this.loading = true;
            try {
                await axios({
                    url: `${this.$store.state.apiHost}/account/sign_in`,
                    method: 'put',
                    data: {
                        nickname: this.name,
                        username: this.username,
                        password: this.password
                    }
                });
                this.successMessage = 'Conta criada, pode fazer login agora!';
                this.username = '';
                this.password = '';
                this.errorMessage = '';
                this.action = 'login';
            } catch (error) {
                this.errorMessage = error.response.data.message;
            }
            this.loading = false;
        },
    }
};
</script>
