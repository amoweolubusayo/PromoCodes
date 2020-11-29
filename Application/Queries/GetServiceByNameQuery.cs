using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromoCodes_main.Infrastructure.Persistence;
using PromoCodes_main.Infrastructure.Utility.Security;
using FluentValidation;


namespace PromoCodes_main.Application.Queries
{
    public class GetServiceByNameQuery : IRequest<GenericResponse<ServiceNameResult>>
    {
        public string ServiceName { get; set; }

    }

    public class ServiceNameResult
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PromoCode { get; set; }

    }

    public class GetServiceByNameQueryValidator : AbstractValidator<GetServiceByNameQuery>
    {
        public GetServiceByNameQueryValidator()
        {
            RuleFor(x => x.ServiceName).NotNull().NotEmpty();
        }
    }

    public class GetServiceByNameQueryHandler : IRequestHandler<GetServiceByNameQuery, GenericResponse<ServiceNameResult>>
    {
        private readonly PromoContext _promoContext;
        private readonly ILogger<GetServiceByNameQueryHandler> _logger;

        public GetServiceByNameQueryHandler(PromoContext promoContext, ILogger<GetServiceByNameQueryHandler> logger)
        {
            _promoContext = promoContext;
            _logger = logger;
        }
        public async Task<GenericResponse<ServiceNameResult>> Handle(GetServiceByNameQuery request, CancellationToken cancellationToken)
        {
            var sname = await _promoContext.TemppData.FirstOrDefaultAsync(x => x.RefName == request.ServiceName);
            if (sname == null)
            {
                return new GenericResponse<ServiceNameResult>(false, "service not found");
            }
           
                var services = new ServiceNameResult
                {
                    Name = sname.RefName,
                    Description = sname.Description,
                    PromoCode = sname.Codes
                };
            

            return new GenericResponse<ServiceNameResult>(true, "service data fetched",services);


        }

    }

}