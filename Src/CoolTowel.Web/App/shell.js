define(['plugins/router'], function (router) {
    return {
        router: router,
        activate: function () {
            return router.map([
                { route: 'overview', moduleId: 'components/overview/index', title: 'Overview', nav: true, hash: "#overview" },
                { route: 'samples*details', moduleId: 'components/samples/index', title: 'Samples', nav: true, hash: "#samples" }
            ]).buildNavigationModel()
              .mapUnknownRoutes('components/overview/index', 'not-found')
              .activate();
        }
    };
});