using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver;
using System;

namespace SudokuSolverTest
{
    [TestClass]
    class DataHandlerTest
    {
        [TestMethod]
        public void DataHandler_IsLengthValid_Length81_True()
        {
            // Arrange - Object inits:
            DataHandlerService dhs = new ConsoleDataHandlerService("008062000030840902906000014012008600300079020060100037001780300685200740400096001");

            // Act - Call method:
            bool solved = dhs.IsLengthValid();

            // Assert:
            Assert.IsTrue(solved);
        }

        [TestMethod]
        public void DataHandler_IsLengthValid_Length79_False()
        {
            // Arrange - Object inits:
            DataHandlerService dhs = new ConsoleDataHandlerService("0080620000308902906000014012008600300079020060100037001780300685200740400096001");

            // Act - Call method:
            bool solved = dhs.IsLengthValid();

            // Assert:
            Assert.IsFalse(solved);
        }

        [TestMethod]
        public void DataHandler_IsDataValid_2inCol_False()
        {
            // Arrange - Object inits:
            Grid g = new Grid("008062000038040902906000014012008600300079020060100037001780300685200740400096001");
            DataHandlerService dhs = new ConsoleDataHandlerService(g.data);

            // Act - Call method:
            bool solved = dhs.IsDataValid(g);

            // Assert:
            Assert.IsFalse(solved);
        }
    }
}
