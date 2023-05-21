using Domain.Models;
using Domain.Ports.Driving;

namespace Domain.UseCases;

public class GetClosest : IGetClosest
{
    private readonly IGetMissing _getMissing;

    private Dictionary<string, int> valores = new Dictionary<string, int>()
        {
            { "K",  5},
            { "N",  2},
            { "V",  1}
        };
    public GetClosest(IGetMissing getMissing)
    {
        _getMissing = getMissing;
    }

    public async Task<ResultClosest> Resolve(string entry)
    {
        var split = entry.Split("vs");
        var parte1 = split[0].Trim();
        var parte2 = split[1].Trim();
        bool kingParte1 = false;
        bool kingParte2 = false;
        bool validatorParte1 = false;
        bool validatorParte2 = false;
        bool leFaltaA1 = parte1.Contains("#");
        bool leFaltaA2 = parte2.Contains("#");

        ResultClosest resultado = new ResultClosest();

        if (leFaltaA1 && leFaltaA2)
        {
            resultado.Score1 = 0;
            resultado.Score2 = 0;
            resultado.Letter = "¡ERROR! A ambos documentos les falta 1 firma";
        }
        else
        {
            if (leFaltaA1)
                parte1 = parte1.Replace("#", "");

            if (leFaltaA2)
                parte2 = parte2.Replace("#", "");

            var scoreList1 = parte1.Select(x =>
            {
                kingParte1 = kingParte1 || x.ToString().Equals("K");
                validatorParte1 = validatorParte1 || x.ToString().Equals("V");

                return new Score
                {

                    Letter = x.ToString(),
                    Value = valores[x.ToString()]
                };

            }).ToList();

            var scoreList2 = parte2.Select(x =>
            {
                kingParte2 = kingParte2 || x.ToString().Equals("K");
                validatorParte2 = validatorParte2 || x.ToString().Equals("V");

                return new Score
                {

                    Letter = x.ToString(),
                    Value = valores[x.ToString()]
                };

            }).ToList();


            var score1 = scoreList1.Sum(x => (!kingParte1 || !x.Letter.Equals("V") ? x.Value : 0));
            var score2 = scoreList2.Sum(x => (!kingParte2 || !x.Letter.Equals("V") ? x.Value : 0));

            int winner = score1.CompareTo(score2);
            

            var missing = _getMissing.Resolve(leFaltaA1 ? score1 : score2,leFaltaA1 ? score2 : score1, leFaltaA1 ? kingParte1 : kingParte2, leFaltaA1 ? validatorParte1 : validatorParte2);

            resultado.Score1 = score1 + (leFaltaA1 && missing.Found ? valores[missing.Letter] : 0);
            resultado.Score2 = score2 + (leFaltaA2 && missing.Found ? valores[missing.Letter] : 0);
           

            resultado.Letter = missing.Letter;
        }
       

        
        return resultado;
    }
}
