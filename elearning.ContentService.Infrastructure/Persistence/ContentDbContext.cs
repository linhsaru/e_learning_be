using elearning.ContentService.Domain.Courses;
using elearning.ContentService.Domain.Knowledge.Entities;
using elearning.ContentService.Domain.Lessons;
using elearning.ContentService.Domain.MasterData.Entities;
using elearning.ContentService.Domain.Media;
using elearning.ContentService.Domain.Questions;
using Microsoft.EntityFrameworkCore;

namespace elearning.ContentService.Infrastructure.Persistence
{
    public class ContentDbContext : DbContext
    {
        public ContentDbContext(DbContextOptions<ContentDbContext> options) : base(options)
        {
        }

        // Master Data
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<Level> Levels => Set<Level>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<PartOfSpeech> PartsOfSpeech => Set<PartOfSpeech>();
        public DbSet<CourseTag> CourseTags => Set<CourseTag>();

        // Course Structure
        public DbSet<LearningPath> LearningPaths => Set<LearningPath>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Unit> Units => Set<Unit>();

        // Lesson & Media
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<LessonBlock> LessonBlocks => Set<LessonBlock>();
        public DbSet<LessonVocabulary> LessonVocabularies => Set<LessonVocabulary>();
        public DbSet<LessonGrammar> LessonGrammars => Set<LessonGrammar>();
        public DbSet<MediaAsset> MediaAssets => Set<MediaAsset>();

        // Knowledge Base
        public DbSet<Vocabulary> Vocabularies => Set<Vocabulary>();
        public DbSet<VocabularyExample> VocabularyExamples => Set<VocabularyExample>();
        public DbSet<VocabularyTag> VocabularyTags => Set<VocabularyTag>();
        public DbSet<Grammar> Grammars => Set<Grammar>();
        public DbSet<GrammarExample> GrammarExamples => Set<GrammarExample>();
        public DbSet<GrammarTag> GrammarTags => Set<GrammarTag>();

        // Question Bank & Assessment
        public DbSet<QuestionSet> QuestionSets => Set<QuestionSet>();
        public DbSet<QuestionGroup> QuestionGroups => Set<QuestionGroup>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<QuestionTag> QuestionTags => Set<QuestionTag>();
        public DbSet<Option> Options => Set<Option>();
        public DbSet<Explanation> Explanations => Set<Explanation>();
        public DbSet<Assessment> Assessments => Set<Assessment>();
        public DbSet<AssessmentQuestionSet> AssessmentQuestionSets => Set<AssessmentQuestionSet>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContentDbContext).Assembly);
        }
    }
}
