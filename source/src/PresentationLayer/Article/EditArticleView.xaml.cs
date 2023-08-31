using BusinessLayer.Articles.ViewModels;
using DataLayer.ArticleGroups.DTOs;
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

            ArticleGroupDTO? articleGroup = ArticleGroupTreeView.SelectedItem as ArticleGroupDTO;

            if (articleGroup == null)
                return;

            viewModel.ArticleGroup = articleGroup;
        }
    }
}
