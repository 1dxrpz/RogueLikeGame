using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Collisions;

namespace Engine
{
	public abstract class BaseComponent : IBaseComponent
	{
		public BaseComponent()
		{
		}
		private GameObject _parent;
		public GameObject Parent
		{
			get => _parent;
			set {
				_parent = value;
				_parent.Scene.DrawEvent += Draw;
				_parent.Scene.StartEvent += Start;
				_parent.Scene.UpdateEvent += Update;
			}
		}

		public virtual void Destroy()
		{
			_parent.Scene.DrawEvent -= Draw;
			_parent.Scene.StartEvent -= Start;
			_parent.Scene.UpdateEvent -= Update;
		}
		public virtual void Draw() { }

		public virtual void Start() { }
		
		bool _init = false;
		public virtual void Update()
		{
			if (!_init)
			{
				Start();
				_init = true;
			}
		}
	}
}
