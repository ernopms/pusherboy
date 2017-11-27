using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

//todo:
//get reading/writing boards set
//get a good testing system set up
//get a board generator

namespace Push
{
    public class Board
    {
        public string BoardString { get; set; }
        public int LevelNumber { get; set; }
        public int BoardX { get; set; }
        public int BoardY { get; set; }
        private const string Pattern = @"param name='FlashVars' value='x=(\d+)&y=(\d+)&board=([a-z\.,]+)";

        //The location - in board state represented by a zero.
        public BallPosition BallPosition { get; private set; }

        //Which index of the row is the zero in?
        public int BallXSection { get; private set; }

        public int BallYSection { get; private set; }

        //one of these guys consists of a series of numbers.
        //this row:
        // .....a...ab...
        //would be stored as -5,1,-3,1,2,-3
        public List<List<int>> Rows = new List<List<int>>();

        public List<List<int>> Cols = new List<List<int>>();

        public Board(string pageData, int levelNumber)
        {
            LevelNumber = levelNumber;
            Setup(pageData);
        }

        //Undoes ball position.
        public void ClearBallPosition()
        {
            if (BallPosition == null)
            {
                throw new Exception("was asked to remove ball when ballposition is null!");
            }

            //how to convert something like -4,3,0,-3, => -4,3,-4
            //todo isn't this already taken care of by "move"...?
            BoardUtils.RemoveZeroAtSectionFromList(Rows[BallPosition.Y], BallXSection);
            BoardUtils.RemoveZeroAtSectionFromList(Cols[BallPosition.X], BallYSection);
            //we've already adjusted the row & cols.

            BallPosition = null;
        }

        public void InitialBallPlacement(int x, int y)
        {
            if (BallPosition != null)
            {
                throw new Exception("Ballposition must be null");
            }
            var pos = new BallPosition
            {
                X = x,
                Y = y
            };

            //figure out the ballXsection.
            BoardUtils.AddZeroAtPositionInList(x, Rows[y], out var xs);
            BallXSection = xs;
            BoardUtils.AddZeroAtPositionInList(y, Cols[x], out var ys);
            BallYSection = ys;
            BallPosition = pos;
        }

        /// <summary>
        /// walk across the row to find the solid brick at pos in, kill it, and return section.
        /// </summary>
        private void BumpIntoArrayAtPosition(List<int> list, int pos, out int section)
        {
            int seen = 0;
            section = 0;
            while (seen < pos)
            {
                if (list[section] < 0)
                {
                    seen += -1 * list[section];
                }
                else
                {
                    seen++;
                }
                section++;
            }
            //insert zero.
            list[section] = 0;
        }


        public void Move(Direction d)
        {
            ValidateMove(d);
            int xs = 0;
            int ys = 0;
            int sectionMoved;

            int xd = 0;
            int yd = 0;
            int moved;
            int pushsize; //if you push a bunch of boxes to the next call, how many is it?
            //zero means you hit a 1.

            switch (d)
            {
                //move in the row/col matching the direction of movement.
                //remove the zero from the opposite list starting point
                //calculate bump from opposite
                //if push size > 0 also add that to the opposite.
                //so much work!
                case Direction.D:
                    //fix cur
                    BoardUtils.MoveInArray(Cols[BallPosition.X], BallYSection, out sectionMoved, out moved,
                        out pushsize);
                    yd = moved;
                    ys = BallYSection + sectionMoved;

                    //fix opp source
                    BoardUtils.RemoveZeroAtSectionFromList(Rows[BallPosition.Y], BallXSection);

                    //fix hit source, also adjusts xs

                    BumpIntoArrayAtPosition(Rows[BallPosition.Y + yd], BallPosition.X, out xs);

                    //adjust push.  no need to mess with xs or ys.
                    BoardUtils.AddPushToListAt(Rows[BallPosition.Y + yd + 1], BallPosition.X, pushsize);

                    break;
                case Direction.U: //this requires some careful reversing.
                    var revsec = Cols[BallPosition.X].Count - BallYSection - 1;
                    var beforeLen = Cols[BallPosition.X].Count;
                    
                    //reverse this column, then move in it.
                    Cols[BallPosition.X] = Cols[BallPosition.X].ToArray().Reverse().ToList();
                    BoardUtils.MoveInArray(Cols[BallPosition.X], revsec, out sectionMoved, out moved,
                        out pushsize);
                    //now reverse it again.
                    Cols[BallPosition.X] = Cols[BallPosition.X].ToArray().Reverse().ToList();
                    yd = -1 * moved;

                    //in reverse mode then shrinking/expanding the list is equivalent to changing sections!
                    sectionMoved += beforeLen - Cols[BallPosition.X].Count;

                    ys = BallYSection - sectionMoved;
                    //the naive way to determine sectionmoved doesn't work when reversing. 
                    //instead look at 

                    //fix opp source
                    BoardUtils.RemoveZeroAtSectionFromList(Rows[BallPosition.Y], BallXSection);

                    //fix hit source, also adjusts xs

                    BumpIntoArrayAtPosition(Rows[BallPosition.Y + yd], BallPosition.X, out xs);

                    //adjust push.  no need to mess with xs or ys.
                    BoardUtils.AddPushToListAt(Rows[BallPosition.Y + yd - 1], BallPosition.X, pushsize);

                    break;

                case Direction.R:
                    BoardUtils.MoveInArray(Rows[BallPosition.Y], BallXSection, out sectionMoved, out moved,
                        out pushsize);
                    xd = moved;
                    xs = BallXSection + sectionMoved;

                    BoardUtils.RemoveZeroAtSectionFromList(Cols[BallPosition.X], BallYSection);

                    BumpIntoArrayAtPosition(Cols[BallPosition.X + xd], BallPosition.Y, out ys);

                    BoardUtils.AddPushToListAt(Cols[BallPosition.X + xd + 1], BallPosition.Y, pushsize);
                    break;
                case Direction.L:
                    var revsec2 = Rows[BallPosition.Y].Count - BallXSection - 1;
                    var beforeLen2 = Rows[BallPosition.Y].Count;

                    Rows[BallPosition.Y] = Rows[BallPosition.Y].ToArray().Reverse().ToList();
                    BoardUtils.MoveInArray(Rows[BallPosition.Y], revsec2, out sectionMoved, out moved,
                        out pushsize);
                    Rows[BallPosition.Y] = Rows[BallPosition.Y].ToArray().Reverse().ToList();

                    xd = -1 * moved;
                    sectionMoved += beforeLen2 - Rows[BallPosition.Y].Count;
                    xs = BallXSection - sectionMoved;

                    //fix opp source
                    BoardUtils.RemoveZeroAtSectionFromList(Cols[BallPosition.X], BallYSection);

                    //fix hit source, also adjusts xs

                    BumpIntoArrayAtPosition(Cols[BallPosition.X + xd], BallPosition.Y, out ys);

                    //adjust push.  no need to mess with xs or ys.
                    BoardUtils.AddPushToListAt(Cols[BallPosition.X + xd - 1], BallPosition.Y, pushsize);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(d), d, null);
            }
            //I also have to adjust the position and xsection shit.
            BallXSection = xs;
            BallYSection = ys;
            MoveBallTo(BallPosition.X + xd, BallPosition.Y + yd);
        }

        private void MoveBallTo(int x, int y)
        {
            var pos = new BallPosition
            {
                X = x,
                Y = y
            };
            BallPosition = pos;
        }


        //is movement this way even valid from current pos?
        //only do this in debug mode.
        private void ValidateMove(Direction d)
        {
            if (Cols[BallPosition.X][BallYSection] != 0)
                throw new Exception("Bad zero");
            if (Rows[BallPosition.Y][BallXSection] != 0)
                throw new Exception("Bad zero");
            switch (d)
            {
                //find the emp
                case Direction.U:
                    if (Cols[BallPosition.X][BallYSection - 1] >= 0)
                        throw new Exception("can't go that way");
                    break;
                case Direction.D:
                    if (Cols[BallPosition.X][BallYSection + 1] >= 0)
                        throw new Exception("can't go that way");
                    break;

                case Direction.R:
                    if (Rows[BallPosition.Y][BallXSection + 1] >= 0)
                        throw new Exception("can't go that way");
                    break;
                case Direction.L:
                    if (Rows[BallPosition.Y][BallXSection - 1] >= 0)
                        throw new Exception("can't go that way");
                    break;
            }
        }

        private void Setup(string pageData)
        {
            var res = Regex.Match(pageData, Pattern);
            if (res.Groups.Count != 4)
            {
                throw new Exception("Invalid board.");
            }
            BoardX = int.Parse(res.Groups[1].Value);
            BoardY = int.Parse(res.Groups[2].Value);
            BoardString = res.Groups[3].Value;
            Rows = BoardUtils.MakeRows(BoardString);
            Cols = BoardUtils.MakeRows(BoardUtils.Transpose(BoardString));
        }

        public void SaveBoardState(string filename)
        {
            var fp = $"{filename}.png";
        }

        public static string Big = @"<param name='bgcolor' value='#888888' />
			<param name='FlashVars' value='x=10&y=22&board=..........,..........,.......b..,..........,...a...a..,..........,.......d..,..........,...a......,.......a..,...b......,.a..aa.a..,..........,.a....g...,..........,.....ba.a.,..........,..........,....a..a..,.......b..,..........,..........' />
			<embed src='Slide.swf'
			width='1050'
			height='650'
			autostart='false'";

        public static string Small = @"<param name='bgcolor' value='#888888' />
			<param name='FlashVars' value='x=10&y=22&board=.....,.....,..b..,.....,.....' />
			<embed src='Slide.swf'
			width='1050'
			height='650'
			autostart='false'";

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
    }
}