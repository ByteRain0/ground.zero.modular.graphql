using GreenDonut;
using HotChocolate;

[assembly: Module("MangaServiceTypes")]
[assembly: DataLoaderDefaults(
    ServiceScope = DataLoaderServiceScope.DataLoaderScope)]