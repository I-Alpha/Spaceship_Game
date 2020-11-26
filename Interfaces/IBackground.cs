using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShip
{
    interface IBackground
    {
        Texture2D Texture { get; set; }
        double ScrollSpeed { get; set; }
        Vector2 CurrPos { get; set; }
        public void Update();
        public void Draw(ref SpriteBatch spriteBatch);
        abstract public IBackground Clone();

    }
}
