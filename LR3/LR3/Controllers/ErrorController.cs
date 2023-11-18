using System.Web.Http;
using LR3.Models;

namespace LR3.Controllers
{
    public class ErrorController : ApiController
    {
        [System.Web.Http.Route("api/error/{code}")]
        public IHttpActionResult Get(int code)
        {
            CustomErrorDetails errorDetails;
            switch (code)
            {
                case 500:
                    errorDetails = new CustomErrorDetails(500, "Server error");
                    break;

                case 444:
                    errorDetails = new CustomErrorDetails(444, "Model is invalid");
                    break;

                case 404:
                    errorDetails = new CustomErrorDetails(404, "Not found");
                    break;

                default:
                    errorDetails = new CustomErrorDetails(999, "Unknown error code");
                    break;
            }

            return Ok(errorDetails);
        }
    }
}
