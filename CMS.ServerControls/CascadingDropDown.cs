using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.ServerControls
{
    public class CascadingDropDown : DropDownList
    {
        #region -- NEW FIELDS --
        protected string dataParentField;
        protected string parentClientID;
        protected string parentControlID;
        #endregion

        #region -- NEW PROPERTIES --
        public string DataParentField
        {
            get { return dataParentField; }
            set { dataParentField = value; }
        }

        public string ParentControlID
        {
            get { return parentControlID; }
            set { parentControlID = value; }
        }

        public string ParentClientID
        {
            get
            {
                if (string.IsNullOrEmpty(parentClientID))
                {
                    if (!string.IsNullOrEmpty(parentControlID))
                    {
                        ListControl parent = Page.FindControl(parentControlID) as ListControl;
                        if (parent != null)
                        {
                            return parent.ClientID;
                        }
                    }
                    return string.Empty;
                }
                return parentClientID;
            }
            set { parentClientID = value; }
        }
        #endregion

        #region -- OVERRIDE --

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (string.IsNullOrEmpty(ParentClientID))
            {
                return;
            }
            const string script =
                @"
function applyCascadingDropdown(sourceId, targetId) {
	var source = document.getElementById(sourceId);
	var target = document.getElementById(targetId);
	if (source && target) {
		addEvent(source, ""change"", function() {
            displayOptionItemsByClass(target, source.value);
            if (target.onchange)
            {
            target.onchange();
            }
		})
		displayOptionItemsByClass(target, source.value);
	}
}

function addEvent(obj, evType, fn){ 
 if (obj.addEventListener){ 
   obj.addEventListener(evType, fn, false); 
   return true; 
 } else if (obj.attachEvent){ 
   var r = obj.attachEvent(""on""+evType, fn); 
   return r; 
 } else { 
   return false; 
 } 
}


function displayOptionItemsByClass(selectElement, className) {
	if (!selectElement.backup) {
		selectElement.backup = selectElement.cloneNode(true);
	}
	var options = selectElement.getElementsByTagName(""option"");
	for(var i=0, length=options.length; i<length; i++) {
		selectElement.removeChild(options[0]);
	}
	var options = selectElement.backup.getElementsByTagName(""option"");
	for(var i=0, length=options.length; i<length; i++) {
		if (options[i].className==className || options[i].className=="""")
			selectElement.appendChild(options[i].cloneNode(true));
	}
}";
            Page.ClientScript.RegisterClientScriptBlock(typeof(CascadingDropDown), "applyCascadingDropDown", script, true);
            string startup = string.Format(@"applyCascadingDropdown(""{0}"", ""{1}"");", ParentClientID, ClientID);
            Page.ClientScript.RegisterStartupScript(typeof(CascadingDropDown), ClientID, startup, true);
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            if (DataSource != null)
            {
                string dataValueField = DataValueField;
                IEnumerable dataSource = (IEnumerable)DataSource;
                foreach (object obj2 in dataSource)
                {
                    string value = DataBinder.GetPropertyValue(obj2, dataValueField, null);
                    Items.FindByValue(value).Attributes.Add("class", DataBinder.GetPropertyValue(obj2, dataParentField, null));
                }
            }
        }

        #endregion
    }
}