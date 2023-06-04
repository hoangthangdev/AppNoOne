const HtmlWebpackPlugin = require('html-webpack-plugin');
const { VueLoaderPlugin } = require('vue-loader');
const path = require('path');
const { DefinePlugin } = require('webpack');

module.exports = {
    mode: 'development',
    devtool: 'eval',
    entry: [
        './ManageApp/main.js',
    ],
    output: {
        clean: true,
        path: path.resolve(__dirname, 'dist'),
        publicPath: '/dist/',
        filename: '[name].bundle.[contenthash].js',
    },
    resolve: {
        // point bundler to the vue template compiler
        alias: {
            'vue$': 'vue/dist/vue.esm-bundler.js',
        },
        // allow imports to omit file exensions, 
        // e.g. "import foo from 'foobar'" instead of "import foo from 'foobar.js'"
        extensions: ['.js', '.vue'],
       
    },
    module: {
        rules: [
            // use vue-loader plugin for .vue files
            {
                test: /\.vue$/,
                use: 'vue-loader'
            },
            {
                test: /vuetify\/.*\.js$/,
                loader: 'babel-loader'
            },
            {
                test: /\.css$/,
                use: ['style-loader', 'css-loader']
            },
            {
                test: /\.s[ac]ss$/,
                use: ['style-loader', 'css-loader', 'sass-loader']
            },
            {
                test: /\.mjs$/,
                include: /node_modules/,
                type: 'javascript/auto',
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env']
                    }
                }
            },
            {
                test: /\.(woff|woff2|eot|ttf|otf)$/,
                use: [
                    {
                        loader: 'file-loader',
                        options: {
                            name: '[name].[contenthash].[ext]',
                            outputPath: 'fonts',
                            publicPath: '/dist/fonts',
                        },
                    },
                ],
            },
        ],
    },
    plugins: [
        new VueLoaderPlugin(),
        new HtmlWebpackPlugin({
            template: 'Views/Dashboard/Index.html',
            inject: true,
            // favicon: 'src/images/favicon.ico',
            publicPath: '/dist'
        }),
        new DefinePlugin({
            __VUE_OPTIONS_API__: true,
            __VUE_PROD_DEVTOOLS__: false,
        }),
    ],
    devServer: {
        historyApiFallback: {
            index: '/dist/index.html',
        },
        proxy: [
            {
                context: '/api/**',
                target: 'http://localhost:51235', // use port from IISExpress
            },
        ],
        open: true
    },
};