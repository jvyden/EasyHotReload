using System.Diagnostics;

namespace EasyHotReload;

/// <summary>
/// The registry of objects that can be hot reloaded.
/// Call RegisterReloadable to initialize an object into the registry, and call UnregisterReloadable when the object is disposed or otherwise collected.
/// </summary>
public static class HotReloadRegistry
{
    /// <summary>
    /// The list of objects to notify when a hot reload occurs.
    /// </summary>
    private static readonly List<IHotReloadable> HotReloadableObjects = new(1);
    /// <summary>
    /// A lock object, used to make this thread-safe.
    /// </summary>
    private static readonly object ListLock = new();

    internal static void ProcessHotReload()
    {
        lock (ListLock)
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
    }

    /// <summary>
    /// Register a reloadable object, signing it up to receive an event when a hot reload occurs.
    /// </summary>
    public static void RegisterReloadable(IHotReloadable reloadable)
    {
        lock (ListLock)
        {
            HotReloadableObjects.Add(reloadable);
        }
    }

    /// <summary>
    /// Remove a reloadable object from the registry. Call this when the object is disposed or otherwise collected.
    /// </summary>
    /// <returns>
    /// true if item is successfully removed; otherwise, false. This method also returns false if item was not found in the internal list.
    /// </returns>
    public static bool UnregisterReloadable(IHotReloadable reloadable)
    {
        lock (ListLock)
        {
            return HotReloadableObjects.Remove(reloadable);
        }
    }
}