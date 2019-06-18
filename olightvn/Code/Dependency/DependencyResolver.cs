using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using olightvn.Repositories;
using T.Core.Authenticate;
using T.Core.DenpendencyResolver;

namespace olightvn.DenpendencyResolver
{
    [ExportEx(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUserRepository, UserRepository>();
            registerComponent.RegisterType<IRoleRepository, RoleRepository>();
            registerComponent.RegisterType<IPermissionRepository, PermissionRepository>();
            registerComponent.RegisterType<IProductRepository, ProductRepository>();
            registerComponent.RegisterType<ICategoryRepository, CategoryRepository>();
            registerComponent.RegisterType<ILocationRepository, LocationRepository>();
            registerComponent.RegisterType<IBookedRepository, BookedRepository>();
            registerComponent.RegisterType<IBasketRepository, BasketRepository>();
            registerComponent.RegisterType<ISiteMapRepository, SiteMapRepository>();
            registerComponent.RegisterType<IImageRepository, ImageRepository>();
            registerComponent.RegisterType<IRatingRepository, RatingRepository>();
            registerComponent.RegisterType<IBreadCrumbRepository, BreadCrumbRepository>();
            registerComponent.RegisterType<IArticle, ArticleRepository>();
            registerComponent.RegisterType<IContactRepository, ContactRepository>();
            registerComponent.RegisterType<IEmailRepository, EmailRepository>();
            registerComponent.RegisterType<IBrandRepository, BrandRepository>();
            registerComponent.RegisterType<IOriginRepository, OriginRepository>();
            registerComponent.RegisterType<ITag, HashtagRepository>();
            registerComponent.RegisterType<ISite, SiteRepository>();
        }
    }
}