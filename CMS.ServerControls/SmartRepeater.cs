using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.ServerControls
{
    public class SmartRepeater : Repeater, IPostBackDataHandler
    {

        #region -- Rendered Item --
        private string _renderedItem;
        /// <summary>
        /// Copy đoạn html đã render của một repeateritem vào đây, thay số thứ tự của item = █ (alt+219)
        /// (Đoạn này đang xử lý theo chiều hướng khá hacking & manual, cần nâng cấp thêm
        /// Lưu ý: không xuống dòng, xử lý các ký tự ", '
        /// ' thay bằng \', \' thay bằng \\\' (@ string)
        /// </summary>
        public string RenderedItem
        {
            get { return _renderedItem; }
            set { _renderedItem = value; }
        }
        #endregion

        #region -- Parent ID --
        private string _parentClientID;
        public string ParentClientID
        {
            get { return _parentClientID; }
            set { _parentClientID = value; }
        }

        private bool _hasHeader;
        public bool HasHeader
        {
            get { return _hasHeader; }
            set { _hasHeader = value; }
        }
        #endregion

        public string AddItemScript
        {
            get { return string.Format("Add{0}();", UniqueID); }
        }

        #region -- Page events --
        protected override void OnLoad(EventArgs e)
        {
            // Chỉ tương thích với Firefox
            string replaceby = string.Empty;
            if (HasHeader)
            {
                replaceby = "replaceby = txtCount.value;";
            }

            string appendFunc =
                @"function AppendData(parentid, data)
    {
        nodes = document.getElementById(parentid).getElementsByTagName('input');
        count = 0;
        idarray = new Array();
        dataarray = new Array();
        for (i=0;i<nodes.length; i++)
        {
            if ((nodes[i]).nodeType == 1)
            {
                idarray[count] = nodes[i].id;
                dataarray[count] = nodes[i].value;
                count++;
            }
        }
        document.getElementById(parentid).innerHTML += data;
        for (i=0;i<count; i++)
        {
            try {
                document.getElementById(idarray[i]).value = dataarray[i];
            }
            catch(err) {}
        }
    }";
            Page.ClientScript.RegisterClientScriptBlock(typeof(SmartRepeater), "appendFunc", appendFunc, true);

            string script =
                string.Format(
                    @"function Add{0}() {{ txtCount = document.getElementById('{1}');replaceby = txtCount.value;newcount= parseInt(txtCount.value)+1;txtCount.value = newcount;"
                    + replaceby +
                    @"if (replaceby.length==1) {{replaceby = '0'+replaceby;}} todo='{2}'.replace(/█/g,replaceby);"
                    + @"AppendData('{3}', todo);}}", UniqueID, ClientID,
                    RenderedItem, ParentClientID);
            Page.ClientScript.RegisterClientScriptBlock(typeof(SmartRepeater), UniqueID, script, true);
            //ScriptManager.re)
            base.OnLoad(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            #region -- ID --

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Items.Count.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            #endregion

            base.Render(writer);

            //if (Page.Request.Browser.Browser.Contains("Firefox"))
            //{
            //    writer.Write(string.Format("<test id='end{0}'></test>", UniqueID));
            //}
        }
        #endregion

        #region Implementation of IPostBackDataHandler

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string postedId = postCollection[postDataKey];

            int presentValue = Items.Count;
            if (!presentValue.Equals(postedId))
            {
                //if (ViewState["_!ItemCount"]!=null &&  ViewState["_!ItemCount"].ToString() != postedId)
                //{
                ViewState["_!ItemCount"] = Convert.ToInt32(postedId);
                Controls.Clear();
                CreateControlHierarchy(false);
                return true;
            }
            return false;
        }

        public void RaisePostDataChangedEvent()
        {
            //
        }

        #endregion
    }
}