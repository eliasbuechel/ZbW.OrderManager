using BusinessLayer.Articles.ViewModels;
using DataLayer.ArticleGroups.DTOs;
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

            ArticleGroupDTO? articleGroup = ArticleGroupTreeView.SelectedItem as ArticleGroupDTO;

            if (articleGroup == null)
                return;

            viewModel.ArticleGroup = articleGroup;
        }
    }
}
