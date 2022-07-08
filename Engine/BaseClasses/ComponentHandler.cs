namespace Engine
{
	public abstract class ComponentHandler
	{
		private List<BaseComponent> Components;
		public Scene Scene;
		public ComponentHandler()
		{
			Components = new List<BaseComponent>();
			Scene = Utils.DefaultScene;
		}
		public ComponentHandler(Scene scene)
		{
			Components = new List<BaseComponent>();
			Scene = scene;
		}
		public virtual void Destroy()
		{
			Components.ForEach(v => v.Destroy());
		}

		public T? GetComponent<T>() where T : BaseComponent
		{
			var value = Components.Find(v => v.GetType() == typeof(T));
			return value == null ? null : (T)Convert.ChangeType(value, typeof(T));
		}
		public T? AddComponent<T>() where T : BaseComponent, new() {
			if (!HasComponent<T>())
			{
				var instance = new T();
				instance.Parent = (GameObject)this;
				//var instance = Activator.CreateInstance(typeof(T),
				//  new object[] { this }) as T;
				//var _comp = (T)Convert.ChangeType(instance, typeof(T));
				Components.Add(instance);
				return instance;
			}
			return null;
		}
		public bool HasComponent<T>() where T : BaseComponent =>
			Components.Exists(v => v.GetType() == typeof(T));
	}
}
