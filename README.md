# Retrieving Items

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

You can use `CocoBuilder<TDto>` to build a runner to retrieve your items:

```csharp
var runner = new CocoBuilder<UserDto>().FromSource(new StubUserSource()).Build();
var users = await runner.Retrieve();
foreach (var user in users)
{
  System.Console.WriteLine(user.Name);
}
```

> Note that the `CocoBuilder` doesn't currently add any functionality over invoking the `Retrieve` method directly on your source, but new functionality is coming soon...

# Web Content

The `Coco.Web` package contains a Coco source for retrieving items from the web.

```
install-package Coco.Web
```

You can retrieve items from a single URI using the provided `WebUriSource<TDto>`. First, define your DTO:

```csharp
public class GoogleResult
{
  public string Title { get; set; }
  public string Description { get; set; }
}
```

Then define your web URI source:

```csharp
var source = new WebUriSource<GoogleResult>(
  new Uri("https://www.google.co.uk/search?q=foo"), 
  new GoogleResultConfiguration());
                
// invoke Retrieve on source, or use with CocoBuilder
```

The `WebUriSource<TDto>` takes the `Uri` of the page that you wish to retrieve items, and an implementation of `IWebEntityConfiguration`. The provided `WebEntityConfiguration` is easier to use. Derive your configuration from this type, and use the provided `Item` and `Property` methods to define how to retrieve the item data from the HTML:

```csharp
public class GoogleResultConfiguration : WebEntityConfiguration<GoogleResult>
{
  public GoogleResultConfiguration()
  {
    this.Item().InnerHtml("div.g");
    this.Property(r => r.Title).InnerHtml("h3.r a");
    this.Property(r => r.Description).InnerHtml("span.st");
  }
}
```

> Note the `InnerHtml` method will return the inner HTML of the element selected with the specified CSS selector. It will attempt to convert the inner HTML to the property type on your DTO.

If you wish to retrieve items over several URIs, you can use the `WebUriPagedSource<TDto>`:

```csharp
var source = new WebUriPagedSource<GoogleResult>(                
  new Uri("https://www.google.co.uk/search?q=foo"),
  new GoogleResultConfiguration(), 
  new GooglePagingConfiguration());
```

The `WebUriPagedSource` takes an implementation of `IPagingConfiguration`, this provides the information needed to perform paging:

```csharp
public interface IPagingConfiguration
{
  PageDetails GetPageDetails(string content);
  int GetNextPage(PageDetails pageDetails, int currentPage);
  string PageSizeParamName { get; }
  string CurrentPageParamName { get; }
}
```

```csharp
public class GooglePagingConfiguration : IPagingConfiguration
{
  public PageDetails GetPageDetails(string content)
  {
    return new PageDetails(10, 0, 30);
  }

  public int GetNextPage(PageDetails pageDetails, int currentPage)
  {
    return new IncrementPageByPageSizeCalculator().GetNextPage(pageDetails, currentPage);
  }

  public string PageSizeParamName => null;

  public string CurrentPageParamName => "start";
}
```

> Note that Coco provides an `IncrementPageByPageSizeCalculator` and `IncrementPageByOneCalculator`
