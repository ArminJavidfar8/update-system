# Update System for Unity

A lightweight and efficient **C# update service** for Unity, designed to help you manage  
`Update`, `FixedUpdate`, and `LateUpdate` calls in a clean, decoupled manner without requiring  
every object to inherit from `MonoBehaviour`.

## Why This System?

In Unity, managing update loops for many objects can sometimes lead to cluttered `MonoBehaviour` scripts or the need for constant `GetComponent` calls.  
This **Update System** provides a centralized, performance-conscious approach to handle your update logic, allowing you to:

- **Decouple Update Logic**: Keep your game logic separate from `MonoBehaviour` lifecycle methods.  
- **Centralized Control**: Register and unregister update actions from a single service.  
- **Clean Architecture**: Promotes a cleaner code structure by enabling Plain Old C# Objects to participate in Unity's update loop.

## Getting Started

### Installation
1. Clone this repository into your Unity project.  
2. Ensure the following namespaces are accessible in your project:
   - `YekGames.UpdateService.Abstraction`  
   - `YekGames.UpdateService.Core`  

### Usage

#### 1. Initialize the Update Service
You'll typically want to initialize the `UpdateService` once, early in your application's lifecycle (e.g., in a bootstrapping script or a dependency injection container).  
The `UpdateService` automatically handles creating and persisting the necessary `GameObject`.

```csharp
using YekGames.UpdateService.Abstraction;
using YekGames.UpdateService.Core;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public IUpdateService UpdateService;

    void Awake()
    {
        UpdateService = new UpdateService();
    }
}
```
### 2. Registering Update Actions

Any class can register methods to be called during `Update`, `FixedUpdate`, or `LateUpdate`.
```csharp
using YekGames.UpdateService.Abstraction;
using System;

public class MyGameLogic
{
    private IUpdateService _updateService;

    public MyGameLogic(IUpdateService updateService)
    {
        _updateService = updateService;
        
        _updateService.RegisterUpdate(DoSomethingOnUpdate);
        _updateService.RegisterFixedUpdate(DoSomethingOnFixedUpdate);
        _updateService.RegisterLateUpdate(DoSomethingOnLateUpdate);
    }

    private void DoSomethingOnUpdate()
    {
        // Debug.Log($"Update called");
    }

    private void DoSomethingOnFixedUpdate()
    {
        // Debug.Log("FixedUpdate called!");
        // Your physics or fixed-time-step logic here
    }

    private void DoSomethingOnLateUpdate()
    {
        // Debug.Log("LateUpdate called!");
        // Your camera or post-processing logic here
    }

    public void Cleanup()
    {
        // Always remember to unregister your actions to prevent memory leaks!
        _updateService.UnRegisterUpdate(DoSomethingOnUpdate);
        _updateService.UnRegisterFixedUpdate(DoSomethingOnFixedUpdate);
        _updateService.UnRegisterLateUpdate(DoSomethingOnLateUpdate);
    }
}
```
## Best Practices

- **Unregister**: Always unregister your actions when an object or system is no longer active (e.g., in `OnDestroy`, `OnDisable`, or a `Dispose` method).  
  Failing to do so can lead to memory leaks and `NullReferenceException` errors if the unregistered object is garbage collected but its action is still being invoked.

- **Dependency Injection**: Consider using a simple Dependency Injection (DI) pattern to provide the `IUpdateService` instance to your classes, making your code more testable and modular.

- **Performance**: While optimized, be mindful of registering an excessive number of actions that perform heavy computations, as this can still impact performance.
