using System.Linq.Expressions;
using ZSpitz.Util;
using ExpressionTreeToString;

namespace ExpressionTreeSample;

public class ExpressTreeExample
{
    public static void ExpressTreeTest()
    {
        Expression<Func<User, bool>> expression = u => u.Age > 18;

        var treeStr = expression.ToString("Object notation", "C#");
        Console.WriteLine(treeStr);

        // // 这种方式,实际是生成SQL语句 SELECT * FROM User as T WHERE T.age > 18
        // Expression<Func<User, bool>> expression1 = u => u.Age > 18;
        // dbcontext.User.Where(expression1).ToList();
        //
        // // 这种,生成的 SELECT * FROM User ,然后将User表中所有数据加载到内存后,进行 age > 18 过滤
        // Func<User, bool> func = x => x.Age > 18;
        // dbcontext.User.Where(func).ToList();
        
        //ParameterExpression
        //BinaryExpression

        //动态创建表达式树 Expression<Func<User, bool>> expression = u => u.Age > 18
        var parameterExpression = Expression.Parameter(typeof(User), "u");
        var right = Expression.Constant(18);
        var left = Expression.MakeMemberAccess(parameterExpression, typeof(User).GetProperty("Age"));
        var body = Expression.GreaterThan(left, right);
        var expression1= Expression.Lambda<Func<User,bool>>(body,parameterExpression);
        var expTreeStr=expression1.ToString("Object notation", "C#");
        Console.WriteLine(expTreeStr);
    } 
}