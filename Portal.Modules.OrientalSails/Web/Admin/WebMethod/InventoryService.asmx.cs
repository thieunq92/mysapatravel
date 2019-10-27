using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Castle.Components.DictionaryAdapter;
using CMS.Web.UI;
using Newtonsoft.Json;
using NHibernate.Linq;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.DataTransferObject.Inventory;
using Portal.Modules.OrientalSails.Domain;

namespace Portal.Modules.OrientalSails.Web.Admin.WebMethod
{
    /// <summary>
    /// Summary description for InventoryService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class InventoryService : WebServiceBase
    {
        public new SailsModule Module
        {
            get { return base.Module as SailsModule; }
        }
        public InventoryService() : base("Portal.Modules.OrientalSails.SailsModule")
        {
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PushExport(string exports)
        {
            InventoryServRes res = new InventoryServRes();
            res.BillSuccess = new Dictionary<int, int>();
            res.BillFails = new List<int>();
            res.ProductFails = new List<int>();
            res.BillProductFails = new Dictionary<int, int>();
            res.BillProductSuccess = new Dictionary<int, int>();
            try
            {
                if (!string.IsNullOrWhiteSpace(exports))
                {
                    var exportList = JsonConvert.DeserializeObject<List<ExportDTO>>(exports);
                    if (exportList.Count > 0)
                    {
                        foreach (var r in exportList)
                        {
                            var count = Module.IvExportCheckByCode(r.Code);
                            if (count <= 0)
                            {

                                var export = new IvExport();

                                try
                                {
                                    if (r.ExportId > 0)
                                    {
                                        export = Module.GetById<IvExport>(r.ExportId);
                                    }
                                    export.ModifiedDate = r.ModifiedDate;
                                    export.Name = r.Name;
                                    if (r.BookingRoomId > 0)
                                    {
                                        export.BookingRoom = Module.BookingRoomGetById(r.BookingRoomId);
                                        if (export.BookingRoom.Customers != null)
                                        {
                                            export.TotalCustomer = export.BookingRoom.Customers.Count;
                                            export.AverageCost = export.Pay / export.TotalCustomer;
                                        }
                                    }
                                    export.Code = r.Code;
                                    export.CreatedDate = r.CreatedDate;
                                    export.CustomerName = r.CustomerName;
                                    export.Deleted = r.Deleted;
                                    export.Detail = r.Detail;
                                    export.Pay = r.Pay;
                                    if (r.RoomId > 0)
                                        export.Room = Module.RoomGetById(r.RoomId);
                                    export.Status = r.Status;
                                    if (r.StorageId > 0)
                                        export.Storage = Module.IvStorageGetById(r.StorageId);
                                    export.ExportDate = r.ExportDate;
                                    export.Total = r.Total;
                                    export.IsDebt = r.IsDebt;
                                    export.Agency = r.Agency;
                                    if (r.CruiseId > 0)
                                        export.Cruise = Module.CruiseGetById(r.CruiseId);
                                    Module.SaveOrUpdate(export);
                                    res.BillSuccess.Add(r.Id, export.Id);
                                    var isInsertProductSuccess = true;
                                    if (r.ExportProducts.Count > 0)
                                    {
                                        foreach (var p in r.ExportProducts)
                                        {
                                            try
                                            {
                                                var productExport = new IvProductExport();
                                                if (p.ExportProductId > 0)
                                                    productExport = Module.GetById<IvProductExport>(p.ExportProductId);
                                                productExport.Discount = p.Discount;
                                                productExport.DiscountType = p.DiscountType;
                                                productExport.Export = export;
                                                if (p.ProductId > 0)
                                                    productExport.Product = Module.IvProductGetById(p.ProductId);
                                                productExport.QuanityRateParentUnit = p.QuanityRateParentUnit;
                                                productExport.Quantity = p.Quantity;
                                                if (p.StorageId > 0)
                                                    productExport.Storage = Module.IvStorageGetById(p.StorageId);
                                                productExport.Total = p.Total;
                                                productExport.UnitPrice = p.UnitPrice;
                                                if (p.UnitId > 0)
                                                    productExport.Unit = Module.IvUnitGetById(p.UnitId);
                                                Module.SaveOrUpdate(productExport);
                                                res.BillProductSuccess.Add(p.Id, productExport.Id);
                                            }
                                            catch (Exception e)
                                            {
                                                isInsertProductSuccess = false;
                                                res.ProductFails.Add(p.Id);
                                                res.Exceptions += p.Id + ".Pr :" + e.Message + Environment.NewLine;
                                            }
                                        }
                                    }
                                    if (!isInsertProductSuccess)
                                    {
                                        res.BillProductFails.Add(r.Id, export.Id);
                                        res.BillFails.Add(r.Id);
                                    }
                                }
                                catch (Exception e)
                                {
                                    res.BillFails.Add(r.Id);
                                    res.Exceptions += r.Id + ".Ex: " + e.Message + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
                res.Status = "1";
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(res));
            }
            catch (Exception e)
            {
                res.Status = "0";
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(res));
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void PushImport(string imports)
        {
            InventoryServRes res = new InventoryServRes();
            res.BillFails = new List<int>();
            res.ProductFails = new List<int>();
            res.BillProductFails = new Dictionary<int, int>();
            try
            {
                if (!string.IsNullOrWhiteSpace(imports))
                {
                    var importDtos = JsonConvert.DeserializeObject<List<ImportDTO>>(imports);
                    if (importDtos.Count > 0)
                    {
                        foreach (var r in importDtos)
                        {
                            var count = Module.IvImportCheckByCode(r.Code);
                            if (count <= 0)
                            {

                                var import = new IvImport();

                                try
                                {
                                    if (r.ImportId > 0)
                                    {
                                        import = Module.GetById<IvImport>(r.ImportId);
                                    }
                                    import.ModifiedDate = r.ModifiedDate;
                                    import.Name = r.Name;
                                    import.Code = r.Code;
                                    import.CreatedDate = r.CreatedDate;
                                    import.ImportedBy = r.ImportedBy;
                                    import.Deleted = r.Deleted;
                                    import.Detail = r.Detail;
                                    if (r.StorageId > 0)
                                        import.Storage = Module.IvStorageGetById(r.StorageId);
                                    import.ImportDate = r.ImportDate;
                                    import.Total = r.Total;
                                    if (r.AgencyId > 0) import.Agency = Module.GetById<Agency>(r.AgencyId);
                                    if (r.CruiseId > 0)
                                        import.Cruise = Module.CruiseGetById(r.CruiseId);
                                    Module.SaveOrUpdate(import);

                                    var isInsertProductSuccess = true;
                                    if (r.ImportProducts.Count > 0)
                                    {
                                        foreach (var p in r.ImportProducts)
                                        {
                                            try
                                            {
                                                var productImport = new IvProductImport();
                                                if (p.ImportProductId > 0)
                                                    productImport = Module.GetById<IvProductImport>(p.ImportProductId);

                                                productImport.Import = import;
                                                if (p.ProductId > 0)
                                                    productImport.Product = Module.IvProductGetById(p.ProductId);
                                                productImport.Quantity = p.Quantity;
                                                if (p.StorageId > 0)
                                                    productImport.Storage = Module.IvStorageGetById(p.StorageId);
                                                productImport.Total = p.Total;
                                                productImport.UnitPrice = p.UnitPrice;
                                                if (p.UnitId > 0)
                                                    productImport.Unit = Module.IvUnitGetById(p.UnitId);
                                                Module.SaveOrUpdate(productImport);
                                            }
                                            catch (Exception e)
                                            {
                                                isInsertProductSuccess = false;
                                                res.ProductFails.Add(p.Id);
                                            }
                                        }
                                    }
                                    if (!isInsertProductSuccess)
                                    {
                                        res.BillProductFails.Add(r.Id, import.Id);
                                        res.BillFails.Add(r.Id);
                                    }
                                }
                                catch (Exception e)
                                {
                                    res.BillFails.Add(r.Id);
                                }
                            }
                        }
                    }
                }
                res.Status = "1";
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(res));
            }
            catch (Exception e)
            {
                res.Status = "0";
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(res));
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UpdateExport(string exports)
        {
            InventoryServRes res = new InventoryServRes();
            res.BillFails = new List<int>();
            res.ProductFails = new List<int>();
            res.BillProductFails = new Dictionary<int, int>();
            res.BillProductSuccess = new Dictionary<int, int>();
            try
            {
                if (!string.IsNullOrWhiteSpace(exports))
                {
                    var exportList = JsonConvert.DeserializeObject<List<ExportDTO>>(exports);
                    if (exportList.Count > 0)
                    {
                        foreach (var r in exportList)
                        {
                            var count = Module.IvExportCheckByCode(r.Code);
                            if (count > 1)
                            {
                                try
                                {
                                    var export = new IvExport();
                                    if (r.ExportId > 0)
                                    {
                                        export = Module.GetById<IvExport>(r.ExportId);
                                    }
                                    export.ModifiedDate = r.ModifiedDate;
                                    export.Name = r.Name;
                                    if (r.BookingRoomId > 0)
                                    {
                                        export.BookingRoom = Module.BookingRoomGetById(r.BookingRoomId);
                                        if (export.BookingRoom.Customers != null)
                                        {
                                            export.TotalCustomer = export.BookingRoom.Customers.Count;
                                            export.AverageCost = export.Pay / export.TotalCustomer;
                                        }
                                    }
                                    export.Code = r.Code;
                                    export.CreatedDate = r.CreatedDate;
                                    export.CustomerName = r.CustomerName;
                                    export.Deleted = r.Deleted;
                                    export.Detail = r.Detail;
                                    export.Pay = r.Pay;
                                    if (r.RoomId > 0)
                                        export.Room = Module.RoomGetById(r.RoomId);
                                    export.Status = r.Status;
                                    if (r.StorageId > 0)
                                        export.Storage = Module.IvStorageGetById(r.StorageId);
                                    export.ExportDate = r.ExportDate;
                                    export.Total = r.Total;
                                    export.IsDebt = r.IsDebt;
                                    export.Agency = r.Agency;
                                    if (r.CruiseId > 0)
                                        export.Cruise = Module.CruiseGetById(r.CruiseId);
                                    Module.Update(export);
                                    if (r.ExportProducts.Count > 0)
                                    {
                                        foreach (var p in r.ExportProducts)
                                        {
                                            try
                                            {
                                                var productExport = new IvProductExport();
                                                if (p.ExportProductId > 0)
                                                    productExport = Module.GetById<IvProductExport>(p.ExportProductId);
                                                productExport.Discount = p.Discount;
                                                productExport.DiscountType = p.DiscountType;
                                                productExport.Export = export;
                                                if (p.ProductId > 0)
                                                    productExport.Product = Module.IvProductGetById(p.ProductId);
                                                productExport.QuanityRateParentUnit = p.QuanityRateParentUnit;
                                                productExport.Quantity = p.Quantity;
                                                if (p.StorageId > 0)
                                                    productExport.Storage = Module.IvStorageGetById(p.StorageId);
                                                productExport.Total = p.Total;
                                                productExport.UnitPrice = p.UnitPrice;
                                                if (p.UnitId > 0)
                                                    productExport.Unit = Module.IvUnitGetById(p.UnitId);
                                                Module.SaveOrUpdate(productExport);
                                                res.BillProductSuccess.Add(p.Id, productExport.Id);
                                            }
                                            catch (Exception e)
                                            {
                                                res.BillProductFails.Add(r.Id, export.Id);
                                                res.Exceptions += p.Id + ".Pr :" + e.Message + Environment.NewLine;
                                            }
                                        };
                                    }
                                }
                                catch (Exception e)
                                {
                                    res.BillFails.Add(r.Id);
                                    res.Exceptions += r.Id + ".Ex: " + e.Message + Environment.NewLine;
                                }

                            }
                        }
                    }
                }
                res.Status = "1";
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(res));
            }
            catch (Exception e)
            {
                res.Status = "0";
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(res));
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetBookingRoom(int cruiseId)
        {
            var bkr = Module.IvsGetBookingRoom(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7), cruiseId);
            var list = new List<BookingRoomDTO>();
            bkr.ForEach(r =>
            {
                var bkRoom = new BookingRoomDTO();
                bkRoom.Id = r.Id;
                bkRoom.BookingId = r.Book.Id;
                bkRoom.RoomTypeId = r.RoomType.Id;
                bkRoom.RoomTypeName = r.RoomType.Name;
                bkRoom.RoomClassId = r.RoomClass.Id;
                bkRoom.RoomClassName = r.RoomClass.Name;
                bkRoom.RoomId = r.Room.Id;
                var info = "";
                foreach (Customer customer in r.Customers)
                {
                    if (!string.IsNullOrWhiteSpace(customer.Fullname))
                    {
                        info += customer.Fullname + "; ";
                    }
                }
                bkRoom.Customer = info;
                bkRoom.CustomerTotal = r.Customers.Count;
                bkRoom.StartDate = r.Book.StartDate;
                bkRoom.EndDate = r.Book.EndDate;
                list.Add(bkRoom);
            });
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(list));
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetRoom(int cruiseId)
        {
            var rooms = Module.RoomGetAll(Module.GetById<Cruise>(cruiseId));
            var list = new List<RoomDTO>();
            foreach (Room r in rooms)
            {
                var bkRoom = new RoomDTO();
                bkRoom.Id = r.Id;
                bkRoom.Name = r.Name;
                bkRoom.RoomTypeId = r.RoomType.Id;
                bkRoom.RoomTypeName = r.RoomType.Name;
                bkRoom.RoomClassId = r.RoomClass.Id;
                bkRoom.RoomClassName = r.RoomClass.Name;
                bkRoom.CruiseId = r.Cruise.Id;
                bkRoom.Floor = r.Floor;
                bkRoom.Order = r.Order;
                bkRoom.CruiseId = r.Cruise.Id;
                list.Add(bkRoom);
            }
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(list));
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetExport(int cruiseId)
        {
            var exports = Module.IvsGetExport(DateTime.Now.AddDays(-15), cruiseId);
            var list = new List<ExportDTO>();
            foreach (IvExport r in exports)
            {
                var exportDto = new ExportDTO();
                exportDto.ModifiedDate = r.ModifiedDate;
                exportDto.Name = r.Name;
                if (r.BookingRoom != null) exportDto.BookingRoomId = r.BookingRoom.Id;
                exportDto.Code = r.Code;
                exportDto.CreatedDate = r.CreatedDate;
                exportDto.CustomerName = r.CustomerName;
                exportDto.Deleted = r.Deleted;
                exportDto.Detail = r.Detail;
                exportDto.ExportId = r.Id;
                exportDto.Pay = r.Pay;
                if (r.Room != null) exportDto.RoomId = r.Room.Id;
                exportDto.Status = r.Status;
                //exportDto.StorageId = r.Storage.Id;
                exportDto.ExportDate = r.ExportDate;
                exportDto.Total = r.Total;
                exportDto.CruiseId = r.Cruise.Id;
                exportDto.IsDebt = r.IsDebt;
                exportDto.Agency = r.Agency;
                exportDto.ExportProducts = new EditableList<ExportProductDTO>();
                var products = Module.IvProductExportGetByExport(exportDto.ExportId);
                if (products != null && products.Count > 0)
                {
                    foreach (IvProductExport productExport in products)
                    {
                        var exportProductDto = new ExportProductDTO();
                        exportProductDto.Discount = productExport.Discount;
                        exportProductDto.DiscountType = productExport.DiscountType;
                        exportProductDto.ExportId = productExport.Export.Id;
                        exportProductDto.ExportProductId = productExport.Id;
                        if (productExport.Product != null) exportProductDto.ProductId = productExport.Product.Id;
                        exportProductDto.QuanityRateParentUnit = productExport.QuanityRateParentUnit;
                        exportProductDto.Quantity = productExport.Quantity;
                        if (productExport.Storage != null) exportProductDto.StorageId = productExport.Storage.Id;
                        exportProductDto.Total = productExport.Total;
                        exportProductDto.UnitPrice = productExport.UnitPrice;
                        if (productExport.Unit != null) exportProductDto.UnitId = productExport.Unit.Id;
                        exportDto.ExportProducts.Add(exportProductDto);
                    }
                }
                list.Add(exportDto);
            }
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(list));
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetUnit()
        {
            var units = Module.IvUnitGetAll();
            var list = new List<UnitDTO>();
            foreach (IvUnit r in units)
            {
                var unitDto = new UnitDTO();
                unitDto.Id = r.Id;
                unitDto.Math = r.Math;
                unitDto.Name = r.Name;
                unitDto.Note = r.Note;
                if (r.Parent != null) unitDto.ParentId = r.Parent.Id;
                unitDto.Rate = r.Rate;
                list.Add(unitDto);
            }
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(list));
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetAgency()
        {
            var role = Module.RoleGetByName("Cruise Suppliers");
            var agencys = Module.AgencyGetAllByRole(role);
            var list = new List<AgencyDTO>();
            foreach (Agency r in agencys)
            {
                var agencyDto = new AgencyDTO();
                agencyDto.Id = r.Id;
                agencyDto.Name = r.Name;
                list.Add(agencyDto);
            }
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(list));
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCategory()
        {
            var categorys = Module.IvCategoryGetAll(null);
            var list = new List<CategoryDTO>();
            foreach (IvCategory r in categorys)
            {
                var dto = new CategoryDTO();
                dto.Id = r.Id;
                dto.Name = r.Name;
                dto.NameTree = r.NameTree;
                if (r.Parent != null) dto.ParentId = r.Parent.Id;
                list.Add(dto);
            }
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(list));
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetStorage(int cruiseId)
        {
            var storages = Module.IvStorageGetAll(cruiseId);
            var list = new List<StorageDTO>();
            foreach (IvStorage r in storages)
            {
                var dto = new StorageDTO();
                dto.Id = r.Id;
                dto.Name = r.Name;
                dto.NameTree = r.NameTree;
                if (r.Parent != null) dto.ParentId = r.Parent.Id;
                list.Add(dto);
            }
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(list));
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetProduct()
        {
            var allProduct = Module.GetAllProduct();
            var list = new List<ProductDTO>();
            foreach (IvProduct r in allProduct)
            {
                var dto = new ProductDTO();
                dto.Id = r.Id;
                dto.Name = r.Name;
                if (r.Category != null) dto.CatId = r.Category.Id;
                if (r.Unit != null) dto.UnitId = r.Unit.Id;
                dto.Code = r.Code;
                list.Add(dto);
            }
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(list));
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetProductPrice(int cruiseId)
        {
            var productPrices = Module.IvGetProductPrices(cruiseId);
            var list = new List<ProductPriceDTO>();
            foreach (IvProductPrice r in productPrices)
            {
                var productPriceDto = new ProductPriceDTO();
                productPriceDto.Price = r.Price;
                productPriceDto.ProductId = r.Product.Id;
                productPriceDto.UnitId = r.Unit.Id;
                productPriceDto.StorageId = r.Storage.Id;
                list.Add(productPriceDto);
            }
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(list));
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetProductInStock(int cruiseId)
        {
            var productPrices = Module.IvsGetProductInStock(cruiseId);
            var list = new List<ProductInStockDTO>();
            foreach (IvInStock r in productPrices)
            {
                var stockDto = new ProductInStockDTO();
                stockDto.CategoryId = r.CategoryId;
                stockDto.CategoryName = r.CategoryName;
                stockDto.UnitId = r.UnitId;
                stockDto.StorageId = r.StorageId;
                stockDto.StorageName = r.StorageName;
                stockDto.Code = r.Code;
                stockDto.Id = r.Id;
                stockDto.Name = r.Name;
                stockDto.Quantity = r.Quantity;
                stockDto.UnitName = r.UnitName;
                stockDto.WarningLimit = r.WarningLimit;
                list.Add(stockDto);
            }
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(list));
        }
        public string Base64Decode(string base64EncodedData)
        {
            if (!string.IsNullOrWhiteSpace(base64EncodedData))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            return "";
        }
    }
}
