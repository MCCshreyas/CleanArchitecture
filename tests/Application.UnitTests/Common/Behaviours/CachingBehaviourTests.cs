using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UnitTests.Common.Behaviours
{
    public class CachingBehaviourTests
    {
        private Mock<ICacheService> cacheService;

        [SetUp]
        public void Setup()
        {
            cacheService = new Mock<ICacheService>();
        }

        [Test]
        public async Task ShouldCallGetCacheValueAsyncWhenRequestInheritsIsICache()
        {
            var logger = Mock.Of<ILogger<CachingBehaviour<GetCachedQuery, TodosVm>>>();
            var cachingBehaviour = new CachingBehaviour<GetCachedQuery, TodosVm>(cacheService.Object, logger);
            var requestHandlerDelegate = Mock.Of<RequestHandlerDelegate<TodosVm>>();

            GetCachedQuery request = new GetCachedQuery();
            await cachingBehaviour.Handle(request, new System.Threading.CancellationToken(), requestHandlerDelegate);


            cacheService.Verify(c=>c.GetCacheValueAsync($"CustomKey"),Times.Once);
        }


        [Test]
        public async Task ShouldNotCallGetCacheValueAsyncWhenRequestDoesNotInheritsIsICache()
        {
            var logger = Mock.Of<ILogger<CachingBehaviour<GetNonCachedQuery, TodosVm>>>();
            var cachingBehaviour = new CachingBehaviour<GetNonCachedQuery, TodosVm>(cacheService.Object, logger);
            var requestHandlerDelegate = Mock.Of<RequestHandlerDelegate<TodosVm>>();

            GetNonCachedQuery request = new GetNonCachedQuery();
            await cachingBehaviour.Handle(request, new System.Threading.CancellationToken(), requestHandlerDelegate);

            cacheService.Verify(c => c.GetCacheValueAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task ShouldNotCallSetCacheValueAsyncWhenObjectInCache()
        {
            var logger = Mock.Of<ILogger<CachingBehaviour<GetCachedQuery, TodosVm>>>();
            var cachingBehaviour = new CachingBehaviour<GetCachedQuery, TodosVm>(cacheService.Object, logger);
            var requestHandlerDelegate = Mock.Of<RequestHandlerDelegate<TodosVm>>();

            GetCachedQuery request = new GetCachedQuery();

            object returnedFromCache = new TodosVm();

            cacheService.Setup(c=>c.GetCacheValueAsync($"CustomKey")).Returns(Task.FromResult(returnedFromCache));

            await cachingBehaviour.Handle(request, new System.Threading.CancellationToken(), requestHandlerDelegate);


            cacheService.Verify(c => c.SetCacheValueAsync($"CustomKey", returnedFromCache, TimeSpan.FromMilliseconds(60000)), Times.Never);
        }

        [Test]
        public async Task ShouldCallSetCacheValueAsyncWhenObjectNotInCache()
        {
            var logger = Mock.Of<ILogger<CachingBehaviour<GetCachedQuery, TodosVm>>>();
            var cachingBehaviour = new CachingBehaviour<GetCachedQuery, TodosVm>(cacheService.Object, logger);
            var requestHandlerDelegate = new Mock<RequestHandlerDelegate<TodosVm>>();

            GetCachedQuery request = new GetCachedQuery();

            object returnedFromCache = null;
            TodosVm toBeCached = new TodosVm();

            cacheService.Setup(c => c.GetCacheValueAsync($"CustomKey")).Returns(Task.FromResult(returnedFromCache));
            requestHandlerDelegate.Setup(r=>r.Invoke()).Returns(Task.FromResult(toBeCached));

            await cachingBehaviour.Handle(request, new System.Threading.CancellationToken(), requestHandlerDelegate.Object);


            cacheService.Verify(c => c.SetCacheValueAsync($"CustomKey", toBeCached, TimeSpan.FromMilliseconds(30000)), Times.Once);
        }

        [Test]
        public async Task ShouldCallSetCacheValueAsyncWithDefaultValuesWhenCachingOptionsIsNotSet()
        {
            var logger = Mock.Of<ILogger<CachingBehaviour<GetCachedNoCacheOptionsQuery, TodosVm>>>();
            var cachingBehaviour = new CachingBehaviour<GetCachedNoCacheOptionsQuery, TodosVm>(cacheService.Object, logger);
            var requestHandlerDelegate = new Mock<RequestHandlerDelegate<TodosVm>>();

            GetCachedNoCacheOptionsQuery request = new GetCachedNoCacheOptionsQuery();

            object returnedFromCache = null;
            TodosVm toBeCached = new TodosVm();

            var cacheKey = CacheHelper.GenerateCacheKeyFromRequest(request);

            cacheService.Setup(c => c.GetCacheValueAsync(cacheKey)).Returns(Task.FromResult(returnedFromCache));
            requestHandlerDelegate.Setup(r => r.Invoke()).Returns(Task.FromResult(toBeCached));

            await cachingBehaviour.Handle(request, new System.Threading.CancellationToken(), requestHandlerDelegate.Object);

            cacheService.Verify(c => c.SetCacheValueAsync(cacheKey, toBeCached, TimeSpan.FromMilliseconds(60000)), Times.Once);
        }


    }
}
