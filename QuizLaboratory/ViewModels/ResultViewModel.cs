using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace QuizLaboratory.ViewModels
{
    /// <summary>
    /// The result view model
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class ResultViewModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ResultViewModel"/> class
        /// </summary>
        public ResultViewModel()
        {

        }

        #region Properties

        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the quiz id
        /// </summary>
        public int QuizId { get; set; }

        /// <summary>
        /// Gets or sets the text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the notes
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the min value
        /// </summary>
        public int? MinValue { get; set; }

        /// <summary>
        /// Gets or sets the max value
        /// </summary>
        public int? MaxValue { get; set; }

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
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last modified date
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        #endregion Properties
    }
}
