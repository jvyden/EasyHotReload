using System.Diagnostics;

namespace EasyHotReload;

/// <summary>
/// A registry containing objects that can be hot reloaded.
/// </summary>
public static class HotReloadRegistry
{
    private static readonly List<IHotReloadable> HotReloadableObjects = new(1);

    internal static void ProcessHotReload()
    {
        Debug.WriteLine($"Processing {HotReloadableObjects.Count} reloadable objects");
        int i = 0;
        foreach (IHotReloadable reloadable in HotReloadableObjects)
        {
            i++;
            Debug.WriteLine($"  ({i}/{HotReloadableObjects.Count}) Reloading {reloadable.GetType().Name}...");
            reloadable.ProcessHotReload();
        }
        
        Debug.WriteLine("Done hot reloading.");
    }

    /// <summary>
    /// Register a reloadable object, signing it up to receive an event when a hot reload occurs.
    /// </summary>
    public static void RegisterReloadable(IHotReloadable reloadable)
    {
        HotReloadableObjects.Add(reloadable);
    }

    /// <summary>
    /// Remove a reloadable object from the registry. Call this when the object is disposed or collected.
    /// </summary>
    public static void UnregisterReloadable(IHotReloadable reloadable)
    {
        HotReloadableObjects.Remove(reloadable);
    }
}