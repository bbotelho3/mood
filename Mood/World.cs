using System.Collections.Generic;

namespace Mood
{
    class World
    {
        public List<WorldObject> Objects { get; private set; }

        public List<IHitable> HitableObjects { get; private set; }

        public List<IShootable> ShootableObjects { get; private set; }

        public bool ShowLaser { get; set; }

        public Laser lastLaser;

        public void AddObject(WorldObject obj)
        {
            Objects.Add(obj);

            if (obj is IHitable)
            {
                HitableObjects.Add(obj as IHitable);
            }

            if (obj is IShootable)
            {
                ShootableObjects.Add(obj as IShootable);
            }

            if (obj is Laser)
                lastLaser = obj as Laser;
        }

        public World()
        {
            Objects = new List<WorldObject>();
            HitableObjects = new List<IHitable>();
            ShootableObjects = new List<IShootable>();
            ShowLaser = true;
            lastLaser = null;
        }

        public IHitable HitTest(IMoveable mvObj)
        {
            foreach (IHitable obj in HitableObjects)
            {
                if (obj != mvObj && obj.HitTest(mvObj))
                {
                    return obj;
                }
            }

            return null;
        }

        public void ShootTest(Laser line)
        {
            List<IShootable> Shot = new List<IShootable>();

            foreach (IShootable obj in ShootableObjects)
            {
                if (obj.ShootTest(line))
                {
                    Shot.Add(obj);

                    obj.Die();
                }
            }

            foreach (IShootable dead in Shot)
            {
                ShootableObjects.Remove(dead);
            }
        }

        public void Draw()
        {
            foreach (WorldObject obj in Objects)
            {
                if (obj is Laser && !ShowLaser && lastLaser != obj)
                    continue;
                
                obj.Draw();
            }
        }
    }
}
