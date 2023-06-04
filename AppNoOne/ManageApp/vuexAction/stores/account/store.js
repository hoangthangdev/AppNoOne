import axios from '/ManageApp/httpHelper/HttpService';
import { valueAlert } from '/ManageApp/components/constants'

const accountStore = {
    state: () => ({
        accountLogin: { name: 123 },
    }),
    mutations: {
        setData(state, payload) {
            // `state` is the local module state
            state.accountLogin = payload;
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
        }
    },
    getters: {
        CurrentUser(state) {
            return state.accountLogin;
        }
    }
}
export default accountStore