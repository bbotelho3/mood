namespace Mood
{
    interface IShootable : IPositionable
    {
        bool IsDead { get; set; }

        bool ShootTest(Laser laser);

        void Die();
    }
}
