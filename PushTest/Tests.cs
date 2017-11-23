using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pusherboy;

namespace PushTest
{
    [TestClass]
    public class PushTests
    {
        /* 10 x 22
          0123456
          ..........    0
          ...0......
          .......b..
          ..........
          ...a...a..
          ..........     5
          .......d..
          ..........
          ...a......
          .......a..
          ...b......    10
          .a..aa.a..
          ..........
          .a....g...
          ..........
          .....ba.a.     15
          ..........
          ..........
          ....a..a..
          .......b..
          ..........      20
          ..........
     */

        [TestMethod]
        public void TestBoardMovement()
        {
            var b = GetBoard();
            b.SetBallPosition(3, 1);
            b.Move(Direction.D);
            S(b.Rows[1], new[] {-10});
            S(b.Rows[4], new[] {-3, 0, -3, 1, -2});
            S(b.Rows[5], new[] {-10});
            S(b.Cols[3], new[] {-4, 0, -3, 1, -1, 2, -11});
        }

        [TestMethod]
        public void TestBallPlacement()
        {
            var b = GetBoard();

            S(b.Rows[1], new[] {-10});
            S(b.Cols[1], new[] {-11, 1, -1, 1, -8});
            S(b.Cols[6], new[] {-13, 7, -1, 1, -6});
            S(b.Cols[7], new[] {-2, 2, -1, 1, -1, 4, -2, 1, -1, 1, -6, 1, 2, -2});
            S(b.Rows[2], new[] {-7, 2, -2});
            S(b.Rows[11], new[] {-1, 1, -2, 1, 1, -1, 1, -2});
            S(b.Rows[17], new[] {-10});

            b.SetBallPosition(1, 1);
            S(b.Rows[1], new[] {-1, 0, -8});
            S(b.Cols[1], new[] {-1, 0, -9, 1, -1, 1, -8});
            b.ClearBallPosition();

            S(b.Rows[1], new[] {-10});
            S(b.Cols[1], new[] {-11, 1, -1, 1, -8});

            b.SetBallPosition(6, 11);
            S(b.Rows[11], new[] {-1, 1, -2, 1, 1, 0, 1, -2});
            S(b.Cols[6], new[] {-11, 0, -1, 7, -1, 1, -6});
            b.ClearBallPosition();
            S(b.Rows[11], new[] {-1, 1, -2, 1, 1, -1, 1, -2});
            S(b.Cols[6], new[] {-13, 7, -1, 1, -6});

            b.SetBallPosition(1, 12);
            S(b.Rows[1], new[] {-10});
            S(b.Cols[1], new[] {-11, 1, 0, 1, -8});
            b.ClearBallPosition();
            S(b.Cols[1], new[] {-11, 1, -1, 1, -8});

            b.SetBallPosition(1, 17);
            S(b.Rows[17], new[] {-1, 0, -8});
            S(b.Cols[1], new[] {-11, 1, -1, 1, -3, 0, -4});
            b.ClearBallPosition();
            S(b.Rows[17], new[] {-10});
            S(b.Cols[1], new[] {-11, 1, -1, 1, -8});

            b.SetBallPosition(7, 1);
            S(b.Cols[7], new[] {-1, 0, 2, -1, 1, -1, 4, -2, 1, -1, 1, -6, 1, 2, -2});
            b.ClearBallPosition();
            S(b.Cols[7], new[] {-2, 2, -1, 1, -1, 4, -2, 1, -1, 1, -6, 1, 2, -2});
            S(b.Rows[1], new[] {-10});
        }

        [TestMethod]
        public void TestMovement1()
        {
            var row = new[] {0, -2, 2, -1};
            var arr = BoardUtils.MoveInArray(4, 0, row, out var outSection, out int endpos, out int pushsize);
            S(arr, new[] {-3, 0, 1});
            Assert.AreEqual(1, outSection);
            Assert.AreEqual(7, endpos);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestMovement2()
        {
            var row = new[] {0, -1, 2, -1};
            var arr = BoardUtils.MoveInArray(4, 0, row, out var outSection, out int endpos, out int pushsize);
            S(arr, new[] {-2, 0, 1});
            Assert.AreEqual(1, outSection);
            Assert.AreEqual(6, endpos);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestMovement3()
        {
            var row = new[] {0, -2, 2, -2};
            var arr = BoardUtils.MoveInArray(4, 0, row, out var outSection, out int endpos, out int pushsize);
            S(arr, new[] {-3, 0, 1, -1});
            Assert.AreEqual(1, outSection);
            Assert.AreEqual(7, endpos);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestMovement4()
        {
            var row = new[] {0, -2, 2, -1};
            var arr = BoardUtils.MoveInArray(4, 0, row, out var outSection, out int endpos, out int pushsize);
            S(arr, new[] {-3, 0, 1});
            Assert.AreEqual(1, outSection);
            Assert.AreEqual(7, endpos);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestMovement5()
        {
            var row = new[] {-1, 0, -2, 2, -1};
            var arr = BoardUtils.MoveInArray(4, 1, row, out var outSection, out int endpos, out int pushsize);
            S(arr, new[] {-4, 0, 1});
            Assert.AreEqual(1, outSection);
            Assert.AreEqual(7, endpos);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestMovement5b()
        {
            var row = new[] {-1, 0, -1, 2, 2, -1};
            var arr = BoardUtils.MoveInArray(4, 1, row, out var outSection, out int endpos, out int pushsize);
            S(arr, new[] {-3, 0, 3, -1});
            Assert.AreEqual(1, outSection);
            Assert.AreEqual(6, endpos);
            Assert.AreEqual(3, pushsize);
        }

        [TestMethod]
        public void TestMovement5c()
        {
            var row = new[] {-1, 0, -1, 2, 1, -1};
            var arr = BoardUtils.MoveInArray(4, 1, row, out var outSection, out int endpos, out int pushsize);
            S(arr, new[] {-3, 0, 2, -1});
            Assert.AreEqual(1, outSection);
            Assert.AreEqual(6, endpos);
            Assert.AreEqual(2, pushsize);
        }

        [TestMethod]
        public void TestMovement5d()
        {
            var row = new[] {-1, 0, -1, 2, 1, -3};
            var arr = BoardUtils.MoveInArray(4, 1, row, out var outSection, out int endpos, out int pushsize);
            S(arr, new[] {-3, 0, 2, -3});
            Assert.AreEqual(1, outSection);
            Assert.AreEqual(6, endpos);
            Assert.AreEqual(2, pushsize);
        }

        [TestMethod]
        public void TestMovement5e()
        {
            var row = new[] {-1, 0, -1, 1, 1, -3};
            var arr = BoardUtils.MoveInArray(4, 1, row, out var outSection, out int endpos, out int pushsize);
            S(arr, new[] {-3, 0, 1, -3});
            Assert.AreEqual(1, outSection);
            Assert.AreEqual(6, endpos);
            Assert.AreEqual(0, pushsize);
        }

        [TestMethod]
        public void TestMovement5f()
        {
            var row = new[] {1, 0, -1, 1, 1, -3};
            var arr = BoardUtils.MoveInArray(4, 1, row, out var outSection, out int endpos, out int pushsize);
            S(arr, new[] {1, -2, 0, 1, -3});
            Assert.AreEqual(2, outSection);
            Assert.AreEqual(6, endpos);
            Assert.AreEqual(0, pushsize);
        }

        [TestMethod]
        public void TestMovement5g()
        {
            var row = new[] {1, -1, 0, -2, 1, 2, -3};
            var arr = BoardUtils.MoveInArray(4, 2, row, out var outSection, out int endpos, out int pushsize);
            S(arr, new[] {1, -4, 0, 2, -3});
            Assert.AreEqual(2, outSection);
            Assert.AreEqual(7, endpos);
            Assert.AreEqual(0, pushsize);
        }

        [TestMethod]
        public void TestMovement6()
        {
            var row = new[] {-1, 2, -7, 2, 2, 0, -2, 2, -1};
            var arr = BoardUtils.MoveInArray(4, 5, row, out var outSection, out int endpos, out int pushsize);
            S(arr, new[] {-1, 2, -7, 2, 2, -3, 0, 1});
            Assert.AreEqual(6, outSection);
            Assert.AreEqual(7, endpos);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestMovement7()
        {
            var row = new[] {-1, 2, -7, 2, 2, 0, -2, 2, -10};
            var arr = BoardUtils.MoveInArray(4, 5, row, out var outSection, out int endpos, out int pushsize);
            S(arr, new[] {-1, 2, -7, 2, 2, -3, 0, 1, -9});
            Assert.AreEqual(6, outSection);
            Assert.AreEqual(7, endpos);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestParsing()
        {
            var b = GetBoard();
            Assert.AreEqual(b.BoardX, 10);
            Assert.AreEqual(b.BoardY, 22);
            Assert.AreEqual(b.BoardString,
                "..........,..........,.......b..,..........,...a...a..,..........,.......d..,..........,...a......,.......a..,...b......,.a..aa.a..,..........,.a....g...,..........,.....ba.a.,..........,..........,....a..a..,.......b..,..........,..........");
        }

        private Board GetBoard()
        {
            var b = new Board(Board.Sample, 0);
            return b;
        }

        [TestMethod]
        public void TestBoardCreation()
        {
            var b = GetBoard();
            b.Show1();
            b.Show2();
            b.Show3();
            b.ShowCols();
            S(b.Rows[1], new[] {-10});
            S(b.Rows[2], new[] {-7, 2, -2});
            S(b.Rows[11], new[] {-1, 1, -2, 1, 1, -1, 1, -2});
            S(b.Rows[21], new[] {-10});
            S(b.Cols[0], new[] {-22});
            S(b.Cols[9], new[] {-22});
            S(b.Cols[1], new[] {-11, 1, -1, 1, -8});
            S(b.Cols[7], new[] {-2, 2, -1, 1, -1, 4, -2, 1, -1, 1, -6, 1, 2, -2});
        }

        [TestMethod]
        public void TestTransposition()
        {
            var input = "...b.";
            var tr = BoardUtils.Transpose(input);
            var output = ".,.,.,b,.";
            Assert.AreEqual(tr, output);
        }

        [TestMethod]
        public void TestTransposition2()
        {
            var input = "...b.,aa.de";
            var tr = BoardUtils.Transpose(input);
            var output = ".a,.a,..,bd,.e";
            Assert.AreEqual(tr, output);
        }

        [TestMethod]
        public void TestBallRemovalFromArrayBySection1()
        {
            var arr = new[] {0, -1, 2, 2};
            arr = BoardUtils.RemoveZeroAtSectionFromArray(0, arr);
            S(arr, new[] {-2, 2, 2});
        }

        [TestMethod]
        public void TestBallRemovalFromArrayBySection2()
        {
            var arr = new[] {0, 1, 2, 2};
            arr = BoardUtils.RemoveZeroAtSectionFromArray(0, arr);
            S(arr, new[] {-1, 1, 2, 2});
        }

        [TestMethod]
        public void TestBallRemovalFromArrayBySection3()
        {
            var arr = new[] {5, 0, 2, 2};
            arr = BoardUtils.RemoveZeroAtSectionFromArray(1, arr);
            S(arr, new[] {5, -1, 2, 2});
        }

        [TestMethod]
        public void TestBallRemovalFromArrayBySection4()
        {
            var arr = new[] {5, 0, -1, 2, 2};
            arr = BoardUtils.RemoveZeroAtSectionFromArray(1, arr);
            S(arr, new[] {5, -2, 2, 2});
        }

        [TestMethod]
        public void TestBallRemovalFromArrayBySection5()
        {
            var arr = new[] {-4, 0, -1, 2, 2};
            arr = BoardUtils.RemoveZeroAtSectionFromArray(1, arr);
            S(arr, new[] {-6, 2, 2});
        }

        [TestMethod]
        public void TestBallRemovalFromArrayBySection6()
        {
            var arr = new[] {-4, 0, 1, 2, 2};
            arr = BoardUtils.RemoveZeroAtSectionFromArray(1, arr);
            S(arr, new[] {-5, 1, 2, 2});
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition0()
        {
            var arr = new[] {-4, 1, -2};
            arr = BoardUtils.AddZeroAtPositionInArray(0, arr, out int section);
            S(arr, new[] {0, -3, 1, -2});
            Assert.AreEqual(0, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition1()
        {
            var arr = new[] {-4, 1, -2};
            arr = BoardUtils.AddZeroAtPositionInArray(1, arr, out int section);
            S(arr, new[] {-1, 0, -2, 1, -2});
            Assert.AreEqual(1, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition3()
        {
            var arr = new[] {-4, 1, -2};
            arr = BoardUtils.AddZeroAtPositionInArray(2, arr, out int section);
            S(arr, new[] {-2, 0, -1, 1, -2});
            Assert.AreEqual(1, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition4()
        {
            var arr = new[] {-4, 1, -2};
            arr = BoardUtils.AddZeroAtPositionInArray(5, arr, out int section);
            S(arr, new[] {-4, 1, 0, -1});
            Assert.AreEqual(2, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition5()
        {
            var arr = new[] {2, -1};
            arr = BoardUtils.AddZeroAtPositionInArray(1, arr, out int section);
            S(arr, new[] {2, 0});
            Assert.AreEqual(1, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition5b()
        {
            var arr = new[] {2, -2};
            arr = BoardUtils.AddZeroAtPositionInArray(2, arr, out int section);
            S(arr, new[] {2, -1, 0});
            Assert.AreEqual(2, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition6()
        {
            var arr = new[] {-4, 2, -2, 2, -2};
            arr = BoardUtils.AddZeroAtPositionInArray(6, arr, out int section);
            S(arr, new[] {-4, 2, -1, 0, 2, -2});
            Assert.AreEqual(3, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition7()
        {
            var arr = new[] {-4, 2, -3, 2, -2};
            arr = BoardUtils.AddZeroAtPositionInArray(7, arr, out int section);
            S(arr, new[] {-4, 2, -2, 0, 2, -2});
            Assert.AreEqual(3, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition8()
        {
            var arr = new[] {2, -3, 2, -2};
            arr = BoardUtils.AddZeroAtPositionInArray(5, arr, out int section);
            S(arr, new[] {2, -3, 2, 0, -1});
            Assert.AreEqual(3, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition9()
        {
            var arr = new[] {-4, 1, 2, -3, 2, -2};
            arr = BoardUtils.AddZeroAtPositionInArray(11, arr, out int section);
            S(arr, new[] {-4, 1, 2, -3, 2, -1, 0});
            Assert.AreEqual(6, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition10()
        {
            var arr = new[] {-3, 2, -3};
            arr = BoardUtils.AddZeroAtPositionInArray(6, arr, out int section);
            S(arr, new[] {-3, 2, -2, 0});
            Assert.AreEqual(3, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition10b()
        {
            var arr = new[] {-3};
            arr = BoardUtils.AddZeroAtPositionInArray(0, arr, out int section);
            S(arr, new[] {0, -2});
            Assert.AreEqual(0, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition10c()
        {
            var arr = new[] {-3};
            arr = BoardUtils.AddZeroAtPositionInArray(1, arr, out int section);
            S(arr, new[] {-1, 0, -1});
            Assert.AreEqual(1, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition10d()
        {
            var arr = new[] {-3};
            arr = BoardUtils.AddZeroAtPositionInArray(2, arr, out int section);
            S(arr, new[] {-2, 0});
            Assert.AreEqual(1, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition11()
        {
            var arr = new[] {-3, 2, -3};
            arr = BoardUtils.AddZeroAtPositionInArray(6, arr, out int section);
            S(arr, new[] {-3, 2, -2, 0});
            Assert.AreEqual(3, section);
        }

        private void S(int[] actual, int[] expected)
        {
            if (expected.SequenceEqual(actual))
            {
                return;
            }

            Console.WriteLine($"actual:  {P(actual)}\nexpected:{P(expected)}");
            Assert.IsFalse(true, $"actual:  {P(actual)}\nexpected:{P(expected)}");
        }

        private string P(int[] input)
        {
            return string.Join(",", input.Select(el => el.ToString()));
        }

        [TestMethod]
        public void TestMovement()
        {
            //place
            //move
            //make sure position and bump is correct
            //make sure bump joining is correct.
        }

        [TestMethod]
        public void TestAvailableDirections()
        {
            //test nothing that way at all
            //test next to something
        }

        [TestMethod]
        public void TestIllegalMovement()
        {
        }

        [TestMethod]
        public void TestGettingStuck()
        {
        }
    }
}