using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.ArticleGroups.Models
{
    public class ArticleGroup
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public virtual ArticleGroup? SuperiorArticleGroup { get; set; }

        public virtual ICollection<ArticleGroup> SubordinateArticleGroups { get; set; } = new List<ArticleGroup>();
    }
}
