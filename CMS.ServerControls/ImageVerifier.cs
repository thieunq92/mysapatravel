using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.ServerControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ImageVerifier runat=server></{0}:ImageVerifier>")]
    public class ImageVerifier : WebControl, IHttpHandler
    {
        private string _uniqueID = string.Empty;

        // Control này sẽ render một thẻ img
        public ImageVerifier()
            : base(HtmlTextWriterTag.Img)
        {
        }

        /// <summary>
        /// ID của ảnh, dùng để lưu giá trị chuỗi
        /// </summary>
        private string ImageUniqueID
        {
            get
            {
                if (_uniqueID == string.Empty) _uniqueID = Guid.NewGuid().ToString();
                return _uniqueID;
            }
        }

        /// <summary>
        /// Giá trị chuỗi của ảnh, được lưu trong cache của server
        /// </summary>
        public string Text
        {
            get { return string.Format("{0}", HttpContext.Current.Cache[ImageUniqueID]); }
        }

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// Xử lý request, dùng để tạo ảnh
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            Bitmap bmp = new Bitmap(180, 40);
            Graphics g = Graphics.FromImage(bmp);

            // Tạo ra một chuỗi ngẫu nhiên
            string randString = GetRandomText();
            string myUID = context.Request["uid"];
            if (context.Cache[myUID] == null)
            {
                // Nếu chưa cache theo ID này, tạo một biến cache mới, thời gian lưu là 5 phút
                context.Cache.Add(myUID, randString, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10),
                                  CacheItemPriority.AboveNormal, null);
            }
            else
            {
                // Nếu đã có, đơn giản chỉ lưu chuỗi mới
                context.Cache[myUID] = randString;
            }

            g.FillRectangle(Brushes.WhiteSmoke, 0, 0, 180, 40);
            // Cấu hình graphics
            g.SmoothingMode = SmoothingMode.Default;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            Random rand = new Random();
            for (int i = 0; i < randString.Length; i++)
            {
                // Tạo font chữ, chữ nghiêng, in đậm hoặc không in đậm ngẫu nhiên
                Font drawFont = new Font("Arial", 18,
                                         FontStyle.Italic | (rand.Next()%2 == 0 ? FontStyle.Bold : FontStyle.Regular));
                // Vẽ các ký tự với độ cao y ngẫu nhiên nhưng tọa độ x không đổi
                g.DrawString(randString.Substring(i, 1), drawFont, Brushes.Black, i*35 + 10, rand.Next()%12);
            }

            // Số vòng tròn làm "nhiễu" hình
            int circle = rand.Next(5, 20);

            // Lấy ngẫu nhiên các điểm, sau đó vẽ một hình elip với các thông số ngẫu nhiên
            Point[] pt = new Point[circle];
            for (int i = 0; i < circle; i++)
            {
                pt[i] = new Point(rand.Next()%180, rand.Next()%35);
                g.DrawEllipse(Pens.LightSteelBlue, pt[i].X, pt[i].Y, rand.Next()%30 + 1, rand.Next()%30 + 1);
            }
            context.Response.Clear();
            context.Response.ClearHeaders();
            context.Response.ContentType = "image/jpeg";
            // Trả về hình ảnh đã tạo dưới dạng Jpeg
            bmp.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            context.Response.End();
        }

        #endregion

        private static string GetRandomText()
        {
            string uniqueID = Guid.NewGuid().ToString();
            string randString = "";
            for (int i = 0, j = 0; i < uniqueID.Length && j < 5; i++)
            {
                char l_ch = uniqueID.ToCharArray()[i];
                if ((l_ch >= 'A' && l_ch <= 'Z') || (l_ch >= 'a' && l_ch <= 'z') || (l_ch >= '0' && l_ch <= '9'))
                {
                    randString += l_ch;
                    j++;
                }
            }
            return randString;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }

        protected override void LoadControlState(object savedState)
        {
            Pair p = savedState as Pair;
            if (p != null)
            {
                _uniqueID = p.Second as string;
            }
        }

        protected override object SaveControlState()
        {
            Object baseState = base.SaveControlState();
            Pair prState = new Pair(baseState, ImageUniqueID);
            return prState;
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Src, "ImageVerifier.axd?uid=" + ImageUniqueID);
            base.Render(output);
            output.Write("<script language='javascript'>");
            output.Write("function RefreshImageVerifier(id,srcname)");
            output.Write("{ var elm = document.getElementById(id);");
            output.Write("  var dt = new Date();");
            output.Write("  elm.src=srcname + '&ts=' + dt;");
            output.Write("  return false;");
            output.Write("}</script>");
            output.Write("&nbsp;<a href='#' onclick=\"return RefreshImageVerifier('" + ClientID +
                         "','ImageVerifier.axd?&uid=" + ImageUniqueID + "');\">Refresh</a>");
        }

        public void Refresh()
        {
            _uniqueID = Guid.NewGuid().ToString();
        }
    }
}