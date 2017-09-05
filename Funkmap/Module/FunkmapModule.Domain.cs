﻿using System;
using System.Linq;
using Autofac;
using Funkmap.Data.Repositories;
using Funkmap.Data.Repositories.Abstract;
using Funkmap.Data.Services;
using Funkmap.Data.Services.Abstract;
using Funkmap.Services;
using Funkmap.Tools;
using Funkmap.Tools.Abstract;

namespace Funkmap.Module
{
    public partial class FunkmapModule 
    {
        private void RegisterDomainDependiences(ContainerBuilder builder)
        {
            builder.RegisterType<BaseRepository>().As<IBaseRepository>().SingleInstance();
            builder.RegisterType<MusicianRepository>().As<IMusicianRepository>().SingleInstance();
            builder.RegisterType<BandRepository>().As<IBandRepository>().SingleInstance();
            builder.RegisterType<ShopRepository>().As<IShopRepository>().SingleInstance();
            builder.RegisterType<RehearsalPointRepository>().As<IRehearsalPointRepository>();
            builder.RegisterType<StudioRepository>().As<IStudioRepository>();

            builder.RegisterType<FilterFactory>().As<IFilterFactory>();
            
            builder.RegisterType<ParameterFactory>().As<IParameterFactory>();

            builder.RegisterType<EntityUpdateService>().As<IEntityUpdateService>();

            var filterServices = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(x => x.GetInterfaces().Contains(typeof(IFilterService)))
                .Distinct()
                .ToList();

            builder.RegisterTypes(filterServices.ToArray()).As<IFilterService>();
        }
    }
}
