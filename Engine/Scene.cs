namespace Engine
{
	public class Scene
	{
		public Action UpdateEvent;
		public Action StartEvent;
		public Action DrawEvent;

		public List<GameObject> _objects = new List<GameObject>();
		
		public void Start ()
		{
			StartEvent?.Invoke();
		}
		public void Draw ()
		{
			DrawEvent?.Invoke();
		}
		public void Update ()
		{
			_objects.Sort((a, b) => (int)((b.transform.Position.Y + b.transform.Size.Y) - (a.transform.Position.Y + a.transform.Size.Y)));
			for (int i = 0; i < _objects.Count; i++)
			{
				_objects[i].transform.Depth = (float)i / _objects.Count;
			}
			UpdateEvent?.Invoke();
		}
	}
}
