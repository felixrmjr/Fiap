using Fiap.Domain.Entities.Base;
using System;

namespace Fiap.Domain.Entities
{
    public class Parceria : EntityBase<int>, ICloneable
    {
        #region [ Propriedades ]

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string UrlPagina { get; private set; }
        public string Empresa { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataTermino { get; private set; }
        public int QtdLikes { get; private set; }

        #endregion

        #region [ Construtores ]

        #endregion

        #region [ Métodos ]

        public void ValidarInserirLike()
        {
            if (Codigo <= 0)
                AddException(nameof(Parceria), nameof(ValidarInserirLike), "Parceira inexistente", nameof(Codigo));

            var dataAtual = DateTime.Now;

            if (dataAtual < DataInicio || dataAtual > DataTermino)
                AddException(nameof(Parceria), nameof(ValidarInserirLike), "Fora do período da parceria", nameof(Codigo));
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
