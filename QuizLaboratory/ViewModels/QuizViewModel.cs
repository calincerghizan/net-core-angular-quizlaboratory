using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace QuizLaboratory.ViewModels
{
    /// <summary>
    /// The quiz view model
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class QuizViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuizViewModel"/> class
        /// </summary>
        public QuizViewModel()
        {

        }

        #region Properties

        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
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
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the view count
        /// </summary>
        [JsonIgnore]
        public int ViewCount { get; set; }

        /// <summary>
        /// Gets or sets the created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last modified date
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        #endregion Properties
    }
}
