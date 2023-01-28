using SqlSugar;

namespace SS.Demo.Entity
{
    [SugarTable("IkWord", tableDescription: "关键字")]
    public class IkWordEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity= true)]
        public int Id { get; set; }

        public string Word { get; set; }

        public int Type { get; set; }
    }
}
