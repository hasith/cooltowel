define(['plugins/router'], function (router) {
    return {
        router: router,
        activate: function () {
            return router.map([
                { route: 'samples*details', moduleId: 'samples/index', title: 'Samples', nav: true, hash: "#samples" }
            ]).buildNavigationModel()
              //.mapUnknownRoutes('samples/hello/index', 'not-found')
              .activate();
        }
    };
});