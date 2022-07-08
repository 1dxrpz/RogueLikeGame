using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Collisions;

namespace Engine
{
	public class Utils
	{

		public static Pool<GameObject> GameObjectPool = new Pool<GameObject>(() => new GameObject());
		public static CollisionComponent _collisionComponent;
		public static Action? UpdateEvent;
		public static Action? StartEvent;
		public static Action? DrawEvent;

		static public SpriteBatch? SpriteBatch;
		static public Camera? MainCamera;
		static public Scene? DefaultScene;

		static public Random Random = new Random();
		static public GraphicsDevice? GraphicsDevice;

		static public void Update() => UpdateEvent?.Invoke();
		static public void Start() => StartEvent?.Invoke();
		static public void Draw() => DrawEvent?.Invoke();
		public static Texture2D GetTexture(Color color)
		{
			var data = new Color[1] { color };
			var texture = new Texture2D(GraphicsDevice, 1, 1);
			texture.SetData(data);
			return texture;
		}
	}
	public class Time
	{
		public static float DeltaTime = 0f;
		public static GameTime? GameTime;
		public static void Update(GameTime gameTime)
		{
			GameTime = gameTime;
			DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
		}
		public static async void Timer(int duration, Action callback)
		{
			await Task.Delay(duration);
			callback();
		}
	}
}
