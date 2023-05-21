using Domain.Models;
using Domain.Ports.Driving;

namespace Domain.UseCases;

public class GetWinner : IGetWinner
{

    private Dictionary<string,int> valores = new Dictionary<string, int>()
        {
            { "K",  5},
            { "N",  2},
            { "V",  1}
        };

    public GetWinner()
    {
    }

    public async Task<ResultWinner> Resolve(string entry)
    {
        var split = entry.Split("vs");
        var parte1 = split[0].Trim();
        var parte2 = split[1].Trim();
        bool kingParte1 = false;
        bool kingParte2 = false;


        var scoreList1 = parte1.Select(x => 
        {
            kingParte1 = kingParte1 || x.ToString().Equals("K");
            return new Score
            {

                Letter = x.ToString(),
                Value = valores[x.ToString()]
            };

        }).ToList();

        var scoreList2 = parte2.Select(x =>
        {
            kingParte2 = kingParte2 || x.ToString().Equals("K");
            return new Score
            {

                Letter = x.ToString(),
                Value = valores[x.ToString()]
            };

        }).ToList();


        var score1 = scoreList1.Sum(x => (!kingParte1 || !x.Letter.Equals("V") ? x.Value : 0));
        var score2 = scoreList2.Sum(x => (!kingParte2 || !x.Letter.Equals("V") ? x.Value : 0));

        //0 empate, < 0 gana 2, >0  gana 1
        int winner = score1.CompareTo(score2);

        ResultWinner resultado = new ResultWinner();
        resultado.Score1 = score1;
        resultado.Score2 = score2;
        resultado.Winner = winner == 0 ? "Empate" : (winner > 0 ? "Parte 1" : "Parte 2");
        return resultado;
    }
}
