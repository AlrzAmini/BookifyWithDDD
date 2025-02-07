using Bookify.Application.Features.Apartments.GetById;
using Bookify.Application.Features.Bookings.GetBookingById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookify.Api.Controllers.Bookings
{
    [Route("bookings")]
    public class BookingsController(ISender sender) : BaseApiController
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetBookingByIdQuery(id);

            var result = await sender.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
