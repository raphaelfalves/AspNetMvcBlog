using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetMvcBlog.Application
{
    public class BaseController : Controller
    {
        const string MSG_SUCCESS = "MSG_SUCCESS";
        const string MSG_ERROR = "MSG_ERROR";

        protected void Success(string message)
        {
            TempData[MSG_SUCCESS] = message;
        }

        protected void Error(string message)
        {
            TempData[MSG_ERROR] = message;
        }
    }
}