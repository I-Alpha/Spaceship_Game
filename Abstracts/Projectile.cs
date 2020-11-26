using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceShip.Abstracts
{
    public class Projectile : Obstacle
    {

        private string Type = "Basic";
        private string Effect = "None";
        private int projectileAge = 0;
        private double rotationAngle = 1;
        private int projectileLife = 1000000;
        public bool active = true;


        public Projectile(Texture2D texture, Vector2 startpos, float MoveSpeed = 10) : base(texture: texture, MoveSpeed)
        {

            currentPos = startpos;
            active = true;
        }

        public Projectile(Vector2 startpos, string effect, int projectileAge, double rotationAngle, int projectileLife)
        {
            active = true;
            currentPos = startpos;
            Effect = effect;
            this.projectileAge = projectileAge;
            this.rotationAngle = rotationAngle;
            this.projectileLife = projectileLife;
        }



        public void Update(GameTime gameTime)
        {
            //projectile active or not?
            if (active == true)
            {
                projectileAge += gameTime.ElapsedGameTime.Milliseconds;
                //up
                currentPos.Y -= (float)(Math.Cos(rotationAngle) * gameTime.ElapsedGameTime.TotalMilliseconds) * MoveSpeed;

            }
            //  if (projectileAge > projectileLife)
            //  {
            //      active = false;
            //  }
        }

        new public void Draw(ref SpriteBatch _spritebatch)
        {
            _spritebatch.Draw(Texture, currentPos, Color.White);
        }
    }
}
