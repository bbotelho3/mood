namespace Mood
{
    public interface IMoveable : IPositionable
    {
        void Move(Point3d direction);
    }
}
