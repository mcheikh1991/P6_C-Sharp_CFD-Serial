using System;
using MyLibrary;

namespace Burgers_Equation
{

public static class Domain_Functions
    {

        // Linear Spacing function
        private static double[] LINSPACE(double xs, double xe, int n)
        {

            double[] array = new double[n];
            double dx = Math.Abs(xs - xe) / Convert.ToDouble(n - 1);
            int j = 0; //will keep a track of the numbers 
            double nextValue = xs;
            for (int i = 0; i < n; i++)
            {
                array.SetValue(nextValue, j);
                j++;
                if (j > n)
                {
                    throw new IndexOutOfRangeException();
                }
                nextValue = nextValue + dx;
            }
            return array;
        }

        // Create a Square/Cube domain
        public static Domain CreateQuadDomain(Domain D, double _xlo, double _xhi, double _ylo, double _yhi, int nx, int ny)
        {
            if (D.Domain_type == "2D")
            {
                D.xlo = _xlo;
                D.xhi = _xhi;
                D.ylo = _ylo;
                D.yhi = _yhi;
                D.zlo = 0;
                D.zhi = 0;
                double[] x = LINSPACE(D.xlo, D.xhi, nx);
                double[] y = LINSPACE(D.ylo, D.yhi, ny);
                D.dx = x[1] - x[0];
                D.dy = y[1] - y[0];
                D.dz = 0;

                string Point_Type;
                int counter = -1;
                for (int ix = 0; ix < nx; ix++)
                {
                    for (int iy = 0; iy < ny; iy++)
                    {
                        counter++;
                        if (ix == 0 && (iy != 0 && iy != ny-1))
                        {
                            Point_Type = "Boundary_Left";
                        }
                        else if (ix == nx - 1 && (iy != 0 && iy != ny - 1))
                        {
                            Point_Type = "Boundary_Right";
                        }
                        else if (iy == 0 && (ix != 0 && ix != nx - 1))
                        {
                            Point_Type = "Boundary_Bottom";
                        }
                        else if (iy == ny - 1 && (ix != 0 && ix != nx - 1))
                        {
                            Point_Type = "Boundary_Top";
                        }
                        else if (ix ==0 || ix == nx -1 || iy == 0 || iy == ny - 1)
                        {
                            Point_Type = "Boundary_Corner";
                        }
                        else
                        {
                            Point_Type = "Interior";
                        }
                        //Console.Write("Id:{0},Type:{1},ix:{2},iy:{3}\n", counter, Point_Type, ix, iy);
                        Grid_Point P = new Grid_Point(counter, x[ix], y[iy], 0);
                        D.AddPoint(P, Point_Type);
                    }
                }
            }
            return D;
        }

        // Define Neighbours
        public static Domain Define_Interior_Neighbours(Domain D)
        {
            if (D.Domain_type == "2D")
            {
                foreach (Grid_Point P in D.All_Points)
                {
                    foreach (Grid_Point PN in D.All_Points)
                    {
                        Vector V = P.CalculateVectorTo(PN);
                        if (Math.Abs(V.y) < 0.1*D.dy)//On the same Hori line
                        {
                            if (Math.Abs(V.x) < 1.1*D.dx)
                            {
                                if (V.x > 0) P.SetNeighbourId(PN.id, "Right");
                                else if (V.x < -0) P.SetNeighbourId(PN.id, "Left");
                            }
                        }
                        else if (Math.Abs(V.x) < 0.1*D.dx)//On the same Hori line
                        {
                            if (Math.Abs(V.y) < 1.1*D.dy)
                            {
                                if (V.y > 0) P.SetNeighbourId(PN.id, "Top");
                                else if (V.y < -0) P.SetNeighbourId(PN.id, "Bottom");
                            }
                        }
                    }
                }
            }
            return D;
        }

        public static Domain Define_Boundary_Neighbours(Domain D, String Boundary_Type)
        {
            if (Boundary_Type == "Periodic")
            {
                foreach (Grid_Point P in D.Boundary_Points_Left)
                {
                    foreach (Grid_Point PN in D.Boundary_Points_Right)
                    {
                        if (Math.Abs(P.location.y - PN.location.y) < 0.1 * D.dy)
                        {
                            P.id_left = PN.id;
                            PN.id_right = P.id;
                            break;
                        }
                    }
                }

                foreach (Grid_Point P in D.Boundary_Points_Top)
                {
                    foreach (Grid_Point PN in D.Boundary_Points_Bottom)
                    {
                        if (Math.Abs(P.location.x - PN.location.x) < 0.1 * D.dx)
                        {
                            P.id_top = PN.id;
                            PN.id_bottom = P.id;
                            break;
                        }
                    }
                }

                // Corner
                foreach (Grid_Point P in D.Boundary_Points_Corner)
                {
                    //Console.Write(P);
                    foreach (Grid_Point PN in D.Boundary_Points_Corner)
                    {
                        if (P.id == PN.id) continue;
                        if (Math.Abs(P.location.y - PN.location.y) < 0.1 * D.dy)
                        {
                            if (P.id_left == -1)
                            {
                                P.id_left = PN.id;
                            }
                            else
                            {
                                P.id_right = PN.id;
                            }
                        }

                        if (Math.Abs(P.location.x - PN.location.x) < 0.1 * D.dx)
                        {
                            if (P.id_top == -1)
                            {
                                P.id_top = PN.id;
                            }
                            else
                            {
                                P.id_bottom = PN.id;
                            }
                        }
                    }
                    //Console.Write(P);
                }
            }
            return D;
        }
        // Initialize the domain
        public static Domain Initialize(Domain D)
        {
            if (D.Domain_type == "2D")
            {
                foreach (Grid_Point P in D.All_Points)
                {
                    double x = P.location.x, y = P.location.y;
                    double u, v;
                    /*
                    if ( Math.Abs(x) < 1 && Math.Abs(y)<1)
                    {
                        P.SetVelocity(u,v,0);
                    }
                    else
                    {
                        P.SetVelocity(0, 0, 0);
                    }
                    //P.SetVelocity(2, 2, 0);
                    */                   
                    u = Math.Sin(x / 0.5) * Math.Cos(y / 0.5) + 1;
                    v = Math.Sin(x / 0.5) * Math.Cos(y / 0.5) + 1;
                    P.SetVelocity(u, v, 0);
                }
            }
            return D;
        }
    }
}
