using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Gui
{
	public abstract class BaseGUI
	{
		public Rectangle Bounds { get; set; }
		public Texture2D Texture { get; set; }

		public Vector2 Position { get; set; }

		Scene scene;

		public BaseGUI (Scene _scene)
		{
			scene = _scene;
			scene.UpdateEvent += Update;
			scene.DrawEvent += Draw;
			scene.StartEvent += Start;
		}

		public virtual void Update() { }

		public virtual void Draw() { }
		public virtual void Start() { }
	}
}
