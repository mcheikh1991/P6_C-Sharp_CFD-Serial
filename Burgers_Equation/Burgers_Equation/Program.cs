using System;
using MyLibrary;

namespace Burgers_Equation
{

class MainClass
    {

        public static void Main(string[] args)
        {
            double xlo = -3.0, xhi = 3.0;
            double ylo = -3.0, yhi = 3.0;
            double sigma = 0.0001, nu = 0.0001;
            int nx = 41, ny = 41;
            int total_t=1000, auto_save_t=10;

            Domain Square = new Domain("2D");
            Domain Square2 = new Domain("2D");
            Square = Domain_Functions.CreateQuadDomain(Square, xlo, xhi, ylo, yhi, nx, ny);
            Square2 = Domain_Functions.CreateQuadDomain(Square2, xlo, xhi, ylo, yhi, nx, ny);
            Console.Write(Square);

            Square = Domain_Functions.Define_Interior_Neighbours(Square);
            Square2 = Domain_Functions.Define_Interior_Neighbours(Square2);

            Square = Domain_Functions.Define_Boundary_Neighbours(Square,"Periodic");
            Square2 = Domain_Functions.Define_Boundary_Neighbours(Square2, "Periodic");

            Square = Domain_Functions.Initialize(Square);
            Square2 = Domain_Functions.Initialize(Square2);

            Square.PrintGridToFile("All", "t=0");
            Console.Write("Saved t=0\n");

            for (int t = 2; t <= total_t; t=t+2)
            {
                //Console.Write("Running t={0}\n", t);
                Square2 = BurgersEquation.SolveEq(Square, Square2, nu, sigma);
                Square = BurgersEquation.SolveEq(Square2, Square, nu, sigma);
                if (t % auto_save_t == 0)
                {
                    Square.PrintGridToFile("All", string.Format("t={0}", t));
                    Console.Write("Saved t={0}\n", t);
                }
            }
            /*

            Console.Write(Square);
            Console.Write(Square.PrintToFile("Interior"));
            */
        }
    }
}
