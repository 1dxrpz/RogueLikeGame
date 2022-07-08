using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RogueLikeGame
{
	public class Player : BaseComponent
	{
		public Action ApplyDamage;

		public int Level = 0;
		public float NextLevelExp = 10;
		public float experience = 0;

		public int Ammo = 6;
		public int MaxAmmo = 6;
		public float ReloadTime = 500f;
		public float FireDistance = 1500f;
		public float FireRate = 5f;
		public float BulletSpeed = 1.5f;
		public int FireCount = 2;
		public float FireDamage = 100f;
		public float Knockback = 20f;

		public float Speed = 1f;
		public int Health = 3;
		
		Vector2 direction;

		public Player()
		{
			direction = Vector2.Zero;
			ApplyDamage += Damage;
		}

		bool _damage = false;
		private void Damage()
		{
			if (!_damage)
			{
				_damage = true;
				Health--;
				Time.Timer(1000, () => _damage = false);
			}
		}

		bool _bullet = true;
		bool _reloading = false;
		Random random = new Random();

		private void Shot()
		{
			if (!_reloading)
			{
				if (--Ammo >= 1) { }
				else
				{
					_reloading = true;
					Time.Timer((int)ReloadTime, () =>
					{
						Ammo = MaxAmmo;
						_reloading = false;
					});
				}
				for (int i = 0; i < FireCount; i++)
				{
					var Bullet = new GameObject(Parent.Scene);
					Bullet.AddComponent<SpriteRenderer>().Texture = MainGame.BlueTexture;
					Bullet.AddComponent<CircleCollider>().Radius = 16;
					Bullet.transform.Size = new Point(32, 8);
					Bullet.transform.Origin = new Vector2(16, 4);
					var _projectile = Bullet.AddComponent<Projectile>();
					_projectile.Lifetime = FireDistance;
					_projectile.Damage = FireDamage;
					_projectile.Speed = BulletSpeed;
					_projectile.Knockback = Knockback;

					var _pos = Utils.MainCamera.ScreenPosiiton(Parent.transform);
					var angle = MathF.Atan2(Mouse.GetState().Position.Y - _pos.Y, Mouse.GetState().Position.X - _pos.X);
					Bullet.transform.Position = Parent.transform.Position;
					Bullet.transform.Rotation = angle - (FireCount / 2 - i) / 4f + (float)(random.NextDouble() - 0.5) / 10f;
					Utils.MainCamera.Shake((FireCount * 15) / 4 + 5, 5, 100);
				}
			}
		}
		public override void Start()
		{
			base.Start();
			Parent.transform.Slippery = .3f;
		}
		public override void Update()
		{
			if (experience >= NextLevelExp)
			{
				Level++;
				NextLevelExp += Level * 10;
				experience = 0;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.D))
				direction.X = 1;
			if (Keyboard.GetState().IsKeyDown(Keys.A))
				direction.X = -1;

			if (Keyboard.GetState().IsKeyDown(Keys.W))
				direction.Y = -1;
			if (Keyboard.GetState().IsKeyDown(Keys.S))
				direction.Y = 1;
			direction.Normalize();

			if (!(Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.W) ||
				Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D)))
			{
				direction = Vector2.Zero;
			}

			Parent.transform.Velocity = direction * Speed * Time.DeltaTime * 200f;

			if (Mouse.GetState().LeftButton == ButtonState.Pressed && _bullet)
			{
				_bullet = false;
				Time.Timer((int)(1000 / FireRate), () =>
				{
					_bullet = true;
				});
				Shot();
			}

			Utils.MainCamera.Position = Vector2.Lerp(
				Utils.MainCamera.Position,
				Parent.transform.Position - Utils.MainCamera.Size / 2 + Parent.transform.Size.ToVector2() / 2, .1f * Time.DeltaTime * 100);
		}
	}
}
