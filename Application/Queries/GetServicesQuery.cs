using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PromoCodes_main.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using PromoCodes_main.Infrastructure.Utility.Security;


namespace PromoCodes_main.Application.Queries
{
    public class GetServicesQuery : IRequest<GenericResponse<List<ServicesResult>>>
    {

    }

    public class ServicesResult
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PromoCode { get; set; }
        public bool IsActive { get; set; }


    }

    public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, GenericResponse<List<ServicesResult>>>
    {
        private readonly PromoContext _promoContext;
        private readonly ILogger<GetServicesQueryHandler> _logger;


        public GetServicesQueryHandler(PromoContext promoContext, ILogger<GetServicesQueryHandler> logger)
        {
            _promoContext = promoContext;
            _logger = logger;
        }
        public async Task<GenericResponse<List<ServicesResult>>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
        {
            var promoservices = _promoContext.TemppData.Select(x => new ServicesResult
            {
                Name = x.RefName,
                Description = x.Description,
                PromoCode = x.Codes,
                IsActive = x.ActivationStatus
            }).ToList();
            if (promoservices == null)
            {
                _logger.LogError("List is empty");
                return new GenericResponse<List<ServicesResult>>(true,"List is empty");

            }
            _logger.LogInformation("List has been successfully fetched");
            return new GenericResponse<List<ServicesResult>>(true,"List has been successfully fetched",promoservices);



        }

    }

}