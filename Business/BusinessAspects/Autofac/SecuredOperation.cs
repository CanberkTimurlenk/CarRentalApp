using Business.Constants;
using Core.Extensions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;


namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        //  istek yapan her bir kişiye bir tane thread bir tane http context oluşur
        //  aynı anda binlerce kişi istek yapabilir sistemimize

        public SecuredOperation(string roles)
        {

            _roles = roles.Split(','); // rollerimiz attribute olduğu için virgülle ayrılarak geliyor.
            //  .split string bir metni bizim belirlediğimiz karaktere göre ayırıp bir array e atıyor (string array)
            //  örneğin product manager da yazdığım admin,editor de .. bu virgülle ayrılan iki kelime string array in birer elemanı oluyor

            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            // kullanıcının rollerini gez eğer claimlerinin içinde ilgili rol varsa metodu çalıştırmaya devam et(return et)
            // yoksa authorization denied, yetkin yok hatası ver.. 

            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
