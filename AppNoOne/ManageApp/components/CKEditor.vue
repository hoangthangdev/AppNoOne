<template>
    <textarea id="editor1" name="editor1"></textarea>
</template>

<script>
    import { ref, onMounted, watch } from 'vue';
    export default {
        name: "CKEditor",
        props: ["content"],
        setup(props) {
            const content = ref(props.content);
            let ckEditor;
            onMounted(() => {
                ckEditor = window.CKEDITOR.replace("editor1", {
                    extraPlugins: 'easyimage'
                });
                ckEditor.setData(content.value);

                ckEditor.on("change", () => {
                    emit('senContent' , ckEditor.getData())
                })
            })
            watch(content, () => {
                if (content.value !== ckEditor.getData())
                    ckEditor.setData(content.value)
            })
        },
    };
</script>
