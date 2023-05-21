using Domain.Models;
using Domain.Ports.Driving;

namespace Domain.UseCases;

public class GetMissing : IGetMissing
{
   

    private Dictionary<string, int> valores = new Dictionary<string, int>()
        {
            { "K",  5},
            { "N",  2},
            { "V",  1}
        };
    public GetMissing()
    {
    }

    public ResultMissing Resolve(int minorScore, int higherScore, bool minorHaveKing, bool containsValidator)
    {
        ResultMissing resultado = new ResultMissing();
        resultado.Found = true;
        int difference = higherScore - minorScore;
        if (difference == 0)
        {
            resultado.Letter = minorHaveKing ? "N" : "V";
        }else if (difference > 5 || (difference == 5 && containsValidator))
        {
            resultado.Letter = "No hay combinación posible para superarlo";
            resultado.Found = false;

        }else if (difference == 5 && !containsValidator)
        {
            resultado.Letter = "No hay combinación para superarlo. La única posibilidad es igualarlo con la firma K";
            resultado.Found = false;

        }
        else
        {
            resultado.Letter = difference < 1 && !minorHaveKing ? "V" : (difference < 2 ? "N" : "K"); 
            
        }


        return resultado;
    }
}
