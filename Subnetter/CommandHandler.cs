using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subnetter
{
    class CommandHandler
    {
        /// <summary>
        /// Parses command with space-separated arguments into argument array. 
        /// Double quotes can be used to specify arguments that include spaces, and 
        /// double quotes can be escaped using a backslash (\).
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string[] GetArgs(string command)
        {
            List<string> ArgList = new List<string>();

            byte QuoteInstance = 0; //0 = not in quote, 1 = in double quotes
            short Index = -1;
            string Current = "";
            char[] ca = command.ToCharArray();
            foreach (char c in ca)
            {
                Index++;

                if (QuoteInstance == 0)
                {
                    if (c == '\"')
                    {
                        try
                        {
                            if (ca[Index - 1] == '\\' && ca[Index-2] != '\\')
                            {
                                Current = Current.Substring(0, Current.Length - 1);
                                Current += c.ToString();
                            }
                            else
                            {
                                QuoteInstance++;
                            }
                        }
                        catch (IndexOutOfRangeException) { QuoteInstance++; }
                    }
                    else if (c == ' ')
                    {
                        try { if (ca[Index - 1] == '\"') continue; }
                        catch (IndexOutOfRangeException) { continue; }

                        ArgList.Add(Current);
                        Current = "";
                    }
                    else
                    {
                        Current += c.ToString();
                    }
                }
                else if (QuoteInstance == 1)
                {
                    if (c == '\"')
                    {
                        if (ca[Index - 1] == '\\' && ca[Index - 2] != '\\')
                        {
                            Current = Current.Substring(0, Current.Length - 1);           
                            Current += c.ToString();
                        }
                        else
                        {
                            if(ca[Index - 1] == '\\' && ca[Index - 2] == '\\')
                                Current = Current.Substring(0, Current.Length - 1);

                            QuoteInstance--;
                            ArgList.Add(Current);
                            Current = "";
                        }
                    }
                    else
                    {
                        Current += c.ToString();
                    }
                }
            }
            if (Current != "")
                ArgList.Add(Current);

            int Length = 0;
            foreach (string s in ArgList)
                Length++;
            string[] ArgArray = new string[Length];
            Index = 0;
            foreach (string s in ArgList)
            {
                ArgArray[Index] = s;
                Index++;
            }

            return ArgArray;
        }
    }
}
