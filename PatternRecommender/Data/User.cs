using NHibernate.Mapping.Attributes;

namespace PatternRecommender.Data
{
    [Class(Lazy = true)]
    public class User
    {
        [Id(Name = "name")]
        [Generator(Class = "native")]
        public int name { get; set; }

        [Property]
        public bool preferFree { get; set; }

        [Property]
        public bool preferDownload { get; set; }

        [Property]
        public bool preferAdventure { get; set; }

        [Property]
        public bool preferFriends { get; set; }
    }
}