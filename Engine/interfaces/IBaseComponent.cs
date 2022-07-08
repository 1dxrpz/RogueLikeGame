namespace Engine
{
	public interface IBaseComponent
	{
		public GameObject Parent { get; set; }
		public void Update();
		public void Draw();
		public void Start();
	}
}
