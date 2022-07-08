using Engine;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace RogueLikeGame
{
	public class Enemy : BaseComponent
	{
		public float Health = 1000f;

		public Enemy()
		{
			
		}

		//public void OnCollision(CollisionEventArgs collisionInfo)
		//{
		//	if (collisionInfo.Other.GetType() == typeof(Enemy))
		//	{
		//		GameObject o = ((Enemy)collisionInfo.Other).Parent;
		//		Bounds.Position += -Parent.transform.DirectionTowards(o.transform) * Time.DeltaTime * 225f;
		//	}
		//	if (collisionInfo.Other.GetType() == typeof(Projectile))
		//	{
		//		GameObject o = ((Enemy)collisionInfo.Other).Parent;

		//		var dmg = o.GetComponent<Projectile>().Damage;
		//		Health -= dmg;
		//		Parent.GetComponent<FloatingNumbers>().AddText(dmg.ToString(), o.transform.Position);
		//		o.Destroy();
		//	}
		//}
		public override void Start()
		{
			Parent.GetComponent<CircleCollider>().OnCollide += Collision;
		}
		public override void Destroy()
		{
			base.Destroy();
			Parent.GetComponent<CircleCollider>().OnCollide -= Collision;
		}
		public void Collision(GameObject obj)
		{
			if (obj.HasComponent<Enemy>())
			{
				Parent.transform.Velocity = -Parent.transform.DirectionTowards(obj.transform) * Time.DeltaTime * 120f;
			}
			if (obj.HasComponent<Projectile>())
			{
				var _p = obj.GetComponent<Projectile>();
				var _dmghalf = _p.Damage / 2 - 10;
				var _dmg = MathF.Round(_p.Damage - _dmghalf + _dmghalf * (1f - _p.Life));
				Health -= _dmg;
				Parent.GetComponent<FloatingNumbers>().AddText(_dmg.ToString(), Parent.transform.Position);
				Parent.transform.Velocity =
					-Parent.transform.DirectionTowards(obj.transform) * Time.DeltaTime * 120f * _p.Knockback * 10f;
				obj.Destroy();
			}
		}

		public override void Update()
		{
			
			base.Update();

			var _pos = Utils.MainCamera.ScreenPosiitonPoint(Parent.transform);
			var _bounds = new Rectangle(_pos, Parent.transform.Size);
			Parent.transform.Velocity =
				Parent.transform.DirectionTowards(MainGame.player.transform) * Time.DeltaTime * 100f;

			if (Health <= 0)
			{
				GameObject exp = new GameObject();
				exp.AddComponent<SpriteRenderer>().Texture = MainGame.YellowTexture;
				exp.AddComponent<Experience>();
				exp.transform.Size = new Point(8, 8);
				exp.transform.Origin = new Vector2(4, 4);
				exp.transform.Position = Parent.transform.Position;
				Parent.Destroy();
			}
		}
	}
}
