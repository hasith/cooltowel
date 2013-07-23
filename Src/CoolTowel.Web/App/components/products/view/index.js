define(['knockout', '../mediator', '../dao'], function (ko, mediator, dao) {

    var activate = function (param) {
        mediator.toolbar(['delete', 'edit']);

        dao.get(parseInt(param), function (data) {
            mediator.selected(data);
        });
    };

    return {
        activate: activate,
        mediator: mediator
    };
});