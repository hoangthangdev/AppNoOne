<template>
    <div>
        <v-alert v-model="isShow"
                 :type="valueAlert?.type"
                 transition="v-slide-x-transition"
                 class="v-slide-x-transition">
            {{valueAlert?.message}}
        </v-alert>
    </div>
</template>
<style scoped>
    .v-slide-x-enter-active,
    .v-slide-x-leave-active {
        transition: transform 0.3s ease;
    }

    .v-slide-x-enter,
    .v-slide-x-leave-to {
        transform: translateX(100%);
    }
</style>
<script>
    import { ref, watch, onMounted, computed } from 'vue';
    import { Alert } from "./constants"

    export default {
        setup() {
            const isShow = ref(false);
            const data = ref([
                { type: 'success', message: 'Success fully ' },
                { type: 'error', message: 'Error message ' },
                { type: 'warning', message: 'Warning message ' },
            ]);
            const valueAlert = computed(() => {
                const val = data.value.find(item => item.type === Alert.type);
                if (!val)
                    return;
                val.message += Alert.message;
                return val;
            });
            onMounted(() => {
                watch(Alert, () => {
                    if (!valueAlert)
                        return;
                    valueAlert.value;
                    isShow.value = true
                    setTimeout(() => {
                        isShow.value = false
                    }, 2000)
                });
            })

            return {
                valueAlert,
                isShow
            };
        },
    };
</script>






