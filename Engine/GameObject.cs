using MonoGame.Extended.Collections;

namespace Engine
{
	public class GameObject : ComponentHandler, IDisposable, IPoolable
	{
		private ReturnToPoolDelegate _returnAction;

		void IPoolable.Initialize(ReturnToPoolDelegate returnAction)
		{
			_returnAction = returnAction;
		}

		public void Return()
		{
			if (_returnAction != null)
			{
				_returnAction.Invoke(this);
				_returnAction = null;
			}
		}

		public IPoolable NextNode { get; set; }
		public IPoolable PreviousNode { get; set; }

		public Transform transform;

		public GameObject()
		{
			Utils.DefaultScene._objects.Add(this);
			transform = AddComponent<Transform>();
		}
		public GameObject(Scene scene) : base(scene)
		{
			scene._objects.Add(this);
			transform = AddComponent<Transform>();
		}
		public override void Destroy()
		{
			Return();
			base.Destroy();
			Dispose();
			Scene._objects.Remove(this);
		}
		public void SpawnObject()
		{

		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
