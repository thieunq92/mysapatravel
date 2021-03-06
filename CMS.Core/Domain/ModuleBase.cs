using System;
using System.Collections;
using Castle.Core;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;

namespace CMS.Core.Domain
{
    /// <summary>
    /// The base class for all Cuyahoga modules. 
    /// </summary>
    [Transient]
    public abstract class ModuleBase
    {
        private string _displayTitle;
        private string[] _moduleParams;
        private string _modulePathInfo;
        private Section _section;
        private string _sectionUrl;
        private bool _sessionFactoryRebuilt;
        private ISessionManager _sessionManager;

        /// <summary>
        /// The NHibernate session from the current ASP.NET context.
        /// </summary>
        protected ISession NHSession
        {
            get
            {
                if (_sessionManager != null)
                {
                    return _sessionManager.OpenSession();
                }
                else
                {
                    throw new NullReferenceException(
                        "Unable to obtain an NHibernate session because the session manager is null.");
                }
            }
        }

        /// <summary>
        /// The cache key used for output caching.
        /// </summary>
        public virtual string CacheKey
        {
            get
            {
                if (_section != null)
                {
                    string cacheKey = "M_" + _section.Id;
                    if (_modulePathInfo != null)
                    {
                        cacheKey += "_" + _modulePathInfo;
                    }
                    return cacheKey;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Flag that indicates if the SessionFactory is rebuilt. TODO: can't we handle this more elegantly?
        /// </summary>
        public bool SessionFactoryRebuilt
        {
            get { return _sessionFactoryRebuilt; }
            set { _sessionFactoryRebuilt = value; }
        }

        /// <summary>
        /// Property ModulePathInfo (string)
        /// </summary>
        public string ModulePathInfo
        {
            get { return _modulePathInfo; }
            set
            {
                _modulePathInfo = value;
                ParsePathInfo();
            }
        }

        /// <summary>
        /// Property ModuleParams (string[])
        /// </summary>
        public string[] ModuleParams
        {
            get { return _moduleParams; }
        }

        /// <summary>
        /// Property Section (Section)
        /// </summary>
        public Section Section
        {
            get { return _section; }
            set { _section = value; }
        }

        /// <summary>
        /// The base url for this module. 
        /// </summary>
        public string SectionUrl
        {
            get { return _sectionUrl; }
            set { _sectionUrl = value; }
        }

        /// <summary>
        /// The path of default view user control from the application root.
        /// </summary>
        public string DefaultViewControlPath
        {
            get { return _section.ModuleType.Path; }
        }

        /// <summary>
        /// Danh sách các quyền mà module này có
        /// </summary>
        public IList Permissions
        {
            get { return _section.ModuleType.ModulePermissions; }
        }

        /// <summary>
        /// Override this property when a different view should be active based on some action.
        /// </summary>
        public virtual string CurrentViewControlPath
        {
            get { return DefaultViewControlPath; }
        }

        /// <summary>
        /// Property DisplayTitle (string)
        /// </summary>
        public string DisplayTitle
        {
            get
            {
                if (_displayTitle != null)
                {
                    return _displayTitle;
                }
                else if (Section != null)
                {
                    return Section.Title;
                }
                else
                {
                    return String.Empty;
                }
            }
            set { _displayTitle = value; }
        }

        /// <summary>
        /// Sets the NHibernate session manager for the module (injected).
        /// </summary>
        public ISessionManager SessionManager
        {
            set { _sessionManager = value; }
        }

        /// <summary>
        /// Override this method if you module needs module-specific pathinfo parsing.
        /// </summary>
        protected virtual void ParsePathInfo()
        {
            // Don't do anything special, just split the PathInfo params.
            if (_modulePathInfo != null)
            {
                string pathInfoParamsAsString;
                if (_modulePathInfo.StartsWith("/"))
                {
                    pathInfoParamsAsString = _modulePathInfo.Substring(1);
                }
                else
                {
                    pathInfoParamsAsString = _modulePathInfo;
                }

                _moduleParams = pathInfoParamsAsString.Split(new char[] {'/'});
            }
        }

        /// <summary>
        /// Delete the ModuleContent in the module. Leave it up to the concrete Module how to do that.
        /// This method will likely be called before deleting a Section.
        /// </summary>
        public virtual void DeleteModuleContent()
        {
            // Do nothing here
            return;
        }

        /// <summary>
        /// Configure the module by reading the settings of the section.
        /// </summary>
        public virtual void ReadSectionSettings()
        {
            if (_section == null)
            {
                throw new NullReferenceException("Can't access the section for settings.");
            }
        }

        /// <summary>
        /// Check if the module contains no invalid settings. Override this method in concrete module controllers.
        /// </summary>
        public virtual void ValidateSectionSettings()
        {
            // Do nothing here
            return;
        }

        public virtual void SaveGlobalSettings(object pageEngine)
        {
            // Do nothing here
            return;
        }
    }
}