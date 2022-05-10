using Domain.Entities;

namespace Application.Commands
{
    public class VisualizarPedido
    {
        public VisualizarPedido(decimal valorTotal, Status status)
        {
            ValorTotal = valorTotal;
            Status = status;
        }

        public decimal ValorTotal { get; set; }
        public Status Status { get; set; }
    }
}
