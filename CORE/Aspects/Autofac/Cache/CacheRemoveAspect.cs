using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Cache
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheService _cacheService;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheService = ServiceTool.ServiceProvider.GetService<ICacheService>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheService.RemoveByPattern(_pattern);
        }
    }
}
