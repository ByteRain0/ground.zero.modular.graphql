using GreenDonut;

[assembly: DataLoaderModule("AnimeDataLoader")]
[assembly: DataLoaderDefaults(
    ServiceScope = DataLoaderServiceScope.DataLoaderScope,
    AccessModifier = DataLoaderAccessModifier.PublicInterface)]
