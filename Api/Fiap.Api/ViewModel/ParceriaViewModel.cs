using System;

namespace Fiap.Api.ViewModel
{
    public class ParceriaViewModel
    {
        public int Codigo { get; set; }
        public string Titulo { get;set; }
        public string Descricao { get;set; }
        public string UrlPagina { get;set; }
        public string Empresa { get;set; }
        public DateTime DataInicio { get;set; }
        public DateTime DataTermino { get;set; }
        public int QtdLikes { get;set; }
    }
}
