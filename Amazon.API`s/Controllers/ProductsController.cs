using Amazon.API_s.DTO_s;
using Amazon.API_s.Errors;
using Amazon.API_s.Helpers;
using Amazon.core;
using Amazon.core.Entities;
using Amazon.core.Repositories;
using Amazon.core.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.API_s.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IGenericRepository<Product> _productsRepo;
        //private readonly IGenericRepository<ProductBrand> _brandsRepo;
        //private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IMapper _mapper;

        public ProductsController(/*IGenericRepository<Product> productsRepo,IGenericRepository<ProductBrand> brandsRepo,IGenericRepository<ProductType> typesRepo*/IUnitOfWork unitOfWork,IMapper mapper) // IGenericRepository<Product> this is where T is known and implicitly passed down the hierarchyi
        {
            //_productsRepo = productsRepo;
            //_brandsRepo = brandsRepo;
            //_typesRepo = typesRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //[Authorize]
        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllProducts([FromQuery]/*means that specParams will get its value from the query params which will bind over specParams properties*/ ProductSpecParams specParams) // we collected all parameters to be in one object and that is clean code
        {
            var spec = new ProductsWithBrandAndTypeSpecification(specParams);
            
            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFilterationTypeSpec(specParams); // does not work

            var count= await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(spec);

            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex,specParams.PageSize,count,data));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductsWithBrandAndTypeSpecification(id);

            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpecAsync(spec);

            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var Brands=await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(Brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Types=await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(Types);
        }
    }
}
