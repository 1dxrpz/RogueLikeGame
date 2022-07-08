using Microsoft.Xna.Framework;

namespace Engine
{
	public class Camera
	{
		private Rectangle _bounds = new Rectangle();
		private Vector2 _size = new Vector2();
		private Point _pointSize = new Point();

		public Vector2 Position = new Vector2();
		public Rectangle Bounds
		{
			get { return _bounds; }
			set
			{
				_bounds = value;
				_size = new Vector2(_bounds.Width, _bounds.Height);
				_pointSize = _size.ToPoint();
			}
		}
		public Vector2 Size
		{
			get => _size;
		}
		public Point PointSize
		{
			get => _pointSize;
		}

		public Camera()
		{
			Utils.UpdateEvent += Update;
		}

		public Vector2 ScreenPosiiton(Transform transform)
		{
			return transform.Position - Position;
		}
		public Vector2 ScreenPosiiton(Vector2 position)
		{
			return position - Position;
		}
		public Point ScreenPosiitonPoint(Transform transform)
		{
			return new Point((int)(transform.Position - Position).X, (int)(transform.Position - Position).Y);
		}
		public Point ScreenPosiitonPoint(Vector2 position)
		{
			return new Point((int)(position - Position).X, (int)(position - Position).Y);
		}
		public void Update()
		{
			if (isShaking)
			{
				Position = Vector2.Lerp(
					Position,
					Position + new Vector2(Utils.Random.Next(-_magnitude, _magnitude), Utils.Random.Next(-_magnitude, _magnitude)) * Time.DeltaTime * 10,
					_velocity);
				isShaking = (float)Time.GameTime.TotalGameTime.TotalMilliseconds - _prev <= _duration;
			}
		}

		bool isShaking = false;

		Transform _transform;
		float _prev = 0f;
		int _magnitude;
		float _velocity;
		float _duration;
		public void Shake(int magnitude, float velocity, float duration)
		{
			if (!isShaking)
			{
				_magnitude = magnitude;
				_velocity = velocity;
				_duration = duration;
				_prev = (float)Time.GameTime.TotalGameTime.TotalMilliseconds;
				isShaking = true;
			}
		}
	}
}
