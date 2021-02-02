(function () {
    'use strict';

    angular.module("umbraco")
        .controller("Our.Umbraco.ImageCropPicker.CropPickerController", cropPickerController);

    cropPickerController.$inject = ['$scope', 'imageCropPickerResource'];

    function cropPickerController($scope, imageCropPickerResource) {
        var vm = this;

        vm.data = {};

        vm.init = function () {
            var dataTypeId = '';

            if ($scope.model.config !== undefined) {
                if ($scope.model.config.dataType !== undefined && ($scope.model.config.dataType.Id !== undefined || $scope.model.config.dataType.Id !== '')) {
                    dataTypeId = $scope.model.config.dataType.Id;
                }
                else if ($scope.model.config.dataTypeId !== undefined && $scope.model.config.dataTypeId !== '') {
                    dataTypeId = $scope.model.config.dataTypeId;
                }
            }

            if ((dataTypeId !== undefined || dataTypeId !== null) && dataTypeId !== '') {
                imageCropPickerResource.GetImageCropsDataForDataType(dataTypeId)
                    .then(function (data) {
                        vm.data.cropsData = data;
                    });
            }
        };

        vm.init();
    }
})();