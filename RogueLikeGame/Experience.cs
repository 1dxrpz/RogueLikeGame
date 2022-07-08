using Engine;
using Microsoft.Xna.Framework;

namespace RogueLikeGame
{
	public class Experience : BaseComponent
	{
		public float experience = 10f;
		public Experience()
		{
		}

		public override void Update()
		{
			var _pos = Utils.MainCamera.ScreenPosiitonPoint(Parent.transform);
			var _bounds = new Rectangle(_pos, Parent.transform.Size);
			if (Utils.MainCamera.Bounds.Intersects(_bounds))
			{
				if (Vector2.Distance(Parent.transform.Position, MainGame.player.transform.Position) < 100)
				{
					Parent.transform.Position = Vector2.Lerp(Parent.transform.Position, MainGame.player.transform.Position, .1f * Time.DeltaTime * 100f);
				}
				if (Vector2.Distance(Parent.transform.Position, MainGame.player.transform.Position) < 20)
				{
					MainGame.player.GetComponent<Player>().experience += experience;
					Parent.Destroy();
				}
			}
		}
	}
}
