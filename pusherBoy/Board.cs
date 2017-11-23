using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

//todo:
//get reading/writing boards set
//get a good testing system set up
//get a board generator

namespace Pusherboy
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
        public List<int[]> Rows = new List<int[]>();

        public List<int[]> Cols = new List<int[]>();

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
            Rows[BallPosition.Y] = BoardUtils.RemoveZeroAtSectionFromArray(BallXSection, Rows[BallPosition.Y]);
            Cols[BallPosition.X] = BoardUtils.RemoveZeroAtSectionFromArray(BallYSection, Cols[BallPosition.X]);
            //we've already adjusted the row & cols.

            BallPosition = null;
        }

        public void SetBallPosition(int x, int y)
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
            Rows[y] = BoardUtils.AddZeroAtPositionInArray(x, Rows[y], out var xs);
            BallXSection = xs;
            Cols[x] = BoardUtils.AddZeroAtPositionInArray(y, Cols[x], out var ys);
            BallYSection = ys;
            BallPosition = pos;
        }

        /// <summary>
        /// walk across the row to find the solid brick at pos in, kill it, and return section.
        /// </summary>
        private int[] BumpIntoArrayAtPosition(int[] array, int pos, out int section)
        {
            int seen = 0;
            section = 0;
            while (seen < pos)
            {
                if (array[section] < 0)
                {
                    seen += -1 * array[section];
                }
                else
                {
                    seen++;
                }
                section++;
            }
            //insert zero.
            array[section] = 0;
            return array;
        }

        public void Move(Direction d)
        {
            ValidateMove(d);
            int xs = 0;
            int ys = 0;
            int xp = 0;
            int yp = 0;
            int pushsize = 0; //if you push a bunch of boxes to the next call, how many is it?
            //zero means you hit a 1.  this ties in with havin
            switch (d)
            {
                //we know the position, and direction.
                //we need to update 
                case Direction.D:
                    //clean up the place i left from
                    //don't we need to know the result of this so we can adjust the corresponding row to yp
                    Cols[BallPosition.X] = BoardUtils.MoveInArray(BallPosition.X, BallYSection, Cols[BallPosition.X], out ys, out yp, out pushsize);

                    Rows[BallPosition.Y] = BoardUtils.RemoveZeroAtSectionFromArray(BallYSection, Rows[BallPosition.Y]);

                    //don't we need to know the output position at this point?

                    //clean up the place i bumped into
                    //we are fixing up the xs
                    //we moved down.  this is actually "bump into row at position"
                    BumpIntoArrayAtPosition(Rows[BallPosition.Y], BallPosition.X, out xp);
                    yp = BoardUtils.SumPositionBeforeZero(Cols[BallPosition.X]);
                    break;
                case Direction.U:
                    Cols[BallPosition.X] = BoardUtils.MoveInArray(BallPosition.X, Cols[BallPosition.X].Length - BallYSection,  Cols[BallPosition.X].Reverse().ToArray(), out ys, out yp, out pushsize);
                    
                    //also have to remove zeros from the orthogonals!
                    Rows[BallPosition.Y] = BoardUtils.RemoveZeroAtSectionFromArray(BallYSection, Rows[BallPosition.Y]);
                    ys = Cols[BallPosition.X].Length - ys;
                    yp = BoardUtils.SumPositionBeforeZero(Cols[BallPosition.X].Reverse());
                    break;
                
                case Direction.R:
                    Rows[BallPosition.Y] = BoardUtils.MoveInArray(BallPosition.Y, BallXSection, Rows[BallPosition.Y], out xs, out xp, out pushsize);

                    Rows[BallPosition.Y] = BoardUtils.RemoveZeroAtSectionFromArray(BallXSection, Cols[BallPosition.X]);
                    xp = BoardUtils.SumPositionBeforeZero(Rows[BallPosition.Y]);
                    break;
                case Direction.L:
                    Rows[BallPosition.Y] = BoardUtils.MoveInArray(BallPosition.Y, Rows[BallPosition.Y].Length - BallXSection, Rows[BallPosition.Y].Reverse().ToArray(), out xs, out xp, out pushsize);
                    xs = Rows[BallPosition.Y].Length - xs;
                    xp = BoardUtils.SumPositionBeforeZero(Rows[BallPosition.Y].Reverse());

                    BoardUtils.RemoveZeroAtSectionFromArray(BallXSection, Cols[BallPosition.X]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(d), d, null);
            }
            //I also have to adjust the position and xsection shit.
            BallXSection = xs;
            BallYSection = ys;
            SetBallPosition(xp, yp);
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
                    if (Rows[BallPosition.Y][BallXSection - 1] >= 0)
                        throw new Exception("can't go that way");
                    break;
                case Direction.L:
                    if (Rows[BallPosition.Y][BallXSection + 1] >= 0)
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

        public static string Sample = @"<param name='bgcolor' value='#888888' />
			<param name='FlashVars' value='x=10&y=22&board=..........,..........,.......b..,..........,...a...a..,..........,.......d..,..........,...a......,.......a..,...b......,.a..aa.a..,..........,.a....g...,..........,.....ba.a.,..........,..........,....a..a..,.......b..,..........,..........' />
			<embed src='Slide.swf'
			width='1050'
			height='650'
			autostart='false'";
    }
}