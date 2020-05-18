using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizLaboratory.Data.Models
{
    /// <summary>
    /// The application user data model
    /// </summary>
    public class ApplicationUser
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationUser"/> class
        /// </summary>
        public ApplicationUser()
        {

        }

        #region Properties

        /// <summary>
        /// Gets or sets the id
        /// </summary>
        [Key]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the user name
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the notes
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the flags
        /// </summary>
        [Required]
        public int Flgas { get; set; }

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
        /// Gets or sets the quizes related to this application user
        /// </summary>
        public virtual List<Quiz> Quizzes { get; set; }

        #endregion Lazy-Load Properties
    }
}
