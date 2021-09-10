using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Models
{
    public class CommentModel
    {
        [Column("CommentId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User name is a required field.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Coment is a required field.")]
        public string Text { get; set; }

        public List<AnswerToCommentModel> AnswerToCommentModels { get; set; }

        [Required(ErrorMessage = "Data is a required field.")]
        public DateTime Date { get; set; }
    }
}
