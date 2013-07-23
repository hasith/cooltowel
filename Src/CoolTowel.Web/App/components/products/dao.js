define(['core/dataservice', 'mapping'], function (ds, mapping) {
    
    var remove = function(id, callback) {
        ds.remove("Products", id, function (data, status, httpRequest) {
            console.log("Product (" + id + ") is successfully deleted");
            callback();
        }, function (request, status, error) {
            console.log(error.message, "Deletion Failed!");
        });

    }
    
    var get = function (id, callback) {
        var query = ds.EntityQuery.from('Products').where('Id', '==', parseInt(id));

        ds.EntityManager.executeQuery(query).then(function (data) {
            callback(mapping.fromJS(data.results[0].value[0]));
        }).fail(function (error) {
            console.log("Data Error: " + error.message);
        });
    };

    var getAll = function (callback) {
        var query = ds.EntityQuery.from('Products');
        ds.EntityManager.executeQuery(query).then(function (data) {
            callback(data.results[0].value);
        }).fail(function (error) {
            console.log("Data Error : " + error.message);
        });
    }

    var save = function (entity, callback) {

        var jsonEntity = ko.toJSON(mapping.toJS(entity));

        ds.insert("Products", jsonEntity, function (data, status, httpRequest) {
            console.log("Product (" + data.Id + ") is successfully saved");
            callback(mapping.fromJS(data));
        },
        function (request, status, error) {
            console.log(error.message, "Saving failed");
        });
    }

    var createNew = function () {
        return mapping.fromJS({ Name: '', Price: 0, GUID: '' });
    }

    return {
        get: get,
        getAll: getAll,
        remove: remove,
        save: save,
        createNew: createNew
    };
});