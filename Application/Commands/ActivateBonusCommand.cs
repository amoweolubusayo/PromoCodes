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
    public class ActivateBonusCommand : IRequest<GenericResponse>
    {
        public string PromoCode { get; set; }
    }

    public class ActivateBonusCommandValidator : AbstractValidator<ActivateBonusCommand>
    {
        public ActivateBonusCommandValidator()
        {
            RuleFor(x => x.PromoCode).NotNull().NotEmpty();
        }
    }

    public class ActivateBonusCommandHandler : IRequestHandler<ActivateBonusCommand, GenericResponse>
    {
        private readonly PromoContext _promoContext;
        private readonly ILogger<ActivateBonusCommandHandler> _logger;
        public ActivateBonusCommandHandler(
            PromoContext promoContext,
            ILogger<ActivateBonusCommandHandler> logger
        )
        {
            _promoContext = promoContext;
            _logger = logger;
        }

        public async Task<GenericResponse> Handle(ActivateBonusCommand request, CancellationToken cancellationToken)
        {
            var promo = await _promoContext.TemppData.FirstOrDefaultAsync(x => x.Codes == request.PromoCode);
            if (promo == null)
            {
                _logger.LogError("Promo Code was not found.");
                return new GenericResponse(false,"Promo Code was not found.");
            }
            if (promo.ActivationStatus == true)
            {
                _logger.LogError("Promocode is already active");
                return new GenericResponse(false,"Promocode is already active.");
            }

            promo.ActivationStatus = true;
            await _promoContext.SaveChangesAsync();
            return new GenericResponse(true, "Promocode Activated.");
        }
    }
}