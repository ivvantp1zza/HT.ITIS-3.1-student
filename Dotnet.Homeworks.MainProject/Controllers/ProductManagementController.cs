using Dotnet.Homeworks.Features.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Products.Commands.UpdateProduct;
using Dotnet.Homeworks.Features.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Homeworks.MainProject.Controllers;

[ApiController]
public class ProductManagementController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductManagementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var res = await _mediator.Send(new GetProductsQuery(), cancellationToken);
        return Ok(res);
    }

    [HttpPost("product")]
    public async Task<IActionResult> InsertProduct(string name, CancellationToken cancellationToken)
    {
        await _mediator.Send(new InsertProductCommand(name), cancellationToken);
        return Ok();
    }

    [HttpDelete("product")]
    public Task<IActionResult> DeleteProduct(Guid guid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    [HttpPut("product")]
    public async Task<IActionResult> UpdateProduct(Guid guid, string name, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateProductCommand(guid, name), cancellationToken);
        return Ok();
    }
}