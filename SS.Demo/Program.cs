using Newtonsoft.Json;
using SqlSugar;
using SS.Demo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(optons =>
{
    optons.Filters.Add<ActionLogFilter>();
});

builder.Services.AddScoped<EventIdProvider>();

var connectionStrings = new string[]
{
    "host=mysql;User Id=root;password=43e50a5f3d554f46846cf0c5681e3d34;Database=Model;CharSet=utf8mb4;",
    "host=mysql;User Id=root;password=43e50a5f3d554f46846cf0c5681e3d34;Database=Basic;CharSet=utf8mb4;"
};

#region SqlSugarScope ʹ�ã�ÿ������ EventId ֵ�̶�����

var serviceProvider = builder.Services.BuildServiceProvider();
foreach (var connectionString in connectionStrings)
{
    builder.Services.AddSingleton<ISqlSugarClient>(new SqlSugarScope(new ConnectionConfig()
    {
        DbType = DbType.MySql,
        ConnectionString = connectionString,
        IsAutoCloseConnection = true,
        ConfigId = connectionString.Contains("Model") ? "Model" : "Basic"
    },
    db => db.Aop.OnLogExecuted = (sql, paras) =>
    {
        var logger = serviceProvider.GetService<ILogger<SqlSugarScope>>();
        var eventIdProvider = serviceProvider.GetService<EventIdProvider>();
        //eventIdProvider.EventId ֵ�̶����䣬 ��ΪSqlSugarScope���ܴ�ί�в���serviceProvider
        logger.LogInformation($"{eventIdProvider.EventId}  sql:{sql} paras:{JsonConvert.SerializeObject(paras)}");
    }));
} 
#endregion


#region SqlSugarClient ʹ�� û����
//foreach (var connectionString in connectionStrings)
//{
//    builder.Services.AddScoped<ISqlSugarClient>(serviceProvider => new SqlSugarClient(new ConnectionConfig()
//    {
//        DbType = DbType.MySql,
//        ConnectionString = connectionString,
//        IsAutoCloseConnection = true,
//        ConfigId = connectionString.Contains("Model") ? "Model" : "Basic"
//    },
//    db => db.Aop.OnLogExecuted = (sql, paras) =>
//    {
//        var logger = serviceProvider.GetService<ILogger<SqlSugarClient>>();
//        var eventIdProvider = serviceProvider.GetService<EventIdProvider>();
//        logger.LogInformation($"ִ����SQL��EventId:{eventIdProvider.EventId} \r\n sql:{sql} paras:{JsonConvert.SerializeObject(paras)}");
//    }));
//} 
#endregion






var app = builder.Build();



app.UseAuthorization();

app.MapControllers();

app.Run();
