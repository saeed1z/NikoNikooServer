
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using OnlineServices.Utilities.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;

namespace OnlineServices.Models
{
    public partial class PagerModel
    {
        #region Constructors

        public PagerModel()
        {

        }
        #endregion Constructors

        #region Fields

        private int individualPagesDisplayedCount;
        private int pageIndex = -2;
        private int pageSize;

        private bool? showFirst;
        private bool? showIndividualPages;
        private bool? showLast;
        private bool? showNext;
        private bool? showPagerItems;
        private bool? showPrevious;
        private bool? showTotalSummary;


        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the current page index
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return (this.PageIndex + 1);
            }
        }

        /// <summary>
        /// Gets or sets a count of individual pages to be displayed
        /// </summary>
        public int IndividualPagesDisplayedCount
        {
            get
            {
                if (individualPagesDisplayedCount <= 0)
                    return 5;

                return individualPagesDisplayedCount;
            }
            set
            {
                individualPagesDisplayedCount = value;
            }
        }

        /// <summary>
        /// Gets the current page index
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (this.pageIndex < 0)
                {
                    return 0;
                }
                return this.pageIndex;
            }
            set
            {
                this.pageIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets a page size
        /// </summary>
        public int PageSize
        {
            get
            {
                return (pageSize <= 0) ? 10 : pageSize;
            }
            set
            {
                pageSize = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show "first"
        /// </summary>
        public bool ShowFirst
        {
            get
            {
                return showFirst ?? true;
            }
            set
            {
                showFirst = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show "individual pages"
        /// </summary>
        public bool ShowIndividualPages
        {
            get
            {
                return showIndividualPages ?? true;
            }
            set
            {
                showIndividualPages = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show "last"
        /// </summary>
        public bool ShowLast
        {
            get
            {
                return showLast ?? true;
            }
            set
            {
                showLast = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show "next"
        /// </summary>
        public bool ShowNext
        {
            get
            {
                return showNext ?? true;
            }
            set
            {
                showNext = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show pager items
        /// </summary>
        public bool ShowPagerItems
        {
            get
            {
                return showPagerItems ?? true;
            }
            set
            {
                showPagerItems = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show "previous"
        /// </summary>
        public bool ShowPrevious
        {
            get
            {
                return showPrevious ?? true;
            }
            set
            {
                showPrevious = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show "total summary"
        /// </summary>
        public bool ShowTotalSummary
        {
            get
            {
                return showTotalSummary ?? false;
            }
            set
            {
                showTotalSummary = value;
            }
        }

        /// <summary>
        /// Gets a total pages count
        /// </summary>
        public int TotalPages
        {
            get
            {
                if ((this.TotalRecords == 0) || (this.PageSize == 0))
                {
                    return 0;
                }
                int num = this.TotalRecords / this.PageSize;
                if ((this.TotalRecords % this.PageSize) > 0)
                {
                    num++;
                }
                return num;
            }
        }

        /// <summary>
        /// Gets or sets a total records count
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets the first button text
        /// </summary>


        /// <summary>
        /// Gets or sets the route name or action name
        /// </summary>
        public string RouteActionName { get; set; }

        /// <summary>
        /// Gets or sets whether the links are created using RouteLink instead of Action Link 
        /// (for additional route values such as slugs or page numbers)
        /// </summary>
        public bool UseRouteLinks { get; set; }

        /// <summary>
        /// Gets or sets the RouteValues object. Allows for custom route values other than page.
        /// </summary>
        public IRouteValues RouteValues { get; set; }

        #endregion Properties

        #region Methods

        
        /// <summary>
        /// Gets first individual page index
        /// </summary>
        /// <returns>Page index</returns>
        public int GetFirstIndividualPageIndex()
        {
            if ((this.TotalPages < this.IndividualPagesDisplayedCount) ||
                ((this.PageIndex - (this.IndividualPagesDisplayedCount / 2)) < 0))
            {
                return 0;
            }
            if ((this.PageIndex + (this.IndividualPagesDisplayedCount / 2)) >= this.TotalPages)
            {
                return (this.TotalPages - this.IndividualPagesDisplayedCount);
            }
            return (this.PageIndex - (this.IndividualPagesDisplayedCount / 2));
        }

        /// <summary>
        /// Get last individual page index
        /// </summary>
        /// <returns>Page index</returns>
        public int GetLastIndividualPageIndex()
        {
            int num = this.IndividualPagesDisplayedCount / 2;
            if ((this.IndividualPagesDisplayedCount % 2) == 0)
            {
                num--;
            }
            if ((this.TotalPages < this.IndividualPagesDisplayedCount) ||
                ((this.PageIndex + num) >= this.TotalPages))
            {
                return (this.TotalPages - 1);
            }
            if ((this.PageIndex - (this.IndividualPagesDisplayedCount / 2)) < 0)
            {
                return (this.IndividualPagesDisplayedCount - 1);
            }
            return (this.PageIndex + num);
        }

        #endregion Methods
    }

    #region Classes

    /// <summary>
    /// Interface for custom RouteValues objects
    /// </summary>
    public interface IRouteValues
    {
        int page { get; set; }
    }

    /// <summary>
    /// Class that has a slug and page for route values. Used for Topic (posts) and 
    /// Forum (topics) pagination
    /// </summary>
    public partial class RouteValues : IRouteValues
    {
        public int page { get; set; }
    }

    #endregion Classes
}