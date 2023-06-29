using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Abstract.RepositoryManager;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.RepositoryManager;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {

        //  override load method

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<CarManager>().As<ICarService>().InstancePerLifetimeScope();
            builder.RegisterType<EfCarDal>().As<ICarDal>().InstancePerLifetimeScope();

            builder.RegisterType<BrandManager>().As<IBrandService>().InstancePerLifetimeScope();
            builder.RegisterType<EfBrandDal>().As<IBrandDal>().InstancePerLifetimeScope();

            builder.RegisterType<ColorManager>().As<IColorService>().InstancePerLifetimeScope();
            builder.RegisterType<EfColorDal>().As<IColorDal>().InstancePerLifetimeScope();

            builder.RegisterType<CustomerManager>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>().InstancePerLifetimeScope();

            builder.RegisterType<RentalManager>().As<IRentalService>().InstancePerLifetimeScope();
            builder.RegisterType<EfRentalDal>().As<IRentalDal>().InstancePerLifetimeScope();

            builder.RegisterType<UserManager>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<EfUserDal>().As<IUserDal>().InstancePerLifetimeScope();

            builder.RegisterType<UserManager>().As<ITokenService>().InstancePerLifetimeScope();

            builder.RegisterType<CarImageManager>().As<ICarImageService>().InstancePerLifetimeScope();
            builder.RegisterType<EfCarImageDal>().As<ICarImageDal>().InstancePerLifetimeScope();

            builder.RegisterType<CartItemManager>().As<ICartItemService>().InstancePerLifetimeScope();
            builder.RegisterType<EfCartItemDal>().As<ICartItemDal>().InstancePerLifetimeScope();

            builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerLifetimeScope();

            builder.RegisterType<RepositoryManager>().As<IRepositoryManager>().InstancePerLifetimeScope();



            builder.RegisterType<FileHelper>().As<IFileHelper>().SingleInstance();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();





            //  get executing assembly and find the interceptors into it then call aspect interceptor selector

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).InstancePerLifetimeScope();

        }

    }
}
