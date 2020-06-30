using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using OriginalCarParts.Models;
using OriginalCarParts.Models.GetData;

namespace OriginalCarParts.Controllers
{
    public class IncomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            using(DatabaseContext db = new DatabaseContext())
            {
                //вытаскиваем с БД всех действующих поставщиков.
                List<SelectListItem> vendors = db.Vendors.Join(db.Price, v => v.VendorId, p => p.VendorId, (v, p) => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.VendorId.ToString()
                }).DistinctBy(v => v.Value).ToList();
                ViewBag.Vendors = vendors;
            }
            return View();
        }


        //получение всех приходов и отправка в View
        [Authorize]
        public ActionResult Incomes()
        {
            using(DatabaseContext db = new DatabaseContext())
            {
                var getIncomes = GetIncomes();
                if(Request.IsAjaxRequest())
                {
                    var formattedData = getIncomes.Select(i => new
                    {
                        IncomeId = i.IncomeId,
                        Date = i.Date.ToShortDateString(),
                        Vendor = i.Vendor,
                        NumberOfLine = i.NumberOfLine,
                        SumIncome = i.SumIncome
                    }).ToList();
                   
                    return Json(formattedData, JsonRequestBehavior.AllowGet);
                }
                else
                    return View();
            }
        }

        // удаление с БД информации о приходе (удаление по двум связанным таблицам)
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using(DatabaseContext db = new DatabaseContext())
            {
                 var incomeDetails = db.IncomeDetails
                            .Where(i => i.IncomeId == id).ToList();

                foreach(var val in incomeDetails)
                {
                    db.IncomeDetails.Remove(val);
                }

                Income income = db.Incomes
                            .Where(i => i.IncomeId == id)
                            .FirstOrDefault();
                db.SaveChanges();
                db.Incomes.Remove(income);
                db.SaveChanges();
            }
            return null;
        }

        //получение деталей по приходу
        [Authorize]
        [HttpPost]
        public ActionResult GetIncomeDetails(int id)
        {
            using(DatabaseContext db = new DatabaseContext())
            {
                var incomeDetails = db.IncomeDetails.Where(d => d.IncomeId == id).Join(db.Price, d => d.PriceId, p => p.PriceId, (d, p) => new
                {
                    Quantity = d.Quantity,
                    Price = p.PurchasePrice,
                    ItemId = d.ItemId
                }).Join(db.Items, d => d.ItemId, i => i.ItemId, (d, i) => new
                {
                    Code = i.ItemCode,
                    Description = i.Description,
                    Quantity = d.Quantity,
                    Price = d.Price,
                    BrandId = i.BrandId
                }).Join(db.Brands, i => i.BrandId, b => b.BrandId, (i, b) => new GetIncomeDetails
                {
                    Code = i.Code,
                    Description = i.Description,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    Brand = b.Name
                }).ToList();

                // формируем в строку Html разметку для вывода деталей в таблице
                string str = "<table border=\"1\"><tr><th>Code</th><th>Brand</th><th>Description</th><th>Quantity</th><th>Price</th></tr>";
                foreach (var item in incomeDetails)
                {
                    str += ("<tr><td>" + item.Code + "</td><td>"
                    + item.Brand + "</td><td>"
                    + item.Description + "</td><td>"
                    + item.Quantity + "</td><td>"
                    + item.Price + "</td></tr>");
                    }
                str += "</table>";
                return Json(str, JsonRequestBehavior.AllowGet);
            }
            
        }

        [Authorize]
        [HttpPost] //В СТАДИИ РАЗРАБОТКИ
        public ActionResult AddIncome(int vendorId)
        {
            //using (DatabaseContext db = new DatabaseContext())
            //{

            //    {
            //        var items = db.Items.Join(db.Price, i => i.ItemId, p => p.ItemId, (i, p) => new OnePrice
            //        {
            //            Code = i.ItemCode,
            //            Description = i.Description,
            //            Price = p.PurchasePrice,
            //            VendorId = p.VendorId
            //        }).Where(i => i.VendorId == vendorId).ToList();
            //        return View(items);
            //    }
            //}
            return null;
        }


        [Authorize]
        public ActionResult IncomeCharts()
        {
            //Получаем суммы приходов по всем поставщикам
            IEnumerable<VendorChart> allIncomes = GetIncomes()
                    .GroupBy(i => i.Vendor)
                    .Select(g => new VendorChart { Name = g.Key, Sum = g.Sum(s => s.SumIncome) })
                    .OrderBy(v => v.Sum)
                    .ToList();
            ViewBag.DataPointsBar = JsonConvert.SerializeObject(allIncomes);


            //Получаем топ 5 поставщиков
            List<VendorChart> topIncomes= new List<VendorChart>( allIncomes
                    .OrderByDescending(v => v.Sum)
                    .Take(5)
                    .ToList());

            IEnumerable<VendorChart> otherIncome = allIncomes
                    .Skip(5)
                    .ToList();

            //Получаем общую сумму всех приходов для рассчета в процентном соотношении
            double allSumma = allIncomes.Sum(s => s.Sum);

            //переводим суммы приходов в проценты
            foreach (var x in topIncomes)
            {
                double sum = x.Sum / allSumma * 100;
                x.Sum = Math.Round(sum, 2); 
            }

            //переводим сумму otherIncomes в проценты
            double otherSumma = Math.Round(otherIncome.Sum(s => s.Sum)/allSumma * 100, 2);
            topIncomes.Add(new VendorChart() { Name = $"Other {otherIncome.Count()} suppliers", Sum = otherSumma });

            ViewBag.DataPointsPie = JsonConvert.SerializeObject(topIncomes);
            return View();
        }

        //метод действия получения всех приходов
        private IEnumerable<GetIncome> GetIncomes()
        {
            IEnumerable<GetIncome> getIncomes;
            using (DatabaseContext db = new DatabaseContext())
            {

                getIncomes = db.Incomes.Join(db.Vendors, i => i.VendorId, v => v.VendorId,
                    (i, v) => new GetIncome
                    {
                        IncomeId = i.IncomeId,
                        Date = i.Date,
                        Vendor = v.Name
                    }).ToList();
            }
            return getIncomes;
        }
       
    }
}