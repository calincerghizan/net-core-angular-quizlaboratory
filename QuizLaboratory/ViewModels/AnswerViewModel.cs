using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace QuizLaboratory.ViewModels
{
    /// <summary>
    /// The answer view model
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class AnswerViewModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AnswerViewModel"/> class
        /// </summary>
        public AnswerViewModel()
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
        /// Gets or sets the question id
        /// </summary>
        public int QuestionId { get; set; }

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
        /// Gets or sets the value
        /// </summary>
        [DefaultValue(0)]
        public int Value { get; set; }

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
