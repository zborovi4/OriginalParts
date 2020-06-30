using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OriginalCarParts.Models;
using OriginalCarParts.Models.GetData;
using ClosedXML.Excel;
using System.IO;

namespace OriginalCarParts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        
        //получение с БД список всех позиций.
        private List<AllItems> GetAllItems()
        {
            List<AllItems> listItems;
            using (DatabaseContext db = new DatabaseContext())
            {
                listItems =
                     db.Items.Join(db.Brands,
                i => i.BrandId,
                b => b.BrandId,
                (i, b) => new AllItems
                {
                    Id = i.ItemId,
                    Code = i.ItemCode,
                    Brand = b.Name,
                    BrandId = b.BrandId,
                    Description = i.Description
                }).ToList();
            }
                return listItems;
        }
        

        public ActionResult Contact()
        {

            return View();
        }

        [Authorize]
        public ActionResult Price()
        {
            return View();
        }

        //подготовка к экспорту данных в прайс
        [Authorize]
        private List<GetPrice> GetPrice()
        {
            List<AllItems> allItems = GetAllItems();
            List<GetPrice> prices;
            using(DatabaseContext db = new DatabaseContext())
            {
                     prices = db.Items
                    .Join(db.Brands, i => i.BrandId, b => b.BrandId, (i, b) => new
                            {
                                ItemId = i.ItemId,
                                ItemCode = i.ItemCode,
                                Description = i.Description,
                                Brand = b.Name
                            })
                    .Join(db.Price, i => i.ItemId, p => p.ItemId, (i, p) => new
                            {
                                ItemCode = i.ItemCode,
                                Brand = i.Brand,
                                Description = i.Description,
                                PurchasePrice = p.PurchasePrice,
                                SellingPrice = p.SellingPrice,
                                VendorId = p.VendorId,
                                PriceId = p.PriceId
                            })
                    .Join(db.Vendors, p => p.VendorId, v => v.VendorId, (p, v) => new GetPrice
                            {
                                PriceId = p.PriceId,
                                ItemCode = p.ItemCode,
                                Brand = p.Brand,
                                PurchasePrice = p.PurchasePrice,
                                SellingPrice = p.SellingPrice,
                                Description = p.Description,
                                VendorId = p.VendorId,
                                VendorCode = v.Code,
                                VendorName = v.Name
                            })
                    .ToList();
            }
            return prices;
        }

        //Экспорт данных в прайс Excel
        public ActionResult ExportPrice()
        {
            List<GetPrice> prices = GetPrice();

            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var worksheet = workbook.Worksheets.Add("Price");
                worksheet.Cell("A1").Value = "Item Code";
                worksheet.Cell("B1").Value = "Brand";
                worksheet.Cell("C1").Value = "Description";
                worksheet.Cell("D1").Value = "Vendor Id";
                worksheet.Cell("E1").Value = "Vendor Name";
                worksheet.Cell("F1").Value = "Selling Price";
                worksheet.Cell("G1").Value = "Purchase Price";
                worksheet.Row(1).Style.Font.Bold = true;

                for (int i =0; i<prices.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = $"'{prices[i].ItemCode}";
                    worksheet.Cell(i + 2, 2).Value = prices[i].Brand;
                    worksheet.Cell(i + 2, 3).Value = prices[i].Description;
                    worksheet.Cell(i + 2, 4).Value = prices[i].VendorId;
                    worksheet.Cell(i + 2, 5).Value = prices[i].VendorName;
                    worksheet.Cell(i + 2, 6).Value = prices[i].SellingPrice;
                    worksheet.Cell(i + 2, 7).Value = prices[i].PurchasePrice;
                }

                using(var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                           {
                               FileDownloadName = $"price_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                           };
                }
            }
        }
    }

}
