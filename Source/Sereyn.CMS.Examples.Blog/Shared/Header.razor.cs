using Microsoft.AspNetCore.Components;
using Sereyn.CMS.Catalogues;
using Sereyn.CMS.Catalogues.Models;
using Sereyn.CMS.Examples.Blog.Models;

namespace Sereyn.CMS.Examples.Blog.Shared
{
    public partial class Header
    {
        #region Members
        private NavItems _navItems = new NavItems();
        #endregion

        #region Override Methods
        protected override void OnInitialized()
        {
            GenerateNav();
            _NavigationManager.LocationChanged += _NavigationManager_LocationChanged;
        }

        private void _NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            _navItems.SetItemActive(
                _NavigationManager.Uri.Replace(_NavigationManager.BaseUri, "")
                );
            
            StateHasChanged();
        }
        #endregion

        #region Methods
        private async void GenerateNav()
        {
            Catalogue<ArticleCategory> categoryCatalogue = await _CatalogueManager.GetCatalogueAsync<ArticleCategory>();

            foreach (var item in categoryCatalogue.Items)
            {
                _navItems.Items.Add(new NavItem
                {
                    Name = item.Name,
                    Route = string.Format("Category/{0}", item.Route)
                });
            }

            StateHasChanged();
        }
        #endregion

        #region Properties
        [Inject] private ICatalogueManager _CatalogueManager { get; set; }
        [Inject] private NavigationManager _NavigationManager { get; set; }
        #endregion
    }
}
