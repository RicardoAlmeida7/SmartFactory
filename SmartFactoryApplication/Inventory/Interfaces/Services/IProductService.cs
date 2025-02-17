﻿using SmartFactoryApplication.Inventory.Models;
using SmartFactoryApplication.Model;

namespace SmartFactoryApplication.Inventory.Interfaces.Services
{
    public interface IProductService
    {
        Task<Response<ProductModel>> CreateProductAsync(ProductModel model);
        Task<Response<ProductModel?>> GetProductByIdAsync(int id);
        Task<Response<IEnumerable<ProductModel>>> GetAllProductsAsync();
        Task<Response<ProductModel>> UpdateProductAsync(ProductModel model);
        Task<Response<ProductModel>> DeleteProductAsync(int id);
    }
}
