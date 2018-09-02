// Oscar McCully    9/14/12

using System;

namespace Subnetter
{
    class Program
    {
        static void Main()
        {
            ConsoleColor OutputForeColor = ConsoleColor.Green;
            ConsoleColor InputForeColor = ConsoleColor.White;
            ConsoleColor OutputBackColor = ConsoleColor.Black;
            ConsoleColor InputBackColor = ConsoleColor.Black;
            
            string mode = "";
            string input = null;
            string temp = null;

            while (true)
            {
                try
                {
                    WriteColor(mode + "> ", InputForeColor, InputBackColor);
                    input = Console.ReadLine().Trim();
                    temp = input.ToUpper().Trim();

                    string[] args = CommandHandler.GetArgs(input);

                    switch (args[0].ToUpper())
                    {
                        case "CLEAR":
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            break;
                        case "EXIT":
                            mode = null;
                            break;
                        case "IP":
                            mode = "IP";
                            break;
                        case "SM":
                            mode = "SM";
                            break;
                        case "TITLE":
                            Console.Title = args[1];
                            break;
                        case "WIDTH":
                            int w = Int32.Parse(args[1]);
                            Console.WindowWidth = w;
                            Console.BufferWidth = w;
                            break;
                        case "BEEP":
                            switch (args[1])
                            {
                                case "?":
                                    WriteColor("<37-32767> frequency (Hz)\n", OutputForeColor, OutputBackColor);
                                    break;
                                default:
                                    int frequency = Int32.Parse(args[1]);
                                    switch (args[2])
                                    {
                                        case "?":
                                            WriteColor("<0-2147483647> duration (ms)\n", OutputForeColor, OutputBackColor);
                                            break;
                                        default:
                                            int duration = Int32.Parse(args[2]);
                                            Console.Beep(frequency,duration);
                                            break;
                                    }    
                                    break;
                            }
                            break;
                        case "COLOR":
                            switch (args[1].ToUpper())
                            {
                                case "INPUT":
                                    switch (args[2].ToUpper())
                                    {
                                        case "FOREGROUND":
                                            if (args[3] == "?")
                                                WriteColor(ColorList, OutputForeColor, OutputBackColor);
                                            else
                                                InputForeColor = ParseColor(args[3]);
                                            break;
                                        case "BACKGROUND":
                                            if (args[3] == "?")
                                                WriteColor(ColorList, OutputForeColor, OutputBackColor);
                                            else
                                                InputBackColor = ParseColor(args[3]);
                                            break;
                                        case "?":
                                            WriteColor("foreground\nbackground\n", OutputForeColor, OutputBackColor);
                                            break;
                                        default:
                                            WriteColor("Invalid command: '" + args[2] + "'\n", OutputForeColor, OutputBackColor);
                                            break;
                                    }
                                    break;
                                case "OUTPUT":
                                    switch (args[2].ToUpper())
                                    {
                                        case "FOREGROUND":
                                            if (args[3] == "?")
                                                WriteColor(ColorList, OutputForeColor, OutputBackColor);
                                            else
                                                OutputForeColor = ParseColor(args[3]);
                                            break;
                                        case "BACKGROUND":
                                            if (args[3] == "?")
                                                WriteColor(ColorList, OutputForeColor, OutputBackColor);
                                            else
                                                OutputBackColor = ParseColor(args[3]);
                                            break;
                                        case "?":
                                            WriteColor("foreground\nbackground\n", OutputForeColor, OutputBackColor);
                                            break;
                                        default:
                                            WriteColor("Invalid command: '" + args[2] + "'\n", OutputForeColor, OutputBackColor);
                                            break;
                                    }
                                    break;
                                case "?":
                                    WriteColor("input\noutput\n", OutputForeColor, OutputBackColor);
                                    break;
                                default:
                                    WriteColor("Invalid command: '" + args[1] + "'\n", OutputForeColor, OutputBackColor);
                                    break;
                            }
                            break;
                        case "?":
                            WriteColor("clear\nexit\nip\nsm\ntitle\nwidth\nbeep\ncolor\n", OutputForeColor, OutputBackColor);
                            break;
                        default:
                            switch (mode)
                            {
                                case "IP":

                                    IPAddress ipaddr = new IPAddress(args[0]);
                                    WriteColor(ipaddr.Info + "\n", OutputForeColor, OutputBackColor);
                                    break;
                                case "SM":
                                    SubnetMask smaddr = new SubnetMask(args[0]);
                                    WriteColor(smaddr.Info + "\n", OutputForeColor, OutputBackColor);
                                    break;
                                default:
                                    WriteColor("Invalid command: '" + args[0] + "'\n", OutputForeColor, OutputBackColor);
                                    break;
                            }
                            break;
                    }
                }
                catch
                {
                    WriteColor("Error\n", OutputForeColor, OutputBackColor);
                }
            }
        }

        static void WriteColor(string s, ConsoleColor ForegroundColor, ConsoleColor BackgroundColor)
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;
            Console.Write(s);
        }

        static string ColorList
        {
            get
            {
                string ret = null;
                var values = Enum.GetValues(typeof(ConsoleColor));
                foreach (ConsoleColor c in values)
                {
                    ret += c.ToString() + "\n";
                }
                return ret;
            }
            set { }
        }

        static ConsoleColor ParseColor(string s)
        {
            s = s.ToLower();

            var values = Enum.GetValues(typeof(ConsoleColor));
            foreach (ConsoleColor c in values)
            {
                if (c.ToString().ToLower() == s)
                {
                    return c;
                }
            }
            throw new InvalidColorException();
        }

        static void Debug(string[] args)
        {
            foreach (string s in args)
            {
                Console.Write("\'");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(s);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\'");
            }
        }
    }
}
