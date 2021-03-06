using System.Collections.Specialized;
using System.Web.UI.WebControls;
using NHibernate.Criterion;

namespace CMS.Core.Util
{
    public class RepeaterOrder
    {
        public static string FILE_NAME = "";

        /// <summary>
        /// Đặt link sắp xếp, sử dụng trong sự kiện item databound
        /// </summary>
        /// <param name="e">Repeater Item, trong item template phải có một control HyperLink và một control Image</param>
        /// <param name="property">Tên trường ứng với link, HyperLink phải có Id="hpl[tên trường]", Image có Id="img[tên trường]Order</param>
        /// <param name="QueryString">Truyền vào Request.QueryString</param>
        public static void SetOrderLink(RepeaterItemEventArgs e, string property, NameValueCollection QueryString)
        {
            using (HyperLink hpl = e.Item.FindControl("hpl" + property) as HyperLink)
            {
                if (hpl != null)
                {
                    NameValueCollection queryCollection = new NameValueCollection(QueryString);
                    SetArrowImage(e, property, QueryString);
                    //string query = Request.QueryString.ToString();
                    if (string.IsNullOrEmpty(QueryString["Order"]))
                    {
                        queryCollection.Add("Order", "asc" + property);
                    }
                    else
                    {
                        if (queryCollection["Order"] == "dsc" + property)
                        {
                            queryCollection["Order"] = "asc" + property;
                        }
                        else
                        {
                            queryCollection["Order"] = "dsc" + property;
                        }
                    }
                    hpl.NavigateUrl = FILE_NAME+"?" + Text.JoinNvcToQs(queryCollection);
                }
            }
        }

        /// <summary>
        /// Đặt mũi tên sắp xếp
        /// </summary>
        /// <param name="e">Repeater Item</param>
        /// <param name="property">Tên trường sắp xếp</param>
        /// <param name="QueryString"></param>
        public static void SetArrowImage(RepeaterItemEventArgs e, string property, NameValueCollection QueryString)
        {
            if (QueryString["Order"] != null && QueryString["Order"].Substring(3) == property)
            {
                Image img = e.Item.FindControl(string.Format("img{0}Order", property)) as Image;
                if (img != null)
                {
                    if (QueryString["Order"].Substring(0, 3).ToLower() == "dsc")
                    {
                        img.ImageUrl = "/Admin/AdminModuleImages/arrow_up.gif";
                    }
                    else
                    {
                        img.ImageUrl = "/Admin/AdminModuleImages/arrow_down.gif";
                    }
                    img.Visible = true;
                }
            }
        }

        public static Order GetOrderFromQueryString(NameValueCollection queryString)
        {
            Order order = null;
            if (!string.IsNullOrEmpty(queryString["Order"]))
            {
                order = new Order(queryString["Order"].Substring(3),
                                  queryString["Order"].Substring(0, 3).ToLower() == "asc");
            }
            return order;
        }
    }
}