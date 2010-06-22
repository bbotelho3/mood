namespace Mood
{
    interface IShootable
    {
        bool IsDead { get; set; }

        double LastShootDistance { get; set; }

        bool ShootTest(Laser laser);

        void Die();

        Vector3d LastPosition();
    }
}
