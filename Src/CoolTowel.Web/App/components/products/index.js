define(['plugins/router', './mediator'], function (router, mediator) {
    var childRouter = router.createChildRouter()
    .makeRelative({
        moduleId: 'components/products',
        route: 'products'
    }).map([
        { route: 'list', moduleId: 'list/index'},
        { route: 'view/*details', moduleId: 'view/index'},
        { route: 'edit/*details', moduleId: 'edit/index' }
    ])
    .buildNavigationModel()
    .mapUnknownRoutes('list/index', 'not-found');

    var edit = function () {
        mediator.edit();
    }

    var remove = function () {
        mediator.remove();
    }

    var save = function () {
        mediator.save();
    }

    return {
        router: childRouter,
        mediator: mediator,
        remove: remove,
        save: save,
        edit: edit
    };
});