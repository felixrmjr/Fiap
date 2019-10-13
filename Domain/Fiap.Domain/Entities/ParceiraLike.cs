using Fiap.Domain.Entities.Base;
using System;

namespace Fiap.Domain.Entities
{
    public class ParceiraLike : EntityBase<int>, ICloneable
    {
        #region [ Propriedades ]

        public int CodigoParceira { get; private set; }

        #endregion

        #region [ Construtores ]

        #endregion

        #region [ Métodos ]

        public void AtualizarCodigoParceira(int codigoParceira)
        {
            if (codigoParceira <= 0)
                AddException(nameof(ParceiraLike), nameof(AtualizarCodigoParceira), "campoObrigatorio", nameof(codigoParceira));

            CodigoParceira = codigoParceira;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
