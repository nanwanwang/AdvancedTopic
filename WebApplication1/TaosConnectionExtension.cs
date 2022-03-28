using System.Text;
using IoTSharp.Data.Taos;

namespace WebApplication1;

public static class TaosConnectionExtension
{
    /// <summary>
    /// 创建数据库Command
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="dbName"></param>
    /// <param name="createIsNotExist"></param>
    /// <returns></returns>
    public static TaosCommand CreateDbCommand(this TaosConnection connection, string dbName,bool createIsNotExist = false)
    {
        ArgumentNullException.ThrowIfNull(dbName);
        var sql = createIsNotExist ? $"CREATE DATABASE IF NOT EXISTS {dbName}  KEEP 3650 DAYS 10 BLOCKS 6;" : $"CREATE DATABASE {dbName};";
        return connection.CreateCommand(sql);
    }

    /// <summary>
    /// 创建表Command
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="dbName"></param>
    /// <param name="tableName"></param>
    /// <param name="tableSchema"></param>
    /// <param name="createIsNotExist"></param>
    /// <returns></returns>
    public static TaosCommand CreateTableCommand(this TaosConnection connection, string dbName, string tableName,
        string tableSchema,bool createIsNotExist= false)
    {
        ArgumentNullException.ThrowIfNull(dbName);
        ArgumentNullException.ThrowIfNull(tableName);
        ArgumentNullException.ThrowIfNull(tableSchema);
        var sql = createIsNotExist ? $"CREATE TABLE IF NOT EXISTS {dbName}.{tableName} ({tableSchema});" : $"CREATE TABLE {dbName}.{tableName} ({tableSchema});";
        return connection.CreateCommand(sql);
    }


    /// <summary>
    /// 以超级表为模板创建数据表
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="dbName"></param>
    /// <param name="tableName"></param>
    /// <param name="superTableName"></param>
    /// <param name="tagsValue"></param>
    /// <returns></returns>
    public static TaosCommand CreateTableBySupperTableCommand(this TaosConnection connection, string dbName,
        string tableName, string superTableName , string tagsValue)
    {
        ArgumentNullException.ThrowIfNull(dbName);
        ArgumentNullException.ThrowIfNull(tableName);
        ArgumentNullException.ThrowIfNull(superTableName);
        ArgumentNullException.ThrowIfNull(tagsValue);
        connection.ChangeDatabase(dbName);
        var sql = $"CREATE TABLE IF NOT EXISTS {tableName} USING {superTableName} TAGS ({tagsValue});";
        return connection.CreateCommand(sql);
    }

    /// <summary>
    /// 创建超级表
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="dbName"></param>
    /// <param name="superTableName"></param>
    /// <param name="tableSchema"></param>
    /// <param name="tagSchema"></param>
    /// <returns></returns>
    public static TaosCommand CreateSupperTableCommand(this TaosConnection connection, string dbName,
        string superTableName, string tableSchema,
        string tagSchema)
    {
        ArgumentNullException.ThrowIfNull(dbName);
        ArgumentNullException.ThrowIfNull(superTableName);
        ArgumentNullException.ThrowIfNull(tableSchema);
        ArgumentNullException.ThrowIfNull(tagSchema);
        connection.ChangeDatabase(dbName);
        var sql =  $"CREATE TABLE IF NOT EXISTS {superTableName} ({tableSchema}) TAGS ({tagSchema});" ;
        return  connection.CreateCommand(sql);
    }

    /// <summary>
    /// 删除数据库Command
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="dbName"></param>
    /// <returns></returns>
    public static TaosCommand DropDbCommand(this TaosConnection connection, string dbName)
    {
        ArgumentNullException.ThrowIfNull(dbName);
        var sql = $"DROP DATABASE IF EXISTS {dbName};";
        return connection.CreateCommand(sql);
    }

    /// <summary>
    /// 删除表
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="dbName"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static TaosCommand DropTableCommand(this TaosConnection connection, string dbName, string tableName)
    {
        ArgumentNullException.ThrowIfNull(dbName);
        ArgumentNullException.ThrowIfNull(tableName);
        var sql = $"DROP TABLE IF EXISTS {dbName}.{tableName};";
        return connection.CreateCommand(sql);
    }
    
    /// <summary>
    /// 删除超级表
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="dbName"></param>
    /// <param name="superTableName"></param>
    /// <returns></returns>
    public static TaosCommand DropSuperTableCommand(this TaosConnection connection, string dbName, string superTableName)
    {
        ArgumentNullException.ThrowIfNull(dbName);
        ArgumentNullException.ThrowIfNull(superTableName);
        var sql = $"DROP STABLE IF EXISTS {dbName}.{superTableName};";
        return connection.CreateCommand(sql);
    }


    /// <summary>
    /// 新增每一层点位数据
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="dbName"></param>
    /// <param name="tableName"></param>
    /// <param name="floorTagsValue"></param>
    /// <returns></returns>
    public static TaosCommand InsertFloorTagsCommand(this TaosConnection connection, string dbName, string tableName,
        List<float> floorTagsValue)
    {
        ArgumentNullException.ThrowIfNull(dbName);
        ArgumentNullException.ThrowIfNull(tableName);
        ArgumentNullException.ThrowIfNull(floorTagsValue);
        var sql = new StringBuilder($"INSERT INTO {dbName}.{tableName} VALUES ");
        sql.Append($"('{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ms")}',{string.Join(",", floorTagsValue)})");
        sql.Append(" ;");

        return connection.CreateCommand(sql.ToString());
    }


    /// <summary>
    /// 查询语句
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="dbName"></param>
    /// <param name="tableName"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static TaosCommand SelectCommand(this TaosConnection connection, string dbName, string tableName,
        string expression)
    {
        ArgumentNullException.ThrowIfNull(dbName);
        ArgumentNullException.ThrowIfNull(tableName);

        var sql = new StringBuilder($"SELECT * FROM {dbName}.{tableName} ");
        if (!string.IsNullOrEmpty(expression))
        {
            sql.Append(expression);
        }

        sql.Append(" ;");
        return connection.CreateCommand(sql.ToString());
    }

  
}