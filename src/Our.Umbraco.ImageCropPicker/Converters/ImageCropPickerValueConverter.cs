using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;

namespace Our.Umbraco.ImageCropPicker.Converters
{
    public class ImageCropPickerValueConverter : PropertyValueConverterBase
    {
        private readonly IEnumerable<ImageCropperConfiguration.Crop> _crops;

        public ImageCropPickerValueConverter(IDataTypeService dataTypeService)
            => _crops = dataTypeService
                .GetByEditorAlias(Constants.PropertyEditors.Aliases.ImageCropper)
                .Select(x => x.Configuration as ImageCropperConfiguration)
                .Where(x => x != null)
                .SelectMany(x => x.Crops)
                .ToArray();

        private const string EditorAlias = "Our.Umbraco.ImageCropPicker";

        private static readonly Type ImageCropperValue = typeof(ImageCropperConfiguration.Crop);

        public override bool IsConverter(PublishedPropertyType propertyType)
            => propertyType.EditorAlias.InvariantEquals(EditorAlias);

        public override PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType)
            => PropertyCacheLevel.Snapshot;

        public override Type GetPropertyValueType(PublishedPropertyType propertyType)
            => ImageCropperValue;

        public override object ConvertIntermediateToObject(IPublishedElement owner, PublishedPropertyType propertyType,
            PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
            => inter as ImageCropperConfiguration.Crop;

        public override object ConvertSourceToIntermediate(IPublishedElement owner, PublishedPropertyType propertyType,
            object source, bool preview)
            => _crops.FirstOrDefault(x => x.Alias.Equals(source.ToString()));
    }
}