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

        public static string ToChar(int el)
        {
            if (el < 0)
            {
                return new String('.', -1 * el);
            }
            else
            {
                return el.ToString();
            }
        }

        public static void Show3(this Board board)
        {
            Console.WriteLine("Board Rows.");
            System.Diagnostics.Debug.WriteLine("Board Rows.");
            foreach (var row in board.Rows)
            {
                foreach (var el in row)
                {
                    Console.Write(ToChar(el));
                    System.Diagnostics.Debug.Write(ToChar(el));
                }
                Console.WriteLine();
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        public static void Show4(this Board board)
        {
            Console.WriteLine("Board Cols.");
            System.Diagnostics.Debug.WriteLine("Board Cols.");
            foreach (var col in board.Cols)
            {
                foreach (var el in col)
                {
                    Console.WriteLine(ToChar(el));
                    System.Diagnostics.Debug.Write(ToChar(el));
                }
                Console.WriteLine();
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        public static List<List<int>> MakeRows(string input)
        {
            var rr = new List<List<int>>();
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
                rr.Add(res);
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
         
        public static void MoveInArray(List<int> list, int section, out int sectionMoved, out int moved, out int pushsize)
        {
            moved = 0;
            pushsize = 0;
            sectionMoved = 0;
            if (section == 0 || list[section - 1] > 0) //hackily determine the resulting section.
            {
                sectionMoved++;
            }
            //row: 0,-2, 2, -1. direction R ==> -4,0,1
            
            list.RemoveAt(section); //-2, 2, -1
            list[section] = list[section] - 1;//the gap. -3S,2,-1
            moved = moved + -1 * list[section]; //-3 is exactly how far to the right it moved.
            list.Insert(section + 1, 0); //-3S,0,2,-1
            if (list[section + 2] > 1) //hit greater than 1, will pushsize.
            {
                if (list[section + 3] > 0) //double pushsize
                {
                    list[section + 2] += list[section + 3] - 1; //shrink the gap on the other side //
                    list.RemoveAt(section + 3);
                }
                else
                {
                    list[section + 2]--;
                    if (list[section + 3] == -1)
                    {
                        list.RemoveAt(section + 3);
                    }
                    else
                    {
                        list[section + 3]++; //shrink the gap on the other side
                    }
                }
                pushsize = list[section + 2];
            }
            else //-3S,0,1,-1
            {
                list.RemoveAt(section + 2); //-7
            }
            if (section > 0)
            {
                if (list[section - 1] < 0 && list[section] < 0)
                {
                    list[section] += list[section - 1];
                    list.RemoveAt(section - 1);
                }
            }
        }

        /// <summary>
        /// we don't know the section we're adding to since this isn't aligned with ballPosition.
        /// </summary>
        public static void AddPushToListAt(List<int> list, int pushat, int pushsize)
        {
            if (pushsize == 0) //leave it untouched.
            {
                return;
            }
            // [-5], 3, 2 => [-2,2,-2]
            var seen = 0;
            var section = 0;
            while (seen < pushat)
            {
                seen += list[section] > 0 ? 1 : -1 * list[section];
                section += 1; //the NEXT section.
            }
            if (seen > pushat) //split
            {
                //pushing 3 at position 5
                //3,-5,3
                //seen is 8
                //going to break up the -5
                section--;
                var removed = list[section];
                list.RemoveAt(section);
                var after = seen - pushat - 1;
                var bef = -1*removed - after - 1;

                if (after > 0)
                {
                    list.Insert(section,-1*after);
                }
                list.Insert(section, pushsize);
                if (bef > 0)
                {
                    list.Insert(section,-1*  bef);
                }
            }
            else //equal
            {
                if (list[section] > 0)
                {
                    list[section] += pushsize;
                }
                else
                {
                    if (list[section] == -1)
                    {
                        list[section]=pushsize; //just replace it.
                    }
                    else
                    {
                        list[section]+=1; //just replace it.
                        list.Insert(section, pushsize); //-4,2,-2,2 => -4,2,P,-1,2
                    }
                }
            }
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
        public static void AddZeroAtPositionInList(int pos, List<int> list, out int section)
        {
            var seen = 0;
            var mySection = 0;
            while (true)
            {
                var next = 0;
                if (list[mySection] > 0)
                {
                    next = next + 1;
                }
                else
                {
                    next = Math.Abs(list[mySection]);
                }
                if (seen + next > pos || mySection==list.Count-1)
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

            if (list[mySection] >= 0)
            {
                throw new Exception("Problem");
            }
            //we are sure that the section we see is negative.
            var before = pos - seen;
            var after = -1*list[mySection] - before - 1;
            section = mySection;
            list.RemoveAt(mySection);
            if (after > 0)
            {
                list.Insert(mySection, -1*after);
            }
            list.Insert(mySection, 0);

            if (before > 0)
            {
                list.Insert(mySection, -1*before);
                section++;
            }
        }

        public static void RemoveZeroAtSectionFromList(List<int> list, int section)
        {
            if (section > 0) //
            {
                list.RemoveAt(section);
                var bef = list[section - 1];
                var aft = list[section];
                if (bef < 0 && aft < 0) //-1,0,-1 (-1,-1P)
                {
                    list.Insert(section, list[section] + list[section - 1] - 1); //-1,-3P,-1
                    list.RemoveAt(section + 1);
                    list.RemoveAt(section - 1);
                }
                else if (bef < 0 && aft > 0) //-1,0,1 (-1,1P)
                {
                    list[section - 1] = list[section - 1] - 1;
                }
                else if (bef > 0 && aft < 0) // 1,0,-1 (1,-1P)
                {
                    list[section] = list[section] - 1;
                }
                else if (bef > 0 && aft > 0) //1,0,1 (1,1P)
                {
                    list.Insert(section, -1);
                }

            }
            else //section==0
            {
                list.RemoveAt(section);
                // 0, 3 or 0,-3
                if (list[section] < 0) // 0,-2 => -3
                {
                    list[section] = list[section] - 1;
                }
                else //0,3 => -1,3
                {
                    list.Insert(0, -1);
                }
            }
        }
    }
}
