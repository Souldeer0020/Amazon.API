using Amazon.API_s.DTO_s;
using Amazon.API_s.Errors;
using Amazon.core.Entities;
using Amazon.core.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.API_s.Controllers
{

    public class basketsController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public basketsController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasket(id);
            return Ok(basket?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto customerBasket) //CustomerBasketDto and BasketItemDto 
                                                                                                       //are only made to validate
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasket);
            var UpdatedOrCreatedBasket = await _basketRepository.UpdateBasket(mappedBasket);
            return Ok(UpdatedOrCreatedBasket);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await _basketRepository.DeleteBasket(id);
        }
    }
}
