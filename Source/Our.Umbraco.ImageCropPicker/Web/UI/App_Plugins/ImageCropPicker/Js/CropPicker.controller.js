(function () {
    'use strict';

    angular.module("umbraco")
        .controller("Our.Umbraco.ImageCropPicker.CropPickerController", cropPickerController);

    cropPickerController.$inject = ['$scope', 'imageCropPickerResource'];

    function cropPickerController($scope, imageCropPickerResource) {
        var vm = this;

        vm.data = {};

        vm.init = function () {
            imageCropPickerResource.GetImageCropsDataForDataType($scope.model.config.dataType.Id)
                .then(function (data) {
                    vm.data.cropsData = data;
                });
        };

        vm.init();
    }
})();