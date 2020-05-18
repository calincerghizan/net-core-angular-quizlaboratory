using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizLaboratory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizLaboratory.Controllers
{
    /// <summary>
    /// The base api controller
    /// </summary>
    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets or sets the db context
        /// </summary>
        protected ApplicationDbContext DbContext { get; private set; }

        /// <summary>
        /// Gets or sets the json serializer settings
        /// </summary>
        protected JsonSerializerSettings JsonSettings { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="BaseApiController"/> class
        /// </summary>
        /// <param name="dbContext">The db context</param>
        public BaseApiController (ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            JsonSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
        }

        #endregion Constructor
    }
}
