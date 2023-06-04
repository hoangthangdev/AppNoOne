const context = require.context("./../", true, /store\.js$/);
const modules = {};
context.keys().forEach((key) => {
    const modulePath = key.replace('./', '').replace('/store.js', '');
    const fileName = key.replace(modulePath + '/.', '');

    modules[modulePath] = context(fileName).default;
});

export default modules;