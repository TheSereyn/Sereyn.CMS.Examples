using Microsoft.AspNetCore.Components;
using Sereyn.CMS.Catalogues;
using Sereyn.CMS.Catalogues.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sereyn.CMS.Examples.Blog.Pages
{
    public partial class Home
    {
        #region Members

        private List<Article> _articles = new List<Article>();

        #endregion

        #region Overrides

        protected override void OnInitialized()
        {
            GetArticlesContent();
            _NavigationManager.LocationChanged += _NavigationManager_LocationChanged;
        }

        private void _NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            GetArticlesContent();
        }

        #endregion

        #region Methods

        private async void GetArticlesContent()
        {
            Catalogue<Article> articlesCatalogue = await _CatalogueManager.GetCatalogueAsync<Article>();

            if (!string.IsNullOrEmpty(Category))
            {
                _articles = articlesCatalogue.Items.Where(x => x.Category.Contains(Category)).OrderByDescending(x => x.Created).ToList();
            }
            else
            {
                _articles = articlesCatalogue.Items.OrderByDescending(x => x.Created).ToList();
            }

            StateHasChanged();
        }

        #endregion

        #region Properties
        [Parameter] public string Category { get; set; } = "";
        [Inject] private ICatalogueManager _CatalogueManager { get; set; }
        [Inject] private NavigationManager _NavigationManager { get; set; }

        #endregion
    }
}
