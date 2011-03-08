namespace Mood
{
    public class BoundingBox
    {
        public Point3d A { get; set; }
        public Point3d B { get; set; }
        public Point3d C { get; set; }
        public Point3d D { get; set; }

        public BoundingBox(Point3d a, Point3d b, Point3d c, Point3d d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }
    }
}
