using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading;

using log4net;

using CMS.Web.Util;
using CMS.Core.Util;
using CMS.Core.Domain;
using CMS.Core.Service;
using CMS.Core.Search;
using CMS.Web.Components;
using CMS.Core.DataAccess;
using CMS.Core;

namespace CMS.Web.Admin
{
	/// <summary>
	/// Summary description for RebuildIndex.
	/// </summary>
	public class RebuildIndex : CMS.Web.Admin.UI.AdminBasePage
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(RebuildIndex));

		private ISiteStructureDao _siteStructureDao;

		protected System.Web.UI.WebControls.Button btnRebuild;
		protected System.Web.UI.WebControls.Label lblMessage;

		/// <summary>
		/// Constructor.
		/// </summary>
		public RebuildIndex()
		{
			this._siteStructureDao = Container.Resolve<ISiteStructureDao>();
		}
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.Title = "Dựng lại bảng tìm kiếm toàn văn";
			if (! this.IsPostBack)
			{
				this.btnRebuild.Attributes.Add("onclick", "this.disabled='true';document.getElementById('pleasewait').style.display = 'block';" + GetPostBackEventReference(btnRebuild).ToString());
				// Make sure all modules are loaded when we enter this page. We can't have a changing
				// module configuration while rebuilding the full-text index.
				EnsureModulesAreLoaded();
			}
			else
			{
				BuildIndex();
				this.lblMessage.Visible = true;
			}
		}

		private void EnsureModulesAreLoaded()
		{
			IList currentModuleTypes = this._siteStructureDao.GetAllModuleTypesInUse();
			foreach (ModuleType moduleType in currentModuleTypes)
			{
				// Just load every module. Just by loading it once, we can be sure that we won't
				// run into strange surprises after pushing the 'Rebuild Index' button becasue
				// some module weren't loaded or configured.
				ModuleBase module = base.ModuleLoader.GetModuleFromType(moduleType);
			}
		}

		private void BuildIndex()
		{
			string indexDir = Context.Server.MapPath(Config.GetConfiguration()["SearchIndexDir"]);
			IndexBuilder ib = new IndexBuilder(indexDir, true);
			IList sites = base.CoreRepository.GetAll(typeof(Site));
			foreach (Site site in sites)
			{
				foreach (Node node in site.RootNodes)
				{
					try
					{
						BuildIndexByNode(node, ib);
					}
					catch (Exception ex)
					{
						log.Error(String.Format("Dựng bảng nội dung cho nút {0} - {1} thất bại.", node.Id, node.Title), ex);
					}
				}
			}
			ib.Close();
		}

		private void BuildIndexByNode(Node node, IndexBuilder ib)
		{
			foreach (Section section in node.Sections)
			{
				ModuleBase module = null;
				try
				{
					module = base.ModuleLoader.GetModuleFromSection(section);
				}
				catch (Exception ex)
				{
					log.Error(String.Format("Không thể khởi động phân hệ cho vùng phân hệ {0} - {1}.", section.Id, section.Title), ex);
				}

				if (module is ISearchable)
				{
					ISearchable searchableModule = (ISearchable)module;
					try
					{
						foreach (SearchContent sc in searchableModule.GetAllSearchableContent())
						{
							ib.AddContent(sc);
						}
					}
					catch (Exception ex)
					{
						log.Error(String.Format("Dựng bảng nội dung cho vùng {0} - {1} thất bại.", section.Id, section.Title), ex);
					}
				}
			}
			foreach (Node childNode in node.ChildNodes)
			{
				BuildIndexByNode(childNode, ib);
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
