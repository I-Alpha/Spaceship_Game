using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceShip.Interfaces;
using System;
using System.Collections.Generic;

namespace SpaceShip
{
    class ObstacleGenerator
    {

        static int count = 0;
        Texture2D Texture { get; set; }
        IObstacle _Obstacle { get; set; }
        Vector2 Bounds { get; set; }
        Random randObj { get; set; }
        public static int ObjPerLevel { get; set; }
        public static double levelTimerDelay;
        public static int failedObjects = 0;
        public static int interceptedObjects = 0;
        public List<IObstacle> AllObstacles { get; set; }

        public ObstacleGenerator() { }
        public ObstacleGenerator(Texture2D texture, Vector2 bounds, IObstacle Obs, float movespeed, double LevelTimerDelay = 40,int seed = 10)
        {
            randObj = new Random(seed);
            _Obstacle = Obs.Clone();
            Texture = texture;
            _Obstacle.Texture = Texture;
            _Obstacle.MoveSpeed = movespeed;
            Bounds = bounds;
            levelTimerDelay = LevelTimerDelay;

            AllObstacles = new List<IObstacle>();
        }

        public List<Vector2> GetAllObstaclesPos()
        {

            var temp = new List<Vector2>();
            foreach (var i in AllObstacles)
                temp.Add(i.CurrentPos);
            return temp;
        }

        public void Delete() { }

        List<float> posList = new List<float>();

        private List<IObstacle> GenerateLvl(int num)
        {

            List<IObstacle> temp = new List<IObstacle>();

            for (var i = 0; i < num; i++)
            {

                temp.Add(GenerateObstacle());
            }
            return temp;
        }

        private IObstacle GenerateObstacle()
        {
            Vector2 temp = _Obstacle.CurrentPos;
            temp.X = GetRandomStartPositionX();
            _Obstacle.CurrentPos = temp;
            return _Obstacle.Clone();
        }

        private float GetRandomStartPositionX()
        {
            float newX = 50 + (float)randObj.NextDouble() * 700;
            float newval = ((Bounds.X - 200) / (float)newX) + 50;


            return newX;
        }
        public void Draw(ref SpriteBatch _spriteBatch)
        {
            failedObjects = _Obstacle.FailedObjects;
            foreach (var i in AllObstacles)
                i.Draw(ref _spriteBatch);
        }

 
        private double prevTime;
        private static int levelCount=0;

        private void AddLevel(int num = 5)
        {
            foreach (var item in GenerateLvl(num))
                AllObstacles.Add(item);
            levelCount++;
        }

        private void checkDisposed()
        {
            AllObstacles.RemoveAll(new Predicate<IObstacle>(x => x.IsDisposed == true));
        }
        public void Update(GameTime gameTime)
        {

            var diff = gameTime.TotalGameTime.TotalMilliseconds - prevTime + 1; //+1 default 


            if (diff > levelTimerDelay)
            {
                AddLevel(ObjPerLevel);
                prevTime = gameTime.TotalGameTime.TotalMilliseconds;
            }

            foreach (var i in AllObstacles)
            {
                i.MoveSpeed = _Obstacle.MoveSpeed;
                i.Update();

            } 
            checkDisposed();
        }


    }
}
