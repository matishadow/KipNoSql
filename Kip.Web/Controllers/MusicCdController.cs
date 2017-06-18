using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Kip.Web.Controllers
{
    public class MusicCdController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection)
        {
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            return await Create(collection, controllerName);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
