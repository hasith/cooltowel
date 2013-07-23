define(['knockout', '../dao', '../mediator'], function (ko, dao, mediator) {

    var activate = function (param) {

        if ('new' === param) {
            mediator.toolbar(['save']);
            mediator.selected(dao.createNew());
        } else {
            mediator.toolbar(['delete', 'save']);
            
            dao.get(parseInt(param), function (data) {
                mediator.selected(data);
            });
        }
    };

    return {
        activate: activate,
        mediator:mediator
    };
});