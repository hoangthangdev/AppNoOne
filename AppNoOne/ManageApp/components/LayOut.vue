<template>
    <v-card theme="dark">
        <v-layout>
            <v-navigation-drawer v-model="drawer"
                                 :rail="rail"
                                 permanent
                                 @click="rail = false">
                <v-list-item prepend-avatar="https://randomuser.me/api/portraits/men/85.jpg"
                             :title="`Hello ${userName} !`"
                             nav>
                    <template v-slot:append>
                        <v-btn variant="text"
                               icon="mdi-chevron-left"
                               @click.stop="rail = !rail"></v-btn>
                    </template>
                </v-list-item>

                <v-divider></v-divider>

                <v-list density="compact" nav>
                    <v-list-item prepend-icon="mdi-home" title="Home" :to="{ name: 'manage-home' }"></v-list-item>
                    <v-list-group value="Category">
                        <template v-slot:activator="{ props }">
                            <v-list-item v-bind="props"
                                         title="Category"
                                         prepend-icon="mdi-store">

                            </v-list-item>
                        </template>
                        <v-list-item v-for="(value,key) in cateMenu"
                                     :key="key"
                                     :to="value.url"
                                     :title="value.title"
                                     :prepend-icon="value.icon"></v-list-item>
                    </v-list-group>
                    <v-list-group value="account">
                        <template v-slot:activator="{ props }">
                            <v-list-item v-bind="props"
                                         title="Account"
                                         prepend-icon="mdi-account-box">

                            </v-list-item>
                        </template>
                        <v-list-item v-for="(value,key) in accountMenu"
                                     :key="key"
                                     :to="value.url"
                                     :title="value.title"
                                     :prepend-icon="value.icon"></v-list-item>
                    </v-list-group>
                    <v-list-item prepend-icon="mdi-logout" title="Logout" @click="logout"></v-list-item>
                </v-list>
            </v-navigation-drawer>
            <v-main style="height: 100vh">
                <alert />
                <router-view>
                </router-view>
                <loading />
            </v-main>
        </v-layout>
    </v-card>
</template>
<script>
    import { useStore, mapActions, mapGetters } from 'vuex';
    import { ref, watch, onBeforeMount, computed, onMounted } from 'vue';
    import CkEditor from './CKEditor';
    import loading from './Loading';
    import alert from './Alerts';
    import constants from './constants';

    export default {
        components: {
            CkEditor,
            loading,
            alert
        },
        setup() {
            const store = useStore();
            const rail = ref(true);
            const drawer = ref(true);

            const accountMenu = ref([
                { title: 'Users', icon: 'mdi-clipboard-account', url: { name: 'list-user' } },
                { title: 'Role', icon: 'mdi-ruler', url: "list-role" },
                { title: 'Claim', icon: 'mdi-key', url: "list-claim" },
            ]);

            const cateMenu = ref([
                { title: 'Category', icon: 'mdi-content-paste', url: { name: 'list-cate' } },
                { title: 'Product', icon: 'mdi-flask-empty', url: "list-product" },
                { title: 'Blog', icon: 'mdi-blogger', url: "list-blog" },
            ]);
            const currentUser = computed(() => store.getters.CurrentUser)
            onBeforeMount(() => {
                store.dispatch('curentUser')
            })

            const userName = computed(() => {
                if (currentUser.value.data && currentUser.value)
                    return currentUser.value.data.userName
            })
            const logout = () => {
                store.dispatch('logOut');
            }

            return {
                drawer,
                accountMenu,
                cateMenu,
                rail,
                currentUser,
                userName,
                logout
            }
        },
    }
</script>