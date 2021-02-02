(function () {
    'use strict';

    angular.module("umbraco")
        .controller("Our.Umbraco.ImageCropPicker.CropPickerController", cropPickerController);

    cropPickerController.$inject = ['$scope', 'imageCropPickerResource'];

    function cropPickerController($scope, imageCropPickerResource) {
        var vm = this;

        vm.data = {};

        vm.init = function () {
            var dataTypeKey = '';

            if ($scope.model.config) {
                if ($scope.model.config.dataType && $scope.model.config.dataType.Key) {
                    dataTypeKey = $scope.model.config.dataType.Key;
                }
                else if ($scope.model.config.dataTypeKey) {
                    dataTypeKey = $scope.model.config.dataTypeKey;
                }
            }

            if (dataTypeKey) {
                imageCropPickerResource.GetImageCropsDataForDataType(dataTypeKey)
                    .then(function (data) {
                        vm.data.cropsData = data;
                    });
            }
        };

        vm.init();
    }
})();