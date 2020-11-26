using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShip
{

    public class GameService : Game
    {

        private SpriteFont mainFont;
        private SpriteFont largerMfont;
        private IBackground background;
        private IBackground background2;
        private ObstacleGenerator obstacleGenerator;
        private ProjectileGenerator projectileGenerator;
        private CollisionTracker projectileCollisionTracker;
        private ISprite shipSprite;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 gameWindowBounds;
        private Vector2 startPos = new Vector2(0, 0);
        private const int levelTimerDelay = 2500;
        private int wHeight;
        private int wWidth;
        private const double fireDelay = 50;
        private const int projectilLimit = 0;
        private int score = 0;
        private int fails = 0;
        private const float bgScroll_speed = 0.51f;
        private const int shipSprite_movement_speed = 10;
        private const float obstacles_movement_speed = 0.1f;
        private const float ProjectileMoveSpeed = 0.1f;
        private const int objPerLevel = 3;
        public GameService()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            wWidth = GraphicsDevice.Viewport.Width;
            wHeight = GraphicsDevice.Viewport.Height;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            gameWindowBounds = new Vector2(wWidth, wHeight);
            shipSprite = new BaseShip(shipSprite_movement_speed, 0, gameWindowBounds, startPos, startPos);
            background = new Background(bgScroll_speed, startPos, gameWindowBounds);

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            mainFont = Content.Load<SpriteFont>("mainAIfont");
            largerMfont = Content.Load<SpriteFont>("largermainAIfont");
            // TODO: use this.Content to load your game content here

            shipSprite.Texture = Content.Load<Texture2D>("triangleShip");
            background.Texture = Content.Load<Texture2D>("Bg");
            var bricks = Content.Load<Texture2D>("brick");
            background2 = background.Clone();
            background2.CurrPos = new Vector2(0, -background.Texture.Height);
            Obstacle Obs = new Obstacle(bricks, gameWindowBounds, obstacles_movement_speed);
            obstacleGenerator = new ObstacleGenerator(bricks, gameWindowBounds, Obs, obstacles_movement_speed, levelTimerDelay);
            projectileGenerator = new ProjectileGenerator(Content.Load<Texture2D>("bullet1"));
            ProjectileGenerator.MoveSpeed = ProjectileMoveSpeed;
            ProjectileGenerator.fireDelay = fireDelay;
            ProjectileGenerator.fireLimit = projectilLimit;
            projectileCollisionTracker = new CollisionTracker();
            ObstacleGenerator.ObjPerLevel = objPerLevel;

        }

        bool gamePaused = false;
        KeyboardState currentKB, previousKB;

        [System.Obsolete]
        protected override void Update(GameTime gameTime)
        {
            previousKB = currentKB;
            KeyboardState State = Keyboard.GetState();
            currentKB = State;



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || State.IsKeyDown(Keys.Escape))
                Exit();

            if (currentKB.IsKeyUp(Keys.P) && previousKB.IsKeyDown(Keys.P)) gamePaused = !gamePaused;
            if (gamePaused) return;
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || State.IsKeyDown(Keys.Space))
                projectileGenerator.FireProjectile(shipSprite.CurrentPos, gameTime);


            shipSprite.Update(State);
            background.Update();
            background2.Update();
            obstacleGenerator.Update(gameTime);
            projectileGenerator.Update(gameTime);

            projectileCollisionTracker.getObjectCoords(projectileGenerator.ActiveProjectiles, obstacleGenerator.AllObstacles);
            projectileCollisionTracker.CheckUpdateCoords();

            fails = Obstacle.failedObjects;
            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            //Update coords using collision tracker class
            obstacleGenerator.AllObstacles = CollisionTracker.currObstacles;
            projectileGenerator.ActiveProjectiles = CollisionTracker.currProjectiles;




            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            background.Draw(ref _spriteBatch);
            background2.Draw(ref _spriteBatch);
            obstacleGenerator.Draw(ref _spriteBatch);
            _spriteBatch.DrawString(mainFont, "Welcome to AI-test Enviroment: Space-Ship.", new Vector2(wWidth / (float)4, 25), Color.Black);
            _spriteBatch.DrawString(largerMfont, "Score : " + projectileCollisionTracker.TotalCollisions, startPos, Color.Black);
            _spriteBatch.DrawString(largerMfont, "Checks : " + projectileCollisionTracker.totalChecks, new Vector2(0, 30), Color.Black);
            _spriteBatch.DrawString(largerMfont, "Fails : " + fails, new Vector2(0, 60), Color.Black);
            _spriteBatch.DrawString(mainFont, "Time : " + gameTime.TotalGameTime.TotalSeconds, new Vector2(600, 10), Color.Black);
            shipSprite.Draw(ref _spriteBatch);
            projectileGenerator.Draw(ref _spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}