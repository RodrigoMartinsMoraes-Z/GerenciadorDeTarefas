namespace Api.GerenciadorDeTarefas.Recursos
{
    public class Authorization
    {
        public TipoDeAutorizacao AuthenticationType { get; set; }
        public string Key { get; set; }
        public string Password { get; set; }
        public string Value { get; set; }
        public string User { get; set; }
    }

    public enum TipoDeAutorizacao
    {
        Api_Key = 1,
        Basic,
        Bearer,

    }
}