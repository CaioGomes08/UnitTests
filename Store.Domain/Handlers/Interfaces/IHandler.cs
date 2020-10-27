using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Handlers.Interfaces
{
    // meu handler recebe um tipo genérico onde esse tipo só pode ser um Icommand
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}