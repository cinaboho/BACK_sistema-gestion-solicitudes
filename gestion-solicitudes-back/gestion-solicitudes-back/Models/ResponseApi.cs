using Proyecto.Data.Interfaces;

namespace Proyecto.Data.General
{
	public enum Operation
	{
		Error = 0,
		Selected = 1,
		Inserted = 2,
		Updated = 3,
		Deleted = 4,
		Validation = 5,
		Duplicate = 6
	}
	public class ResponseMessage
	{
		public bool IsSuccessfully { get; set; }
		public List<string> Messages { get; set; }

		public ResponseMessage()
		{
			this.IsSuccessfully = false;
			this.Messages = new List<string>();
		}
	}
	public class ResponseApi<T> where T : IEntity
	{
		public Operation Operation { get; set; }
		public ResponseMessage Status { get; set; }
		public int StatusCode { get; set; }
		public int Count { get; set; }
		public T Data { get; set; }
		public IEnumerable<T> ListData { get; set; }
	}
}
