using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
	public class AnimationRenderer : Renderer
	{
		public int MaxFrames = 0;
		public float AnimationSpeed = 1.0f;
		public int AnimationDelay = 0;
		public Point FrameSize = new Point(32, 32);

		int _frame = 0;
		bool _isPlaying = false;
		public override void Start()
		{
			base.Start();
			Time.Timer(AnimationDelay, () => _isPlaying = true);
		}
		public override void Draw()
		{
			base.Draw();
			var _pos = Utils.MainCamera.ScreenPosiitonPoint(Parent.transform);
			var _bounds = new Rectangle(_pos, Parent.transform.Size);
			var _animBounds = new Rectangle(_frame * FrameSize.X, 0, FrameSize.X, FrameSize.Y);
			if (Utils.MainCamera.Bounds.Intersects(_bounds))
				Utils.SpriteBatch.Draw(Texture, _bounds, _animBounds, Color.White,
					Parent.transform.Rotation, Parent.transform.Origin, SpriteEffects.None, Parent.transform.Depth);
		}
		public override void Update()
		{
			base.Update();
			if (_isPlaying)
			{
				_frame = _frame >= MaxFrames - 1 ? 0 : _frame + 1;
				_isPlaying = false;
				Time.Timer((int)(1000f / AnimationSpeed), () => _isPlaying = true);
			}
		}
	}
}
