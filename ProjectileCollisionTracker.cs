using SpaceShip.Abstracts;
using SpaceShip.Interfaces;
using System;
using System.Collections.Generic;

namespace SpaceShip
{
    public class ProjectileCollisionTracker
    {

        public double TotalCollisions { get; set; }
        public double totalChecks { get; set; }

        public static List<Projectile> currProjectiles;
        public static List<IObstacle> currObstacles;
        public ProjectileCollisionTracker()
        {
            currProjectiles = new List<Projectile>();
            currObstacles = new List<IObstacle>();
        }

        public void getObjectCoords(List<Projectile> projectiles, List<IObstacle> obstacles)
        {
            //Check if they match then return colliding IDs.            
            currProjectiles = projectiles;
            currObstacles = obstacles;
        }

        public void CheckUpdateCoords()
        {
            totalChecks++;
            var tempItems = new List<IObstacle>();

            foreach (var i in currObstacles)
            {
                var obYcoord = i.CurrentPos.Y;
                var obXcoord = i.CurrentPos.X;
                var obHeight = i.Texture.Height;
                var obWidth = i.Texture.Width;
                Predicate<Projectile> query = proj => proj.CurrentPos.Y < obYcoord + obHeight && (proj.CurrentPos.X + proj.Texture.Width >= obXcoord && proj.CurrentPos.X + proj.Texture.Width <= obXcoord + obWidth);

                var CollisionExists = currProjectiles.Exists(query);

                if (CollisionExists)
                {
                    currProjectiles.RemoveAll(query);
                    TotalCollisions++;
                }
                else
                {
                    tempItems.Add(i);
                }
            }
            currObstacles = tempItems;

        }
    }
}