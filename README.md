# EasyHotReload
A .NET package that lets you easily register for Hot Reload events.

Works with `dotnet watch` and JetBrains Rider in addition to other environments that support hot reload.

## Usage

First, define a class that implements `IHotReloadable`:
```csharp
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
```

Then, initialize the object and register it:
```csharp
using ExampleHotReloadableImplementation reloadableImplementation = new();

// Add this object to the registry so that EasyHotReload knows to call the function.
// You can also just do this in the constructor - this call is here to demonstrate that it's necessary.
// It's also good practice to UnregisterReloadable to avoid null reference problems. This is done in the implementation via IDisposable.
HotReloadRegistry.RegisterReloadable(reloadableImplementation);
```

The registry is thread-safe, so don't worry about calling from multiple threads.
Just beware that hot reload events can come through any of the ThreadPool worker threads.