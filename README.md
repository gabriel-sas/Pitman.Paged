# Pitman.Paged

This code provides a flexible and easy-to-use implementation of paged results for use in .NET applications. The IPagedResult<T> interface defines the properties necessary for implementing paged results, and the PagedResult<T> class provides a concrete implementation of this interface. Additionally, the PagedResultExtensions class offers extension methods for easily retrieving paged results from IQueryable and IIncludableQueryable sources, as well as converting the results of one PagedResult<T> to another PagedResult<TProperty>. This package is useful for applications that require efficient handling of large datasets and pagination of query results.

## Usage

You can use "Pitman.Paged" in your .NET project by installing the "Pitman.Paged" NuGet package.

## Installation

You can install "Pitman.Paged" by running the following command in the Package Manager Console:

Install-Package Pitman.Paged

## Example

Here's an example of how to use "Pitman.Paged" to paginate an IQueryable collection of entities.

### Using Asp.net
On a controller add "int page, int pageSize". In the URL add the parameters "page" with the current page number and "pageSize" with the size of each page.

Example of an url sorting the by temperatureC as property name:
```
https://localhost:7201/customers?page=2&pageSize=10
```

#### Full Example
```
[HttpGet]
public async Task<IActionResult> Get(int page, int pageSize) // receive paging information from url parameters
{
    var queryable = dbContext.Customers.AsQueryable();
    
    pagedResult = await queryable.GetPaged(page, pageSize); // apply the paging on the IQueryable

    return Ok(pagedResult);
}
```

## Contributing
Contributions to "Pitman.Paged" are welcome and encouraged! If you find a bug or have a feature request, please create an issue in the repository. If you'd like to contribute code to the project, please open a pull request with your changes.

## License
"Pitman.Paged" is released under the MIT License. See LICENSE for details.
