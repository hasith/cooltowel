define(['plugins/dialog', 'knockout', './dao'],
    function (dialog, ko, dao) {

        var CustomDialog = function (mediator) {
            var self = this;
            this.mediator = mediator;

            this.remove = function () {
                var idToDelete = mediator.selected().Id();
                dao.remove(idToDelete, function () {
                    self.mediator.close()
                });
                dialog.close(self);
            };

            this.cancel = function () {
                return dialog.close(self);
            };

        };


        CustomDialog.show = function (mediator) {
            return dialog.show(new CustomDialog(mediator));
        };

        return CustomDialog;
    });