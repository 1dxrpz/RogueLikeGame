using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
	public class SpriteRenderer : Renderer
	{
		public override void Start()
		{
			base.Start();
		}
		public override void Draw()
		{
			var _pos = Utils.MainCamera.ScreenPosiitonPoint(Parent.transform);
			var _bounds = new Rectangle(_pos, Parent.transform.Size);
			if (Utils.MainCamera.Bounds.Intersects(_bounds))
				Utils.SpriteBatch.Draw(Texture, _bounds, _bounds, Color.White,
					Parent.transform.Rotation, Parent.transform.Origin, SpriteEffects.None, Parent.transform.Depth);
		}
	}
}
