const path = require('path');
const {VueLoaderPlugin} = require('vue-loader');

module.exports = {
    entry: "./wwwroot/js/Index.js",
    output: {
        path: path.resolve(__dirname, './wwwroot/dist'),
        filename: "bundle.js"
    },
    module:{
        rules:[
            {test:/\.css$/,use:[
                {loader:'style-loader'},
                {
                    loader:'css-loader',
                    options:{
                        modules:true
                    }
                }
            ]},
            {test: /\.(woff|svg|eot|ttf)\??.*$/,use:'url-loader'},
            {test:/\.vue$/,loader:'vue-loader'}
        ]
    },
    resolve:{alias:{'vue':'vue/dist/vue.js'}},
    devServer:{
        historyApiFallback:true,
    },
    plugins:[
        new VueLoaderPlugin()
    ]
};