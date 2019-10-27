using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MKB.TimePicker;

// ToDo:
// Second Bug
// Test clock a little
// Option for read only clock
// Ticking clock
// release
// support seconds
// release
// Read only and Enabled functionality doesn't work
// If no control to the left is selected, select the left hour
// Release
// Make this control databindable
// Release
// Make support military time, 24 hour format
// Release
// add some validators for required fill in, required between time, 
// Release DONE TO HEREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
// Make design time pretty. right now it sucks
// Release
// Give it diff preset looks, figure out how MS did that with gridviews

namespace CMS.ServerControls
{
    /// <summary>
    /// Control that allows you to display a time picker in the same format as the Windows XP clock settings view.    
    /// </summary>
    [ValidationProperty("Date")]
    [DefaultProperty("Text")]
    [ParseChildren(ChildrenAsProperties = true)]
    [ToolboxData("<{0}:TimeSelector runat=server></{0}:TimeSelector>")]
    public class TimeSelector : WebControl, IPostBackEventHandler, IPostBackDataHandler, IScriptControl
    {
        #region AmPmSpec enum

        /// <summary>
        /// Enumerator representing AM/PM values
        /// </summary>
        public enum AmPmSpec
        {
            AM,
            PM
        }

        #endregion

        #region TimeFormat enum

        /// <summary>
        /// Enumerator Representing 12/24 Hour Formats
        /// </summary>
        public enum TimeFormat
        {
            Twelve = 12,
            TwentyFour = 24
        }

        #endregion

        #region TimePartType enum

        /// <summary>
        /// Enumerator representing the 3 adjustable parts of time for this control
        /// </summary>
        public enum TimePartType
        {
            Hour,
            Minute,
            Second
        }

        #endregion

        private ScriptManager _sm;

        #region Properties

        #region Public Properties

        private int _MinuteIncrement = 15;
        private int _SecondIncrement = 1;

        [Category("Appearance"),
         Description("Allows settings for the incremental buttons."),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
         NotifyParentProperty(true),
         PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public ButtonSettings ButtonSettings
        {
            get
            {
                object o = ViewState["ButtonSettings"];
                return (o == null) ? null : (ButtonSettings) o;
            }
            set { ViewState["ButtonSettings"] = value; }
        }

        public override Color BorderColor
        {
            get
            {
                if (base.BackColor.IsEmpty)
                    return ColorTranslator.FromHtml("Silver");
                else
                    return base.BorderColor;
            }
            set { base.BorderColor = value; }
        }

        /// <summary>
        /// Returns the current hour value. 1-12.
        /// </summary>
        [DefaultValue("0")]
        public int Hour
        {
            get
            {
                if ((ViewState["Hour"] != null) && (ViewState["Hour"] != string.Empty))
                    return Convert.ToInt32(ViewState["Hour"]);
                else
                    return 0;
            }
            set { ViewState["Hour"] = value; }
        }

        /// <summary>
        ///  Returns the AM<![CDATA[/]]>PM value
        /// </summary>
        [DefaultValue("PM")]
        public AmPmSpec AmPm
        {
            get
            {
                if (ViewState["AmPm"] != null)
                    return (AmPmSpec) ViewState["AmPm"];
                else
                    return AmPmSpec.PM;
            }
            set { ViewState["AmPm"] = value; }
        }

        /// <summary>
        ///  Gets or sets the 12<![CDATA[/]]>24 time format preference
        /// </summary>
        [DefaultValue("Twelve")]
        public TimeFormat SelectedTimeFormat
        {
            get
            {
                if (ViewState["SelectedTimeFormat"] != null)
                    return (TimeFormat) ViewState["SelectedTimeFormat"];
                else
                    return TimeFormat.Twelve;
            }
            set { ViewState["SelectedTimeFormat"] = value; }
        }

        /// <summary>
        /// Returns the current minute value. 0-59.
        /// </summary>
        [DefaultValue("0")]
        public int Minute
        {
            get
            {
                if ((ViewState["Minute"] != null) && (ViewState["Minute"] != string.Empty))
                    return Convert.ToInt32(ViewState["Minute"]);
                else
                    return 0;
            }
            set { ViewState["Minute"] = value; }
        }

        /// <summary>
        /// Returns the current second value. 0-59.
        /// </summary>
        [DefaultValue("0")]
        public int Second
        {
            get
            {
                if ((ViewState["Second"] != null) && (ViewState["Second"] != string.Empty))
                    return Convert.ToInt32(ViewState["Second"]);
                else
                    return 0;
            }
            set { ViewState["Second"] = value; }
        }

        /// <summary>
        /// DateTime value. Defaults to current day if not set. Otherwise, maintains a value of the date assigned plus time shown.
        /// </summary>
        [Browsable(true)]
        public DateTime Date
        {
            get
            {
                if (ViewState["Date"] == null)
                    return DateTime.MinValue;
                else
                {
                    ViewState["Date"] = Convert.ToDateTime(ViewState["Date"]).ToShortDateString() + " " + Hour + ":" +
                                        Minute + ":" + Second + " " + AmPm;
                    return Convert.ToDateTime(ViewState["Date"]);
                }
            }
            set
            {
                ViewState["Date"] = value;
                SetTime(value);
            }
        }

        /// <summary>
        /// Determines whether or not this control is editable by a user within a browser.
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Determines whether or not this control is editable by a user within a browser.")]
        public bool ReadOnly
        {
            get
            {
                if (ViewState["ReadOnly"] != null)
                    return Convert.ToBoolean(ViewState["ReadOnly"]);
                else
                    return false;
            }
            set { ViewState["ReadOnly"] = value; }
        }

        /// <summary>
        /// Determines whether or not this control displays the seconds slot in the browser.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description(
            "Determines whether or not this control displays the up and down buttons for adjusting time in the browser."
            )]
        public bool DisplayButtons
        {
            get
            {
                if (ViewState["DisplayButtons"] != null)
                    return Convert.ToBoolean(ViewState["DisplayButtons"]);
                else
                    return true;
            }
            set { ViewState["DisplayButtons"] = value; }
        }

        /// <summary>
        /// Determines whether or not this control displays the seconds slot in the browser.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(true)]
        [Description("Determines whether or not this control displays the seconds slot in the browser.")]
        public bool DisplaySeconds
        {
            get
            {
                if (ViewState["DisplaySeconds"] != null)
                    return Convert.ToBoolean(ViewState["DisplaySeconds"]);
                else
                    return true;
            }
            set { ViewState["DisplaySeconds"] = value; }
        }

        ///// <summary>
        ///// Determines the color of the border surrounding the main time box area.
        ///// </summary>
        //[Browsable(true)]
        //[Category("Appearance")]
        //[DefaultValue("Silver")]
        //[Description("Determines the color of the border surrounding the main time box area.")]
        //public string BorderColor
        //{
        //    get
        //    {
        //        if (ViewState["BorderColor"] != null)
        //            return Convert.ToString(ViewState["BorderColor"]);
        //        else
        //            return "Silver";
        //    }
        //    set
        //    {
        //        ViewState["BorderColor"] = value;
        //    }
        //}

        ///// <summary>
        ///// Determines the color of the background of the main time box area.
        ///// </summary>
        //[Browsable(true)]
        //[Category("Appearance")]
        //[DefaultValue("White")]
        //[Description("Determines the color of the background of the main time box area.")]
        //public string BackgroundColor
        //{
        //    get
        //    {
        //        if (ViewState["BackgroundColor"] != null)
        //            return Convert.ToString(ViewState["BackgroundColor"]);
        //        else
        //            return "White";
        //    }
        //    set
        //    {
        //        ViewState["BackgroundColor"] = value;
        //    }
        //}

        /// <summary>
        /// Determines whether or not seconds are a factor in this control. If set to false, the second will always be 00.
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description(
            "Boolean value that determines whether or not seconds are a factor in this control. If set to false, the second will always be 00."
            )]
        public bool AllowSecondEditing
        {
            get
            {
                if (ViewState["AllowSecondEditing"] != null)
                    return Convert.ToBoolean(ViewState["AllowSecondEditing"]);
                else
                    return false;
            }
            set { ViewState["AllowSecondEditing"] = value; }
        }

        /// <summary>
        /// Enables ticking clock-like functionality.
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Boolean indicating whether or not the control acts as a clock in nature rather than a timepicker."
            )]
        public bool EnableClock
        {
            get
            {
                if (ViewState["EnableClock"] != null)
                    return Convert.ToBoolean(ViewState["EnableClock"]);
                else
                    return false;
            }
            set { ViewState["EnableClock"] = value; }
        }

        /// <summary>
        /// Determines how many minutes are added or subtracted from the minute slot when a user clicks the up and down arrows.
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(15)]
        [Description(
            "Determines how many minutes are added or subtracted from the minute slot when a user clicks the up and down arrows."
            )]
        public int MinuteIncrement
        {
            get { return _MinuteIncrement; }
            set { _MinuteIncrement = value; }
        }

        /// <summary>
        /// Determines how many seconds are added or subtracted from the second slot when a user clicks the up and down arrows.
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(1)]
        [Description(
            "Determines how many seconds are added or subtracted from the second slot when a user clicks the up and down arrows."
            )]
        public int SecondIncrement
        {
            get { return _SecondIncrement; }
            set { _SecondIncrement = value; }
        }

        #endregion

        #region Private Properties

        private string AppPath
        {
            get
            {
                return (HttpContext.Current.Request.ApplicationPath.EndsWith("/"))
                           ? HttpContext.Current.Request.ApplicationPath
                           : HttpContext.Current.Request.ApplicationPath + "/";
            }
        }

        #endregion

        #endregion

        #region Lifecycle Methods

        #region Postback & State Maintenance Methods

        #region IPostBackDataHandler Members

        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection values)
        {
            return LoadPostData(postDataKey, values);
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
            RaisePostDataChangedEvent();
        }

        #endregion

        #region IPostBackEventHandler Members

        void IPostBackEventHandler.RaisePostBackEvent(string args)
        {
            RaisePostBackEvent(args);
        }

        #endregion

        public virtual bool LoadPostData(string postDataKey, NameValueCollection values)
        {
            bool hasHourChange = false;
            bool hasMinuteChange = false;
            bool hasSecondChange = false;
            bool hasAmPmChange = false;

            string sHour = values[UniqueID];
            if (sHour != string.Empty)
                if (int.Parse(sHour) != Hour)
                {
                    Hour = int.Parse(sHour);
                    hasHourChange = true;
                }

            string sMinute = values[UniqueID + "_txtMinute"];
            if (sMinute != string.Empty)
                if (int.Parse(sMinute) != Minute)
                {
                    Minute = int.Parse(sMinute);
                    hasMinuteChange = true;
                }

            string sSecond = values[UniqueID + "_txtSecond"];
            if (!String.IsNullOrEmpty(sSecond))
                if (int.Parse(sSecond) != Second)
                {
                    Second = int.Parse(sSecond);
                    hasSecondChange = true;
                }

            AmPmSpec tAmPm = (values[UniqueID + "_txtAmPm"] == "AM") ? AmPmSpec.AM : AmPmSpec.PM;
            if (tAmPm != AmPm)
            {
                AmPm = tAmPm;
                hasAmPmChange = true;
            }

            return hasHourChange || hasAmPmChange || hasMinuteChange || hasSecondChange;
        }

        public virtual void RaisePostDataChangedEvent()
        {
            SetTime(Hour, Minute, Second, AmPm);
        }

        protected virtual void RaisePostBackEvent(string args)
        {
        }

        #endregion

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.AddAttribute("CellSpacing", "0");
            output.AddAttribute("CellPadding", "0");
            output.AddStyleAttribute(HtmlTextWriterStyle.BorderCollapse, "collapse");
            output.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "None");
            output.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
            output.RenderBeginTag(HtmlTextWriterTag.Table);


            output.RenderBeginTag(HtmlTextWriterTag.Tr);


            output.AddStyleAttribute(HtmlTextWriterStyle.VerticalAlign, "Bottom");
            output.RenderBeginTag(HtmlTextWriterTag.Td);

            #region Hour textBox

            // Hour Input TextBox
            output.AddAttribute(HtmlTextWriterAttribute.Id, UniqueID + @"_txtHour");
            output.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
            if (ReadOnly)
            {
                output.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
                output.AddAttribute(HtmlTextWriterAttribute.Onclick, "this.blur();");
            }
            else
            {
                //output.AddAttribute(HtmlTextWriterAttribute.Tabindex, "1");
                output.AddAttribute(HtmlTextWriterAttribute.Onclick, "this.select();lastFocusCtrl=this;");
                output.AddAttribute(HtmlTextWriterAttribute.Onchange,
                                    "intOnly(this," + Convert.ToString((int) SelectedTimeFormat) + ");");
                output.AddAttribute("onFocus", "this.select();lastFocusCtrl=this;");
                output.AddAttribute("onKeyDown",
                                    "return updownArrow(event, this," + Convert.ToString((int) SelectedTimeFormat) + "," +
                                    MinuteIncrement + "," + SecondIncrement + ");");
            }

            //output.AddAttribute(HtmlTextWriterAttribute.Class, "Hour");
            output.AddStyleAttribute(HtmlTextWriterStyle.Height, "15px");
            output.AddStyleAttribute("border-bottom", "solid 1px " + ColorTranslator.ToHtml(BorderColor));
            output.AddStyleAttribute("border-left", "solid 1px " + ColorTranslator.ToHtml(BorderColor));
            output.AddStyleAttribute("border-top", "solid 1px " + ColorTranslator.ToHtml(BorderColor));
            output.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(BackColor));
            output.AddStyleAttribute("border-right", "none");
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "17px");
            output.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "9pt");

            output.AddAttribute(HtmlTextWriterAttribute.Cols, "2");
            output.AddAttribute(HtmlTextWriterAttribute.Maxlength, "2");

            if (Hour > 0)
                output.AddAttribute(HtmlTextWriterAttribute.Value, getTimePartString(Hour, TimePartType.Hour));
            output.RenderBeginTag(HtmlTextWriterTag.Input);
            output.RenderEndTag();

            #endregion

            AddSeperator(output);

            #region Minute TextBox

            // Minute Input TextBox
            output.AddAttribute(HtmlTextWriterAttribute.Id, UniqueID + @"_txtMinute");
            output.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID + "_txtMinute");
            if (ReadOnly)
            {
                output.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
                output.AddAttribute(HtmlTextWriterAttribute.Onclick, "this.blur();");
            }
            else
            {
                //output.AddAttribute(HtmlTextWriterAttribute.Tabindex, "2");
                output.AddAttribute(HtmlTextWriterAttribute.Onclick, "this.select();lastFocusCtrl=this;");
                output.AddAttribute(HtmlTextWriterAttribute.Onchange,
                                    "intOnly(this," + Convert.ToString((int) SelectedTimeFormat) + ");");
                output.AddAttribute("onFocus", "this.select();lastFocusCtrl=this;");
                output.AddAttribute("onKeyDown",
                                    "return updownArrow(event, this," + Convert.ToString((int) SelectedTimeFormat) + "," +
                                    MinuteIncrement + "," + SecondIncrement + ");");
            }
            //output.AddAttribute(HtmlTextWriterAttribute.Class, "Minute");
            output.AddStyleAttribute(HtmlTextWriterStyle.Height, "15px");
            output.AddStyleAttribute("border-bottom", "solid 1px " + ColorTranslator.ToHtml(BorderColor));
            output.AddStyleAttribute("border-left", "none");
            output.AddStyleAttribute("border-top", "solid 1px " + ColorTranslator.ToHtml(BorderColor));
            output.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(BackColor));
            output.AddStyleAttribute("border-right", "none");
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "17px");
            output.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "9pt");

            output.AddAttribute(HtmlTextWriterAttribute.Cols, "2");
            output.AddAttribute(HtmlTextWriterAttribute.Maxlength, "2");
            if (Minute >= 0)
                output.AddAttribute(HtmlTextWriterAttribute.Value, getTimePartString(Minute, TimePartType.Minute));
            output.RenderBeginTag(HtmlTextWriterTag.Input);
            output.RenderEndTag();

            #endregion

            #region Seconds TextBox

            // Second Input TextBox
            if (DisplaySeconds)
            {
                AddSeperator(output);

                output.AddAttribute(HtmlTextWriterAttribute.Id, UniqueID + @"_txtSecond");
                output.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID + "_txtSecond");
                if ((ReadOnly) || (!AllowSecondEditing))
                {
                    output.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
                    output.AddAttribute(HtmlTextWriterAttribute.Onclick, "this.blur();");
                }
                if (AllowSecondEditing)
                {
                    //output.AddAttribute(HtmlTextWriterAttribute.Tabindex, "3");
                    output.AddAttribute(HtmlTextWriterAttribute.Onclick, "this.select();lastFocusCtrl=this;");
                    output.AddAttribute(HtmlTextWriterAttribute.Onchange,
                                        "intOnly(this," + Convert.ToString((int) SelectedTimeFormat) + ");");
                    output.AddAttribute("onFocus", "this.select();lastFocusCtrl=this;");
                    output.AddAttribute("onKeyDown",
                                        "return updownArrow(event, this," + Convert.ToString((int) SelectedTimeFormat) +
                                        "," + MinuteIncrement + "," + SecondIncrement + ");");
                }

                output.AddStyleAttribute(HtmlTextWriterStyle.Height, "15px");
                output.AddStyleAttribute("border-bottom", "solid 1px " + ColorTranslator.ToHtml(BorderColor));
                output.AddStyleAttribute("border-left", "none");
                output.AddStyleAttribute("border-top", "solid 1px " + ColorTranslator.ToHtml(BorderColor));
                output.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(BackColor));
                output.AddStyleAttribute("border-right", "none");
                output.AddStyleAttribute(HtmlTextWriterStyle.Width, "17px");
                output.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "9pt");
                if (SelectedTimeFormat == TimeFormat.Twelve)
                    output.AddStyleAttribute("border-right", "none");
                else
                    output.AddStyleAttribute("border-right", "solid 1px " + ColorTranslator.ToHtml(BorderColor));

                output.AddAttribute(HtmlTextWriterAttribute.Cols, "2");
                output.AddAttribute(HtmlTextWriterAttribute.Maxlength, "2");
                if (AllowSecondEditing) //&&(!LoadZeroSecondsInitially))
                    output.AddAttribute(HtmlTextWriterAttribute.Value, getTimePartString(Second, TimePartType.Second));
                else
                    output.AddAttribute(HtmlTextWriterAttribute.Value, "00");
                output.RenderBeginTag(HtmlTextWriterTag.Input);
                output.RenderEndTag();
            }

            #endregion

            #region AM/PM TextBox

            // AmPm Input TextBox
            if (SelectedTimeFormat != TimeFormat.TwentyFour) // Don't render unless it's 12 hour format
            {
                output.AddAttribute(HtmlTextWriterAttribute.Id, UniqueID + @"_txtAmPm");
                output.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID + "_txtAmPm");
                output.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
                if (ReadOnly)
                    output.AddAttribute(HtmlTextWriterAttribute.Onclick, "this.blur();");
                else
                {
                    //output.AddAttribute(HtmlTextWriterAttribute.Tabindex, "4");
                    output.AddAttribute(HtmlTextWriterAttribute.Onclick, "this.select();lastFocusCtrl=this;");
                    output.AddAttribute(HtmlTextWriterAttribute.Onchange, "keepAmPm();");
                    output.AddAttribute("onFocus", "this.select();lastFocusCtrl=this;");
                    output.AddAttribute("onKeyDown",
                                        "return updownArrow(event, this," + Convert.ToString((int) SelectedTimeFormat) +
                                        "," + MinuteIncrement + "," + SecondIncrement + ");");
                }
                //output.AddAttribute(HtmlTextWriterAttribute.Class, "AmPm");
                output.AddStyleAttribute(HtmlTextWriterStyle.Height, "15px");
                output.AddStyleAttribute("border-bottom", "solid 1px " + ColorTranslator.ToHtml(BorderColor));
                output.AddStyleAttribute("border-left", "none");
                output.AddStyleAttribute("border-right", "solid 1px " + ColorTranslator.ToHtml(BorderColor));
                output.AddStyleAttribute("border-top", "solid 1px " + ColorTranslator.ToHtml(BorderColor));
                output.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(BackColor));
                output.AddStyleAttribute(HtmlTextWriterStyle.Width, "22px");
                output.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "9pt");

                output.AddAttribute(HtmlTextWriterAttribute.Cols, "2");
                output.AddAttribute(HtmlTextWriterAttribute.Maxlength, "2");
                output.AddAttribute(HtmlTextWriterAttribute.Value, AmPm.ToString());
                output.RenderBeginTag(HtmlTextWriterTag.Input);
                output.RenderEndTag();
            }

            output.RenderEndTag(); // End AmPm cell

            #endregion

            #region Up and Down Buttons

            if ((!ReadOnly) && (!EnableClock) && (DisplayButtons))
            {
                //output.AddStyleAttribute(HtmlTextWriterStyle.VerticalAlign, "Bottom");
                output.RenderBeginTag(HtmlTextWriterTag.Td);

                // Up Arrow for Adding Time
                output.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "pointer");
                output.AddAttribute(HtmlTextWriterAttribute.Id, UniqueID + @"_imgUp");

                if (!ReadOnly)
                    output.AddAttribute(HtmlTextWriterAttribute.Onclick,
                                        "addTime(this," + Convert.ToString((int) SelectedTimeFormat) + "," +
                                        MinuteIncrement + "," + SecondIncrement + ");");

                #region Up Button Settings

                if (ButtonSettings == null)
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Src,
                                        Page.ClientScript.GetWebResourceUrl(typeof (TimeSelector),
                                                                            "TimePicker.Images.up.jpg"));

                    output.AddAttribute("onMouseOver", "this.src='" +
                                                       Page.ClientScript.GetWebResourceUrl(typeof (TimeSelector),
                                                                                           "TimePicker.Images.up-over.jpg") +
                                                       "';");
                    output.AddAttribute("onMouseOut", "this.src='" +
                                                      Page.ClientScript.GetWebResourceUrl(typeof (TimeSelector),
                                                                                          "TimePicker.Images.up.jpg") +
                                                      "';");
                }
                else
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Src, ButtonSettings.UpImageURL);

                    if (ButtonSettings.MouseOversActive)
                    {
                        output.AddAttribute("onMouseOver", "this.src='" + ButtonSettings.UpOverImageURL + "';");
                        output.AddAttribute("onMouseOut", "this.src='" + ButtonSettings.UpImageURL + "';");
                    }
                }

                #endregion

                output.RenderBeginTag(HtmlTextWriterTag.Img);
                output.RenderEndTag();


                output.Write("<br />");

                // Down Arrow for Subtracting Time
                output.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "pointer");
                output.AddAttribute(HtmlTextWriterAttribute.Id, UniqueID + @"_imgDown");
                if (!ReadOnly)
                    output.AddAttribute(HtmlTextWriterAttribute.Onclick,
                                        "subtractTime(this," + Convert.ToString((int) SelectedTimeFormat) + "," +
                                        MinuteIncrement + "," + SecondIncrement + ");");

                #region Down Button Settings

                if (ButtonSettings == null)
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Src,
                                        Page.ClientScript.GetWebResourceUrl(typeof (TimeSelector),
                                                                            "TimePicker.Images.down.jpg"));

                    output.AddAttribute("onMouseOver", "this.src='" +
                                                       Page.ClientScript.GetWebResourceUrl(typeof (TimeSelector),
                                                                                           "TimePicker.Images.down-over.jpg") +
                                                       "';");
                    output.AddAttribute("onMouseOut", "this.src='" +
                                                      Page.ClientScript.GetWebResourceUrl(typeof (TimeSelector),
                                                                                          "TimePicker.Images.down.jpg") +
                                                      "';");
                }
                else
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Src, ButtonSettings.DownImageURL);

                    if (ButtonSettings.MouseOversActive)
                    {
                        output.AddAttribute("onMouseOver", "this.src='" + ButtonSettings.DownOverImageURL + "';");
                        output.AddAttribute("onMouseOut", "this.src='" + ButtonSettings.DownImageURL + "';");
                    }
                }

                #endregion

                output.RenderBeginTag(HtmlTextWriterTag.Img);
                output.RenderEndTag();

                output.RenderEndTag(); // End Button Cell
            }

            #endregion

            output.RenderEndTag(); // Row
            output.RenderEndTag(); // Table

            AddJavaScript();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!DesignMode)
                _sm.RegisterScriptDescriptors(this);

            base.Render(writer);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!DesignMode)
            {
                // Make sure ScriptManager exists
                _sm = ScriptManager.GetCurrent(Page);
                if (_sm == null)
                    throw new HttpException("A ScriptManager control must exist on the page.");

                // Register as client control.
                _sm.RegisterScriptControl(this);
            }


            base.OnPreRender(e);

            //            BuildCSS();

            if ((Date == DateTime.MinValue) && (Hour == 0))
            {
                if (SelectedTimeFormat == TimeFormat.Twelve)
                    Date = DateTime.Now;
                else // 24 hour format
                {
                    Date = DateTime.Now;
                    if (Hour < 12)
                        Hour += 12;
                    Date = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " +
                                              (Hour) + ":" + Minute + ":" + Second + " " + AmPm);
                }
            }
            else if ((Hour != 0) && (Date == DateTime.MinValue))
            {
                Date = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " +
                                          Hour + ":" + Minute + ":" + Second + " " + AmPm);
            }

            // Moved to the onload for situations when the control isn't shown to begin with in an updatepanel
            //Page.ClientScript.RegisterClientScriptResource(typeof(MKB.TimePicker.TimeSelector), "TimePicker.js.MKB_TimePicker.js");
        }

        protected override void OnLoad(EventArgs e)
        {
            if (EnableClock)
            {
                ReadOnly = true;
                AllowSecondEditing = true;
            }

            Page.ClientScript.RegisterClientScriptResource(typeof (TimeSelector), "TimePicker.js.MKB_TimePicker.js");

            base.OnLoad(e);
        }

        #endregion

        #region Custom Functions

        private void SetTime(DateTime DateAndTime)
        {
            if (SelectedTimeFormat == TimeFormat.Twelve)
            {
                Hour = Convert.ToInt32(DateAndTime.ToString("hh", DateTimeFormatInfo.InvariantInfo));
                AmPm = (DateAndTime.ToString("tt", DateTimeFormatInfo.InvariantInfo) == "AM")
                           ? AmPmSpec.AM
                           : AmPmSpec.PM;
            }
            else // 24 hour format
            {
                Hour = Convert.ToInt32(DateAndTime.ToString("HH", DateTimeFormatInfo.InvariantInfo));
            }

            Minute = Convert.ToInt32(DateAndTime.ToString("mm", DateTimeFormatInfo.InvariantInfo));
            Second = (AllowSecondEditing)
                         ? Convert.ToInt32(DateAndTime.ToString("ss", DateTimeFormatInfo.InvariantInfo))
                         : 0;

            string sMinute = (Minute.ToString().Length == 1) ? "0" + Minute : Minute.ToString();

            ViewState["Date"] = Convert.ToDateTime(ViewState["Date"]).ToShortDateString() + " " + Hour + ":" + sMinute +
                                ":00 " + AmPm;
        }

        /// <summary>
        /// Preset to an hour and a minute such as 2:30 PM
        /// </summary>
        /// <param name="hour">Integer representing the hour</param>
        /// <param name="minute">Integer representing the minute</param>
        /// <param name="AmOrPm">Enum indicating which part of the day the time is being set</param>
        public void SetTime(int hour, int minute, AmPmSpec AmOrPm)
        {
            SetTime(hour, minute, 0, AmOrPm);
        }

        /// <summary>
        /// Preset to an hour and a minute such as 2:30 PM
        /// </summary>
        /// <param name="hour">Integer representing the hour</param>
        /// <param name="minute">Integer representing the minute</param>
        /// <param name="second">Integer representing the seconds</param>
        /// <param name="AmOrPm">Enum indicating which part of the day the time is being set</param>
        public void SetTime(int hour, int minute, int second, AmPmSpec AmOrPm)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
            AmPm = AmOrPm;

            string DateToUse = (Date.Date.ToShortDateString() == DateTime.Now.ToShortDateString())
                                   ? DateTime.Now.ToShortDateString()
                                   : Date.Date.ToShortDateString();

            Date = Convert.ToDateTime(DateToUse + " " +
                                      Hour + ":" + Minute + ":" + Second + " " + AmOrPm);
        }

        private void AddSeperator(HtmlTextWriter output)
        {
            // Seperator Input TextBox
            output.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
            //output.AddAttribute(HtmlTextWriterAttribute.Class, "Seperator");
            output.AddAttribute(HtmlTextWriterAttribute.Cols, "1");
            output.AddAttribute(HtmlTextWriterAttribute.Maxlength, "1");
            output.AddAttribute(HtmlTextWriterAttribute.Value, ":");

            //output.AddStyleAttribute(HtmlTextWriterStyle.Height, "15px");
            output.AddStyleAttribute(HtmlTextWriterStyle.Height, "15px");
            output.AddStyleAttribute("border-bottom", "solid 1px " + ColorTranslator.ToHtml(BorderColor));
            output.AddStyleAttribute("border-left", "none");
            output.AddStyleAttribute("border-right", "none");
            output.AddStyleAttribute("border-top", "solid 1px " + ColorTranslator.ToHtml(BorderColor));
            output.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(BackColor));
            output.AddStyleAttribute(HtmlTextWriterStyle.Width, "4px");

            output.AddAttribute(HtmlTextWriterAttribute.Onclick, "this.nextSibling.focus();//this.blur();return false;");
            output.AddAttribute("onfocus", "this.nextSibling.focus();//blur();return false;");
            output.RenderBeginTag(HtmlTextWriterTag.Input);
            output.RenderEndTag();
        }

        /// <summary>
        /// Registers and inserts a javascript client script block that handles time changes from the user
        /// </summary>
        protected virtual void AddJavaScript()
        {
            //Page.ClientScript.RegisterClientScriptInclude("TimePicker.js.MKB_TimePicker.js",
            //    Page.ClientScript.GetWebResourceUrl(typeof(MKB.TimePicker.TimeSelector), "TimePicker.js.MKB_TimePicker.js"));


            if (EnableClock)
            {
                Page.ClientScript.RegisterStartupScript(typeof (String), "OnClickEvents",
                                                        @"
                <SCRIPT LANGUAGE=""JavaScript"">
                


                function updateClock ( )    {
                    var selectedTimeFormat = " +
                                                        (int) SelectedTimeFormat +
                                                        @";
                    var currentTime = new Date ( );

                    var currentHours = currentTime.getHours ( );
                    var currentMinutes = currentTime.getMinutes ( );
                    var currentSeconds = currentTime.getSeconds ( );

                    // Convert an hours component of '0' to '12'
                    if(document.getElementById('" +
                                                        UniqueID +
                                                        @"_txtAmPm') != null)
                        document.getElementById('" +
                                                        UniqueID +
                                                        @"_txtAmPm').value = ( currentHours < 12 ) ? 'AM' : 'PM';
                    

                    if(selectedTimeFormat == 12)
                    {
                        currentHours = ( currentHours == 0 ) ? 12 : currentHours;
                        currentHours = ( currentHours > 12 ) ? currentHours - 12 : currentHours;
                    }
                    currentHours = ( currentHours < 10 ? '0' : '' ) + currentHours;

                    // Update the time display
                    if(selectedTimeFormat == 12)
                        document.getElementById('" +
                                                        UniqueID +
                                                        @"_txtHour').value = ( currentHours > 12 ) ? currentHours - 12 : currentHours;
                    else
                        document.getElementById('" +
                                                        UniqueID +
                                                        @"_txtHour').value = currentHours;

                    document.getElementById('" +
                                                        UniqueID +
                                                        @"_txtMinute').value = ( currentMinutes < 10 ? '0' : '' ) + currentMinutes;
                    if(" +
                                                        DisplaySeconds.ToString().ToLower() +
                                                        @")
                        document.getElementById('" +
                                                        UniqueID +
                                                        @"_txtSecond').value = ( currentSeconds < 10 ? '0' : '' ) + currentSeconds;
                }












                </script>");


                //'" + Date.ToString("MMMM") + " " + Date.ToString("dd") + ", " + Date.ToString("yyyy")
                //                      + " " + Date.ToString("HH") + ":" + Date.ToString("mm") + ":" + Date.ToString("ss") + "'" + @");


                Page.ClientScript.RegisterStartupScript(typeof (String), "OnLocalCloclEvents",
                                                        @"
            
                var ie = /MSIE/.test(navigator.userAgent);
                /* =================================================== */
                var addEvent;
                if (document.addEventListener) {
                    addEvent = function(element, type, handler) {
                        element.addEventListener(type, handler, null);
                    };
                } else if (document.attachEvent) {
                    addEvent = function(element, type, handler) {
                        element.attachEvent('on' + type, handler);
                    };
                } else {
                    addEvent = new Function; // not supported
                }



                addEvent(window,'load',startClock);
                function startClock(){
                    updateClock();	
                    setInterval('updateClock()', 1000);
                }
            ",
                                                        true);
            }
        }

        /// <summary>
        /// Builds and inserts a style client script block to style the control
        /// </summary>
        protected virtual void BuildCSS()
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered("TimeSelectorStyling"))
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof (String), "TimeSelectorStyling",
                                                            @"
                <style>
	                .Hour
	                {
		                border-bottom: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @";
		                border-left: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @";
		                border-top: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @";
                        background-color: " +
                                                            ColorTranslator.ToHtml(BackColor) +
                                                            @";
		                border-right:none;

		                width:17px;
		                height:19px;
		                font-size:9pt;
	                }

	                .Minute
	                {
		                border-bottom: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @";
		                border-top: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @";
                        background-color: " +
                                                            ColorTranslator.ToHtml(BackColor) +
                                                            @";
		                border-left: none;
		                border-right:none;

		                width:17px;
		                height:19px;
		                font-size:9pt;
	                }

	                .Second
	                {
		                border-top: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @"; 
		                border-right: none;
		                border-bottom: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @";
                        background-color: " +
                                                            ColorTranslator.ToHtml(BackColor) +
                                                            @";
		                border-left: none; 

		                width:17px;
		                height:19px;
		                font-size:9pt;
	                }

                    .SecondTwentyFour
	                {
		                border-top: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @"; 
		                border-right: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @";
		                border-bottom: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @";
                        background-color: " +
                                                            ColorTranslator.ToHtml(BackColor) +
                                                            @";
		                border-left: none; 

		                width:17px;
		                height:19px;
		                font-size:9pt;
	                }

	                .Seperator
	                {
		                border-bottom: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @";
		                border-top: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @";
                        background-color: " +
                                                            ColorTranslator.ToHtml(BackColor) +
                                                            @";
		                border-left: none;
		                border-right: none;

		                width:4px;
		                height:19px;
		                font-size:9pt;
	                }

	                .AmPm
	                {
		                border-top: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @"; 
		                border-right: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @";
		                border-bottom: solid 1px " +
                                                            ColorTranslator.ToHtml(BorderColor) +
                                                            @";
                        background-color: " +
                                                            ColorTranslator.ToHtml(BackColor) +
                                                            @";
		                border-left: none; 

		                width:22px;
		                height:19px;
		                font-size:9pt;
	                }
                </style>
                ");
            }
        }

        /// <summary>
        /// Gets a string representation of the time with leading 0's in number less than 10
        /// </summary>
        /// <param name="TimePartValue">The hour, minute, or second that you want formatted</param>
        /// <param name="TimePart">A <see cref="TimePartType">TimePart</see> that determines which part of the time TimePartValue is.</param>
        /// <returns></returns>
        protected virtual string getTimePartString(int TimePartValue, TimePartType TimePart)
        {
            string sTemp = TimePartValue.ToString();
            switch (TimePart)
            {
                case TimePartType.Hour:
                    if ((TimePartValue >= 0) && (TimePartValue <= 9))
                        sTemp = "0" + TimePartValue;
                    break;
                case TimePartType.Minute:
                    if ((TimePartValue >= 0) && (TimePartValue <= 9))
                        sTemp = "0" + TimePartValue;
                    break;
                case TimePartType.Second:
                    if ((TimePartValue >= 0) && (TimePartValue <= 9))
                        sTemp = "0" + TimePartValue;
                    break;
            }
            return sTemp;
        }

        #endregion

        /// <summary>
        /// Constructor initializes control as HtmlTextWriterTag.Span
        /// </summary>
        public TimeSelector() : base(HtmlTextWriterTag.Span)
        {
        }

        #region IScriptControl Members

        IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor(
                "MKB.TimePicker.TimeSelector", ClientID);
            descriptor.AddProperty("date", ResolveClientUrl(Date.ToString()));
            descriptor.AddProperty("hour", ResolveClientUrl(Hour.ToString()));
            descriptor.AddProperty("minute", ResolveClientUrl(Minute.ToString()));
            descriptor.AddProperty("second", ResolveClientUrl(Second.ToString()));
            descriptor.AddProperty("ampm", ResolveClientUrl(AmPm.ToString()));

            return new ScriptDescriptor[] {descriptor};
        }

        IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
        {
            ScriptReference reference = new ScriptReference();
            if (Page != null)
                reference.Path = Page.ClientScript.GetWebResourceUrl(typeof (TimeSelector),
                                                                     "TimePicker.js.TimeSelector.js");

            return new ScriptReference[] {reference};
        }

        #endregion
    }
}