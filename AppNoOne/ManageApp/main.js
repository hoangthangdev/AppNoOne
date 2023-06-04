import { createApp } from 'vue';
import router from './router';
import App from "./App";
import 'vuetify/styles';
import { createVuetify } from 'vuetify';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
const vuetify = createVuetify({
    components,
    directives,
})

import store from './vuexAction/main';

createApp(App)
    .use(router)
    .use(vuetify)
    .use(store)
    .mount("#app");