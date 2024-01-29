using Consolidado.API.Domain.Interfaces;
using System;

namespace Consolidado.API.Domain.Models
{
    public class QueueMessageModel : IQueueMessageModel
    {
        public DateTime Data { get; set; }
        public decimal Creditos { get; set; }
        public decimal Debitos { get; set; }
    }
}
