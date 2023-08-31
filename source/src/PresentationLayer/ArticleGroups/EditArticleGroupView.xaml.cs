using BusinessLayer.ArticleGroups.ViewModels;
using DataLayer.ArticleGroups.DTOs;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.ArticleGroups
{
    public partial class EditArticleGroupView : UserControl
    {
        public EditArticleGroupView()
        {
            InitializeComponent();
        }

        private void ArticleGroupTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            EditArticleGroupViewModel viewModel = (EditArticleGroupViewModel)DataContext;

            if (ArticleGroupTreeView.SelectedItem is not ArticleGroupDTO articleGroup)
                return;

            viewModel.SuperiorArticleGroup = articleGroup;
        }
    }
}
