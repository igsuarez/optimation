using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using ProcessingService.Api.Application.Commands;
using ProcessingService.Api.Infrastructure.Services;

namespace ProcessingService.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule        : Autofac.Module
    {       

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<XmlService>()
                .As<IXmlService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TaxesService>()
                .As<ITaxesService>()
                .InstancePerLifetimeScope();

        }
    }
}
