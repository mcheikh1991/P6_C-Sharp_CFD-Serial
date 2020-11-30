using System;
using System.Collections.Generic;
using System.IO;

namespace MyLibrary
{
	public class Domain 
    {

        // Object Variables:
        //-------------------
        public string Domain_type; // '2D' or '3D'
        public double xlo, xhi, ylo, yhi, zlo, zhi;
        int nx, ny;
        public double dx, dy, dz;
        // Lists
        public List<Grid_Point> All_Points = new List<Grid_Point>();
        public List<Grid_Point> Interior_Points = new List<Grid_Point>();
        public List<Grid_Point> Boundary_Points_Left = new List<Grid_Point>();
        public List<Grid_Point> Boundary_Points_Right = new List<Grid_Point>();
        public List<Grid_Point> Boundary_Points_Top = new List<Grid_Point>();
        public List<Grid_Point> Boundary_Points_Bottom = new List<Grid_Point>();
        public List<Grid_Point> Boundary_Points_Corner = new List<Grid_Point>();

        // Constructors:
        //-------------------
		public Domain (string _Domain_type)
        {
            Domain_type = _Domain_type;
        }

        // Methods:
        //-------------------
        public void AddPoint(Grid_Point P, String Point_Type)
        {
            All_Points.Add(P);
            if (Point_Type == "Interior")
            {
                Interior_Points.Add(P);
            }
            else if (Point_Type == "Boundary_Left")
            {
                Boundary_Points_Left.Add(P);
            }
            else if (Point_Type == "Boundary_Right")
            {
                Boundary_Points_Right.Add(P);
            }
            else if (Point_Type == "Boundary_Top")
            {
                Boundary_Points_Top.Add(P);
            }
            else if (Point_Type == "Boundary_Bottom")
            {
                Boundary_Points_Bottom.Add(P);
            }
            else if (Point_Type == "Boundary_Corner")
            {
                Boundary_Points_Corner.Add(P);
            }
            else
            {
                throw new ArgumentException("Error: Incorrect Point Type");
            }
        }

        // Method that enable what is written on Console when printing
        public override string ToString()
        {
            return string.Format("Domain Information:\nX=[{0},{1}]\nY=[{2},{3}]\nZ=[{4},{5}]\nNumber of All Points={6}\nNumber of Interior Points={7}\nNumber of Boundary Points Left={8}\nNumber of Boundary Points Right={9}\nNumber of Boundary Points Up={10}\nNumber of Boundary Points Down={11}\n",xlo, xhi, ylo, yhi, zlo, zhi,All_Points.Count,Interior_Points.Count,Boundary_Points_Left.Count,Boundary_Points_Right.Count,Boundary_Points_Top.Count,Boundary_Points_Bottom.Count);
        }

        public void PrintGridToFile(String Point_Type, String File_name) // Real location
        {
            string output = "";
            if (Point_Type == "Interior")
            {
                output += "Data Set: " +  Point_Type + "\n";
                output += "Number of Points: " +  Interior_Points.Count + "\n";
                output += "Id,x,y,z,u,v,w,Id_l,Id_r,Id_t,Id_b\n";
                foreach (Grid_Point P in Interior_Points)
                {
                    output += P.id + ",";
                    output += P.location.x + ",";
                    output += P.location.y + ",";
                    output += P.location.z + ",";
                    output += P.velocity.x + ",";
                    output += P.velocity.y + ",";
                    output += P.velocity.z + ",";
                    output += P.id_left + ",";
                    output += P.id_right + ",";
                    output += P.id_top + ",";
                    output += P.id_bottom + "\n";
                }
            }
            else if (Point_Type == "All")
            {
                output += "Data Set: " +  Point_Type + "\n";
                output += "Number of Points: " +  All_Points.Count + "\n";
                output += "Id,x,y,z,u,v,w,Id_l,Id_r,Id_t,Id_b\n";
                foreach (Grid_Point P in All_Points)
                {
                    output += P.id + ",";
                    output += P.location.x + ",";
                    output += P.location.y + ",";
                    output += P.location.z + ",";
                    output += P.velocity.x + ",";
                    output += P.velocity.y + ",";
                    output += P.velocity.z + ",";
                    output += P.id_left + ",";
                    output += P.id_right + ",";
                    output += P.id_top + ",";
                    output += P.id_bottom + "\n";
                }
            }
            System.IO.File.WriteAllText(File_name+".csv", output);

        }



    }
}

