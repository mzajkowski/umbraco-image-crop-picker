using Newtonsoft.Json;
using System;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Our.Umbraco.ImageCropPicker.Converters
{
    public class ImageCropPickerValueConverter : PropertyValueConverterBase
    {
        private const string EditorAlias = "Our.Umbraco.ImageCropPicker";
        private static readonly Type ImageCropperValue = typeof(ImageCropperConfiguration.Crop);
        private readonly ILogger _logger;

        public ImageCropPickerValueConverter(ILogger logger)
            => _logger = logger;

        public override bool IsConverter(IPublishedPropertyType propertyType)
            => propertyType.EditorAlias.InvariantEquals(EditorAlias);

        public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType)
            => PropertyCacheLevel.Snapshot;

        public override Type GetPropertyValueType(IPublishedPropertyType propertyType)
            => ImageCropperValue;

        public override object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType,
            PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
            => inter as ImageCropperConfiguration.Crop;

        public override object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType,
            object source, bool preview)
        {
            try
            {
                if (source != null && !source.ToString().IsNullOrWhiteSpace())
                {
                    return JsonConvert.DeserializeObject<ImageCropperConfiguration.Crop>(source.ToString());
                }
            }
            catch (Exception e)
            {
                _logger.Error<ImageCropPickerValueConverter>("Error converting value", e);
            }

            return null;
        }
    }
}