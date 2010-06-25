namespace Mood
{
    public interface IMoveable : IPositionable
    {
        void Move(Vector3d direction);
    }
}
