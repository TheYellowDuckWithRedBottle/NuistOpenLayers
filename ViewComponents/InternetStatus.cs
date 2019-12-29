using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace XinYiThree.ViewComponents
{
    public class InternetStatus:ViewComponent
    {
        public async  Task<IViewComponentResult> InvokeAsync()
        {
            var httpClient = new HttpClient();
            var response =await httpClient.GetAsync("http://baidu.com");
          if(response.StatusCode==HttpStatusCode.OK)
            {
                return View(true);
            }
            return View(false);
        }
    }
}
