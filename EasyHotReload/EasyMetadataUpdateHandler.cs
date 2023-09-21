using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using EasyHotReload;

[assembly: MetadataUpdateHandler(typeof(EasyMetadataUpdateHandler))]

namespace EasyHotReload;

/// <summary>
/// A receiver for updates of type metadata.
/// For more information, see https://learn.microsoft.com/en-us/dotnet/api/system.reflection.metadata.metadataupdatehandlerattribute
/// </summary>

// these all have to do with implicit use, but we can't import JetBrains.Annotations
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "ParameterTypeCanBeEnumerable.Global")]
internal class EasyMetadataUpdateHandler
{
    /// <summary>
    /// Called when a hot reload is triggered and the program's metadata has updated.
    /// </summary>
    /// <param name="updatedTypes">The types affected by the metadata update. If null, any type may have been updated.</param>
    public static void UpdateApplication(Type[]? updatedTypes)
    {
        HotReloadRegistry.ProcessHotReload();
    } 
}