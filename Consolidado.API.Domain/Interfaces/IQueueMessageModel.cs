using System;

namespace Consolidado.API.Domain.Interfaces
{
    public interface IQueueMessageModel
    {
        public DateTime Data { get; set; }
        public decimal Creditos { get; set; }
        public decimal Debitos { get; set; }
    }
}
