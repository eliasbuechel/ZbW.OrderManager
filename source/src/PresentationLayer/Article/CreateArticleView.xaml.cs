using BusinessLayer.Articles.ViewModels;
using DataLayer.ArticleGroups.Models;
using System.Windows.Controls;

namespace PresentationLayer.Article
{
    public partial class CreateArticleView : UserControl
    {
        public CreateArticleView()
        {
            InitializeComponent();
        }

        private void ArticleGroupTreeView_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            CreateArticleViewModel viewModel = (CreateArticleViewModel)DataContext;

            ArticleGroup? articleGroup = ArticleGroupTreeView.SelectedItem as ArticleGroup;

            if (articleGroup == null)
                return;

            viewModel.ArticleGroup = articleGroup;
        }
    }
}
