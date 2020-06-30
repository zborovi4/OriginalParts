using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OriginalCarParts.Models;

namespace OriginalCarParts
{
    /// <summary>
    /// Summary description for WebServiceTable
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceTable : System.Web.Services.WebService
    {

        [WebMethod]
        public Brand ShowAllBrand()
        {
            Brand _brands;
            using(DatabaseContext db = new DatabaseContext())
            {
                _brands = db.Brands.FirstOrDefault();
            }
            return _brands;
        }
    }
}
