using BusinessLayer.Base.Commands;
using BusinessLayer.Base.Services;
using BusinessLayer.Orders.ViewModels;
using DataLayer.Orders.DTOs;

namespace BusinessLayer.Orders.Commands
{
    public class DeleteCreatingPositionCommand : BaseCommand
    {
        public DeleteCreatingPositionCommand(CreateOrderViewModel createOrderViewModel, CreatingPositionDTO position)
        {
            _createOrderViewModel = createOrderViewModel;
            _position = position;
        }
        public override void Execute(object? parameter)
        {
            _createOrderViewModel.RemovePosition(_position);
        }

        private readonly CreateOrderViewModel _createOrderViewModel;
        private readonly CreatingPositionDTO _position;
    }
}
