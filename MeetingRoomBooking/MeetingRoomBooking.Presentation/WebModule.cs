using Autofac;
using MeetingRoomBooking.DataAccess;
using MeetingRoomBooking.DataAccess.RepoClasses;
using MeetingRoomBooking.DataAccess.UnitOfWorkClasses;
using MeetingRoomBooking.Domain.RepoInterface;
using MeetingRoomBooking.Domain.UnitOfWorkInterfaces;
using MeetingRoomBooking.Services.Services;

namespace MeetingRoomBooking.Presentation
{
    public class WebModule(string connectionString, string migrationAssembly) : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", connectionString)
                .WithParameter("migrationAssembly", migrationAssembly)
                .InstancePerLifetimeScope();

            builder.RegisterType<MeetingRoomRepository>()
                       .As<IMeetingRoomRepository>()
                       .InstancePerLifetimeScope();

            builder.RegisterType<MeetingRoomUnitOfWork>()
                .As<IMeetingRoomUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MeetingRoomManagementService>()
                .As<IMeetingRoomManagementService>()
                .InstancePerLifetimeScope();


        }

    }
}
