define(['knockout', '../mediator', '../dao'], function (ko, mediator, dao) {

    var items = ko.observableArray();
    
    var activate = function () {
        //set toolbar buttons
        mediator.toolbar([]);

        dao.getAll(function (data) {
            items(data);
        });

    };

    this.view = function (item) {
        mediator.view(item.Id);
    };

    this.edit = function (item) {
        mediator.edit(item.Id);
    };

    return {
        activate: activate,
        items: items,
        view: view,
        edit: edit
    };

});