<template>
    <div class="text-center">
        <v-overlay :model-value="isOverlay"
                   class="align-center justify-center">
            <v-progress-circular color="primary"
                                 indeterminate
                                 size="64"></v-progress-circular>
        </v-overlay>
    </div>
</template>

<script>
    import { watch, onMounted, ref } from 'vue';
    import { overlay } from './constants.js';
    export default {
        name: "overloading",
        setup() {
            const isOverlay = ref(false);
            onMounted(() => {
                watch(overlay, (newValue) => {
                    if (newValue)
                        isOverlay.value = newValue.value;
                });
            });
            return {
                isOverlay
            }
        }
    }
</script>