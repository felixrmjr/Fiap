using System;

namespace Fiap.Domain.Entities.Base
{
    public abstract class EntityBase<TIdentity> : ValidatableObject
    {
        public virtual TIdentity Codigo { get; protected set; }
        public virtual DateTime DataHoraCadastro { get; protected set; }

        public virtual void AtualizarCodigo(TIdentity codigo)
        {
            if (codigo == null)
                AddException(nameof(EntityBase<TIdentity>), nameof(this.Codigo), "campoObrigatorio", "codigo");

            if (string.IsNullOrEmpty(codigo.ToString()))
                AddException(nameof(EntityBase<TIdentity>), nameof(this.Codigo), "campoObrigatorio", "codigo");

            this.Codigo = codigo;
        }

        public virtual void AtualizarDataCriacao(DateTime? data = null) => this.DataHoraCadastro = data.HasValue ? data.GetValueOrDefault() : DateTime.Now;
    }
}
