define(['plugins/router'], function (router) {
    var childRouter = router.createChildRouter()
    .makeRelative({
        moduleId: 'components/samples',
        route: 'samples'
    }).map([
        { route: '', moduleId: 'hello/index', title: 'Hello World', nav: true },
        { route: 'modal', moduleId: 'modal/index', title: 'Modal Dialogs', nav: true },
        { route: 'event-aggregator', moduleId: 'eventAggregator/index', title: 'Events', nav: true },
        { route: 'widgets', moduleId: 'widgets/index', title: 'Widgets', nav: true },
        { route: 'master-detail', moduleId: 'masterDetail/index', title: 'Master Detail', nav: true },
        { route: 'knockout-samples*details', moduleId: 'ko/index', title: 'Knockout Samples', nav: true, hash: '#samples/knockout-samples' }
    ]).buildNavigationModel();


    return {
        router: childRouter
    };
});