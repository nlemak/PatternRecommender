using NHibernate.Mapping.Attributes;

namespace PatternRecommender.Data
{
    [Class(Lazy = true)]
    public class Bookmark
    {
        [Id(Name = "id")]
        [Generator(Class = "native")]
        public int patternId { get; set; }

        [Id(Name = "id")]
        public string userName { get; set; }
    }
}