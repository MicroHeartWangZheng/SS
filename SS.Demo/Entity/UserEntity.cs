using SqlSugar;

namespace SS.Demo.Entity
{
    [SugarTable("User", tableDescription: "用户表")]
    public class UserEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity= true)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
