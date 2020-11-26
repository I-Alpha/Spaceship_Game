using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceShip.Interfaces;
using System.Collections.Generic;

namespace SpaceShip
{
    public class Obstacle : IObstacle
    {
        public int ID { get; private set; }
        private static List<bool> UsedCounter = new List<bool>();
        private static object Lock = new object();
        public Vector2 currentPos;
        public Vector2 Bounds { get; set; }
        public static int failedObjects = 0;
        private bool isDisposed = false;


        public void Update(KeyboardState State) { }
        public Obstacle()
        {
            incrementID();
        }
        public Obstacle(Vector2 bounds)
        {
            incrementID();
            if (bounds == null)
            {
                Bounds = new Vector2(0, 0);
            }
        }

        public void Dispose()
        {
            lock (Lock)
            {
                UsedCounter[ID] = false;
                isDisposed = true;
            }
        }
        public Texture2D Texture { get; set; }

        public float MoveSpeed { get; set; }
        public float Damage { get; set; }
        public int FailedObjects { get => failedObjects; set => failedObjects = value; }
        public bool IsDisposed { get => isDisposed; set => isDisposed = value; }

        public Vector2 CurrentPos { get => currentPos; set => currentPos = value; }

        public Obstacle(Obstacle obstacle)
        {
            Texture = obstacle.Texture;
            currentPos = obstacle.currentPos;
            MoveSpeed = obstacle.MoveSpeed;
            Damage = obstacle.Damage;
            Bounds = obstacle.Bounds;
            incrementID();
        }

        public Obstacle(Texture2D texture)
        {
            Texture = texture;
            incrementID();
        }

        public Obstacle(Texture2D texture, Vector2 gameWindowBounds, float obstacles_movement_speed, Vector2 CurrentPos = default) : this(texture)
        {
            Bounds = gameWindowBounds;
            Texture = texture;
            MoveSpeed = obstacles_movement_speed;
            currentPos = CurrentPos;
        }

        public Obstacle(Texture2D texture, float moveSpeed) : this(texture)
        {
            MoveSpeed = moveSpeed;
        }

        public void Update()
        {
            if (CheckOutOfBounds())
                Dispose();
            else

                currentPos.Y += MoveSpeed;
        }

        public bool CheckOutOfBounds()
        {
            if (currentPos.Y > Bounds.Y - 50)
            {
                currentPos.Y = Bounds.Y + 1;
                failedObjects++;
                return true;
            }
            return false;
        }

        public void incrementID()
        {
            lock (Lock)
            {
                int nextIndex = GetAvailableIndex();
                if (nextIndex == -1)
                {
                    nextIndex = UsedCounter.Count;
                    UsedCounter.Add(true);
                }

                ID = nextIndex;
            }
        }
        public void Draw(ref SpriteBatch _spriteBatch)
        {
            if (CheckOutOfBounds())
            {
                Dispose();
            }

            _spriteBatch.Draw(Texture, currentPos, Color.White);

        }

        private int GetAvailableIndex()
        {
            for (int i = 0; i < UsedCounter.Count; i++)
            {
                if (UsedCounter[i] == false)
                {
                    return i;
                }
            }

            // Nothing available.
            return -1;
        }


        public IObstacle Clone()
        {
            return new Obstacle(this);
        }

        ISprite ISprite.Clone()
        {
            return new Obstacle(this);
        }
    }

}
