using System;

namespace MyLibrary
{
	public class Grid_Point 
    {

        // Object Variables:
        //-------------------
        public int id;
        public int id_left = -1, id_right = -1, id_top = -1, id_bottom = -1;
        public Point location;
        public Vector velocity;

        // Constructors:
        //-------------------
		public Grid_Point (int _id, double _x, double _y, double _z)	
        {
            id = _id;
            location = new Point( _x, _y, _z);
		}
        // Functions:
        //-------------------

        public void SetVelocity(double _u, double _v, double _w)
        {
            velocity = new Vector( _u, _v, _w);
        }

        public void SetNeighbourId(int Nid, String Neighbour_Location)
        {
            if (Neighbour_Location == "Left")           id_left = Nid;
            else if (Neighbour_Location == "Right")     id_right = Nid;
            else if (Neighbour_Location == "Top")       id_top = Nid;
            else if (Neighbour_Location == "Bottom")    id_bottom = Nid;
            else throw new ArgumentException("Error: Incorrect Neighbour Location");
        }

        public double CalculateDistanceTo(Grid_Point Neighbour_Point)
        {
            Vector AB = new Vector(location, Neighbour_Point.location);
            return AB.magnitude();
        }

        public Vector CalculateVectorTo(Grid_Point Neighbour_Point)
        {
            Vector AB = new Vector(location, Neighbour_Point.location);
            return AB;
        }

        public override string ToString()
        {
            return string.Format(
               "Id: {0}, Location: {1}, Neighbours: ({2},{3},{4},{5})\n", id, location, id_left, id_right, id_top, id_bottom);
        }
    }
}

