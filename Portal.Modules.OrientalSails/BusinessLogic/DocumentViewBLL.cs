using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class DocumentViewBLL
    {
        public DocumentCategoryRepository DocumentCategoryRepository { get; set; }

        public DocumentViewBLL()
        {
            DocumentCategoryRepository = new DocumentCategoryRepository();
        }

        public void Dispose()
        {
            if (DocumentCategoryRepository != null)
            {
                DocumentCategoryRepository.Dispose();
                DocumentCategoryRepository = null;
            }
        }

        public IList<DocumentCategory> DocumentGetAll()
        {
            return DocumentCategoryRepository.DocumentGetAll();
        }
    }
}