using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Push;

namespace PushTest
{
    [TestClass]
    public class PushTests
    {
        //SMALL
        /* 5x5
          .....
          .....
          ..b..
          .....
          .....
         */

        //BIG
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
        public void TestSmallMovementDown()
        {
            var b = GetSmallBoard();
            b.InitialBallPlacement(2, 0);
            b.Show3();
            b.Show4();
            Assert.AreEqual(2, b.BallPosition.X);
            Assert.AreEqual(0, b.BallPosition.Y);
            Assert.AreEqual(1, b.BallXSection);
            Assert.AreEqual(0, b.BallYSection);
            b.Move(Direction.D);
            Assert.AreEqual(2, b.BallPosition.X);
            Assert.AreEqual(2, b.BallPosition.Y);
            Assert.AreEqual(1, b.BallXSection);
            Assert.AreEqual(1, b.BallYSection);
            b.Show3();
            b.Show4();
            S(b.Rows[0], new List<int> {-5});
            S(b.Rows[1], new List<int> {-5});
            S(b.Rows[2], new List<int> {-2, 0, -2});
            S(b.Rows[3], new List<int> {-2, 1, -2});
            S(b.Rows[4], new List<int> {-5});

            S(b.Cols[0], new List<int> {-5});
            S(b.Cols[1], new List<int> {-5});
            S(b.Cols[2], new List<int> {-2, 0, 1, -1});
            S(b.Cols[3], new List<int> {-5});
            S(b.Cols[4], new List<int> {-5});
        }

        [TestMethod]
        public void TestSmallMovementRight()
        {
            var b = GetSmallBoard();
            b.InitialBallPlacement(0, 2);
            b.Show3();
            b.Show4();
            Assert.AreEqual(0, b.BallPosition.X);
            Assert.AreEqual(2, b.BallPosition.Y);
            Assert.AreEqual(0, b.BallXSection);
            Assert.AreEqual(1, b.BallYSection);
            b.Move(Direction.R);
            Assert.AreEqual(2, b.BallPosition.X);
            Assert.AreEqual(2, b.BallPosition.Y);
            Assert.AreEqual(1, b.BallXSection);
            Assert.AreEqual(1, b.BallYSection);
            b.Show3();
            b.Show4();
            S(b.Rows[0], new List<int> {-5});
            S(b.Rows[1], new List<int> {-5});
            S(b.Rows[2], new List<int> {-2, 0, 1, -1});
            S(b.Rows[3], new List<int> {-5});
            S(b.Rows[4], new List<int> {-5});
            S(b.Cols[0], new List<int> {-5});
            S(b.Cols[1], new List<int> {-5});
            S(b.Cols[2], new List<int> {-2, 0, -2});
            S(b.Cols[3], new List<int> {-2, 1, -2});
            S(b.Cols[4], new List<int> {-5});
        }

        [TestMethod]
        public void TestSmallMovementUp()
        {
            var b = GetSmallBoard();
            b.InitialBallPlacement(2, 4);
            b.Show3();
            b.Show4();
            Assert.AreEqual(2, b.BallPosition.X);
            Assert.AreEqual(4, b.BallPosition.Y);
            Assert.AreEqual(1, b.BallXSection);
            Assert.AreEqual(3, b.BallYSection);
            b.Move(Direction.U);
            Assert.AreEqual(2, b.BallPosition.X);
            Assert.AreEqual(2, b.BallPosition.Y);
            Assert.AreEqual(1, b.BallXSection);
            Assert.AreEqual(2, b.BallYSection);
            b.Show3();
            b.Show4();
            S(b.Rows[0], new List<int> {-5});
            S(b.Rows[1], new List<int> {-2, 1, -2});
            S(b.Rows[2], new List<int> {-2, 0, -2});
            S(b.Rows[3], new List<int> {-5});
            S(b.Rows[4], new List<int> {-5});
            S(b.Cols[0], new List<int> {-5});
            S(b.Cols[1], new List<int> {-5});
            S(b.Cols[2], new List<int> {-1, 1, 0, -2});
            S(b.Cols[3], new List<int> {-5});
            S(b.Cols[4], new List<int> {-5});
        }

        [TestMethod]
        public void TestSmallMovementLeft()
        {
            var b = GetSmallBoard();
            b.InitialBallPlacement(4, 2);
            b.Show3();
            b.Show4();
            Assert.AreEqual(4, b.BallPosition.X);
            Assert.AreEqual(2, b.BallPosition.Y);
            Assert.AreEqual(3, b.BallXSection);
            Assert.AreEqual(1, b.BallYSection);
            b.Move(Direction.L);
            Assert.AreEqual(2, b.BallPosition.X);
            Assert.AreEqual(2, b.BallPosition.Y);
            Assert.AreEqual(2, b.BallXSection);
            Assert.AreEqual(1, b.BallYSection);
            b.Show3();
            b.Show4();
            S(b.Rows[0], new List<int> {-5});
            S(b.Rows[1], new List<int> {-5});
            S(b.Rows[2], new List<int> {-1, 1, 0, -2});
            S(b.Rows[3], new List<int> {-5});
            S(b.Rows[4], new List<int> {-5});
            S(b.Cols[0], new List<int> {-5});
            S(b.Cols[1], new List<int> {-2, 1, -2});
            S(b.Cols[2], new List<int> {-2, 0, -2});
            S(b.Cols[3], new List<int> {-5});
            S(b.Cols[4], new List<int> {-5});
        }

        [TestMethod]
        public void TestBoardMovement()
        {
            var b = GetBigBoard();
            b.Show3();
            b.Show4();

            b.InitialBallPlacement(3, 1);
            b.Show3();
            b.Show4();

            b.Move(Direction.D);
            b.Show3();
            b.Show4();
            Assert.AreEqual(1, b.BallXSection);
            Assert.AreEqual(1, b.BallYSection);
            S(b.Rows[1], new List<int> {-10});
            S(b.Rows[4], new List<int> {-3, 0, -3, 1, -2});
            S(b.Rows[5], new List<int> {-10});
            S(b.Cols[3], new List<int> {-4, 0, -3, 1, -1, 2, -11});

            b.Move(Direction.R);
            b.Show3();
            b.Show4();
            Assert.AreEqual(1, b.BallXSection);
            Assert.AreEqual(3, b.BallYSection);

            b.Move(Direction.U);
            b.Show3();
            b.Show4();
            Assert.AreEqual(1, b.BallXSection);
            Assert.AreEqual(2, b.BallYSection);
            S(b.Rows[1], new List<int> {-7,1,-2});
            S(b.Rows[2], new List<int> { -7, 0, -2 });
            S(b.Rows[4], new List<int> { -10 });

            b.Move(Direction.D);
            b.Show3();
            b.Show4();
            Assert.AreEqual(1, b.BallXSection);
            Assert.AreEqual(3, b.BallYSection);
            S(b.Rows[6], new List<int> { -7,0,-2});
            S(b.Rows[7], new List<int> { -7, 3, -2 });

            b.Move(Direction.U);
            b.Show3();
            b.Show4();
            Assert.AreEqual(1, b.BallXSection);
            Assert.AreEqual(1, b.BallYSection);
            S(b.Rows[0], new List<int> { -10 });
            S(b.Rows[1], new List<int> { -7, 0, -2 });
        }

        [TestMethod]
        public void TestBallPlacement()
        {
            var b = GetBigBoard();

            S(b.Rows[1], new List<int> {-10});
            S(b.Cols[1], new List<int> {-11, 1, -1, 1, -8});
            S(b.Cols[6], new List<int> {-13, 7, -1, 1, -6});
            S(b.Cols[7], new List<int> {-2, 2, -1, 1, -1, 4, -2, 1, -1, 1, -6, 1, 2, -2});
            S(b.Rows[2], new List<int> {-7, 2, -2});
            S(b.Rows[11], new List<int> {-1, 1, -2, 1, 1, -1, 1, -2});
            S(b.Rows[17], new List<int> {-10});

            b.InitialBallPlacement(1, 1);
            S(b.Rows[1], new List<int> {-1, 0, -8});
            S(b.Cols[1], new List<int> {-1, 0, -9, 1, -1, 1, -8});
            b.ClearBallPosition();

            S(b.Rows[1], new List<int> {-10});
            S(b.Cols[1], new List<int> {-11, 1, -1, 1, -8});

            b.InitialBallPlacement(6, 11);
            S(b.Rows[11], new List<int> {-1, 1, -2, 1, 1, 0, 1, -2});
            S(b.Cols[6], new List<int> {-11, 0, -1, 7, -1, 1, -6});
            b.ClearBallPosition();
            S(b.Rows[11], new List<int> {-1, 1, -2, 1, 1, -1, 1, -2});
            S(b.Cols[6], new List<int> {-13, 7, -1, 1, -6});

            b.InitialBallPlacement(1, 12);
            S(b.Rows[1], new List<int> {-10});
            S(b.Cols[1], new List<int> {-11, 1, 0, 1, -8});
            b.ClearBallPosition();
            S(b.Cols[1], new List<int> {-11, 1, -1, 1, -8});

            b.InitialBallPlacement(1, 17);
            S(b.Rows[17], new List<int> {-1, 0, -8});
            S(b.Cols[1], new List<int> {-11, 1, -1, 1, -3, 0, -4});
            b.ClearBallPosition();
            S(b.Rows[17], new List<int> {-10});
            S(b.Cols[1], new List<int> {-11, 1, -1, 1, -8});

            b.InitialBallPlacement(7, 1);
            S(b.Cols[7], new List<int> {-1, 0, 2, -1, 1, -1, 4, -2, 1, -1, 1, -6, 1, 2, -2});
            b.ClearBallPosition();
            S(b.Cols[7], new List<int> {-2, 2, -1, 1, -1, 4, -2, 1, -1, 1, -6, 1, 2, -2});
            S(b.Rows[1], new List<int> {-10});
        }

        [TestMethod]
        public void TestMovement1()
        {
            var row = new List<int> {0, -2, 2, -1};
            BoardUtils.MoveInArray(row, 0, out var sectionMoved, out int moved, out int pushsize);
            S(row, new List<int> {-3, 0, 1});
            Assert.AreEqual(1, sectionMoved);
            Assert.AreEqual(3, moved);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestMovement2()
        {
            var row = new List<int> {0, -1, 2, -1};
            BoardUtils.MoveInArray(row, 0, out var sectionMoved, out int moved, out int pushsize);
            S(row, new List<int> {-2, 0, 1});
            Assert.AreEqual(1, sectionMoved);
            Assert.AreEqual(2, moved);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestMovement3()
        {
            var row = new List<int> {0, -2, 2, -2};
            BoardUtils.MoveInArray(row, 0, out var sectionMoved, out int moved, out int pushsize);
            S(row, new List<int> {-3, 0, 1, -1});
            Assert.AreEqual(1, sectionMoved);
            Assert.AreEqual(3, moved);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestMovement4()
        {
            var row = new List<int> {0, -2, 2, -1};
            BoardUtils.MoveInArray(row, 0, out var sectionMoved, out int moved, out int pushsize);
            S(row, new List<int> {-3, 0, 1});
            Assert.AreEqual(1, sectionMoved);
            Assert.AreEqual(3, moved);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestMovement5()
        {
            var row = new List<int> {-1, 0, -2, 2, -1};
            BoardUtils.MoveInArray(row, 1, out var sectionMoved, out int moved, out int pushsize);
            S(row, new List<int> {-4, 0, 1});
            Assert.AreEqual(0, sectionMoved);
            Assert.AreEqual(3, moved);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestMovement5B()
        {
            var row = new List<int> {-1, 0, -1, 2, 2, -1};
            BoardUtils.MoveInArray(row, 1, out var sectionMoved, out int moved, out int pushsize);
            S(row, new List<int> {-3, 0, 3, -1});
            Assert.AreEqual(0, sectionMoved);
            Assert.AreEqual(2, moved);
            Assert.AreEqual(3, pushsize);
        }

        [TestMethod]
        public void TestMovement5C()
        {
            var row = new List<int> {-1, 0, -1, 2, 1, -1};
            BoardUtils.MoveInArray(row, 1, out var sectionMoved, out int moved, out int pushsize);
            S(row, new List<int> {-3, 0, 2, -1});
            Assert.AreEqual(0, sectionMoved);
            Assert.AreEqual(2, moved);
            Assert.AreEqual(2, pushsize);
        }

        [TestMethod]
        public void TestMovement5D()
        {
            var row = new List<int> {-1, 0, -1, 2, 1, -3};
            BoardUtils.MoveInArray(row, 1, out var sectionMoved, out int moved, out int pushsize);
            S(row, new List<int> {-3, 0, 2, -3});
            Assert.AreEqual(0, sectionMoved);
            Assert.AreEqual(2, moved);
            Assert.AreEqual(2, pushsize);
        }

        [TestMethod]
        public void TestMovement5E()
        {
            var row = new List<int> {-1, 0, -1, 1, 1, -3};
            BoardUtils.MoveInArray(row, 1, out var sectionMoved, out int moved, out int pushsize);
            S(row, new List<int> {-3, 0, 1, -3});
            Assert.AreEqual(0, sectionMoved);
            Assert.AreEqual(2, moved);
            Assert.AreEqual(0, pushsize);
        }

        [TestMethod]
        public void TestMovement5F()
        {
            var row = new List<int> {1, 0, -1, 1, 1, -3};
            BoardUtils.MoveInArray(row, 1, out var sectionMoved, out int moved, out int pushsize);
            S(row, new List<int> {1, -2, 0, 1, -3});
            Assert.AreEqual(1, sectionMoved);
            Assert.AreEqual(2, moved);
            Assert.AreEqual(0, pushsize);
        }

        [TestMethod]
        public void TestMovement5G()
        {
            var row = new List<int> {1, -1, 0, -2, 1, 2, -3};
            BoardUtils.MoveInArray(row, 2, out var sectionMoved, out int moved, out int pushsize);
            S(row, new List<int> {1, -4, 0, 2, -3});
            Assert.AreEqual(0, sectionMoved);
            Assert.AreEqual(3, moved);
            Assert.AreEqual(0, pushsize);
        }

        [TestMethod]
        public void TestMovement6()
        {
            var row = new List<int> {-1, 2, -7, 2, 2, 0, -2, 2, -1};
            BoardUtils.MoveInArray(row, 5, out var sectionMoved, out int moved, out int pushsize);
            S(row, new List<int> {-1, 2, -7, 2, 2, -3, 0, 1});
            Assert.AreEqual(1, sectionMoved);
            Assert.AreEqual(3, moved);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestMovement7()
        {
            var row = new List<int> {-1, 2, -7, 2, 2, 0, -2, 2, -10};
            BoardUtils.MoveInArray(row, 5, out var sectionMoved, out int moved, out int pushsize);
            S(row, new List<int> {-1, 2, -7, 2, 2, -3, 0, 1, -9});
            Assert.AreEqual(1, sectionMoved);
            Assert.AreEqual(3, moved);
            Assert.AreEqual(1, pushsize);
        }

        [TestMethod]
        public void TestParsing()
        {
            var b = GetBigBoard();
            Assert.AreEqual(b.BoardX, 10);
            Assert.AreEqual(b.BoardY, 22);
            Assert.AreEqual(b.BoardString,
                "..........,..........,.......b..,..........,...a...a..,..........,.......d..,..........,...a......,.......a..,...b......,.a..aa.a..,..........,.a....g...,..........,.....ba.a.,..........,..........,....a..a..,.......b..,..........,..........");
        }

        private Board GetBigBoard()
        {
            var b = new Board(Board.Big, 0);
            return b;
        }


        private Board GetSmallBoard()
        {
            var b = new Board(Board.Small, 0);
            return b;
        }

        [TestMethod]
        public void TestBoardCreation()
        {
            var b = GetBigBoard();
            b.Show1();
            b.Show2();
            b.Show3();
            b.ShowCols();
            S(b.Rows[1], new List<int> {-10});
            S(b.Rows[2], new List<int> {-7, 2, -2});
            S(b.Rows[11], new List<int> {-1, 1, -2, 1, 1, -1, 1, -2});
            S(b.Rows[21], new List<int> {-10});
            S(b.Cols[0], new List<int> {-22});
            S(b.Cols[9], new List<int> {-22});
            S(b.Cols[1], new List<int> {-11, 1, -1, 1, -8});
            S(b.Cols[7], new List<int> {-2, 2, -1, 1, -1, 4, -2, 1, -1, 1, -6, 1, 2, -2});
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
            var arr = new List<int> {0, -1, 2, 2};
            BoardUtils.RemoveZeroAtSectionFromList(arr, 0);
            S(arr, new List<int> {-2, 2, 2});
        }

        [TestMethod]
        public void TestBallRemovalFromArrayBySection2()
        {
            var arr = new List<int> {0, 1, 2, 2};
            BoardUtils.RemoveZeroAtSectionFromList(arr, 0);
            S(arr, new List<int> {-1, 1, 2, 2});
        }

        [TestMethod]
        public void TestBallRemovalFromArrayBySection3()
        {
            var arr = new List<int> {5, 0, 2, 2};
            BoardUtils.RemoveZeroAtSectionFromList(arr, 1);
            S(arr, new List<int> {5, -1, 2, 2});
        }

        [TestMethod]
        public void TestBallRemovalFromArrayBySection4()
        {
            var arr = new List<int> {5, 0, -1, 2, 2};
            BoardUtils.RemoveZeroAtSectionFromList(arr, 1);
            S(arr, new List<int> {5, -2, 2, 2});
        }

        [TestMethod]
        public void TestBallRemovalFromArrayBySection5()
        {
            var arr = new List<int> {-4, 0, -1, 2, 2};
            BoardUtils.RemoveZeroAtSectionFromList(arr, 1);
            S(arr, new List<int> {-6, 2, 2});
        }

        [TestMethod]
        public void TestBallRemovalFromArrayBySection6()
        {
            var arr = new List<int> {-4, 0, 1, 2, 2};
            BoardUtils.RemoveZeroAtSectionFromList(arr, 1);
            S(arr, new List<int> {-5, 1, 2, 2});
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition0()
        {
            var arr = new List<int> {-4, 1, -2};
            BoardUtils.AddZeroAtPositionInList(0, arr, out int section);
            S(arr, new List<int> {0, -3, 1, -2});
            Assert.AreEqual(0, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition1()
        {
            var arr = new List<int> {-4, 1, -2};
            BoardUtils.AddZeroAtPositionInList(1, arr, out int section);
            S(arr, new List<int> {-1, 0, -2, 1, -2});
            Assert.AreEqual(1, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition3()
        {
            var arr = new List<int> {-4, 1, -2};
            BoardUtils.AddZeroAtPositionInList(2, arr, out int section);
            S(arr, new List<int> {-2, 0, -1, 1, -2});
            Assert.AreEqual(1, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition4()
        {
            var arr = new List<int> {-4, 1, -2};
            BoardUtils.AddZeroAtPositionInList(5, arr, out int section);
            S(arr, new List<int> {-4, 1, 0, -1});
            Assert.AreEqual(2, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition5()
        {
            var arr = new List<int> {2, -1};
            BoardUtils.AddZeroAtPositionInList(1, arr, out int section);
            S(arr, new List<int> {2, 0});
            Assert.AreEqual(1, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition5B()
        {
            var arr = new List<int> {2, -2};
            BoardUtils.AddZeroAtPositionInList(2, arr, out int section);
            S(arr, new List<int> {2, -1, 0});
            Assert.AreEqual(2, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition6()
        {
            var arr = new List<int> {-4, 2, -2, 2, -2};
            BoardUtils.AddZeroAtPositionInList(6, arr, out int section);
            S(arr, new List<int> {-4, 2, -1, 0, 2, -2});
            Assert.AreEqual(3, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition7()
        {
            var arr = new List<int> {-4, 2, -3, 2, -2};
            BoardUtils.AddZeroAtPositionInList(7, arr, out int section);
            S(arr, new List<int> {-4, 2, -2, 0, 2, -2});
            Assert.AreEqual(3, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition8()
        {
            var arr = new List<int> {2, -3, 2, -2};
            BoardUtils.AddZeroAtPositionInList(5, arr, out int section);
            S(arr, new List<int> {2, -3, 2, 0, -1});
            Assert.AreEqual(3, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition9()
        {
            var arr = new List<int> {-4, 1, 2, -3, 2, -2};
            BoardUtils.AddZeroAtPositionInList(11, arr, out int section);
            S(arr, new List<int> {-4, 1, 2, -3, 2, -1, 0});
            Assert.AreEqual(6, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition10()
        {
            var arr = new List<int> {-3, 2, -3};
            BoardUtils.AddZeroAtPositionInList(6, arr, out int section);
            S(arr, new List<int> {-3, 2, -2, 0});
            Assert.AreEqual(3, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition10B()
        {
            var arr = new List<int> {-3};
            BoardUtils.AddZeroAtPositionInList(0, arr, out int section);
            S(arr, new List<int> {0, -2});
            Assert.AreEqual(0, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition10C()
        {
            var arr = new List<int> {-3};
            BoardUtils.AddZeroAtPositionInList(1, arr, out int section);
            S(arr, new List<int> {-1, 0, -1});
            Assert.AreEqual(1, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition10D()
        {
            var arr = new List<int> {-3};
            BoardUtils.AddZeroAtPositionInList(2, arr, out int section);
            S(arr, new List<int> {-2, 0});
            Assert.AreEqual(1, section);
        }

        [TestMethod]
        public void TestBallPlacementInArrayByPosition11()
        {
            var arr = new List<int> {-3, 2, -3};
            BoardUtils.AddZeroAtPositionInList(6, arr, out int section);
            S(arr, new List<int> {-3, 2, -2, 0});
            Assert.AreEqual(3, section);
        }

        private void S(List<int> actual, List<int> expected)
        {
            if (expected.SequenceEqual(actual))
            {
                return;
            }

            Console.WriteLine($"actual:  {P(actual)}\nexpected:{P(expected)}");
            Assert.IsFalse(true, $"actual:  {P(actual)}\nexpected:{P(expected)}");
        }

        private string P(List<int> input)
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

        [TestMethod]
        public void TestPushAdder1()
        {
            var arr = new List<int> {-5};
            BoardUtils.AddPushToListAt(arr, 2, 2);
            S(arr, new List<int> {-2, 2, -2});
        }

        [TestMethod]
        public void TestPushAdder2()
        {
            var arr = new List<int> {-5};
            BoardUtils.AddPushToListAt(arr, 4, 2);
            S(arr, new List<int> {-4, 2});
        }

        [TestMethod]
        public void TestPushAdder3()
        {
            var arr = new List<int> {-5};
            BoardUtils.AddPushToListAt(arr, 0, 2);
            S(arr, new List<int> {2, -4});
        }

        [TestMethod]
        public void TestPushAdder4()
        {
            var arr = new List<int> {-5};
            BoardUtils.AddPushToListAt(arr, 2, 1);
            S(arr, new List<int> {-2, 1, -2});
        }

        [TestMethod]
        public void TestPushAdder5()
        {
            var arr = new List<int> {2, -5};
            BoardUtils.AddPushToListAt(arr, 2, 2);
            S(arr, new List<int> {2, -1, 2, -3});
        }

        [TestMethod]
        public void TestPushAdder6()
        {
            var arr = new List<int> {2, -5};
            BoardUtils.AddPushToListAt(arr, 0, 2);
            S(arr, new List<int> {4, -5});
        }

        [TestMethod]
        public void TestPushAdder7()
        {
            var arr = new List<int> {2, -5};
            BoardUtils.AddPushToListAt(arr, 1, 2);
            S(arr, new List<int> {2, 2, -4});
        }

        [TestMethod]
        public void TestPushAdder8()
        {
            var arr = new List<int> {2, -5};
            BoardUtils.AddPushToListAt(arr, 5, 2);
            S(arr, new List<int> {2, -4, 2});
        }
    }
}