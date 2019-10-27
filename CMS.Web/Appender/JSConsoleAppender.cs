using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CMS.Web.Appender
{
    namespace log4net.Appender
    {
        // log4net JSConsoleAppender
        // Writes log strings to client's javascript console if available

        public class JSConsoleAppender : AppenderSkeleton
        {
            // each JavaScript emitted requires a unique id, this counter provides it
            private int m_IDCounter = 0;

            // what to do if no HttpContext is found
            private bool m_ExceptionOnNoHttpContext = true;
            public bool ExceptionOnNoHttpContext
            {
                get { return m_ExceptionOnNoHttpContext; }
                set { m_ExceptionOnNoHttpContext = value; }
            }

            // The meat of the Appender
            override protected void Append(LoggingEvent loggingEvent)
            {
                // optional test for HttpContext, set in config file.
                // default is true
                if (ExceptionOnNoHttpContext == true)
                {
                    if (HttpContext.Current == null)
                    {
                        ErrorHandler.Error(
                          "JSConsoleAppender: No HttpContext to write javascript to.");
                        return;
                    }
                }

                // newlines mess up JavaScript...check for them in the pattern
                PatternLayout Layout = this.Layout as PatternLayout;

                if (Layout.ConversionPattern.Contains("%newline"))
                {
                    ErrorHandler.Error(
                      "JSConsoleAppender: Pattern may not contain %newline.");
                    return;
                }

                // format the Log string
                String LogStr = this.RenderLoggingEvent(loggingEvent);

                // single quotes in the log message will mess up our JavaScript
                LogStr = LogStr.Replace("'", "\\'");

                // Check if console exists before writing to it
                String OutputScript =
                  String.Format("if (window.console) console.log('{0}');", LogStr);

                // This sends the script to the bottom of the page
                Page page = HttpContext.Current.CurrentHandler as Page;
                page.ClientScript.RegisterStartupScript(page.GetType(),
                           m_IDCounter++.ToString(), OutputScript, true);
            }

            // There is no default layout
            override protected bool RequiresLayout
            {
                get { return true; }
            }
        }
    }
}