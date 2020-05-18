using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizLaboratory.Data.Models
{
    /// <summary>
    /// The quiz data model
    /// </summary>
    public class Quiz
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Quiz"/> class
        /// </summary>
        public Quiz()
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
        /// Gets or sets the title
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the text
        /// </summary>
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
        /// Gets or sets the user id
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the view count
        /// </summary>
        public int ViewCount { get; set; }

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
        /// Gets or sets the quiz author
        /// It will be loaded on first use thanks to the EF Lazy-Loading feature
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// Gets or sets the questions related to this quiz
        /// It will be loaded on first use thanks to the EF Lazy-Loading feature
        /// </summary>
        public virtual List<Question> Questions { get; set; }

        /// <summary>
        /// Gets or sets the results related to this quiz
        /// It will be loaded on first use thanks to the EF Lazy-Loading feature
        /// </summary>
        public virtual List<Result> Results { get; set; }

        #endregion Lazy-Load Properties
    }
}
