using BusinessLayer.ArticleGroups.ViewModels;
using DataLayer.ArticleGroups.DTOs;
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

            if (ArticleGroupTreeView.SelectedItem is not ArticleGroupDTO articleGroup)
                return;

            viewModel.SuperiorArticleGroup = articleGroup;
        }
    }
}