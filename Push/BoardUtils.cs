using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Push
{
    public static class BoardUtils
    {
        public static void ShowCols(this Board board)
        {
            foreach (var col in board.Cols)
            {
                var ss = String.Join("", col.Select(el => el.ToString()));
                Console.WriteLine(ss);
            }
        }

        public static void Show1(this Board board)
        {
            foreach (var row in board.BoardString.Split(','))
            {
                Console.WriteLine(row);
            }
        }

        public static void Show2(this Board board)
        {
            foreach (var row in board.Rows)
            {
                var ss = String.Join("", row.Select(el => el.ToString()));
                Console.WriteLine(ss);
            }
        }

        public static void Show3(this Board board)
        {
            foreach (var row in board.Rows)
            {
                foreach (var el in row)
                {
                    if (el < 0)
                    {
                        Console.Write(new String('.', -1 * el));
                    }
                    else
                    {
                        Console.Write(el);
                    }
                }
                Console.WriteLine();
            }
        }

        public static List<int[]> MakeRows(string input)
        {
            var rr = new List<int[]>();
            //this sets up the rows.  now the hard part.
            foreach (var row in input.Split(','))
            {
                var res = new List<int>();
                short emptyCount = 0;
                foreach (var @char in row)
                {
                    if (@char == '.')
                    {
                        emptyCount--;
                    }
                    else
                    {
                        if (emptyCount < 0)
                        {
                            res.Add(emptyCount);
                            emptyCount = 0;
                        }
                        var val = (Convert.ToInt16(@char) - 96);
                        if (val > 127 || val < -127)
                        {
                            throw new Exception($"Val was too big in level construction! {val}");
                        }
                        res.Add(val);
                    }
                }
                if (emptyCount < 0)
                {
                    res.Add(emptyCount);
                }
                rr.Add(res.ToArray());
            }
            return rr;
        }

        public static string Transpose(string input)
        {
            // ...b. => .,.,.,b,.
            //
            // .ab  =>  ..
            // ..c  =>  a.
            //          bc
            var res = new List<List<char>>();
            foreach (var row in input.Split(','))
            {
                var ii = 0;
                foreach (var @char in row)
                {
                    if (res.Count < ii + 1)
                    {
                        res.Add(new List<char>());
                    }
                    res[ii].Add(@char);
                    ii++;
                }
            }

            var res2 = new List<string>();
            foreach (var row in res)
            {
                res2.Add(String.Join("", row));
            }

            return String.Join(",", res2);
        }
         
        //because we know the absolute position of the incoming
        //pos represents the original pos.  it should be fairly easy to handle maintaining this
        //so that we can do orthogonality checking.
        public static int[] MoveInArray(int pos, int section, int[] array, out int outSection, out int endpos, out int pushsize)
        {
            endpos = pos;
            pushsize = 0;
            outSection = section;
            if (section == 0 || array[section - 1] > 0) //hackily determine the resulting section.
            {
                outSection++;
            }
            //row: 0,-2, 2, -1. direction R ==> -4,0,1
            var newrow = new List<int>(array);
            newrow.RemoveAt(section); //-2, 2, -1
            newrow[section] = newrow[section] - 1;//the gap. -3S,2,-1
            endpos = endpos + -1 * newrow[section]; //-3 is exactly how far to the right it moved.
            newrow.Insert(section + 1, 0); //-3S,0,2,-1
            if (newrow[section + 2] > 1) //hit greater than 1, will pushsize.
            {
                if (newrow[section + 3] > 0) //double pushsize
                {
                    newrow[section + 2] += newrow[section + 3] - 1; //shrink the gap on the other side //
                    newrow.RemoveAt(section + 3);
                }
                else
                {
                    newrow[section + 2]--;
                    if (newrow[section + 3] == -1)
                    {
                        newrow.RemoveAt(section + 3);
                    }
                    else
                    {
                        newrow[section + 3]++; //shrink the gap on the other side
                    }
                }
                pushsize = newrow[section + 2];
            }
            else //-3S,0,1,-1
            {
                newrow.RemoveAt(section + 2); //-7
            }
            if (section > 0)
            {
                if (newrow[section - 1] < 0 && newrow[section] < 0)
                {
                    newrow[section] += newrow[section - 1];
                    newrow.RemoveAt(section - 1);
                }
            }
            return newrow.ToArray();
        }

        public static int SumPositionBeforeZero(IEnumerable<int> array)
        {
            var res = 0;
            foreach (var el in array)
            {
                if (el < 0)
                {
                    res += -1 * el;
                }
                else
                {
                    res += 1;
                }
            }
            return res;
        }
        /// <summary>
        /// This is different than the other one.  This adds by absolute sum.
        /// what section actually means is a little unclear.  section is zero indexed.
        /// 
        /// //this is very confusing - positive numbers represent single cells only!
        /// </summary>
        public static int[] AddZeroAtPositionInArray(int pos, int[] array, out int section)
        {
            var newrow = new List<int>(array);

            var seen = 0;
            var mySection = 0;
            while (true)
            {
                var next = 0;
                if (array[mySection] > 0)
                {
                    next = next + 1;
                }
                else
                {
                    next = Math.Abs(array[mySection]);
                }
                if (seen + next > pos || mySection==array.Length-1)
                {
                    break;
                }
                seen += next;
                mySection++;
                //section should point to the negative section you sohuld insert in.
                //seen should say how many positions you are advanced before that.
            }
            //-2,2,    0 => 0section, 0 seen 
            //-2,2,    1 => 0section, 0 seen 
            //-2,2,    2,2 => BoardX
            //-2,2,-2  3 => 2section, 4 seen
            //-2,2,-2  4 => 2section, 4 seen 

            if (array[mySection] >= 0)
            {
                throw new Exception("Problem");
            }
            //we are sure that the section we see is negative.
            var before = pos - seen;
            var after = -1*array[mySection] - before - 1;
            section = mySection;
            newrow.RemoveAt(mySection);
            if (after > 0)
            {
                newrow.Insert(mySection, -1*after);
            }
            newrow.Insert(mySection, 0);

            if (before > 0)
            {
                newrow.Insert(mySection, -1*before);
                section++;
            }

            //we need to update this carefully.
            
            return newrow.ToArray();
        }

        public static int[] RemoveZeroAtSectionFromArray(int section, int[] array)
        {
            var newrow = new List<int>(array);

            if (section > 0) //
            {
                newrow.RemoveAt(section);
                var bef = newrow[section - 1];
                var aft = newrow[section];
                if (bef < 0 && aft < 0) //-1,0,-1 (-1,-1P)
                {
                    newrow.Insert(section, newrow[section] + newrow[section - 1] - 1); //-1,-3P,-1
                    newrow.RemoveAt(section + 1);
                    newrow.RemoveAt(section - 1);
                }
                else if (bef < 0 && aft > 0) //-1,0,1 (-1,1P)
                {
                    newrow[section - 1] = newrow[section - 1] - 1;
                }
                else if (bef > 0 && aft < 0) // 1,0,-1 (1,-1P)
                {
                    newrow[section] = newrow[section] - 1;
                }
                else if (bef > 0 && aft > 0) //1,0,1 (1,1P)
                {
                    newrow.Insert(section, -1);
                }

            }
            else //section==0
            {
                newrow.RemoveAt(section);
                // 0, 3 or 0,-3
                if (newrow[section] < 0) // 0,-2 => -3
                {
                    newrow[section] = newrow[section] - 1;
                }
                else //0,3 => -1,3
                {
                    newrow.Insert(0, -1);
                }
            }
            return newrow.ToArray();
        }
    }
}
