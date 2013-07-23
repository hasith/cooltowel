define(['knockout', './dao', './deleteDialog'], function (ko, dao, DeleteDialog) {
    
    //to keep the tool bar state - which buttons to be enabled
    var toolbar = ko.observableArray([]); 

    //this is the selected model instance
    var selected = ko.observable();

    var view = function (id) {
        if (typeof id === 'undefined') {
            id = selected().Id();
        }
        location.hash = 'products/view/' + id;
    }

    var create = function () {
        location.hash = 'products/edit/new';
    }

    var remove = function () {
        DeleteDialog.show(this);
    }

    var edit = function (id) {
        if (typeof id === 'undefined') {
            id = selected().Id();
        }
        location.hash = 'products/edit/' + id;
    }

    var save = function () {
        dao.save(selected(), function (data) {
            view(data.Id());
        });
    }

    var list = function () {
        location.hash = 'products/list';
    }

    var close = function () {
        selected(null);
        list();
    }


    return {
        //shared models
        selected: selected,
        toolbar: toolbar,
        //shared operations
        remove: remove,
        edit: edit,
        save: save,
        view: view,
        close: close,
        create: create,
        list: list
    };
});