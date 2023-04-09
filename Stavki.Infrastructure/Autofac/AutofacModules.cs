using Autofac;
using Microsoft.EntityFrameworkCore;
using Stavki.Infrastructure.EF;
using Stavki.Infrastructure.EF.EF;
using Stavki.Infrastructure.Services;
using Stavki.Infrastructure.Services.Interfaces;

namespace Stavki.Infrastructure.Autofac
{
    public class AutofacModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationContext>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<RequestService>().As<IRequestService>().InstancePerRequest();
            builder.RegisterType<CalcService>().As<ICalcService>().InstancePerRequest();
        }
    }
}
