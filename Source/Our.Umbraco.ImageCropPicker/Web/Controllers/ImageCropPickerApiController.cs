using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.Editors;

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

        public IEnumerable<object> GetDataTypes()
        {
            return _dataTypeService.GetAllDataTypeDefinitions();
        }

        [HttpGet]
        public IEnumerable<object> GetImageCropsDataForDataType(int id)
        {
            var imageCropperPreValuesCollection =
                _dataTypeService.GetPreValuesCollectionByDataTypeId(id);

            PreValue crops;
            if (!imageCropperPreValuesCollection.PreValuesAsDictionary.TryGetValue(CropsAlias, out crops))
                return null;

            var cropsDataList = JsonConvert.DeserializeObject<List<ImageCropData>>(crops.Value);
            return cropsDataList;
        }
    }
}
