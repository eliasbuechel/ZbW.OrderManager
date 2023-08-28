using BusinessLayer.Articles.ViewModels;
using DataLayer.ArticleGroups.Models;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Article
{
    public partial class EditArticleView : UserControl
    {
        public EditArticleView()
        {
            InitializeComponent();
        }

        private void ArticleGroupTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            EditArticleViewModel viewModel = (EditArticleViewModel)DataContext;

            ArticleGroup? articleGroup = ArticleGroupTreeView.SelectedItem as ArticleGroup;

            if (articleGroup == null)
                return;

            viewModel.ArticleGroup = articleGroup;
        }
    }
}
