using Autofac;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //BL her HTTP isteği için yeni bir örnek
            builder.RegisterType<TravelManager>().As<ITravelService>().InstancePerLifetimeScope();
            builder.RegisterType<StatusManager>().As<IStatusService>().InstancePerLifetimeScope();
            builder.RegisterType<StaffManager>().As<IStaffService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageManager>().As<IMessageService>().InstancePerLifetimeScope();
            //DAL her HTTP isteği için yeni bir örnek
            builder.RegisterType<EfTravelDal>().As<ITravelDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfStatusDal>().As<IStatusDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfStaffDal>().As<IStaffDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfMessageDal>().As<IMessageDal>().InstancePerLifetimeScope();

            //builder.RegisterType<Context>().InstancePerLifetimeScope();

            //builder.RegisterType<TravelManager>().As<ITravelService>().SingleInstance();
            //builder.RegisterType<StatusManager>().As<IStatusService>().SingleInstance();
            //builder.RegisterType<StaffManager>().As<IStaffService>().SingleInstance();
            //builder.RegisterType<MessageManager>().As<IMessageService>().SingleInstance();
            //         builder.RegisterType<EfTravelDal>().As<ITravelDal>().SingleInstance();
            //         builder.RegisterType<EfStatusDal>().As<IStatusDal>().SingleInstance();
            //         builder.RegisterType<EfStaffDal>().As<IStaffDal>().SingleInstance();
            //builder.RegisterType<EfMessageDal>().As<IMessageDal>().SingleInstance();
        }
    }
}