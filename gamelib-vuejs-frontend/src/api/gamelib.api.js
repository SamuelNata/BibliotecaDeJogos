import axios from 'axios';

let http = axios.create({
    baseURL: process.env.GAMELIB_API_HOST || 'https://game-lib-api.herokuapp.com'
});

export default {
    accounts: {
        login(username, password) {
            return http({
                url: '/account/login',
                method: 'post',
                data: {
                    username: username,
                    password: password
                }
            });
        },
        signIn(nickname, username, password) {
            return http({
                url: '/account/sign_in',
                method: 'put',
                data: {
                    nickname: nickname,
                    username: username,
                    password: password
                }
            });
        },
        authenticatedUserInfo(token){
            return http({
                url: '/account/me',
                method: 'get',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
        }
    },
    games: {
        listGames(token) {
            return http({
                url: '/game',
                method: 'get',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
        },
        getGameById(id, token) {
            return http({
                url: `/game/${id}`,
                method: 'get',
                headers: { 'Authorization': `Bearer ${token}` }
            });
        },
        createGame(name, year, token) {
            return http({
                url: '/game',
                method: 'put',
                data: { name, year },
                headers: { 'Authorization': `Bearer ${token}` }
            });
        },
        editGame(id, name, year, token) {
            return http({
                url: `/game/${id}`,
                method: 'post',
                data: { name, year },
                headers: { 'Authorization': `Bearer ${token}` }
            });
        }
    },
    users: {
        listUsers(token) {
            return http({
                url: '/user',
                method: 'get',
                headers: { 'Authorization': `Bearer ${token}` }
            });
        },
        getUserById(id, token) {
            return http({
                url: `/user/${id}`,
                method: 'get',
                headers: { 'Authorization': `Bearer ${token}` }
            });
        },
        listMyGames(token) {
            return http({
                url: '/user/my-games',
                method: 'get',
                headers: { 'Authorization': `Bearer ${token}` }
            });
        },
        acquireGame(gameId, token) {
            return http({
                url: '/user/my-games',
                method: 'put',
                data: { gameId },
                headers: { 'Authorization': `Bearer ${token}` }
            });
        },
        removeGameFromMyList(gameId, token) {
            return http({
                url: `/user/my-games/${gameId}`,
                method: 'delete',
                headers: { 'Authorization': `Bearer ${token}` }
            });
        },
        borrowGameForFriend(gameOwnershipId, predictedDevolution, userGetingBorrowedId, token) {
            return http({
                url: '/user/my-games/borrow-to-friend',
                method: 'put',
                data: { gameOwnershipId, predictedDevolution, userGetingBorrowedId },
                headers: { 'Authorization': `Bearer ${token}` }
            });
        },
        receiveBorrowedGameBack(borrowId, token) {
            return http({
                url: '/user/my-games/receive-borrowed-back',
                method: 'post',
                params: { id: borrowId },
                headers: { 'Authorization': `Bearer ${token}` }
            });
        }
    }
};