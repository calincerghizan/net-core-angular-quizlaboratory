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
    /// The result controller
    /// </summary>
    public class ResultController : BaseApiController
    {
        #region Private Fields

        private readonly ApplicationDbContext _dbContext;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="ResultController"/> class
        /// </summary>
        /// <param name="dbContext">The injected db context</param>
        public ResultController(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        #endregion Constructor

        #region RESTful Conventions Methods

        /// <summary>
        /// Retrieves the result with the given {id}
        /// </summary>
        /// <param name="id">The id of the result</param>
        /// <returns>The result with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _dbContext.Results.Where(x => x.Id == id).FirstOrDefault();

            if (result == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Result ID {0} has not been found", id)
                });
            }

            return new JsonResult(result.Adapt<ResultViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Adds a new result in the database
        /// </summary>
        /// <param name="model">The result to be added</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Put([FromBody]ResultViewModel model)
        {
            if (model == null)
            {
                // Return a generic HTTP Status 500 (Server Error)
                return new StatusCodeResult(500);
            }

            var result = model.Adapt<Result>();

            result.CreatedDate = DateTime.Now;
            result.LastModifiedDate = DateTime.Now;

            _dbContext.Results.Add(result);
            _dbContext.SaveChanges();

            return new JsonResult(result.Adapt<ResultViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Edits the result with the given {id}
        /// </summary>
        /// <param name="model">The result to be edited</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Post([FromBody]ResultViewModel model)
        {
            if (model == null)
            {
                // Return a generic HTTP Status 500 (Server Error)
                return new StatusCodeResult(500);
            }

            var result = _dbContext.Results.Where(x => x.Id == model.Id).FirstOrDefault();

            if (result == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Result ID {0} has not been found", model.Id)
                });
            }

            result.QuizId = model.QuizId;
            result.Text = model.Text;
            result.Notes = model.Notes;
            result.MinValue = model.MinValue;
            result.MaxValue = model.MaxValue;
            result.LastModifiedDate = DateTime.Now;

            _dbContext.SaveChanges();

            return new JsonResult(result.Adapt<ResultViewModel>(), JsonSettings);
        }

        /// <summary>
        /// Deletes the result with the given {id}
        /// </summary>
        /// <param name="id">The id of the result to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _dbContext.Results.Where(x => x.Id == id).FirstOrDefault();

            if (result == null)
            {
                return NotFound(new
                {
                    Error = string.Format("Result ID {0} has not been found", id)
                });
            }

            _dbContext.Results.Remove(result);
            _dbContext.SaveChanges();

            // Return an HTTP Status 200 (OK)
            //return new OkResult();

            // Return NonContentResult instead of OkResult - the bug was fixed with new version.
            return new NoContentResult();
        }

        #endregion RESTful Conventions Methods

        #region Attribute-based Routing Methods

        /// <summary>
        /// Retrieves the results related to the quiz {quizId}
        /// </summary>
        /// <param name="quizId">The quiz id</param>
        /// <returns>The results related to a quiz {quizId}</returns>
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var results = _dbContext.Results.Where(x => x.QuizId == quizId).ToArray();

            return new JsonResult(results.Adapt<ResultViewModel[]>(), JsonSettings);
        }

        #endregion Attribute-based Routing Methods
    }
}
