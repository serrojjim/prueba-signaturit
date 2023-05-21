
using Domain.Ports.Driving;
using Microsoft.AspNetCore.Mvc;

using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Service.DrivingAdapters.RestAdapters;


public static class ScoreRestAdapter
{

    public static void RegisterApis(WebApplication app)
    {
        // GET
        app.MapGet("/getwinner", async (string entry, IGetWinner getWinner) =>
            await getWinner.Resolve(entry));

        // POST
        app.MapPost("/getclosest", async ([FromBody] string entry, IGetClosest getClosest) =>
        {
            return await getClosest.Resolve(entry);
        });


    }
}