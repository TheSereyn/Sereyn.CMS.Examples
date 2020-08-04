using Microsoft.AspNetCore.Components;
using Sereyn.CMS.Catalogues;
using Sereyn.CMS.Catalogues.Models;
using Sereyn.CMS.Contents;
using Sereyn.CMS.Contents.Models;
using System.Linq;

namespace Sereyn.CMS.Examples.Blog.Pages
{
    public partial class ArticleTemplate
    {
        #region Members

        private Article _articleMetadata;
        private Content _content;

        #endregion

        protected override void OnInitialized()
        {
            GetArticle();
        }

        #region Methods

        private async void GetArticle()
        {
            Catalogue<Article> catalogue = await _CatalogueManager.GetCatalogueAsync<Article>();

            _articleMetadata = catalogue.Items.Where(x => x.Route.Contains(_FullRoute)).FirstOrDefault();

            _content = await _ContentManager.GetContentAsync(
                _articleMetadata.File);

            StateHasChanged();
        }

        private MarkupString ContentHtml()
        {
            return new MarkupString(
                _content.HtmlMarkup
                );
        }

        #endregion

        #region Properties

        [Parameter] public string Category { get; set; }
        [Parameter] public string SubCategory { get; set; }
        [Parameter] public string Article { get; set; }

        private string _FullRoute
        {
            get
            {
                string route = "";

                if (!string.IsNullOrEmpty(Category))
                    route += Category + "/";
                if (!string.IsNullOrEmpty(SubCategory))
                    route += SubCategory + "/";
                if (!string.IsNullOrEmpty(Article))
                    route += Article;

                return route;
            }
        }

        [Inject] private IContentManager _ContentManager { get; set; }
        [Inject] private ICatalogueManager _CatalogueManager { get; set; }

        #endregion
    }
}
