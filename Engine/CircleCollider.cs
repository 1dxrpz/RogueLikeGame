using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace Engine
{
	public class CircleCollider : BaseComponent, ICollisionActor
	{
		public float Radius = 0f;
		public IShapeF Bounds { get; }

		public CircleCollider()
		{
			Bounds = new CircleF(new Point(16, 16), 16);
			Bounds.Position = new Point(-40000, -40000);
			Utils._collisionComponent.Insert(this);
		}
		public void OnCollision(CollisionEventArgs collisionInfo)
		{
			OnCollide?.Invoke(((BaseComponent)collisionInfo.Other).Parent);
		}
		public Action<GameObject> OnCollide;

		public override void Destroy()
		{
			Utils._collisionComponent.Remove(this);
			base.Destroy();
		}
		public override void Draw()
		{
			base.Draw();
			//if (Utils._collisionComponent.Contains(this))
			//	Utils.SpriteBatch.DrawCircle((CircleF)Bounds, 5, Color.Black, 2);
		}
		public override void Start()
		{
			Parent = Parent;
			base.Start();
		}

		public override void Update()
		{
			Bounds.Position = Parent.transform.Position;
			base.Update();
		}
	}
}
