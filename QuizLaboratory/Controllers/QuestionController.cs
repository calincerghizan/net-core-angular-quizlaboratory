using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizLaboratory.Data;
using QuizLaboratory.Data.Models;
using QuizLaboratory.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizLaboratory.Controllers
{
    /// <summary>
    /// The question controller
    /// </summary>
    public class QuestionController : BaseApiController
    {
        #region Private Fields

        private readonly ApplicationDbContext _dbContext;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="QuestionController"/> class
        /// </summary>
        /// <param name="dbContext">The injected db context</param>
        public QuestionController(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        #endregion Constructor

        #region RESTful Conventions Methods

        /// <summary>
        /// Retrieves the question with the given {id}
        /// </summary>
        /// <param name="id">The id of the question</param>
        /// <returns>The question with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var question = _dbContext.Questions.Where(x => x.Id == id).FirstOrDefault();

            if (question == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Question ID {0} has not been found", id)
                });
            }

            return new JsonResult(question.Adapt<QuestionViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Adds a new question in the database
        /// </summary>
        /// <param name="model">The question to be added</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]QuestionViewModel model)
        {
            if (model == null)
            {
                // Return a generic HTTP Status 500 (Server Error)
                return new StatusCodeResult(500);
            }

            var question = new Question
            {
                QuizId = model.QuizId,
                Text = model.Text,
                Notes = model.Notes,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };

            _dbContext.Questions.Add(question);
            _dbContext.SaveChanges();

            return new JsonResult(question.Adapt<QuestionViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Edits the question with the given {id}
        /// </summary>
        /// <param name="model">The question to be edited</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]QuestionViewModel model)
        {
            if (model == null)
            {
                return new StatusCodeResult(500);
            }

            var question = _dbContext.Questions.Where(x => x.Id == model.Id).FirstOrDefault();

            if (question == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Question ID {0} has not been found", model.Id)
                });
            }

            question.QuizId = model.QuizId;
            question.Text = model.Text;
            question.Notes = model.Notes;
            question.LastModifiedDate = DateTime.Now;

            _dbContext.SaveChanges();

            return new JsonResult(question.Adapt<QuestionViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Deletes the question with the given {id}
        /// </summary>
        /// <param name="id">The id of the question to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var question = _dbContext.Questions.Where(x => x.Id == id).FirstOrDefault();

            if (question == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Question ID {0} has not been found", id)
                });
            }

            _dbContext.Questions.Remove(question);
            _dbContext.SaveChanges();

            // Return an HTTP Status 200 (OK)
            //return new OkResult();

            // Return NonContentResult instead of OkResult - the bug was fixed with new version.
            return new NoContentResult();
        }

        #endregion RESTful Conventions Methods

        /// <summary>
        /// Retrieves the all the questions related to the quiz {quizId}
        /// </summary>
        /// <param name="quizId">The quiz id</param>
        /// <returns>Questions</returns>
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var questions = _dbContext.Questions.Where(x => x.Id == quizId).ToArray();

            return new JsonResult(questions.Adapt<QuestionViewModel[]>(), JsonSettings);
        }
    }
}
