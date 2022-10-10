using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { } //   e is a member of system.exception
        protected virtual void OnSuccess(IInvocation invocation) { } // invocation is the method that will be used (in business add, delete) etc.

        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;

            OnBefore(invocation);

            try
            {
                invocation.Proceed();

            }

            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;

            }

            finally
            {
                if(isSuccess)
                {
                    OnSuccess(invocation);

                }
            }

            OnAfter(invocation);

        }








    }
}
