﻿using Catalog.API.Models.Dto;
using Catalog.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ProductDto>> CreateProduct(NewProductDto newProductDto)
    {
        if (await _productService.ProductExists(newProductDto.Sku)) return Conflict("Product exists");

        var productDto = await _productService.CreateProduct(newProductDto);

        return Created("", productDto);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDto>))]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts() => Ok(await _productService.GetProducts());
}