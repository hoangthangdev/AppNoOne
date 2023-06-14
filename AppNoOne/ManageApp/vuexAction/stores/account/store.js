import axios from '/ManageApp/httpHelper/HttpService';
import { valueAlert } from '/ManageApp/components/constants'

const accountStore = {
    state: () => ({
        accountLogin: {},
        allUsers: {}
    }),
    mutations: {
        setData(state, payload) {
            // `state` is the local module state
            state.accountLogin = payload;
        },
        dataUsers(state, payload) {
            state.allUsers = payload;
        }
    },
    actions: {
        curentUser({ commit, state }) {
            axios.get('/api/Account/GetUserLogin')
                .then(response => {
                    // Khi nhận được dữ liệu từ API, gọi mutation để cập nhật state
                    commit('setData', response.data);
                })
                .catch(error => {
                    // Xử lý lỗi nếu có
                    console.error(error);
                });
        },
        getAllUser({ commit, state }, payload) {
            console.log(payload)
            axios.get('/api/Account/GetAllUsers', {
                params:
                {
                    pageIndex: payload.pageIndex,
                    pageSize: payload.pageSize
                }
            })
            .then(response => {
                console.log(response);
                commit('dataUsers', response.data);
            })
            .catch(error => {
                // Xử lý lỗi nếu có
                console.error(error);
            });
        },
        logOut() {
            axios.get('/api/Account/Logout')
                .then(response => {
                    console.log(response);
                    window.location.reload();
                })
                .catch(error => {
                    // Xử lý lỗi nếu có
                    console.error(error);
                });
        }

    },
    getters: {
        CurrentUser(state) {
            return state.accountLogin;
        },
        AllUsers(state) {
            return state.allUsers;
        }
    }
}
export default accountStore