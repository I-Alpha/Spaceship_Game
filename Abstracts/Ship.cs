using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShip
{
    abstract class Ship : ISprite
    {
        public Texture2D Texture { get; set; }
        public float MoveSpeed { get; set; }
        public float Damage { get; set; }

        private Vector2 bounds;
        private Vector2 currentPos;
        private Vector2 startPosition;

        public Vector2 CurrentPos { get => currentPos; set => currentPos = value; }
        public Vector2 StartPosition { get => startPosition; set => startPosition = value; }
        public Vector2 Bounds { get => bounds; set => bounds = value; }

        public Ship()
        {
            CurrentPos = new Vector2(0, 0);
            Bounds = new Vector2(0, 0);
            MoveSpeed = 10;
            Damage = 10;
        }

        public Ship(int moveSpeed, float damage, Vector2 bounds, Vector2 currentPos, Vector2 startPosition)
        {
            MoveSpeed = moveSpeed;
            Damage = damage;
            CurrentPos = currentPos;
            StartPosition = startPosition;
            Bounds = bounds;
        }

        public Ship(Ship ship)
        {
            MoveSpeed = ship.MoveSpeed;
            Damage = ship.Damage;
            CurrentPos = ship.currentPos;
            StartPosition = ship.startPosition;
            Bounds = ship.bounds;
        }

        public void Draw(ref SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Texture, CurrentPos, Color.White);
        }

        public void Update(KeyboardState state)
        {


            if (state.IsKeyDown(Keys.Right))
                currentPos.X += MoveSpeed;
            if (state.IsKeyDown(Keys.Left))
                currentPos.X -= MoveSpeed;
            if (state.IsKeyDown(Keys.Up))
                currentPos.Y -= MoveSpeed;
            if (state.IsKeyDown(Keys.Down))
                currentPos.Y += MoveSpeed;

            // TODO: Add your update logic here 
            // Check Boundaries

            if (currentPos.Y < 5)
                currentPos.Y = 5;
            if (currentPos.X < 5)
                currentPos.X = 5;
            if (currentPos.Y > Bounds.Y - Texture.Height)
                currentPos.Y = Bounds.Y - Texture.Height;
            if (currentPos.X > Bounds.X - Texture.Width)
                currentPos.X = Bounds.X - Texture.Width;
        }

        abstract public ISprite Clone();

    }
}
