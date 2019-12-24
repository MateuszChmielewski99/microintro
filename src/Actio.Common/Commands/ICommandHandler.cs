using System.Threading.Tasks;

namespace Actio.Common.Commands
{
    interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync();
    }
}
