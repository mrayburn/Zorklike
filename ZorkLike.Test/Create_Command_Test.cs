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
    public class Create_Command_Test
    {
        private CreateCommand target;
        private IConsoleFacade console;
        private IRepositoryFactoryFactory factory;
        private IRepositoryFactory repoFactory;
        private IRepository repo;
        private IUnitOfWork uow;
        private IGameObjectQueries goQueries;
        private IFormatter[] formatters;

        [TestInitialize]
        public void GlobalArrange()
        {
            repoFactory = MockRepository.GenerateMock<IRepositoryFactory>();
            factory = MockRepository.GenerateMock<IRepositoryFactoryFactory>();
            console = MockRepository.GenerateMock<IConsoleFacade>();
            repo = MockRepository.GenerateMock<IRepository>();
            uow = MockRepository.GenerateMock<IUnitOfWork>();
            goQueries = MockRepository.GenerateMock<IGameObjectQueries>();
            //formatters = MockRepository.GenerateMock<IFormatter[]>();

            factory.Stub(m => m.Create()).Return(repoFactory);
            repoFactory.Stub(m => m.Create()).Return(repo);
            repo.Stub(m => m.UnitOfWork).Return(uow);

            //target = new CreateCommand(console, factory, goQueries, formatters);
        }
        [TestMethod]
        public void Create_Command_Should_Create_And_Save_A_New_GameObject()
        {
            // Arrange
            var cmd = "@create shrubbery";
            var list = new List<GameObject>();
            repo.Stub(m => m.Add<GameObject>(Arg<GameObject>.Is.Anything))
                .WhenCalled(m => {
                    GameObject obj = m.Arguments.First() as GameObject;
                    list.Add(obj);
                    m.ReturnValue = obj;
                }).Return(new GameObject());

            // Act
            if (target.IsValid(cmd)) target.Execute(cmd);

            // Assert
            repo.AssertWasCalled(m => m.Add(Arg<GameObject>.Is.Anything));
            uow.AssertWasCalled(m => m.SaveChanges());
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("shrubbery", list.First().Name);
        }
    }
}
