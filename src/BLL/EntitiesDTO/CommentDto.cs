using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.EntitiesDTO
{
    public class CommentDto
    {
        [Column("CommentDtoId")]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
