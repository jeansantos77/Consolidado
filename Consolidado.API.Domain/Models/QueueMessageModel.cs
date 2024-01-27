using System;

namespace Consolidado.API.Domain.Models
{
    public class QueueMessageModel
    {
        public DateTime Data { get; set; }
        public decimal Creditos { get; set; }
        public decimal Debitos { get; set; }
        public bool Atualizar { get; set; }
    }
}
