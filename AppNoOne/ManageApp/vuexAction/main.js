import { createStore } from 'vuex';
import modules from "./stores"

const store = createStore({
    modules
});

export default store;
