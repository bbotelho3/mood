namespace Mood
{
    interface IShootable : IPositionable
    {
        bool IsDead { get; set; }

        bool ShootTest(Hit laser);

        void Die();
    }
}
