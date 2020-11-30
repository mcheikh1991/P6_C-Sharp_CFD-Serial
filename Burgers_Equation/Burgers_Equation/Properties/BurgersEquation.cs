using System;
using MyLibrary;

namespace Burgers_Equation
{
    public static class BurgersEquation
    {

        // Initialize the domain
        public static Domain SolveEq(Domain D, Domain D2, double nu, double sigma)
        {
            Grid_Point PN, Pl, Pr, Pt, Pb; // left,right,top,bottom
            double u, ul, ur, ut, ub;
            double v, vl, vr, vt, vb;
            double x, xl, xr, xt, xb;
            double y, yl, yr, yt, yb;
            double conv_u, conv_v;
            double diff_u, diff_v;
            double dt, dx, dy;
            int id;

            if (D.Domain_type == "2D")
            {
                dx = D.dx;
                dy = D.dy;
                dt = sigma * dx * dy / nu;
                foreach (Grid_Point P in D.All_Points)
                {
                    id = P.id;

                    // Points
                    Pl = D.All_Points[P.id_left];
                    Pr = D.All_Points[P.id_right];
                    Pt = D.All_Points[P.id_top];
                    Pb = D.All_Points[P.id_bottom];

                    // Velocity 
                    u  = P.velocity.x;  v  = P.velocity.y;
                    ul = Pl.velocity.x; vl = Pl.velocity.y;
                    ur = Pr.velocity.x; vr = Pr.velocity.y;
                    ut = Pt.velocity.x; vt = Pt.velocity.y;
                    ub = Pb.velocity.x; vb = Pb.velocity.y;
                    //Console.Write("{0},{1},{2},{3},{4},{5}\n",id, u, ul, ur, ut, ub);

                    // Location
                    x  = P.location.x;  y  = P.location.y;
                    xl = Pl.location.x; yl = Pl.location.y;
                    xr = Pr.location.x; yr = Pr.location.y;
                    xt = Pt.location.x; yt = Pt.location.y;
                    xb = Pb.location.x; yb = Pb.location.y;

                    // Convective (Upwind)
                    conv_u = u*(u-ul)/dx + v*(u-ub)/dy;
                    conv_v = u*(v-vl)/dx + v*(v-vb)/dy;
                    //Console.Write("conv_u:{0}\n", conv_u);

                    // Diffusive (Central)
                    diff_u = nu*( (ur-2*u+ul)/(dx*dx) + (ut-2*u+ub)/(dy*dy) );
                    diff_v = nu*( (vr-2*v+vl)/(dx*dx) + (vt-2*v+vb)/(dy*dy) );
                    //Console.Write("diff_u:{0}\n", diff_u);

                    // Solve the full equation
                    PN = D2.All_Points[id];

                    PN.velocity.x = dt * (diff_u - conv_u) + u;
                    PN.velocity.y = dt * (diff_v - conv_v) + v;
                }
            }
            //Console.Write("-----------------\n");
            return D2;
        }
    }
}
