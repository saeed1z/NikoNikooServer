using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineServices.Entity;

namespace OnlineServices.Persistence
{
    public class OnlineServicesDbContext : IdentityDbContext
    {
        public OnlineServicesDbContext(DbContextOptions<OnlineServicesDbContext> options) : base(options)
        {
        }

        public DbSet<City> City { get; set; }

        public DbSet<Person> Person { get; set; }

        public DbSet<Banner> Banner { get; set; }

        public DbSet<PersonService> PersonService { get; set; }

        public DbSet<PersonType> PersonType { get; set; }

        public DbSet<ServiceType> ServiceType { get; set; }

        public DbSet<State> State { get; set; }

        public DbSet<Brand> Brand { get; set; }

        public DbSet<Model> Model { get; set; }

        public DbSet<PackageTemplate> PackageTemplate { get; set; }

        public DbSet<PackageTemplateDetail> PackageTemplateDetail { get; set; }

        public DbSet<PersonCar> PersonCar { get; set; }

        public DbSet<PersonPackage> PersonPackage { get; set; }

        public DbSet<PersonPackageDetail> PersonPackageDetail { get; set; }

        public DbSet<ServiceRequest> ServiceRequest { get; set; }

        public DbSet<Status> Status { get; set; }

        public DbSet<ServiceRequestFile> ServiceRequestFile { get; set; }

        public DbSet<ServiceTypeUnitPrice> ServiceTypeUnitPrice { get; set; }

        public DbSet<ConfirmMap> ConfirmMap { get; set; }

        public DbSet<ServiceTypeRelation> ServiceTypeRelation { get; set; }

        public DbSet<BaseKind> BaseKind { get; set; }

        public DbSet<ServiceRequestAccept> ServiceRequestAccept { get; set; }

        public DbSet<Base> Base { get; set; }

        public DbSet<ModelTechnicalInfo> ModelTechnicalInfo { get; set; }

        public DbSet<ServiceCenter> ServiceCenter { get; set; }

        public DbSet<ServiceCenterDetail> ServiceCenterDetail { get; set; }

        public DbSet<ServiceRequestDetail> ServiceRequestDetail { get; set; }

        public DbSet<ModelGallery> ModelGallery { get; set; }

        public DbSet<Message> Message { get; set; }

        public DbSet<ServiceCapture> ServiceCapture { get; set; }

        public DbSet<SystemSetting> SystemSetting { get; set; }

        public DbSet<ServiceTypeQuestion> ServiceTypeQuestion { get; set; }

        public DbSet<ServiceRequestSurvey> ServiceRequestSurvey { get; set; }

        public DbSet<ServiceFactor> ServiceFactor { get; set; }

        public DbSet<ServiceFactorDetail> ServiceFactorDetail { get; set; }

        public DbSet<CommercialUserRequest> CommercialUserRequest { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketComments> TicketComments { get; set; }
    }
}
