using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml;

namespace Pitman.Paged.Tests
{
    public class PagedResultTests
    {
        [Fact]
        public async Task PagedResult_GetResultsAsync_ReturnsPagedResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MyDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;

            using (var dbContext = new MyDbContext(options))
            {
                for (int i = 0; i < 10; i++)
                {
                    dbContext.MyEntities.Add(new MyEntity { Id = new Guid(), Name = $"Entity {i}" });
                }

                dbContext.SaveChanges();
            }

            var expectedPageSize = 3;
            var expectedPageCount = 4;
            var expectedCurrentPage = 1;
            var expectedTotalItemCount = 10;

            using (var dbContext = new MyDbContext(options))
            {
                // Act
                var pagedResult = await PagedResult<MyEntity>.GetResultsAsync(dbContext.MyEntities, expectedCurrentPage, expectedPageSize);

                // Assert
                Assert.Equal(expectedPageSize, pagedResult.PageSize);
                Assert.Equal(expectedPageCount, pagedResult.PageCount);
                Assert.Equal(expectedCurrentPage, pagedResult.CurrentPage);
                Assert.Equal(expectedTotalItemCount, pagedResult.TotalItemCount);
                Assert.Equal(expectedPageSize, pagedResult.Results.Count);
            }
        }

        [Fact]
        public async Task PagedResultExtensions_GetPaged_ReturnsPagedResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MyDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;

            using (var dbContext = new MyDbContext(options))
            {
                for (int i = 0; i < 10; i++)
                {
                    dbContext.MyEntities.Add(new MyEntity { Id = new Guid(), Name = $"Entity {i}" });
                }

                dbContext.SaveChanges();
            }
            var expectedPageSize = 3;
            var expectedPageCount = 4;
            var expectedCurrentPage = 1;
            var expectedTotalItemCount = 10;

            using (var dbContext = new MyDbContext(options))
            {
                // Act
                var pagedResult = await dbContext.MyEntities.GetPaged(expectedCurrentPage, expectedPageSize);

                // Assert
                Assert.Equal(expectedPageSize, pagedResult.PageSize);
                Assert.Equal(expectedPageCount, pagedResult.PageCount);
                Assert.Equal(expectedCurrentPage, pagedResult.CurrentPage);
                Assert.Equal(expectedTotalItemCount, pagedResult.TotalItemCount);
                Assert.Equal(expectedPageSize, pagedResult.Results.Count);
            }
        }

        [Fact]
        public async Task PagedResultExtensions_GetPaged_WithSelector_ReturnsPagedResult()
        {
            // Arrange
            var mockList = new List<string>() { "a", "b", "c", "d", "e" };
            var mockPagedResult = new PagedResult<string>(mockList, 5, 1, 5);
            Expression<Func<string, int>> selector = s => s.Length;
            var expectedPageSize = 5;
            var expectedPageCount = 1;
            var expectedCurrentPage = 1;
            var expectedTotalItemCount = 5;

            // Act
            var pagedResult = await mockPagedResult.GetPaged(selector);

            // Assert
            Assert.Equal(expectedPageSize, pagedResult.PageSize);
            Assert.Equal(expectedPageCount, pagedResult.PageCount);
            Assert.Equal(expectedCurrentPage, pagedResult.CurrentPage);
            Assert.Equal(expectedTotalItemCount, pagedResult.TotalItemCount);
            Assert.Equal(expectedPageSize, pagedResult.Results.Count);
        }

    }
}