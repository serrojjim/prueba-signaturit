using Domain.Models;

namespace Domain.Ports.Driving;

public interface IGetMissing
{
    ResultMissing Resolve(int minorScore,int higherScore, bool minorHaveKing, bool containsValidator);
}
