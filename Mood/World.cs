using System.Collections.Generic;
using System;
using System.Drawing;

namespace Mood
{
    class World
    {
        public List<WorldObject> Objects { get; private set; }
        public List<IHitable> HitableObjects { get; private set; }
        public List<IShootable> ShootableObjects { get; private set; }

        public Hit lastLaser;

        public bool ShowAllLasers { get; set; }
        public bool ShowLastLaser { get; set; }

        public World()
        {
            Objects = new List<WorldObject>();
            HitableObjects = new List<IHitable>();
            ShootableObjects = new List<IShootable>();
            ShowAllLasers = false;
            ShowLastLaser = true;
            lastLaser = null;
        }

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

            if (obj is Hit)
                lastLaser = obj as Hit;
        }

        public IHitable HitTest(IHitable mvObj)
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

        public void ShootTest(Hit laser)
        {
            IShootable shot = null;

            double minDistance = 100d;

            foreach (IShootable obj in ShootableObjects)
            {
                double dist = Geometry.PointDistance(laser.A, obj.GetPosition());

                if (obj.ShootTest(laser) && dist < minDistance)
                {
                    minDistance = dist;
                    shot = obj;
                }
            }

            if (shot != null)
            {
                ShootableObjects.Remove(shot);

                if (shot is IHitable)
                {
                    HitableObjects.Remove(shot as IHitable);
                }

                laser.SetRange((float)Geometry.PointDistance(shot.GetPosition(), laser.A));

                shot.Die();
            }
        }

        public bool MoveObject(IMoveable mvObj, Point3d direction)
        {
            Point3d oldPosition = new Point3d(mvObj.GetPosition().X, mvObj.GetPosition().Y, mvObj.GetPosition().Z);

            mvObj.Move(direction);

            if (mvObj is IHitable)
            {
                IHitable hit = HitTest(mvObj as IHitable);

                if (hit != null)
                {
                    if (!(hit is IMoveable) || !MoveObject(hit as IMoveable, direction))
                    {
                        mvObj.SetPosition(oldPosition);

                        return false;
                    }
                }
            }

            return true;
        }

        public void AddSphere()
        {
            Random random = new Random();

            Sphere s = new Sphere(new Point3d((float)random.NextDouble() * 6f - 3f, -0.5f, (float)random.NextDouble() * 6f - 3f), 0.5d, Color.Blue);

            while (HitTest(s) != null)
            {
                s = new Sphere(new Point3d((float)random.NextDouble() * 6f - 3f, -0.5f, (float)random.NextDouble() * 6f - 3f), 0.5d, Color.Blue);
            }

            AddObject(s);
        }

        public void Draw()
        {
            foreach (WorldObject obj in Objects)
            {
                if (obj is Hit && ((lastLaser == obj && !ShowLastLaser) || (lastLaser != obj && !ShowAllLasers)))
                    continue;
                
                obj.Draw();
            }
        }
    }
}
