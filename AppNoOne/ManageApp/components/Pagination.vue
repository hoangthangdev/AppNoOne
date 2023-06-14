<template>
    <div class="text-center">
        <v-container>
            <v-row justify="center">
                <v-col cols="8">
                    <v-container class="max-width">
                        <v-pagination v-model="dataPagi.pageIndex"
                                      class="my-4"
                                      :length="dataPagi.totalPage"
                                      :total-visible="7"
                                      :key="dataPagi.totalPage"
                                      @update:modelValue="fetchData"></v-pagination>
                    </v-container>
                </v-col>
            </v-row>
        </v-container>
    </div>
</template>
<script>
    import { ref, getCurrentInstance } from 'vue'
    export default {
        props: ["data"],
        setup(props) {
            const dataPagi = ref(props.data);
            const { emit } = getCurrentInstance();

            const fetchData = (curentPage) => {
               emit("pagination-change", curentPage);
            }
            return {
                dataPagi,
                fetchData
            }
        }
    }
</script>