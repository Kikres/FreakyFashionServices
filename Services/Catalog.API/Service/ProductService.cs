using AutoMapper;
using Catalog.API.Models.Domain;
using Catalog.API.Models.Dto;
using Catalog.API.Repository;

namespace Catalog.API.Service;

public class ProductService
{
    private readonly ProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(ProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto?> CreateProduct(NewProductDto newProductDto)
    {
        var newProduct = _mapper.Map<Product>(newProductDto);
        newProduct.UrlSlug = newProduct.Name.Replace(" ", "-").Replace("-", "").ToLower();
        return _mapper.Map<ProductDto>(await _productRepository.CreateProduct(newProduct));
    }

    //Checks
    public async Task<bool> ProductExists(string sku) => await _productRepository.GetProductBySku(sku) != null;

    public async Task<bool> ProductExists(int id) => await _productRepository.GetProductById(id) != null;
}