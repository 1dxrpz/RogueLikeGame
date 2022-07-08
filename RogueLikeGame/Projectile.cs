using Engine;

namespace RogueLikeGame
{
	public class Projectile : BaseComponent
	{

		public float Speed = 1;
		public float Lifetime = 1000f;
		public float Damage = 10f;
		public float Knockback = 1f;
		public float Life = 0f;

		private float _init;
		private float _life;

		public Projectile()
		{
			_init = (float)Time.GameTime.TotalGameTime.TotalMilliseconds;
		}

		public override void Update()
		{
			base.Update();
			Parent.transform.Position += Parent.transform.Forward * Time.DeltaTime * Speed * 200f;
			_life = (float)Time.GameTime.TotalGameTime.TotalMilliseconds - _init;
			Life = _life / Lifetime;
			if (_life >= Lifetime)
			{
				Parent.Destroy();
			}
		}
	}
}
