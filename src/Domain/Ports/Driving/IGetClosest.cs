using Domain.Models;

namespace Domain.Ports.Driving;

public interface IGetClosest
{
    Task<ResultClosest> Resolve(string entry);
}
