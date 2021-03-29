<template>
    <span>
        Conteudo
    </span>
</template>

<script>
import axios from 'axios';

export default {
    name: 'Home',
    components: {
    },
    data: () => ({
        loadingUsersList: false,
        usersList: []
    }),
    methods: {
        async getUsersList() {
            try {
                await axios({
                    url: `${this.$store.state.apiHost}/account/login`,
                    method: 'get'
                });
            } catch (error) {
                return {
                    success: false,
                    message: error.response.data.message
                }
            }
        }
    }
};
</script>
