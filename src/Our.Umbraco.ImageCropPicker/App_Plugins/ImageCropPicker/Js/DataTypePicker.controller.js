(function () {
    'use strict';

    angular.module("umbraco")
        .controller("Our.Umbraco.ImageCropPicker.DataTypePickerController", dataTypePickerController);

    dataTypePickerController.$inject = ['$scope', 'imageCropPickerResource'];

    function dataTypePickerController($scope, imageCropPickerResource) {
        var vm = this;

        vm.data = {};

        vm.init = function () {
            imageCropPickerResource.GetDataTypes()
                .then(function (data) {
                    vm.data.dataTypes = data;
                });
        };

        vm.init();
    }
})();