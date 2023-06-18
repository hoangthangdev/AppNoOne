<template>
    <breadcrum :items="dataBrc"></breadcrum>
     <h3 class="title">List User</h3>
    <v-table>
        <thead>
            <tr>
                <th class="text-left">
                    Name
                </th>
                <th class="text-left">
                    Calories
                </th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="item in dataUsers.data"
                :key="item">
                <td>{{ dataUsers }}</td>
                <td>{{ item }}</td>
            </tr>
        </tbody>
    </v-table>
    <pagi :data="dataPagi"  @pagination-change="ReloadDataUser"></pagi>
</template>
<style scoped>
    .title{
        padding-left: 16px;
    }
</style>
<script>
    import pagi from '/ManageApp/components/Pagination';
    import breadcrum from '/ManageApp/components/breadcrumb';
    import { useStore } from 'vuex';
    import { computed, onBeforeMount, onMounted, ref, watch, reactive } from 'vue';
    export default {
        components: {
            pagi,
            breadcrum
        },
        setup() {
            const store = useStore();
            let dataPagi = reactive({})
            const pageSize = ref(10);

            const dataBrc = [
                {
                    title: 'Dashboard',
                    disabled: false,
                    href: '/manage',
                },
                {
                    title: 'Account',
                    disabled: true,
                    name: { name: '' },
                }
            ];

            const dataUsers = computed(() => {
                return store.getters.AllUsers
            })

            const LoadDataUser = (pageIndex,pageSize) => {
                store.dispatch('getAllUser',
                    {
                        pageIndex: pageIndex,
                        pageSize: pageSize
                    }
                ).then(() => {
                    dataUsers.value;
                });
            }

            onBeforeMount(() => {
                LoadDataUser(1, pageSize.value);
            })
            const ReloadDataUser = (changePage) => {
                LoadDataUser(changePage, pageSize.value)
            }
            onMounted(() => {
                watch(dataUsers.totalPage, (newDataUsers) => {
                    if (newDataUsers) {
                        dataPagi = { pageIndex: 1, totalPage: newDataUsers.totalPage };
                    }
                });
            })
            return {
                dataBrc,
                dataPagi,
                ReloadDataUser,
                dataUsers
            }
        }
    }
</script>