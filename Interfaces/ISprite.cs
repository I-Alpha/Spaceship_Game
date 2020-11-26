using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShip
{
    public interface ISprite
    {

        abstract public void Update(KeyboardState State);
        static int ID = 0;
        static int nextID = 0;
        Texture2D Texture { get; set; }
        float MoveSpeed { get; set; }
        float Damage { get; set; }
        Vector2 Bounds { get; set; }
        public Vector2 CurrentPos { get; set; }
        public void Draw(ref SpriteBatch _);
        public ISprite Clone();
    }

}
