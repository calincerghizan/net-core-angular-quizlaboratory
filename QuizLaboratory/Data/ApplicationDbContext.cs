using Microsoft.EntityFrameworkCore;
using QuizLaboratory.Data.Models;

namespace QuizLaboratory.Data
{
    /// <summary>
    /// The application db context class
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instace of <see cref="ApplicationDbContext"/> class
        /// </summary>
        /// <param name="options">The db context options</param>
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        #region Properties

        /// <summary>
        /// Gets or sets the users
        /// </summary>
        public DbSet<ApplicationUser> Users { get; set; }

        /// <summary>
        /// Gets or sets the quizzes
        /// </summary>
        public DbSet<Quiz> Quizzes { get; set; }

        /// <summary>
        /// Gets or sets the questions
        /// </summary>
        public DbSet<Question> Questions { get; set; }

        /// <summary>
        /// Gets or sets the answers
        /// </summary>
        public DbSet<Answer> Answers { get; set; }

        /// <summary>
        /// Gets or sets the results
        /// </summary>
        public DbSet<Result> Results { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //based on the Lazy-Load properties

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.Quizzes).WithOne(i => i.User);

            modelBuilder.Entity<Quiz>().ToTable("Quizzes");
            modelBuilder.Entity<Quiz>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Quiz>().HasOne(i => i.User).WithMany(x => x.Quizzes);
            modelBuilder.Entity<Quiz>().HasMany(x => x.Questions).WithOne(i => i.Quiz);

            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<Question>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Question>().HasOne(i => i.Quiz).WithMany(x => x.Questions);
            modelBuilder.Entity<Question>().HasMany(x => x.Answers).WithOne(i => i.Question);

            modelBuilder.Entity<Answer>().ToTable("Answers");
            modelBuilder.Entity<Answer>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Answer>().HasOne(i => i.Question).WithMany(x => x.Answers);

            modelBuilder.Entity<Result>().ToTable("Results");
            modelBuilder.Entity<Result>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Result>().HasOne(i => i.Quiz).WithMany(x => x.Results);
        }

        #endregion Methods
    }
}
