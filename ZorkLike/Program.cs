using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using ZorkLike.Data;
using System.Data.Entity;
using Castle.Facilities.TypedFactory;
using ZorkLike.Commands;

namespace ZorkLike
{
    class Program
    {
        static void Main(string[] args)
        {
            IWindsorContainer container = new WindsorContainer();
            container.Kernel.AddFacility<TypedFactoryFacility>();
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, true));
            container.Register(
                AllTypes.FromThisAssembly().BasedOn<ICommand>().WithServiceAllInterfaces(),
                Component.For<IConsoleFacade>().ImplementedBy<ColorConsole>(),
                Component.For<IFormatter>().ImplementedBy<NullFormatter>(),
                Component.For<IFormatter>().ImplementedBy<InventoryFormatter>(),
                Component.For<IFormatter>().ImplementedBy<LookFormatter>(),
                Component.For<IGameObjectQueries>().ImplementedBy<GameObjectQueries>(),
                Component.For<GameEngine>(),
                Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().LifeStyle.Transient,
                Component.For<IRepository>().ImplementedBy<Repository>().LifeStyle.Transient,
                Component.For<DbContext>().ImplementedBy<GameDbContext>().LifeStyle.Transient,
                Component.For<IRepositoryFactory>().AsFactory().LifeStyle.Transient,
                Component.For<IRepositoryFactoryFactory>().AsFactory().LifeStyle.Singleton
                );

            var engine = container.Resolve<GameEngine>();
            engine.Run();

        }
    }
}
