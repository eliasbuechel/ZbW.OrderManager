using DataLayer.ArticleGroups.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Articles.Models
{
    public class Article
    {
        public Article()
        {
            Name = string.Empty;
            ArticleGroup = new ArticleGroup();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ArticleGroup ArticleGroup { get; set; }
    }
}
