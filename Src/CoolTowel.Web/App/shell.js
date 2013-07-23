define(['plugins/router'], function (router) {
    return {
        router: router,
        activate: function () {
            return router.map([
                { route: 'overview', moduleId: 'components/overview/index', title: 'Overview', nav: true, hash: "#overview" },
                { route: 'suppliers*details', moduleId: 'components/suppliers/index', title: 'Suppliers', nav: true, hash: "#suppliers" },
                { route: 'products*details', moduleId: 'components/products/index', title: 'Products', nav: true, hash: "#products" },
                { route: 'samples*details', moduleId: 'components/samples/index', title: 'Durandal Samples', nav: true, hash: "#samples" }
            ]).buildNavigationModel()
              .mapUnknownRoutes('components/overview/index', 'not-found')
              .activate();
        }
    };
});