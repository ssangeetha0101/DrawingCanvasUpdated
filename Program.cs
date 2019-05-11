using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingCanvasUpdated
{
    class Program
    {
        #region Static Variable Declaration
        static string commandExp = "Please enter command => 'C - Canvas', 'L - Line', 'R - Rectangle', 'B - Fill Color' , 'Q - Quit' ";
        #endregion

        #region Main Program
        static void Main(string[] args)
        {
            Console.WriteLine("Drawing Canvas Application");

            Console.WindowHeight = Console.LargestWindowHeight - 20;
            Console.WindowWidth = Console.LargestWindowWidth - 50;
            Console.SetWindowPosition(0, 0);

            Console.WriteLine(commandExp);
            string command = Console.ReadLine();
            GetCommand(command);

        }
        #endregion

        #region Get Command Line from Users
        private static void GetCommand(string command)
        {
            command = command.ToUpper();
            switch (command)
            {
                case "C":
                case "B":
                case "R":
                case "L":
                    CommandCanvas(command);
                    Console.WriteLine("\n" + commandExp);
                    command = Console.ReadLine();
                    GetCommand(command);
                    break;
                case "Q":
                    Console.WriteLine("Quit");
                    break;
                default:
                    Console.WriteLine("Please enter proper argument");
                    command = Console.ReadLine();
                    GetCommand(command);
                    break;
            }
        }
        #endregion

        #region Create Canvas Based on User input
        private static void CommandCanvas(string command)
        {
            Console.WriteLine("Please enter canvas width/height for ex: 20=> Press Enter=> 20 ");
            string canvasw = Console.ReadLine();
            int cw = 0;
            int[] values;
            bool success = (int.TryParse(canvasw, out cw));
            if (!success)
            {
                Console.WriteLine("Please enter proper width");
            }
            else
            {
                string canvash = Console.ReadLine();
                int ch = 0;
                success = (int.TryParse(canvash, out ch));
                if (!success)
                {
                    Console.WriteLine("Please enter proper height");
                }
                else
                {
                    int bcolor = 0;
                    if (command.ToUpper() == "B")
                    {
                        Console.WriteLine("Please enter background color number range between 0 to 15.");
                        success = (int.TryParse(Console.ReadLine(), out bcolor));
                        if (!success)
                        {
                            Console.WriteLine("Please enter proper value.");
                        }
                    }
                    if (command.ToUpper() == "R")
                    {
                        Console.WriteLine("Please enter width, height, x, y location for rectangle");
                        int rw = 0;
                        int rh = 0;
                        int rt = 0;
                        int rl = 0;
                        success = (int.TryParse(Console.ReadLine(), out rw));
                        if (success)
                        {
                            success = (int.TryParse(Console.ReadLine(), out rh));
                            if (success)
                            {
                                success = (int.TryParse(Console.ReadLine(), out rt));
                                if (success)
                                {
                                    success = (int.TryParse(Console.ReadLine(), out rl));
                                }
                            }
                        }

                        if (!success || cw < rw || ch < rh)
                        {
                            Console.WriteLine("Please enter proper value or rect height/width is highter than canvas height/width");
                            Console.ReadKey();
                        }
                        else
                        {
                            values = new int[] { cw, ch, bcolor };
                            
                            int top = Console.CursorTop;
                            int left = Console.CursorLeft;
                          
                            DrawCanvas dc = new DrawCanvas();
                            dc.Draw(values);
                            int originalTop = Console.CursorTop;
                            
                            Console.SetCursorPosition(left, top);
                            values=new int[] { rw, rh, rt, rl };
                            DrawRectange dr = new DrawRectange();
                            dr.Draw(values);
                            Console.SetCursorPosition(left, originalTop);

                        }
                    }
                    else if (command.ToUpper() == "L")
                    {
                        Console.WriteLine("Please enter x1,x2,y1,y2 for Line");
                        int x1 = 0;
                        int y1 = 0;
                        int x2 = 0;
                        int y2 = 0;
                        success = (int.TryParse(Console.ReadLine(), out x1));
                        if (success)
                        {
                            success = (int.TryParse(Console.ReadLine(), out y1));
                            if (success)
                            {
                                success = (int.TryParse(Console.ReadLine(), out x2));
                                if (success)
                                {
                                    success = (int.TryParse(Console.ReadLine(), out y2));
                                }
                            }
                        }
                        if (!success)
                        {
                            Console.WriteLine("Please enter proper value.");
                            Console.ReadKey();
                        }
                        else
                        {
                            if (x1 != x2 && y1 != y2)
                            {
                                Console.WriteLine("Please enter x1 and x2 same or y1 and y2 same.");
                                Console.ReadKey();
                            }
                            else
                            {
                                int linetype = (x1 == x2) ? 1 : 0;
                                values = new int[] { cw, ch, bcolor };
                                int top = Console.CursorTop;
                                int left = Console.CursorLeft;

                                DrawCanvas dc = new DrawCanvas();
                                dc.Draw(values);
                                int originalTop = Console.CursorTop;
                                int originalLeft = Console.CursorLeft;

                                left = left + x1;
                                top = top + y1;

                                Console.SetCursorPosition(left, top);
                                values = new int[] { x1, y1, x2, y2, linetype };
                                DrawLine dl = new DrawLine();
                                dl.Draw(values);
                                Console.SetCursorPosition(originalLeft, originalTop);

                              
                            }
                        }
                    }
                    else
                    {
                        values = new int[] { cw, ch, bcolor };
                        DrawCanvas dc = new DrawCanvas();
                        dc.Draw(values);
                    }
                }
            }
        }
        #endregion

    }
        
    #region Interface Shape
    interface IShape
    {
        void Draw(params int[] values);
    }

    #endregion

    #region Class Drawing Canvas 
    public class DrawCanvas : IShape
    {
        public void Draw(params int[] values)
        {
            /*
         * Draw Canvas using width and height with selected fill color using x character
         */


            int CanvasWidth = values[0];
            int CanvasHeight = values[1];
            int BackgroundColor = values[2];

            string strCanvas = "x";
            string space = "";
            string temp = "";
            int top = Console.CursorTop;
            int left = Console.CursorLeft;


            for (int i = 0; i < CanvasWidth; i++)
            {
                space += " ";
                strCanvas += "x";
            }

            for (int j = 0; j < left; j++)
                temp += " ";

            strCanvas += "x" + "\n";

            for (int i = 0; i < CanvasHeight; i++)
                strCanvas += temp + "x" + space + "x" + "\n";

            strCanvas += temp + "x";
            for (int i = 0; i < CanvasWidth; i++)
                strCanvas += "x";

            strCanvas += "x" + "\n";


            Type type = typeof(ConsoleColor);
            ConsoleColor c = (ConsoleColor)BackgroundColor;
            Console.BackgroundColor = c;

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(strCanvas);
            Console.ResetColor();

            int originalTop = Console.CursorTop;

        }
    }
    #endregion

    #region Class Drawing Rectangle 
    public class DrawRectange : IShape
    {
        public void Draw(params int[] values)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            int RectWidth = values[0];
            int RectHeight = values[1];
            int RectTop = values[2];
            int RectLeft = values[3];

            int top = Console.CursorTop;
            int left = Console.CursorLeft;

            RectTop = top + RectTop;
            RectLeft = left + RectLeft;
            Console.SetCursorPosition(RectLeft, RectTop);

            string strCanvas = "x";
            string space = "";

            top = RectTop;
            left = RectLeft;
            for (int i = 0; i <= RectWidth; i++)
            {
                space += " ";
                strCanvas += "x";
            }
            strCanvas += "x" + "\n";
            Console.Write(strCanvas);
            Console.CursorLeft = RectLeft;
            strCanvas = "";
            for (int i = 0; i < RectHeight; i++)
            {
                strCanvas = "x" + space + "x" + "\n";
                Console.Write(strCanvas);
                Console.CursorLeft = RectLeft;
            }

            Console.CursorLeft = RectLeft;
            strCanvas = "x";
            space = "";

            for (int i = 0; i <= RectWidth; i++)
            {
                space += " ";
                strCanvas += "x";
            }
            strCanvas += "x" + "\n";

            Console.Write(strCanvas);
            Console.ResetColor();
        }
    }
    #endregion

    #region Class Drawing Line
    public class DrawLine : IShape
    {
        public void Draw(params int[] values)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string strCanvas = "";
            string space = "";
            int LX1 = values[0];
            int LY1 = values[1];
            int LX2 = values[2];
            int LY2 = values[3];
            int LineType = values[4];

            int top = LY1;
            int left = LX1;

            if (LineType == 0)
            {
                for (int i = LX1; i <= LX2; i++)
                {
                    space += " ";
                    strCanvas += "x";
                }
            }
            else
            {
                for (int i = LY1; i <= LY2; i++)
                {
                    Console.CursorLeft = left;
                    space += " ";
                    strCanvas = "x" + "\n";
                    Console.Write(strCanvas);
                    
                }
            }
            strCanvas += "\n";

            Console.Write(strCanvas);
            Console.ResetColor();
        }
    }
    #endregion
}
