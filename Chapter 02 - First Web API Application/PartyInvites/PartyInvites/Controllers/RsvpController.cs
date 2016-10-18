using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PartyInvites.Models;

namespace PartyInvites.Controllers {

    public class RsvpController : ApiController {

        public IEnumerable<GuestResponse> GetAttendees() {
            return Repository.Responses.Where(x => x.WillAttend == true);
        }

        public void PostResponse(GuestResponse response) {
            if (ModelState.IsValid) {
                Repository.Add(response);
            }
        }
    }
}
