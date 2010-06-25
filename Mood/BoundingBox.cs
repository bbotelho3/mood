namespace Mood
{
    public class BoundingBox
    {
        public Vector3d A { get; set; }
        public Vector3d B { get; set; }
        public Vector3d C { get; set; }
        public Vector3d D { get; set; }

        public BoundingBox(Vector3d a, Vector3d b, Vector3d c, Vector3d d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }
    }
}
