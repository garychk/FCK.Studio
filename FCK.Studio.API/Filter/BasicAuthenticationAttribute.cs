using FCK.Common;
using FCK.Studio.Core;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace FCK.Studio.API.Filter
{
    /// <summary>
    /// API授权基础验证
    /// </summary>
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 检查用户是否有该Action执行的操作权限
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            bool isRquired = false;
            var skb = actionContext.ControllerContext.RouteData;
            var controllerName = (actionContext.ControllerContext.RouteData.Values["controller"]).ToString().ToLower();
            var actionName = (actionContext.ControllerContext.RouteData.Values["action"]).ToString().ToLower();

            FCKAPI fckapi = new FCKAPI();
            //FCKAPIDto input = new FCKAPIDto();            
            //input.API = controllerName + "_" + actionName;
            //input.Status = 1;
            //fckapi.CreateOrUpdate(input);

            try
            {
                //获取Action权限开关
                isRquired = Utility.cBool(Utility.WebApiAuthFlag);
            }
            catch (Exception e)
            {
                //抛出异常
            }

            //如果开启...
            if (isRquired)
            {
                bool isLogin = false;
                string partner = HttpContext.Current.Request["partner"];

                var api_lists = fckapi.GetPageList(1, 10, controllerName + "_" + actionName);
                if (api_lists.total > 0)
                {
                    var api = api_lists.datas.FirstOrDefault();
                    //API状态验证
                    if (api.Status == 1)
                    {
                        fckapi.UpdateUseTimes(api.ID);
                        //用户权限验证
                        try
                        {
                            FCKRegisters register = new FCKRegisters();
                            var power = register.GetModelByNumber(partner);
                            if (power.datas.Register_Status > 0 && power.datas.Register_DeadLine >= DateTime.Now)
                            {
                                Utility.SetSession("RegisterId", power.datas.Register_ID);
                                Utility.SetSession("Commission", power.datas.Register_ApiPower);
                                if (!string.IsNullOrEmpty(power.datas.Register_ApiPower))
                                {
                                    if (power.datas.Register_ApiPower.IndexOf(controllerName + "_" + actionName) >= 0)
                                    {
                                        isLogin = true;
                                    }
                                }
                            }
                            
                        }
                        catch (Exception e) { }
                    }
                }

                if (isLogin)
                {
                    //如果已经登录，则跳过验证
                    base.OnActionExecuting(actionContext);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}
