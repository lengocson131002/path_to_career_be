using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Application.Accounts.Commands
{
    public class AcceptFreelancerRequest : IRequest<StatusResponse>
    {
        public long Id { get; set; }
    }
}
