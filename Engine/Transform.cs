using Microsoft.Xna.Framework;

namespace Engine
{
	public class Transform : BaseComponent
	{
		public Vector2 Position = new Vector2();
		public Vector2 Velocity = new Vector2();
		public Point Size = new Point(32, 32);
		public Vector2 Origin = Vector2.Zero;
		public float Rotation = 0f;
		public float Depth = 0;
		public float Slippery = .95f;

		public Vector2 Forward
		{
			get
			{
				return new Vector2(MathF.Cos(Rotation), MathF.Sin(Rotation));
			}
		}
		public Vector2 DirectionTowards(Transform transform)
		{
			var angle = MathF.Atan2(transform.Position.Y - Position.Y, transform.Position.X - Position.X);
			return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
		}
		Vector2 _smoothVelocity;
		public override void Update()
		{
			base.Update();

			_smoothVelocity = Vector2.Lerp(_smoothVelocity, Velocity, 1f - Slippery);

			Position = Position + _smoothVelocity;
		}
	}
}
