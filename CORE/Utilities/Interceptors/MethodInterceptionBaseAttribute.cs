﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {

        public int Priority { get; set; }  //    determine priority of the interceptor
        public virtual void Intercept(IInvocation invocation)
        {

        }

    }
}
