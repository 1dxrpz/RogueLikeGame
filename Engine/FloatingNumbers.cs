using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
	public class FloatingText
	{
		public Vector2 position;
		public string text;

		double _timer = 0;
		FloatingNumbers Parent;
		public FloatingText(FloatingNumbers parent)
		{
			Parent = parent;
			_timer = Time.GameTime.TotalGameTime.TotalMilliseconds;
		}

		public void Update()
		{
			if (Time.GameTime.TotalGameTime.TotalMilliseconds - _timer >= Parent.Lifetime)
				Parent.texts.Remove(this);
			position.Y -= 100 * Time.DeltaTime;
		}
	}
	public class FloatingNumbers : BaseComponent
	{
		public List<FloatingText> texts = new List<FloatingText>();

		public Action DestroyEvent;
		public SpriteFont Font;
		Vector2 _position = Vector2.Zero;
		public int Lifetime;
		
		public FloatingNumbers() { }
		public void AddText(string text)
		{
			texts.Add(new FloatingText(this)
			{
				text = text,
				position = Parent.transform.Position
			});
		}
		public void AddText(string text, Vector2 position)
		{
			texts.Add(new FloatingText(this)
			{
				text = text,
				position = position
			});
		}
		float _scale = 1.2f;
		float _outline = 2f;
		public override void Update()
		{
			texts.FindAll(v => v != null).ForEach(v => v.Update());
		}
		private void DrawOutline(string text, Vector2 position, Vector2 offset)
		{
			Utils.SpriteBatch.DrawString(Font, 
				text, 
				Utils.MainCamera.ScreenPosiiton(position + offset * _outline * _scale),
				Color.Black, 0f, Font.MeasureString(text) / 2f, _scale, SpriteEffects.None, 0.1f);
		}
		public override void Draw()
		{
			texts.FindAll(v => v != null).ForEach(v =>
			{
				DrawOutline(v.text, v.position, new Vector2(1, 1));
				DrawOutline(v.text, v.position, new Vector2(1, -1));
				DrawOutline(v.text, v.position, new Vector2(-1, 1));
				DrawOutline(v.text, v.position, new Vector2(-1, -1));

				DrawOutline(v.text, v.position, new Vector2(0, -1));
				DrawOutline(v.text, v.position, new Vector2(0, 1));
				DrawOutline(v.text, v.position, new Vector2(-1, 0));
				DrawOutline(v.text, v.position, new Vector2(1, 0));

				Utils.SpriteBatch.DrawString(
					Font, v.text,
					Utils.MainCamera.ScreenPosiiton(v.position),
					Color.White, 0f, Font.MeasureString(v.text) / 2f,
					_scale, SpriteEffects.None, 0);
			});
		}
	}
}
