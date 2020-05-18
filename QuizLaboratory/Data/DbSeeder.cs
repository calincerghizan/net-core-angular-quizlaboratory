using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuizLaboratory.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizLaboratory.Data
{
    /// <summary>
    /// The db seeder class.
    /// </summary>
    public class DbSeeder
    {
        #region Public Methods

        /// <summary>
        /// Seeds default entries in db
        /// </summary>
        /// <param name="dbContext">The db context</param>
        public static void Seed(ApplicationDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                CreateUsers(dbContext);
            }

            if (!dbContext.Quizzes.Any())
            {
                CreateQuizzes(dbContext);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static void CreateUsers(ApplicationDbContext dbContext)
        {
            DateTime createdDate = DateTime.Now;
            DateTime lastModifiedDate = DateTime.Now;

            var user_Admin = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "admin@quizlaboratory.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            dbContext.Users.Add(user_Admin);

#if DEBUG
            var user_Ryan = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Ryan",
                Email = "ryan@quizlaboratory.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };
            var user_Solice = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Solice",
                Email = "solice@quizlaboratory.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };
            var user_Vodan = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Vodan",
                Email = "vodan@quizlaboratory.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };

            dbContext.Users.AddRange(user_Ryan, user_Solice, user_Vodan);
#endif

            dbContext.SaveChanges();
        }

        private static void CreateQuizzes(ApplicationDbContext dbContext)
        {
            DateTime createdDate = DateTime.Now;
            DateTime lastModifiedDate = DateTime.Now;
            var authorId = dbContext.Users.Where(x => x.UserName == "Admin").FirstOrDefault().Id;

#if DEBUG
            // Create 47 sample quizzes with auto-generated data
            // including questions, answers and results
            var num = 47;
            for (int i = 0; i <= num; i++)
            {
                CreateSampleQuiz(dbContext, i, authorId, num - i, 3, 3, 3, createdDate.AddDays(-num));

            }
#endif

            // Create 3 more quizzes with better descriptive data
            dbContext.Quizzes.Add(new Quiz
            {
                UserId = authorId,
                Title = "Are you more Light or Dark side of the Force?",
                Description = "Star Wars personality test",
                Text = @"Choose wisely you must, young padawan: " +
                        "this test will prove if your will is strong enough " +
                        "to adhere to the principles of the light side of the Force " +
                        "or if you're fated to embrace the dark side. " +
                        "No  you want to become a true JEDI, you can't possibly miss this!",
                ViewCount = 2343,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Quiz> e2 = dbContext.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "GenX, GenY or Genz?",
                Description = "Find out what decade most represents you",
                Text = @"Do you feel confortable in your generation? " +
            "What year should you have been born in?" +
            "Here's a bunch of questions that will help you to find out!",
                ViewCount = 4180,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            EntityEntry<Quiz> e3 = dbContext.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "Which Shingeki No Kyojin character are you?",
                Description = "Attack On Titan personality test",
                Text = @"Do you relentlessly seek revenge like Eren? " +
                        "Are you willing to put your like on the stake to protect your friends like Mikasa? " +
                        "Would you trust your fighting skills like Levi " +
                        "or rely on your strategies and tactics like Arwin? " +
                        "Unveil your true self with this Attack On Titan personality test!",
                ViewCount = 5203,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });

            dbContext.SaveChanges();
        }

        #endregion Private Methods

        #region Utility Methods

        /// <summary>
        /// Creates a sample quiz together with a set of questions, answers and results
        /// </summary>
        /// <param name="dbContext">The db context</param>
        /// <param name="num">The quiz id</param>
        /// <param name="authorId">The author id</param>
        /// <param name="viewCount">The view count</param>
        /// <param name="numberOfQuestions">The number of questions</param>
        /// <param name="numberOfAnswersPerQuestion">The number of answers per questions</param>
        /// <param name="numberOfResults">The number of results</param>
        /// <param name="createdDate">The created date</param>
        private static void CreateSampleQuiz(ApplicationDbContext dbContext,
            int num,
            string authorId,
            int viewCount,
            int numberOfQuestions,
            int numberOfAnswersPerQuestion,
            int numberOfResults,
            DateTime createdDate)
        {
            var quiz = new Quiz
            {
                UserId = authorId,
                Title = string.Format("Quiz {0} Title", num),
                Description = string.Format("This is a sample description for quiz {0}", num),
                Text = "This is a sample quiz created by the DbSeeder class for testing purposes",
                ViewCount = viewCount,
                CreatedDate = createdDate,
                LastModifiedDate = createdDate
            };

            dbContext.Quizzes.Add(quiz);
            dbContext.SaveChanges();

            for (int i = 0; i < numberOfQuestions; i++)
            {
                var question = new Question
                {
                    QuizId = quiz.Id,
                    Text = "This is a sample question created by the DbSeeder class for testing purposes",
                    CreatedDate = createdDate,
                    LastModifiedDate = createdDate
                };

                dbContext.Questions.Add(question);
                dbContext.SaveChanges();

                for (int j = 0; j < numberOfAnswersPerQuestion; j++)
                {
                    dbContext.Answers.Add(new Answer
                    {
                        QuestionId = question.Id,
                        Text = "This is a sample answer created by the DbSeeder class for testing purposes",
                        CreatedDate = createdDate,
                        LastModifiedDate = createdDate
                    });
                }
            }

            for (int i = 0; i < numberOfResults; i++)
            {
                dbContext.Results.Add(new Result
                {
                    QuizId = quiz.Id,
                    Text = "This is a sample result created by the DbSeeder class for testing purposes",
                    MinValue = default(int),
                    MaxValue = numberOfAnswersPerQuestion + 2,
                    CreatedDate = createdDate,
                    LastModifiedDate = createdDate
                });
            }

            dbContext.SaveChanges();
        }

        #endregion Utility Methods
    }
}
