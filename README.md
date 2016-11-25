# Core

```
install-package Coco
```

Create the type that you wish to retrieve:

```csharp
public class UserDto
{
  public string Name { get; set; }
}
```

Coco provides an `ICocoSource<TDto>` type with a `Task<IEnumerable<TDto>> Retrieve` method for retrieving collections of your type:

```csharp
public class StubUserSource : ICocoSource<UserDto>
{
  public async Task<IEnumerable<UserDto>> Retrieve()
  {
    return await Task.FromResult(new[] { new UserDto { Name = "Test" } });
  }
}
```

You can use the `CocoBuilder` to build a runner to retrieve your items:

```csharp
var runner = new CocoBuilder<UserDto>().FromSource(new StubUserSource()).Build();
var users = await runner.Retrieve();
foreach (var user in users)
{
  System.Console.WriteLine(user.Name);
}
```

> Note that the `CocoBuilder` doesn't currently add any functionality over invoking the `Retrieve` method directly on your source, but new functionality is coming soon...
