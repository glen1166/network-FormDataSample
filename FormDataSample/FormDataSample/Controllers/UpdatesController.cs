using FormDataSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FormDataSample.Controllers
{
    public class UpdatesController : ApiController
    {
        static readonly Dictionary<Guid, Update> updates = new Dictionary<Guid, Update>();

        [HttpPost]
        [ActionName("Simple")]
        public HttpResponseMessage PostSimple([FromBody] string value)
        {
            if (value != null)
            {
                Update update = new Update()
                {
                    Status = HttpUtility.HtmlEncode(value),
                    Date = DateTime.UtcNow
                };

                var id = Guid.NewGuid();
                updates[id] = update;

                var response = new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent(update.Status)
                };

                response.Headers.Location =
                    new Uri(Url.Link("DefaultApi", new { action = "status", id = id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}