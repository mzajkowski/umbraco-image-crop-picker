(function () {
    'use strict';

    angular.module("umbraco")
        .controller("Our.Umbraco.ImageCropPicker.CropPickerController", cropPickerController);

    cropPickerController.$inject = ['$scope', 'imageCropPickerResource'];

    function cropPickerController($scope, imageCropPickerResource) {
        var vm = this;

        vm.data = {};

        vm.init = function () {
            var dataTypeName = '';

            if ($scope.model.config) {
                if ($scope.model.config.dataType && $scope.model.config.dataType.Name) {
                    dataTypeName = $scope.model.config.dataType.Name;
                }
                else if ($scope.model.config.dataTypeName && $scope.model.config.dataTypeName !== '') {
                    dataTypeName = $scope.model.config.dataTypeName;
                }
            }

            if (dataTypeName) {
                imageCropPickerResource.GetImageCropsDataForDataType(dataTypeName)
                    .then(function (data) {
                        vm.data.cropsData = data;
                    });
            }
        };

        vm.init();
    }
})();