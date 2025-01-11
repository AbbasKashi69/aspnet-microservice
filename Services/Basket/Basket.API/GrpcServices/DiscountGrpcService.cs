using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtosService.DiscountProtosServiceClient _service;

        public DiscountGrpcService(DiscountProtosService.DiscountProtosServiceClient service)
        {
            _service = service;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            GetDiscountRequest discountRequest = new GetDiscountRequest
            {
                ProductName = productName,
            };

            return await _service.GetDiscountAsync(discountRequest);
        }
    }
}
