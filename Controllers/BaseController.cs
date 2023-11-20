using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QuanLyBanGiay.Controllers
{
    public class BaseController : Controller
    {
        public string CurrentUser
        {
            get {
                //Đọc từ session 
                return HttpContext.Session.GetString("USER_NAME");

            }
            
            set
            {
                //Gán dữ liệu cho session 
                HttpContext.Session.SetString("USER_NAME", value);
            }
        }

        public bool IsLogin
        {
            get
            {
                return !string.IsNullOrEmpty(CurrentUser);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}
