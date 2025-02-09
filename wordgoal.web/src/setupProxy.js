﻿const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/api/projects",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7085',
        secure: false
    });

    app.use(appProxy);
};
