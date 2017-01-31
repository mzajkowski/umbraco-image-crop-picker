using System;
using Newtonsoft.Json;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web.Models;

namespace Our.Umbraco.ImageCropPicker.Converters
{
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    [PropertyValueType(typeof(ImageCropData))]
    public class ImageCropPickerValueConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.InvariantEquals("Our.Umbraco.ImageCropPicker");
        }

        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            try
            {
                if (source != null && !source.ToString().IsNullOrWhiteSpace())
                {
                    return JsonConvert.DeserializeObject<ImageCropData>(source.ToString());
                }
            }
            catch (Exception e)
            {
                LogHelper.Error<ImageCropPickerValueConverter>("Error converting value", e);
            }

            return null;
        }
    }
}
