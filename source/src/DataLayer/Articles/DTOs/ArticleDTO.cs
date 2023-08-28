using DataLayer.ArticleGroups.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Articles.DTOs
{
    public class ArticleDTO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ArticleGroupDTO ArticleGroup { get; set; }
    }
}
