using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceShip
{
    class Background : IBackground
    {

        public Texture2D Texture { get; set; }
        public double ScrollSpeed { get; set; }
        public Vector2 Bounds { get => bounds; set => bounds = value; }
        private Vector2 currPos;
        private Vector2 bounds;

        public Background(Background bgscript)
        {


            Texture = bgscript.Texture;
            ScrollSpeed = bgscript.ScrollSpeed;
            Bounds = bgscript.Bounds;
            currPos = bgscript.currPos;

        }
        public Background(double scrollSpeed, Vector2 currPos, Vector2 _bounds)
        {

            ScrollSpeed = scrollSpeed;
            CurrPos = this.currPos;
            Bounds = _bounds;

        }
        public Background(Texture2D texture, double scrollSpeed, Vector2 currPost, Vector2 bounds)
        {
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            ScrollSpeed = scrollSpeed;
            CurrPos = currPost;
            Bounds = bounds;
        }

        public Vector2 CurrPos { get => currPos; set => currPos = value; }
        public void Draw(ref SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Texture, currPos, Color.White);
        }

        public IBackground Clone() { return new Background(this); }

        public void Update()
        {
            {

                if (currPos.Y >= Bounds.Y)
                {
                    currPos.Y = (-Texture.Height) - bounds.Y * 2;
                }
                else
                    currPos.Y += (float)ScrollSpeed;

            }
        }
    }
}
