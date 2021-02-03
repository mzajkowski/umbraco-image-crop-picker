using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Editors;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Our.Umbraco.ImageCropPicker.Web.Controllers
{
    [PluginController("ImageCropPicker")]
    public class ImageCropPickerApiController : UmbracoAuthorizedJsonController
    {
        private readonly IDataTypeService _dataTypeService;
        private const string CropsAlias = "crops";

        public ImageCropPickerApiController()
        {
            _dataTypeService = Services.DataTypeService;
        }

        public IEnumerable<IDataTypeDefinition> GetDataTypes()
        {
            return _dataTypeService.GetAllDataTypeDefinitions()
                .Where(dataType => dataType.PropertyEditorAlias
                    .InvariantEquals(Constants.PropertyEditors.ImageCropperAlias))
                .ToArray();
        }

        [HttpGet]
        public IEnumerable<ImageCropData> GetImageCropsDataForDataType(Guid dataTypeKey)
        {
            if (dataTypeKey == default(Guid))
            {
                return Enumerable.Empty<ImageCropData>();
            }

            var dataTypeDefinition = _dataTypeService
                .GetDataTypeDefinitionByPropertyEditorAlias(Constants.PropertyEditors.ImageCropperAlias)
                .FirstOrDefault(dataType => dataType.Key.Equals(dataTypeKey));

            if (dataTypeDefinition == null)
            {
                return Enumerable.Empty<ImageCropData>();
            }

            var imageCropperPreValuesCollection = _dataTypeService.GetPreValuesCollectionByDataTypeId(dataTypeDefinition.Id);

            if (!imageCropperPreValuesCollection.PreValuesAsDictionary.TryGetValue(CropsAlias, out var crops))
            {
                return Enumerable.Empty<ImageCropData>();
            }

            var cropsDataList = JsonConvert.DeserializeObject<List<ImageCropData>>(crops.Value);

            return cropsDataList;
        }
    }
}