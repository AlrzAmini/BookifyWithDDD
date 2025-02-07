using Bookify.Application.Features.Apartments.GetById;
using Bookify.Application.Features.Apartments.Search;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookify.Api.Controllers.Apartments
{
    [Route("apartments")]
    public class ApartmentsController(ISender sender) : BaseApiController
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetApartmentByIdQuery(id);

            var result = await sender.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchApartmentsQuery query, CancellationToken cancellationToken)
        {
            var result = await sender.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
