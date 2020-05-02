using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UnitTests.Common.Behaviours
{
    public class CachingPreProcessorTests
    {
        private Mock<ICacheService> cacheService;

        [SetUp]
        public void Setup()
        {
            cacheService = new Mock<ICacheService>();

        }

        [Test]
        public async Task ShouldNotCallRemoveCacheValueWhenInvalidateCacheForQueriesHasNoEntries()
        {
            var logger = Mock.Of<ILogger<CacheInvalidatorPostProcessor<GetCachedQuery, TodosVm>>>();
            InvalidateCacheForQueries invalidateCacheForQueries = new InvalidateCacheForQueries();

            var cacheInvalidatorPostProcessor = new  CacheInvalidatorPostProcessor<GetCachedQuery, TodosVm>(logger, invalidateCacheForQueries, cacheService.Object);

            await cacheInvalidatorPostProcessor.Process(new GetCachedQuery(), new TodosVm(), new CancellationToken());

            cacheService.Verify(c=>c.RemoveCacheValue(It.IsAny<string>()), Times.Never);
        }

        [TestCase(new Type[]{typeof(GetCachedQuery) },1)]
        [TestCase(new Type[]{ typeof(GetCachedQuery), typeof(string) },2)]
        [TestCase(new Type[]{ typeof(GetCachedQuery), typeof(string), typeof(int) },3)]
        public async Task ShouldCallRemoveCacheValueWhenInvalidateCacheForQueriesHasMultipleEntries(Type[] keys, int expected)
        {
            var logger = Mock.Of<ILogger<CacheInvalidatorPostProcessor<GetCachedQuery, TodosVm>>>();
            InvalidateCacheForQueries invalidateCacheForQueries = new InvalidateCacheForQueries();

            foreach (var key in keys)
            {
                invalidateCacheForQueries.Add(key, new GetCachedQuery());
            }

            var cacheInvalidatorPostProcessor = new CacheInvalidatorPostProcessor<GetCachedQuery, TodosVm>(logger, invalidateCacheForQueries, cacheService.Object);

            await cacheInvalidatorPostProcessor.Process(new GetCachedQuery(), new TodosVm(), new CancellationToken());

            cacheService.Verify(c => c.RemoveCacheValue(It.IsAny<string>()), Times.Exactly(expected));
        }
    }
}
