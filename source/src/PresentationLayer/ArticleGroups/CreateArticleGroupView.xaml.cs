using BusinessLayer.ArticleGroups.ViewModels;
using BusinessLayer.Articles.ViewModels;
using DataLayer.ArticleGroups.Models;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.ArticleGroups
{
    public partial class CreateArticleGroupView : UserControl
    {
        public CreateArticleGroupView()
        {
            InitializeComponent();
        }

        private void ArticleGroupTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CreateArticleGroupViewModel viewModel = (CreateArticleGroupViewModel)DataContext;

            ArticleGroup? articleGroup = ArticleGroupTreeView.SelectedItem as ArticleGroup;

            if (articleGroup == null)
                return;

            viewModel.ArticleGroup = articleGroup;
        }
    }
}