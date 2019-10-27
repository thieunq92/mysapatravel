using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MKB.TimePicker
{
    [Serializable]
    public class ButtonSettings
    {
        private bool _ContainsCustomImages = false;
        private bool _ContainsCustomMouseOverImages = false;
        
        private string _UpImageURL;
        private string _UpOverImageURL;
        private string _DownImageURL;
        private string _DownOverImageURL;

        private string ReplacementPath
        {
            get
            {
                string relAppPath = HttpRuntime.AppDomainAppVirtualPath;
                if (!relAppPath.EndsWith("/"))
                    relAppPath += "/";

                return relAppPath;
            }
        }

        /// <summary>
        /// Determines whether or not mouseover images are used for the increment buttons.
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("Determines whether or not mouseover images are used for the increment buttons.")]
        public bool MouseOversActive
        {
            get
            {
                if (!_ContainsCustomImages)
                    return true;
                if ((_ContainsCustomImages) && (_ContainsCustomMouseOverImages))
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Gets or sets the URL of the default image that is displayed for positive time increments.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue("The assembly embedded image.")]
        [Description("Gets or sets the URL of the default image that is displayed for positive time increments.")]
        public string UpImageURL
        {
            get { return _UpImageURL.Replace("~/", ReplacementPath); }
            set
            {
                _UpImageURL = value;
                _ContainsCustomImages = true;
            }
        }

        /// <summary>
        /// Gets or sets the URL of the onmouseover image that is displayed for positive time increments.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue("The assembly embedded image.")]
        [Description("Gets or sets the URL of the onmouseover image that is displayed for positive time increments.")]
        public string UpOverImageURL
        {
            get { return _UpOverImageURL.Replace("~/", ReplacementPath); }
            set
            {
                _UpOverImageURL = value;
                _ContainsCustomMouseOverImages = true;
            }
        }

        /// <summary>
        /// Gets or sets the URL of the default image that is displayed for negative time increments.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue("The assembly embedded image.")]
        [Description("Gets or sets the URL of the default image that is displayed for negative time increments.")]
        public string DownImageURL
        {
            get { return _DownImageURL.Replace("~/", ReplacementPath); }
            set
            {
                _DownImageURL = value;
                _ContainsCustomImages = true;
            }
        }

        /// <summary>
        /// Gets or sets the URL of the onmouseover image that is displayed for negative time increments.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue("The assembly embedded image.")]
        [Description("Gets or sets the URL of the onmouseover image that is displayed for negative time increments.")]
        public string DownOverImageURL
        {
            get { return _DownOverImageURL.Replace("~/", ReplacementPath); }
            set
            {
                _DownOverImageURL = value;
                _ContainsCustomMouseOverImages = true;
            }
        }
    }
}
