using System;

namespace Consolidado.API.Domain.Entities
{
    public class LactoConsolidado
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Creditos { get; set; }
        public decimal Debitos { get; set; }
        public decimal Saldo { get; set; }
    }
}
