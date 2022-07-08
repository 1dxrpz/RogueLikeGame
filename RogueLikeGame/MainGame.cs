using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Collisions;
using SuperSimpleTcp;

namespace RogueLikeGame
{
	internal class MainGame : Game
	{
		SimpleTcpClient client = new SimpleTcpClient("127.0.0.1:8000");

		private readonly GraphicsDeviceManager _graphics;
		private readonly FrameCounter _frameCounter = new();
		SpriteBatch _spriteBatch;

		static public GameObject player;

		bool _start = false;
		bool _toggle = false;
		public SpriteFont font;

		static public Texture2D RedTexture;
		static public Texture2D BlueTexture;
		static public Texture2D YellowTexture;

		public MainGame()
		{
			_graphics = new GraphicsDeviceManager(this)
			{
				SynchronizeWithVerticalRetrace = false
			};
			Content.RootDirectory = "Content";
			IsFixedTimeStep = false;
			IsMouseVisible = true;
			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += WindowResize;
			Utils.DefaultScene = new Scene();
			Utils._collisionComponent = new CollisionComponent(new RectangleF(-20000, -20000, 40000, 40000));

			//client.Connect();
		}
		private void WindowResize(object sender, EventArgs e)
		{
			Utils.MainCamera.Bounds = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
		}
		protected override void Initialize()
		{
			base.Initialize();
		}

		Texture2D animation;
		Player _pl;
		protected override void LoadContent()
		{
			animation = Content.Load<Texture2D>("animation");
			font = Content.Load<SpriteFont>("font");
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			Utils.GraphicsDevice = GraphicsDevice;
			Utils.MainCamera = new Camera
			{
				Bounds = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight)
			};
			Utils.SpriteBatch = _spriteBatch;

			player = new GameObject();
			player.AddComponent<SpriteRenderer>().Texture = Utils.GetTexture(Color.White);
			player.AddComponent<CircleCollider>().Radius = 16;
			player.transform.Origin = new Vector2(16, 16);
			_pl = player.AddComponent<Player>();

			RedTexture = Utils.GetTexture(Color.Red);
			BlueTexture = Utils.GetTexture(Color.Blue);
			YellowTexture = Utils.GetTexture(Color.Yellow);

			//SpawnEnemy();

			base.LoadContent();
		}
		private void SpawnEnemy()
		{
			Random random = new();
			var en = CreateEnemy();
			en.transform.Position = new Vector2(random.Next(-300, 300), random.Next(-300, 300));
			Time.Timer(100, () =>
			{
				SpawnEnemy();
			});
		}
		private GameObject CreateEnemy()
		{
			GameObject enemy = Utils.GameObjectPool.Obtain();
			var _an = enemy.AddComponent<AnimationRenderer>();
			var _col = enemy.AddComponent<CircleCollider>();
			_col.Radius = 16;
			var _fn = enemy.AddComponent<FloatingNumbers>();
			_fn.Lifetime = 500;
			_fn.Font = font;
			_an.MaxFrames = 4;
			_an.Texture = animation;

			enemy.transform.Origin = new Vector2(16, 16);
			var _en = enemy.AddComponent<Enemy>();
			return enemy;
		}

		protected override void Update(GameTime gameTime)
		{
			Utils._collisionComponent.Update(gameTime);
			Utils.DefaultScene.Update();

			if (Keyboard.GetState().IsKeyDown(Keys.F))
				Utils.MainCamera.Shake(20, .5f, 1000);
			if (Keyboard.GetState().IsKeyDown(Keys.V) && !_toggle)
			{
				_toggle = true;
				IsFixedTimeStep = !IsFixedTimeStep;
			}
			if (Keyboard.GetState().IsKeyUp(Keys.V))
				_toggle = false;

			Utils.MainCamera.Update();
			if (Mouse.GetState().RightButton == ButtonState.Pressed)
			{
				var en = CreateEnemy();
				en.transform.Position = new Vector2(Utils.Random.Next(-300, 300), Utils.Random.Next(-300, 300));
			}
			base.Update(gameTime);
		}
		protected override void Draw(GameTime gameTime)
		{
			Time.Update(gameTime);
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);

			Utils.DefaultScene.Draw();
			_frameCounter.Update(Time.DeltaTime);
			var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);
			_spriteBatch.DrawString(font, fps, new Vector2(1, 1), Color.Black);
			_spriteBatch.DrawString(font, "Ammo: " + _pl.Ammo + " / " + _pl.MaxAmmo, new Vector2(1, 20), Color.Black);
			_spriteBatch.DrawString(font, "Reload: " + _pl.ReloadTime / 1000f + "s", new Vector2(1, 60), Color.Black);
			_spriteBatch.DrawString(font, "Distance: " + _pl.FireDistance / 1000f + "m", new Vector2(1, 80), Color.Black);
			_spriteBatch.DrawString(font, "Fire rate: " + _pl.FireRate, new Vector2(1, 100), Color.Black);
			_spriteBatch.DrawString(font, "Shot speed: " + _pl.BulletSpeed, new Vector2(1, 120), Color.Black);
			_spriteBatch.DrawString(font, "Bullet count: " + _pl.FireCount, new Vector2(1, 140), Color.Black);
			_spriteBatch.DrawString(font, "exp: " + _pl.experience, new Vector2(1, 160), Color.Black);
			_spriteBatch.DrawString(font, "level: " + _pl.Level, new Vector2(1, 180), Color.Black);
			_spriteBatch.DrawString(font, "health: " + _pl.Health, new Vector2(1, 200), Color.Black);
			_spriteBatch.DrawString(font, "pos: " + player.transform.Position.ToString(), new Vector2(1, 240), Color.Black);

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
