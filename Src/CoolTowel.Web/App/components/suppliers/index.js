define(['core/dataservice', 'knockout', 'mapping'], function (ds, ko, mapping) {
    
    var viewmodel = {
        self: this,

        items: ko.observableArray(),

        selected: ko.observable(),
               
        callServer: function () {
            var self = this;
            self.selected(null);
            var query = ds.EntityQuery.from('Suppliers');
            ds.EntityManager.executeQuery(query).then(function (data) {
                mapping.fromJS(data.results[0].value, {}, self.items);
            }).fail(function (error) {
                console.log("Error retrieving lists: " + error.message);
            });
        }
    };
    return viewmodel;
});