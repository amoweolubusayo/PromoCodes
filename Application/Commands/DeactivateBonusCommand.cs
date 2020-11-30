using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using PromoCodes_main.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using PromoCodes_main.Infrastructure.Utility.Security;

namespace PromoCodes_main.Application.Commands
{
    public class DeactivateBonusCommand : IRequest<GenericResponse>
    {
        public string PromoCode { get; set; }
    }

    public class DeactivateBonusCommandValidator : AbstractValidator<DeactivateBonusCommand>
    {
        public DeactivateBonusCommandValidator()
        {
            RuleFor(x => x.PromoCode).NotNull().NotEmpty();
        }
    }

    public class DeactivateBonusCommandHandler : IRequestHandler<DeactivateBonusCommand, GenericResponse>
    {
        private readonly PromoContext _promoContext;
        private readonly ILogger<DeactivateBonusCommandHandler> _logger;
        public DeactivateBonusCommandHandler(
            PromoContext promoContext,
            ILogger<DeactivateBonusCommandHandler> logger
        )
        {
            _promoContext = promoContext;
            _logger = logger;
        }

        public async Task<GenericResponse> Handle(DeactivateBonusCommand request, CancellationToken cancellationToken)
        {
            var promo = await _promoContext.TemppData.FirstOrDefaultAsync(x => x.Codes == request.PromoCode);
            if (promo == null)
            {
                _logger.LogError("Promo Code was not found.");
                return new GenericResponse(false,"Promo Code was not found.");
            }
            if (promo.ActivationStatus == false)
            {
                _logger.LogError("Promocode is currently deactived");
                return new GenericResponse(false,"Promocode is currently deactived.");
            }

            promo.ActivationStatus = false;
            await _promoContext.SaveChangesAsync();
            return new GenericResponse(true, "Promocode Deactivated.");
        }
    }
}