using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.ArticleGroups.DTOs
{
    public class ArticleGroupDTO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ArticleGroupDTO? SuperiorArticleGroup { get; set; }

        public virtual ICollection<ArticleGroupDTO> SubordinateArticleGroups { get; set; } = new List<ArticleGroupDTO>();
    }
}
