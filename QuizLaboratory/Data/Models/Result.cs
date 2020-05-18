using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizLaboratory.Data.Models
{
    /// <summary>
    /// The result data model
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Result"/> class
        /// </summary>
        public Result()
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
        /// Gets or sets the min value
        /// </summary>
        public int? MinValue { get; set; }

        /// <summary>
        /// Gets or sets the max value
        /// </summary>
        public int? MaxValue { get; set; }

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

        #endregion Lazy-Load Properties
    }
}
