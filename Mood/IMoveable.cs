namespace Mood
{
    public interface IMoveable
    {
        void Move(Vector3d direction);
        
        Vector3d GetPosition();

        void SetPosition(Vector3d position);
    }
}
