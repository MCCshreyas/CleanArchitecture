using CleanArchitecture.Application.Common.Behaviours;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.Common.Behaviours
{
    public class CacheHelperTests
    {
        [Test]
        public void ShouldReturnDefaultCacheKeyWhenCacheOptionsAreNotOverridden()
        {
            GetCachedNoCacheOptionsQuery request = new GetCachedNoCacheOptionsQuery();
            CacheHelper.GenerateCacheKeyFromRequest(request).Should().Be($"{request.GetType().Name}|");
        }

        [Test]
        public void ShouldReturnDefaultCacheKeyWhenCacheOptionsAreNotOverriddenAndQueryHasParameters()
        {
            GetCachedWithParametersQuery request = new GetCachedWithParametersQuery { Id = 1 };
            CacheHelper.GenerateCacheKeyFromRequest(request).Should().Be($"{request.GetType().Name}|Id| 1|");
        }
        
        [Test]
        public void ShouldReturnDefinedCacheKeyWhenCacheOptionsAreOverridden()
        {
            GetCachedQuery request = new GetCachedQuery();

            CacheHelper.GenerateCacheKeyFromRequest(request).Should().Be($"CustomKey");
        }
    }
}
