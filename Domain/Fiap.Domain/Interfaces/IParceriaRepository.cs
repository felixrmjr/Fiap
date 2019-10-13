using Fiap.Domain.Entities;
using Fiap.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fiap.Domain.Interfaces
{
    public interface IParceriaRepository
    {
        IEnumerable<Parceria> ObterTodas();
        Parceria ObterPorId(int codigo);
        void Salvar(Parceria parceria, int operacao);
        IEnumerable<Parceria> ObterTodasValidas();
    }
}
