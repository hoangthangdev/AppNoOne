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
                    <v-list-item prepend-icon="mdi-home" title="Home"></v-list-item>
                    <v-list-group value="Actions">
                        <template v-slot:activator="{ props }">
                            <v-list-item v-bind="props"
                                         title="Actions" prepend-icon="mdi-home"></v-list-item>
                        </template>
                        <v-list-item v-for="(value,key) in itemsMenu"
                                     :key="key"
                                     :to="value.url"
                                     :title="value.title"
                                     :prepend-icon="value.icon"></v-list-item>
                    </v-list-group>
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

            const itemsMenu = ref([
                { title: 'Home', icon: 'mdi-home-city', url: { name: 'manage-home' } },
                { title: 'Product', icon: 'mdi-dropbox', url: { name: 'test-router' } },
                { title: 'Users', icon: 'mdi-account-group-outline', url: "" },
            ]);
            const currentUser = computed(() => store.getters.CurrentUser)
            onBeforeMount(() => {
                store.dispatch('curentUser')
            })

            const userName = computed(() => {
                if (currentUser.value.data && currentUser.value)
                    return currentUser.value.data.userName
            })


            return {
                drawer,
                itemsMenu,
                rail,
                currentUser,
                userName,
            }
        },
    }
</script>