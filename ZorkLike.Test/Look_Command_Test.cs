using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ZorkLike.Data;
using ZorkLike.Commands;

namespace ZorkLike.Test
{
    [TestClass]
    public class Look_Command_Test
    {
        private LookCommand target;
        private IConsoleFacade console;
        private IRepositoryFactoryFactory factory;
        private IRepositoryFactory repoFactory;
        private IRepository repo;
        private IUnitOfWork uow;
        private IGameObjectQueries goQueries;

        [TestInitialize]
        public void GlobalArrange()
        {
            repoFactory = MockRepository.GenerateMock<IRepositoryFactory>();
            factory = MockRepository.GenerateMock<IRepositoryFactoryFactory>();
            console = MockRepository.GenerateMock<IConsoleFacade>();
            repo = MockRepository.GenerateMock<IRepository>();
            uow = MockRepository.GenerateMock<IUnitOfWork>();
            goQueries = MockRepository.GenerateMock<IGameObjectQueries>();

            factory.Stub(m => m.Create()).Return(repoFactory);
            repoFactory.Stub(m => m.Create()).Return(repo);
            repo.Stub(m => m.UnitOfWork).Return(uow);

            target = new LookCommand(console, factory, goQueries);

        }
        [TestMethod]
        public void Look_Should_Return_Nothing_Message_Without_Description()
        {
            // Arrange
            var cmd = "look";
            repo.Stub(m => m.AsQueryable<GameObject>()).Return(
                new List<GameObject>()
                {
                    new GameObject() { Name = "Sword" },
                    new Player() { Name = "Hero", Description = null }
                }.AsQueryable()
                );

            // Act
            if (target.IsValid(cmd)) target.Execute(cmd);

            // Assert
            console.AssertWasCalled(m => m.WriteLine("You see nothing."));
        }
        [TestMethod]
        public void Look_Should_Create_A_Player_If_One_Does_Not_Exist()
        {
            // Arrange
            var cmd = "look";
            repo.Stub(m => m.AsQueryable<GameObject>()).Return(
                new List<GameObject>()
                {
                    new GameObject() { Name = "Sword" },
                }.AsQueryable()
                );

            // Act
            if (target.IsValid(cmd)) target.Execute(cmd);

            // Assert
            repo.AssertWasCalled(m => m.Add(Arg<Player>.Is.Anything));
            uow.AssertWasCalled(m => m.SaveChanges());
        }
    }
}
