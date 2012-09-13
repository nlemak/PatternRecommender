using NHibernate.Mapping.Attributes;

namespace PatternRecommender.Data
{
    [Class(Lazy = true)]
    public class Yarn
    {
        [Id(Name = "id")]
        [Generator(Class = "native")]
        public int id { get; set; }

        [Property]
        public string name { get; set; }

        [Property]
        public YarnWeight weight { get; set; }

        public enum YarnWeight : byte
        {
            None,
            Thread,
            Cobweb,
            Lace,
            LightFingering,
            Fingering,
            Sport,
            DK,
            Worsted,
            Aran,
            Bulky,
            SuperBulky
        }
    }
}