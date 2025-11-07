namespace Aplicacao.Comum
{
	public class ResultadoComando<T>
	{
		public bool Sucesso { get; init; }
		public T? Dados { get; init; }
		public List<string> Erros { get; init; } = new();

		private ResultadoComando() { }

		private ResultadoComando(bool sucesso, T? dados, List<string> erros)
		{
			Sucesso = sucesso;
			Dados = dados;
			Erros = erros;
		}

		public static ResultadoComando<T> Ok(T dados) =>
			new ResultadoComando<T>(true, dados, new List<string>());

		public static ResultadoComando<T> Falha(IEnumerable<string> erros) =>
			new ResultadoComando<T>(false, default, erros.ToList());
	}	
}
