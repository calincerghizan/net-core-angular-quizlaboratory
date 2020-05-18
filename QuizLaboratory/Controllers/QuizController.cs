using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;
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
    /// The quiz controller
    /// </summary>
    public class QuizController : BaseApiController
    {
        #region Private Fields

        private readonly ApplicationDbContext _dbContext;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="QuizController"/> class
        /// </summary>
        /// <param name="dbContext">The injected db context</param>
        public QuizController(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion Constructor

        #region RESTful Conventions Methods

        /// <summary>
        /// Retrieves the quiz with the given {id}
        /// </summary>
        /// <param name="id">The quiz id</param>
        /// <returns>The quiz with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quiz = _dbContext.Quizzes.Where(x => x.Id == id).FirstOrDefault();

            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Quiz ID {0} has not been found", id)
                });
            }

            return new JsonResult(quiz.Adapt<QuizViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Adds a new quiz in the database
        /// </summary>
        /// <param name="model">The quiz view model containing the data to insert</param>
        [HttpPut]
        public IActionResult Put([FromBody] QuizViewModel model)
        {
            if (model == null)
            {
                // Return a generic HTTP Status 500 (Server Error)
                return new StatusCodeResult(500);
            }

            var quiz = new Quiz
            {
                // Properties taken from the request
                Title = model.Title,
                Description = model.Description,
                Text = model.Text,
                Notes = model.Notes,

                // Properties set from server-side
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,

                // Set a temporary author using the Admin user's Id
                // because the login isn't supported yet
                UserId = _dbContext.Users.Where(x => x.UserName == "Admin").FirstOrDefault().Id
            };

            _dbContext.Quizzes.Add(quiz);
            _dbContext.SaveChanges();

            return new JsonResult(quiz.Adapt<QuizViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Edits the quiz with the given id
        /// </summary>
        /// <param name="model">The quiz view model containing the data to be updated</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] QuizViewModel model)
        {
            if (model == null)
            {
                // Return a generic HTTP Status 500 (Server Error)
                return new StatusCodeResult(500);
            }

            var quiz = _dbContext.Quizzes.Where(x => x.Id == model.Id).FirstOrDefault();

            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Quiz ID {0} has not been found", model.Id)
                });
            }

            // Handle the update
            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;
            quiz.LastModifiedDate = DateTime.Now;

            _dbContext.SaveChanges();

            return new JsonResult(quiz.Adapt<QuizViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Deletes the quiz with the given {id}
        /// </summary>
        /// <param name="id">The id of the quiz to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quiz = _dbContext.Quizzes.Where(x => x.Id == id).FirstOrDefault();

            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Quiz ID {0} has not been found", id)
                });
            }

            _dbContext.Quizzes.Remove(quiz);
            _dbContext.SaveChanges();

            // Return an HTTP Status 200 (OK)
            //return new OkResult();

            // Return NonContentResult instead of OkResult - the bug was fixed with new version.
            return new NoContentResult();
        }

        #endregion RESTful Conventions Methods

        #region Attribute-based Routing Methods

        /// <summary>
        /// Retrieves the {num} quizzes 
        /// </summary>
        /// <param name="num">The number of quizzes to retrieve</param>
        /// <returns>{num} quizzes</returns>
        [HttpGet("Latest/{num:int?}")]
        public IActionResult Latest(int num = 10)
        {
            var latest = _dbContext.Quizzes.OrderByDescending(x => x.CreatedDate).Take(num).ToArray();

            return new JsonResult(latest.Adapt<QuizViewModel[]>(), JsonSettings);
        }

        /// <summary>
        /// Retrieves the {num} quizzes sorted by title (A to Z)
        /// </summary>
        /// <param name="num">The number of quizzes to retrieve</param>
        /// <returns>{num} quizzes sorted by title</returns>
        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var byTitle = _dbContext.Quizzes.OrderBy(x => x.Title).Take(10).ToArray();

            return new JsonResult(byTitle.Adapt<QuizViewModel[]>(), JsonSettings);
        }

        /// <summary>
        /// Retrieves the {num} quizzes in a random order
        /// </summary>
        /// <param name="num">The number of quizzes to retrieve</param>
        /// <returns>{num} quizzes</returns>
        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var random = _dbContext.Quizzes.OrderBy(x => Guid.NewGuid()).Take(num).ToArray();

            return new JsonResult(random.Adapt<QuizViewModel[]>(), JsonSettings);
        }

        #endregion Attribute-based Routing Methods
    }
}
