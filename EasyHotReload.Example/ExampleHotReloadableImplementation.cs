namespace EasyHotReload.Example;

using EasyHotReload;

public class ExampleHotReloadableImplementation : IHotReloadable, IDisposable
{
    private int Value { get; set; } = 1;
    
    // This function will be called whenever a hot reload is triggered.
    public void ProcessHotReload()
    {
        Console.WriteLine($"{this.GetType().Name} has been hot reloaded {Value++} times");
    }

    // Here, we use a disposal method to unregister the object to avoid null reference problems.
    public void Dispose()
    {
        HotReloadRegistry.UnregisterReloadable(this);
        GC.SuppressFinalize(this);
    }
}