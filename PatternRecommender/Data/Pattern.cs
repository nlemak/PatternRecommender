using NHibernate.Mapping.Attributes;

namespace PatternRecommender.Data
{
    [Class(Lazy = true)]
    public class Pattern
    {
        [Id(Name = "id")]
        [Generator(Class = "native")]
        public int id { get; set; }

        [Property]
        public string name { get; set; }

        [Property]
        public string author { get; set; }

        // yarnid? or yarn object?

        [Property]
        public int yardage { get; set; }

        /// <summary>
        /// Needle size is 100x multiplied to allow storing of the value as an int accurately.
        /// </summary>
        [Property]
        public NeedleSize needleSize { get; set; }

        [Property]
        public byte difficulty { get; set; }

        [Property]
        public byte rating { get; set; }

        public enum NeedleSize : ushort
        {
            mm50 = 50,
            mm75 = 75,
            mm100 = 100,
            mm125 = 125,
            mm150 = 150,
            mm175 = 175,
            mm200 = 200,
            mm225 = 225,
            mm250 = 250,
            mm275 = 275,
            mm300 = 300,
            mm325 = 325,
            mm350 = 350,
            mm375 = 375,
            mm400 = 400,
            mm450 = 450,
            mm500 = 500,
            mm550 = 550,
            mm600 = 600,
            mm650 = 650,
            mm700 = 700,
            mm750 = 750,
            mm800 = 800,
            mm900 = 900,
            mm1000 = 1000,
            mm1275 = 1275,
            mm1500 = 1500,
            mm1900 = 1900
        }
    }
}