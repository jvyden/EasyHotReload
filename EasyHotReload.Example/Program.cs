using EasyHotReload;
using EasyHotReload.Example;

// Create an object implementing IHotReloadable
using ExampleHotReloadableImplementation reloadableImplementation = new();

// Add this object to the registry so that EasyHotReload knows to call the function.
// You can also just do this in the constructor - this call is here to demonstrate that it's necessary.
// It's also good practice to UnregisterReloadable to avoid null reference problems. This is done in the implementation via IDisposable.
HotReloadRegistry.RegisterReloadable(reloadableImplementation);

Console.WriteLine(1); // change this while running to test hot reload!

await Task.Delay(-1); // Sleep to allow demonstration

