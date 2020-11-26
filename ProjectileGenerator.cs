using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Abstracts;
using System;
using System.Collections.Generic;

namespace SpaceShip
{
    class ProjectileGenerator
    {
        private static List<Projectile> activeProjectiles = new List<Projectile>();
        Texture2D Texture;
        public static float MoveSpeed = 10;
        public static double prevTime = 0;
        public static int fireLimit = 8;
        public static double fireDelay = 500;

        public List<Projectile> ActiveProjectiles { get => activeProjectiles; set => activeProjectiles = value; }

        public ProjectileGenerator(Texture2D texture)
        {
            Texture = texture;
        }
        public void FireProjectile(Vector2 shipPostion, GameTime gameTime)
        {



            var diff = gameTime.TotalGameTime.TotalMilliseconds - prevTime + 1; //+1 default 

            bool flDefault = (fireLimit == 0) ? true : false;

            if (diff > fireDelay)
            {

                if (flDefault)
                { //check if firelimit is set to default 0, in that case do not place limit. 

                    ActiveProjectiles.Add(new Projectile(Texture, new Vector2(shipPostion.X, shipPostion.Y - 20), MoveSpeed));
                }
                else
                {
                    if (ActiveProjectiles.Count < fireLimit)
                        ActiveProjectiles.Add(new Projectile(Texture, new Vector2(shipPostion.X, shipPostion.Y - 20), MoveSpeed));
                }
            }

            prevTime = gameTime.TotalGameTime.TotalMilliseconds;
        }
        public void Draw(ref SpriteBatch spriteBatch)
        {
            foreach (var i in ActiveProjectiles)
            {
                i.Draw(ref spriteBatch);
            }
        }

        private void CheckInactiveProjectiles()
        {
            ActiveProjectiles.RemoveAll(new Predicate<Projectile>(x => x.active == false));
        }
        public void Update(GameTime gameTime)
        {
            foreach (var i in ActiveProjectiles)
            {
                i.Update(gameTime);
            }
            CheckInactiveProjectiles();
        }



        /*   public List<Vector2> GetAllObstaclesPos()
           {
               var temp = new List<Vector2>();
               foreach (var i in ActiveProjectiles)
                   temp.Add(i.CurrentPos);
               return temp;
           }*/
    }
}
