<template>
    <v-breadcrumbs :items="items">
        <template v-slot:divider>
            <v-icon icon="mdi-chevron-right"></v-icon>
        </template>
    </v-breadcrumbs>
</template>

<script>
    export default {
        props: ['items'],
        setup(props) {
            const items = props.items;
            return {
                items
            }
        }
    }
</script>