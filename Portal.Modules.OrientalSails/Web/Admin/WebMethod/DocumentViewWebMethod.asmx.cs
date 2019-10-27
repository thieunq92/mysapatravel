using Newtonsoft.Json;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Portal.Modules.OrientalSails.Web.Admin.WebMethod
{
    /// <summary>
    /// Summary description for DocumentViewWebMethod
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class DocumentViewWebMethod : System.Web.Services.WebService
    {
        private DocumentViewBLL documentViewBLL;
        public DocumentViewBLL DocumentViewBLL
        {
            get
            {
                if (documentViewBLL == null)
                {
                    documentViewBLL = new DocumentViewBLL();
                }
                return documentViewBLL;
            }
        }

        public new void Dispose()
        {
            if (documentViewBLL != null)
            {
                documentViewBLL.Dispose();
                documentViewBLL = null;
            }
        }

        [WebMethod]
        public string DocumentGetAll()
        {
            var listDocument = DocumentViewBLL.DocumentGetAll().Where(x => x.Parent != null);
            var listDocumentDTO = new List<DocumentCategoryDTO.DocumentDTO>();
            foreach (var document in listDocument)
            {
                var documentCategoryName = "";
                try
                {
                    documentCategoryName = document.Parent.Name;
                }
                catch { }
                var documentDTO = new DocumentCategoryDTO.DocumentDTO()
                {
                    Name = document.Name,
                    CategoryName = documentCategoryName,
                    Url = document.Url,
                };
                listDocumentDTO.Add(documentDTO);
            }
            Dispose();
            return JsonConvert.SerializeObject(listDocumentDTO);
        }

        [WebMethod]
        public string DocumentCategoryGetAll()
        {
            var listDocumentCategory = DocumentViewBLL.DocumentGetAll().Where(x => x.Parent == null);
            var listDocumentCategoryDTO = new List<DocumentCategoryDTO>();
            foreach (var documentCategory in listDocumentCategory)
            {
                var documentCategoryDTO = new DocumentCategoryDTO()
                {
                    Id = documentCategory.Id,
                    Name = documentCategory.Name,
                };
                var listDocument = DocumentViewBLL.DocumentGetAll().Where(x => x.Parent != null && x.Parent.Id == documentCategory.Id);
                var listDocumentDTO = new List<DocumentCategoryDTO.DocumentDTO>();
                foreach (var document in listDocument)
                {
                    var documentDTO = new DocumentCategoryDTO.DocumentDTO() { 
                        Name = document.Name,
                        CategoryName = documentCategory.Name,
                        Url = document.Url,
                    };
                    listDocumentDTO.Add(documentDTO);
                }
                documentCategoryDTO.ListDocumentDTO = listDocumentDTO;
                listDocumentCategoryDTO.Add(documentCategoryDTO);
            }
            Dispose();
            return JsonConvert.SerializeObject(listDocumentCategoryDTO);
        }
    }
}
