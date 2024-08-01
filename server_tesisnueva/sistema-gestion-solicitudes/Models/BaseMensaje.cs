using System.Diagnostics;

namespace Proyecto.Models
{
	public class BaseMensaje
	{
		public const int SUCCESS = 0;
		public const int WARNING = 200;
		public const int ERROR = 300;

		protected string _mensaje = String.Empty;

		protected object _item;

		protected int _estado { get; set; }

		protected string _stacktrace { get; set; }

		protected string _strEstado
		{
			get
			{
				switch (this._estado)
				{
					case 0:
						return "OK";
					case 200:
						return "WARNING";
					case 300:
						return "ERROR";
					default:
						return "Estado no identificado";
				}
			}
		}

		virtual protected string _toString()
		{
			return "Mensaje:" + this._mensaje + "- Estado:" + this._estado + "- StackTrace:" + this._stacktrace;
		}

		public BaseMensaje()
		{
			this._estado = SUCCESS;
		}

		public BaseMensaje(int estado, string mensaje, string stacktrace, object item = null)
		{
			this._estado = estado;
			this._mensaje = mensaje;
			this._stacktrace = stacktrace;
			this._item = item;
		}

		public BaseMensaje(Exception e, string mensajeCliente = null, object item = null)
		{
			setException(e, mensajeCliente);
			_item = item;
		}

		public virtual void setException(Exception e, string mensaje = null)
		{
			string traceId = Activity.Current?.Id;
			//string traceId = Activity.Current?.TraceId.ToString();

			if (!string.IsNullOrWhiteSpace(traceId))
			{
				traceId = "[" + traceId + "] ";
			}
			_estado = BaseMensaje.ERROR;

			if (string.IsNullOrWhiteSpace(mensaje))
			{
				_mensaje = e.Message;
			}
			else
			{
				_mensaje = mensaje;
			}


#if DEBUG
			_stacktrace = e.StackTrace;
#endif
		}
	}
}
