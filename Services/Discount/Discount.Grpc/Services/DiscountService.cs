using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtosService.DiscountProtosServiceBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountService(IDiscountRepository repository)
        {
            _repository = repository;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            Coupon? coupon = await _repository.GetAsync(request.ProductName);
            if (coupon is null)
                throw new RpcException(
                    status: new Status(
                        statusCode: StatusCode.NotFound,
                        detail: $"Discount with product name {request.ProductName} is not found."
                        ));

            return new CouponModel
            {
                Amount = coupon.Amount,
                Description = coupon.Description,
                Id = coupon.Id,
                ProductName = coupon.ProductName
            };
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            Coupon coupon = new Coupon
            {
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description,
                Id = request.Coupon.Id,
                ProductName = request.Coupon.ProductName
            };

            bool result = await _repository.CreateAsync(coupon);

            return new CouponModel
            {
                Amount = coupon.Amount,
                ProductName = coupon.ProductName,
                Id = coupon.Id,
                Description = coupon.Description,
            };
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            Coupon coupon = new Coupon
            {
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description,
                Id = request.Coupon.Id,
                ProductName = request.Coupon.ProductName,
            };

            bool result = await _repository.UpdateAsync(coupon);

            return new CouponModel
            {
                Amount = coupon.Amount,
                ProductName = coupon.ProductName,
                Id = coupon.Id,
                Description = coupon.Description,
            };
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            bool result = await _repository.DeleteAsync(request.ProductName);

            return new DeleteDiscountResponse
            {
                Success = result,
            };
        }
    }
}
