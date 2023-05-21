using Domain.Models;

namespace Domain.Ports.Driving;

public interface IGetWinner
{
    Task<ResultWinner> Resolve(string entry);
}
