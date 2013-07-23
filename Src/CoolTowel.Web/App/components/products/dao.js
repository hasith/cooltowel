define(['core/dataservice', 'mapping', 'core/notifier'], function (ds, mapping, notifier) {
    
    var remove = function(id, callback) {
        ds.remove("Products", id, function (data, status, httpRequest) {
            notifier.success("Product (" + id + ") is successfully deleted");
            callback();
        }, function (request, status, error) {
            notifier.error(error.message, "Deletion Failed!");
        });

    }
    
    var get = function (id, callback) {
        var query = ds.EntityQuery.from('Products').where('Id', '==', parseInt(id));

        ds.EntityManager.executeQuery(query).then(function (data) {
            callback(mapping.fromJS(data.results[0].value[0]));
        }).fail(function (error) {
            notifier.error(error.message, "Data Retreival Error");
        });
    };

    var getAll = function (callback) {
        var query = ds.EntityQuery.from('Products');
        ds.EntityManager.executeQuery(query).then(function (data) {
            callback(data.results[0].value);
        }).fail(function (error) {
            notifier.error(error.message, "Data Retreival Error");
        });
    }

    var save = function (entity, callback) {

        var jsonEntity = ko.toJSON(mapping.toJS(entity));

        ds.insert("Products", jsonEntity, function (data, status, httpRequest) {
            notifier.success("Product (" + data.Id + ") is successfully saved");
            callback(mapping.fromJS(data));
        },
        function (request, status, error) {
            notifier.error(error.message, "Saving Failed");
        });
    }

    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
                   .toString(16)
                   .substring(1);
    };

    function guid() {
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
               s4() + '-' + s4() + s4() + s4();
    }

    var createNew = function () {
        return mapping.fromJS({ Name: '', Price: '', GUID: guid() });
    }

    return {
        get: get,
        getAll: getAll,
        remove: remove,
        save: save,
        createNew: createNew
    };
});