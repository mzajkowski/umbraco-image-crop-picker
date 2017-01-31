(function () {
    'use strict';

    angular.module("umbraco").factory("imageCropPickerResource", imageCropPickerResource);

    imageCropPickerResource.$inject = ['$http', 'umbRequestHelper'];

    function imageCropPickerResource($http, umbRequestHelper) {
        var service = {};

        service.GetDataTypes = getDataTypes;
        service.GetImageCropsDataForDataType = getImageCropsDataForDataType;

        return service;

        function getDataTypes() {
            var config = {
                params: {},
                headers: { 'Accept': 'application/json' }
            };

            return umbRequestHelper.resourcePromise($http.get("/Umbraco/backoffice/ImageCropPicker/ImageCropPickerApi/GetDataTypes", config),
                "Failed to retrieve data types");
        }

        function getImageCropsDataForDataType(id) {
            var config = {
                params: {
                    id: id
                },
                headers: { 'Accept': 'application/json' }
            };

            return umbRequestHelper.resourcePromise($http.get("/Umbraco/backoffice/ImageCropPicker/ImageCropPickerApi/GetImageCropsDataForDataType", config),
                "Failed to retrieve crops data");
        }
    };
})();

