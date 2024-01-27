using Consolidado.API.Domain.Interfaces;
using System;

namespace Consolidado.API.Domain.Models
{
    public class LactoConsolidadoModel: ILactoConsolidadoModel
    {

        public DateTime Data { get; set; }
        public decimal SaldoDiaAnterior { get; set; }
        public decimal Creditos { get; set; }
        public decimal Debitos { get; set; }
        public decimal Saldo { get; set; }
    }
}
