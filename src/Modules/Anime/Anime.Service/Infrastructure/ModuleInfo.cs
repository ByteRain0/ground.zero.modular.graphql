using GreenDonut;
using HotChocolate;

[assembly: Module("AnimeServiceTypes")]
[assembly: DataLoaderDefaults(
    ServiceScope = DataLoaderServiceScope.DataLoaderScope)]