using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ZorkLike.Test
{

    [TestClass]
    public class GameEngineTest
    {
        [TestMethod]
        public void Run_Should_Write_A_Prompt_And_Read_Input()
        {
            // Arrange
            var console = MockRepository.GenerateMock<IConsoleFacade>();
            var engine = new GameEngine(console);
            console.Stub(m => m.ReadLine()).Return("exit");

            // Act
            engine.Run();

            // Assert
            console.AssertWasCalled(m => m.Write(Arg<string>.Is.Anything));
        }
        [TestMethod]
        public void Run_Should_Stop_Looping_When_Exit_Is_Read()
        {
            // Arrange
            var console = MockRepository.GenerateMock<IConsoleFacade>();
            var engine = new GameEngine(console);
            StubReadLine(console, "foo");
            StubReadLine(console, "bar");
            StubReadLine(console, "exit");
            // Act
            engine.Run();

            // Assert
            console.AssertWasCalled(m => m.ReadLine(), mo => mo.Repeat.Times(3));
        }
        private void StubReadLine(IConsoleFacade console, string msg)
        {
            console.Stub(m => m.ReadLine()).Return(msg).Repeat.Times(1);
        }
        [TestMethod]
        public void Run_Should_Stop_Looping_Regardless_Of_Case()
        {
            // Arrange
            var console = MockRepository.GenerateMock<IConsoleFacade>();
            var engine = new GameEngine(console);
            StubReadLine(console, "eXIt");
            // Act
            engine.Run();

            // Assert
            console.AssertWasCalled(m => m.ReadLine());
        }
        [TestMethod]
        public void Run_Should_Stop_Looping_Regardless_Of_Spaces()
        {
            // Arrange
            var console = MockRepository.GenerateMock<IConsoleFacade>();
            var engine = new GameEngine(console);
            StubReadLine(console, "  exit ");
            // Act
            engine.Run();

            // Assert
            console.AssertWasCalled(m => m.ReadLine());
        }
    }


}
