using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizLaboratory.Data;
using QuizLaboratory.Data.Models;
using QuizLaboratory.ViewModels;

namespace QuizLaboratory.Controllers
{
    /// <summary>
    /// The answer controller
    /// </summary>
    public class AnswerController : BaseApiController
    {
        #region Private Fields

        private readonly ApplicationDbContext _dbContext;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <seealso cref="AnswerController"/> class
        /// </summary>
        /// <param name="dbContext">The injected db context</param>
        public AnswerController(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        #endregion Constructor

        #region RESTful Conventions Methods 

        /// <summary>
        /// Retrieves the answer with the given {id}
        /// </summary>
        /// <param name="id">The id of the answer</param>
        /// <returns>The answer with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var answer = _dbContext.Answers.Where(x => x.Id == id).FirstOrDefault();

            if (answer == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Answer ID {0} has not been found", id)
                });
            }

            return new JsonResult(answer.Adapt<AnswerViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Adds a new answer in the database
        /// </summary>
        /// <param name="model">The answer to be added</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]AnswerViewModel model)
        {
            if (model == null)
            {
                // Return a generic HTTP Status 500 (Server Error)
                return new StatusCodeResult(500);
            }

            var answer = new Answer
            {
                QuestionId = model.QuestionId,
                Text = model.Text,
                Notes = model.Notes,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };

            _dbContext.Answers.Add(answer);

            return new JsonResult(answer.Adapt<AnswerViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Edits the answer with the given {id}
        /// </summary>
        /// <param name="model">The the answer to be edited</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]AnswerViewModel model)
        {
            if (model == null)
            {
                // Return a generic HTTP Status 500 (Server Error)
                return new StatusCodeResult(500);
            }

            var answer = _dbContext.Answers.Where(x => x.Id == model.Id).FirstOrDefault();

            if (answer == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Answer ID {0} has not been found", model.Id)
                });
            }

            answer.QuestionId = model.QuestionId;
            answer.Text = model.Text;
            answer.Notes = model.Notes;
            answer.LastModifiedDate = DateTime.Now;

            _dbContext.SaveChanges();

            return new JsonResult(answer.Adapt<AnswerViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Deletes the answer with the given {id}
        /// </summary>
        /// <param name="id">The id of the answer to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var answer = _dbContext.Answers.Where(x => x.Id == id).FirstOrDefault();

            if (answer == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Answer ID {0} has not been found", id)
                });
            }

            _dbContext.Answers.Remove(answer);
            _dbContext.SaveChanges();

            // Return an HTTP Status 200 (OK)
            //return new OkResult();

            // Return NonContentResult instead of OkResult - the bug was fixed with new version.
            return new NoContentResult();
        }

        #endregion RESTful Conventions Methods

        #region Attribute-based Routing Methods

        /// <summary>
        /// Retrieves the answers related to the question {questionId}
        /// </summary>
        /// <param name="questionId">The question id</param>
        /// <returns>The answers related to the question {questionId}</returns>
        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId)
        {
            var answers = _dbContext.Answers.Where(x => x.Id == questionId).ToArray();

            return new JsonResult(answers.Adapt<AnswerViewModel[]>(), JsonSettings);
        }

        #endregion Attribute-based Routing Methods
    }
}
