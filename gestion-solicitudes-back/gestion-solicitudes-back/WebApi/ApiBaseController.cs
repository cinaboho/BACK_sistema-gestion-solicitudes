using Microsoft.AspNetCore.Mvc;
using Proyecto.Data.General;
using Proyecto.Data.Interfaces;

namespace PlantillaApiJWT.WebApi
{
	public class ApiBaseController : ControllerBase
	{
		public ResponseApi<T> ResponseFromSingleModel<T>(T dto) where T : IEntity
		{
			ResponseApi<T> response = new ResponseApi<T>();
			response.Operation = Operation.Selected;
			response.Status = new ResponseMessage { IsSuccessfully = true, Messages = new List<string>() };
			response.Count = 1;
			response.StatusCode = 200;
			response.Data = dto;
			return response;
		}

		public ResponseApi<T> ResponseFromListModel<T>(IEnumerable<T> dto) where T : IEntity
		{
			ResponseApi<T> response = new ResponseApi<T>();
			response.Operation = Operation.Selected;
			response.Status = new ResponseMessage { IsSuccessfully = true, Messages = new List<string>() };
			response.Count = dto.Count();
			response.StatusCode = 200;
			response.ListData = dto;
			return response;
		}

		public ResponseApi<T> ResponseFromInsertModel<T>(T dto) where T : IEntity
		{
			ResponseApi<T> response = new ResponseApi<T>();
			response.Operation = Operation.Inserted;
			response.Status = new ResponseMessage { IsSuccessfully = true, Messages = new List<string>() };
			response.Count = 1;
			response.StatusCode = 200;
			response.Data = dto;
			return response;
		}

		public ResponseApi<T> ResponseFromUpdateModel<T>(T dto) where T : IEntity
		{
			ResponseApi<T> response = new ResponseApi<T>();
			response.Operation = Operation.Updated;
			response.Status = new ResponseMessage { IsSuccessfully = true, Messages = new List<string>() };
			response.Count = 1;
			response.StatusCode = 200;
			response.Data = dto;
			return response;
		}

		public ResponseApi<T> ResponseFromDeleteModel<T>(T dto) where T : IEntity
		{
			ResponseApi<T> response = new ResponseApi<T>();
			response.Operation = Operation.Deleted;
			response.Status = new ResponseMessage { IsSuccessfully = true, Messages = new List<string>() };
			response.Count = 1;
			response.StatusCode = 200;
			response.Data = dto;
			return response;
		}

		public ResponseApi<T> ResponseFromError<T>(Exception ex) where T : IEntity
		{
			ResponseApi<T> response = new ResponseApi<T>();
			response.Operation = Operation.Error;
			response.Status = new ResponseMessage { IsSuccessfully = false, Messages = new List<string>() };
			response.Status.Messages.Add(ex.Message);
			if (ex.InnerException != null) response.Status.Messages.Add(ex.InnerException.Message);
			response.Count = 0;
			response.StatusCode = 500;
			return response;
		}

		public ResponseApi<T> ResponseFromDuplicate<T>(Exception ex) where T : IEntity
		{
			ResponseApi<T> response = new ResponseApi<T>();
			response.Operation = Operation.Duplicate;
			response.Status = new ResponseMessage { IsSuccessfully = false, Messages = new List<string>() };
			response.Status.Messages.Add(ex.Message);
			if (ex.InnerException != null) response.Status.Messages.Add(ex.InnerException.Message);
			response.Count = 0;
			response.StatusCode = 500;
			return response;
		}

		public ResponseApi<T> ResponseFromValidationError<T>(T dto) where T : IEntity
		{
			ResponseApi<T> response = new ResponseApi<T>();
			response.Operation = Operation.Validation;
			response.Status = new ResponseMessage { IsSuccessfully = true, Messages = new List<string>() };
			response.Count = 1;
			response.StatusCode = 200;
			response.Data = dto;
			return response;
		}
	}
}
