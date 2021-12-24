using System.Linq.Expressions;
using System.Reflection;
using ZSpitz.Util;

namespace ExpressionTreeSample;

public class ExpressionExtension<T> where T : class, new()
{
    public static Expression<Func<T, bool>> ExpressionSplice(List<QueryEntity> entities)
    {
        if (entities.Count < 1)
            return ex => true;
        var expression_first = CreateExpressionDelegate(entities[0]);
        foreach (var entity in entities.Skip(1))
        {
            var expression = CreateExpressionDelegate(entity);
            InvocationExpression invocation =
                Expression.Invoke(expression_first, expression.Parameters.Cast<Expression>());
            BinaryExpression binary;

            if (entity.LogicalOperator.ToUpper().Equals("OR"))
            {
                binary = Expression.Or(expression.Body, invocation);
            }
            else
            {
                binary = Expression.And(expression.Body, invocation);
            }

            expression_first = Expression.Lambda<Func<T, bool>>(binary, expression.Parameters);
        }

        return expression_first;
    }

    private static Expression<Func<T, bool>> CreateExpressionDelegate(QueryEntity entity)
    {
        var parameterExpression = Expression.Parameter(typeof(T));
        Expression key = parameterExpression;
        var entityKey = entity.Key.Trim();

        if (entityKey.Contains('.'))
        {
            var tableNameAndField = entityKey.Split('.');
            key = Expression.Property(key, tableNameAndField[0].ToString());
            key = Expression.Property(key, tableNameAndField[1].ToString());
        }
        else
        {
            key = Expression.Property(key, entityKey);
        }

        Expression value = Expression.Constant(ParseType(entity));
        Expression body = CreateExpression(key, value, entity.Operator);
        var lambda = Expression.Lambda<Func<T, bool>>(body, parameterExpression);
        return lambda;
    }

    private static object ParseType(QueryEntity entity)
    {
        try
        {
            PropertyInfo property;
            if (entity.Key.Contains('.'))
            {
                var tableNameAndField = entity.Key.Split('.');
                property = typeof(T).GetProperty(tableNameAndField[0],
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                property = property.PropertyType.GetProperty(tableNameAndField[1],
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            }
            else
            {
                property = typeof(T).GetProperty(entity.Key,
                    BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
            }

            return Convert.ChangeType(entity.Value, property.PropertyType);
        }
        catch (Exception)
        {
            throw new ArgumentException("字段类型转换失败：字段名错误或值类型不正确");
        }
    }

    private static Expression CreateExpression(Expression left, Expression value, string entityOperator)
    {
        if (!Enum.TryParse(entityOperator, true, out OperatorEnum operatorEnum))
        {
            throw new ArgumentException("操作方法不存在,请检查operator的值");
        }

        return operatorEnum switch
        {
            OperatorEnum.Equals => Expression.Equal(left, Expression.Convert(value, left.Type)),
            OperatorEnum.NotEqual => Expression.NotEqual(left, Expression.Convert(value, left.Type)),
            OperatorEnum.Contains => Expression.Call(left,
                typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), value),
            OperatorEnum.StartsWith => Expression.Call(left,
                typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }), value),
            OperatorEnum.EndsWith => Expression.Call(left,
                typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }), value),
            OperatorEnum.Greater => Expression.GreaterThan(left, Expression.Convert(value, left.Type)),
            OperatorEnum.GreaterEqual => Expression.GreaterThanOrEqual(left, Expression.Convert(value, left.Type)),
            OperatorEnum.Less => Expression.LessThan(left, Expression.Convert(value, left.Type)),
            OperatorEnum.LessEqual => Expression.LessThanOrEqual(left, Expression.Convert(value, left.Type)),
            _ => Expression.Equal(left, Expression.Convert(left, left.Type))
        };
    }
}