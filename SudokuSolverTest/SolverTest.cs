using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver;
using System;

namespace SudokuSolverTest
{
    [TestClass]
    public class SolverTest
    {
        [TestMethod]
        public void Solver_1x1_True()
        {
            // Arrange - Object inits:
            Grid g = new Grid("0");
            DataHandlerService dhs = new ConsoleDataHandlerService(g.data);

            // Act - Call method:
            bool solved = Solver.Solve(ref g);

            // Assert:
            Assert.IsTrue(solved);

            // If solution is not right, an exception will be thrown:
            dhs.IsDataValid(g);
        }

        [TestMethod]
        public void Solver_4x4_True()
        {
            // Arrange - Object inits:
            Grid g = new Grid("0010400000020300");
            DataHandlerService dhs = new ConsoleDataHandlerService(g.data);

            // Act - Call method:
            bool solved = Solver.Solve(ref g);

            // Assert:
            Assert.IsTrue(solved);

            // If solution is not right, an exception will be thrown:
            dhs.IsDataValid(g);
        }

        [TestMethod]
        public void Solver_Empty9x9_True()
        {
            // Arrange - Object inits:
            Grid g = new Grid("000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            DataHandlerService dhs = new ConsoleDataHandlerService(g.data);

            // Act - Call method:
            bool solved = Solver.Solve(ref g);

            // Assert:
            Assert.IsTrue(solved);

            // If solution is not right, an exception will be thrown:
            dhs.IsDataValid(g);
        }

        [TestMethod]
        public void Solver_Easy9x9_True()
        {
            // Arrange - Object inits:
            Grid g = new Grid("008062000030840902906000014012008600300079020060100037001780300685200740400096001");
            DataHandlerService dhs = new ConsoleDataHandlerService(g.data);

            // Act - Call method:
            bool solved = Solver.Solve(ref g);

            // Assert:
            Assert.IsTrue(solved);

            // If solution is not right, an exception will be thrown:
            dhs.IsDataValid(g);
        }

        [TestMethod]
        public void Solver_Hard9x9_True()
        {
            // Arrange - Object inits:
            Grid g = new Grid("006000007970000040520000800000700500400003170050008006000301002000805000603902000");
            DataHandlerService dhs = new ConsoleDataHandlerService(g.data);

            // Act - Call method:
            bool solved = Solver.Solve(ref g);

            // Assert:
            Assert.IsTrue(solved);

            // If solution is not right, an exception will be thrown:
            dhs.IsDataValid(g);
        }

        [TestMethod]
        public void Solver_Empty16x16_True()
        {
            // Arrange - Object inits:
            Grid g = new Grid("0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            DataHandlerService dhs = new ConsoleDataHandlerService(g.data);

            // Act - Call method:
            bool solved = Solver.Solve(ref g);

            // Assert:
            Assert.IsTrue(solved);

            // If solution is not right, an exception will be thrown:
            dhs.IsDataValid(g);
        }

        [TestMethod]
        public void Solver_Easy16x16_True()
        {
            // Arrange - Object inits:
            Grid g = new Grid("00000>000000000000>000000000000000100000070000000000400060000000000000000000010000000000000000600060000000000000000>0000000?0000000000000000000000000?00800000>0000000>0000;0050@0400000000000;0000000000?000000000000000000000000000000000000000000000400000000");
            DataHandlerService dhs = new ConsoleDataHandlerService(g.data);

            // Act - Call method:
            bool solved = Solver.Solve(ref g);

            // Assert:
            Assert.IsTrue(solved);

            // If solution is not right, an exception will be thrown:
            dhs.IsDataValid(g);
        }

        [TestMethod]
        public void Solver_Hard16x16_True()
        {
            // Arrange - Object inits:
            Grid g = new Grid("030>0:060092040?5@00<00;006300070?09000000040;@007000010@;00000000010>0000000=00600;0000092=4001090008;00000207000040<0?0008050000>=160<0:700983000200001000;<?0<000;00:00@0=000@090>3070200:006000041?020050009>000070000;06030000060000>0:1@50?20000300000000:");
            DataHandlerService dhs = new ConsoleDataHandlerService(g.data);

            // Act - Call method:
            bool solved = Solver.Solve(ref g);

            // Assert:
            Assert.IsTrue(solved);

            // If solution is not right, an exception will be thrown:
            dhs.IsDataValid(g);
        }

        [TestMethod]
        public void Solver_Unsolveable9x9_False()
        {
            // Arrange - Object inits:
            Grid g = new Grid("100000000000100000000000005000000100000000000000000000000000000000000010000000000");
            DataHandlerService dhs = new ConsoleDataHandlerService(g.data);

            // Act - Call method:
            bool solved = Solver.Solve(ref g);

            // Assert:
            Assert.IsFalse(solved);

            // If solution is not right, an exception will be thrown:
            dhs.IsDataValid(g);
        }
    }
}
