using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Products.Category;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProductsByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListProducts([FromQuery] PaginatedListRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<PaginatedListCommand<ListProductsResult>>(request);
            var response = await _mediator.Send(command, cancellationToken);

            var paginatedList = await PaginatedList<GetProductResponse>.CreateAsync(
                source: _mapper.ProjectTo<GetProductResponse>(response.Products),
                request.Page,
                request.Size);

            return OkPaginated(paginatedList);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProducts([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetProductRequest { Id = id };

            var validator = new GetProductRequestValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<GetProductResponse>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = _mapper.Map<GetProductResponse>(response)
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
            {
                Success = true,
                Message = "Product created successfully",
                Data = _mapper.Map<CreateProductResponse>(response)
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateProductCommand>(request, opts => opts.Items["Id"] = id);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<UpdateProductResponse>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = _mapper.Map<UpdateProductResponse>(response)
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteProductRequest { Id = id };
            var command = _mapper.Map<DeleteProductCommand>(request);

            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
                return BadRequest("Error deleting product");

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Product deleted successfully"
            });
        }

        [HttpGet("categories")]
        [ProducesResponseType(typeof(ListCategoriesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListCategories(CancellationToken cancellationToken)
        {
            var command = new ListCategoriesCommand();
            var response = await _mediator.Send(command, cancellationToken);

            var categories = _mapper.Map<ListCategoriesResponse>(response);

            return Ok(categories);
        }

        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(PaginatedResponse<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListProductsByCategory([FromQuery] PaginatedListRequest request, [FromRoute] string category, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<PaginatedListCommand<ListProductsResult>>(request);
            command.By = category;

            var response = await _mediator.Send(command, cancellationToken);

            var paginatedList = await PaginatedList<GetProductResponse>.CreateAsync(
                source: _mapper.ProjectTo<GetProductResponse>(response.Products),
                request.Page,
                request.Size);

            return OkPaginated(paginatedList);

        }
    }
}
