using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace Our.Umbraco.ImageCropPicker.Web.Controllers
{
    [PluginController("ImageCropPicker")]
    public class ImageCropPickerApiController : UmbracoAuthorizedJsonController
    {
        private readonly IDataTypeService _dataTypeService;

        public ImageCropPickerApiController(IDataTypeService dataTypeService)
            => _dataTypeService = dataTypeService;

        public IEnumerable<IDataType> GetDataTypes()
            => _dataTypeService.GetAll()
                .Where(dataType => dataType.EditorAlias.InvariantEquals(Constants.PropertyEditors.Aliases.ImageCropper))
                .ToArray();

        [HttpGet]
        public IEnumerable<ImageCropperConfiguration.Crop> GetImageCropsDataForDataType(Guid dataTypeKey)
        {
            if (dataTypeKey == default(Guid))
            {
                return Enumerable.Empty<ImageCropperConfiguration.Crop>();
            }

            var dataType = _dataTypeService.GetDataType(dataTypeKey);

            return dataType?.Configuration == null
                ? Enumerable.Empty<ImageCropperConfiguration.Crop>()
                : (dataType.Configuration as ImageCropperConfiguration).Crops
                    ?? Enumerable.Empty<ImageCropperConfiguration.Crop>();
        }
    }
}