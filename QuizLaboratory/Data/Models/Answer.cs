using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizLaboratory.Data.Models
{
    /// <summary>
    /// The answer data model
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Answer"/> class
        /// </summary>
        public Answer()
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
        /// Gets or sets the question id
        /// </summary>
        [Required]
        public int QuestionId { get; set; }

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
        /// Gets or sets the value
        /// </summary>
        [Required]
        public int Value { get; set; }

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
        /// Gets or sets the parent question
        /// </summary>
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        #endregion Lazy-Load Properties
    }
}
