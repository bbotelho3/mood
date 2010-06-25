namespace Mood
{
    public interface IHitable
    {
        BoundingBox BoundingBox { get; set; }

        bool HitTest(IHitable obj);
    }
}
