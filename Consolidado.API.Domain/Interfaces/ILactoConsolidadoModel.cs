using System;

namespace Consolidado.API.Domain.Interfaces
{
    public interface ILactoConsolidadoModel
    {
        public DateTime Data { get; set; }
        public decimal Creditos { get; set; }
        public decimal Debitos { get; set; }
        public decimal Saldo { get; set; }
        public bool Atualizar { get; set; }
    }
}
