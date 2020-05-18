using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizLaboratory.Data.Models
{
    /// <summary>
    /// The question data model
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Question"/> class
        /// </summary>
        public Question()
        {

        }

        #region Properties

        /// <summary>
        /// Gets or sets the id
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the quiz id
        /// </summary>
        [Required]
        public int QuizId { get; set; }

        /// <summary>
        /// Gets or sets the text
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the notes
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        [DefaultValue(0)]
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the flags
        /// </summary>
        [DefaultValue(0)]
        public int Flags { get; set; }

        /// <summary>
        /// Gets or sets the created date
        /// </summary>
        [Required]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last modified date
        /// </summary>
        [Required]
        public DateTime LastModifiedDate { get; set; }

        #endregion Properties

        #region Lazy-Load Properties

        /// <summary>
        /// Gets or sets the parent quiz
        /// </summary>
        [ForeignKey("QuizId")]
        public virtual Quiz Quiz { get; set; }

        /// <summary>
        /// Gets or sets the answers related to this question
        /// </summary>
        public virtual List<Answer> Answers { get; set; }

        #endregion Lazy-Load Properties
    }
}
