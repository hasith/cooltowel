define(['breeze'], function (breeze) {
   
    var apiBaseUrl = 'http://localhost:1471/';

    breeze.NamingConvention.none.setAsDefault();

    var dataService = new breeze.DataService({
        serviceName: apiBaseUrl + 'rest/',
        hasServerMetadata: false
    });

    var action = function (entity, action, id, success, error) {
        $.ajax({
            url: apiBaseUrl + 'rest/' + entity + "(" + id + ")/" + action,
            type: 'POST',
            contentType: "application/json",
            success: success,
            error: error
        });
    };

    var remove = function (entity, id, success, error) {
        $.ajax({
            url: apiBaseUrl + 'rest/' + entity + "(" + id + ")",
            type: 'DELETE',
            contentType: "application/json",
            success: success,
            error: error
        });
    };
    var insert = function (entity, dataInJson, success, error) {
        $.ajax({
            url: apiBaseUrl + 'rest/' + entity,
            data: dataInJson,
            type: 'POST',
            contentType: "application/json",
            success: success,
            error: error
        });
    };

    return {
        EntityQuery: breeze.EntityQuery,
        EntityManager: new breeze.EntityManager({ dataService: dataService }),
        Predicate: breeze.Predicate,
        insert: insert,
        remove: remove,
        action: action
    };
});