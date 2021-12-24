using ExpressionTreeSample;

ExpressTreeExample.ExpressTreeTest();

//单条件查询
var list1 = new List<QueryEntity>()
{
    new QueryEntity()
    {
        Key = "name",
        Value = "demon",
        Operator = "Contains"
    }
};

var expression1 = ExpressionExtension<User>.ExpressionSplice(list1);


//多条件查询
var list2 = new List<QueryEntity>()
{
    new QueryEntity()
    {
        Key = "name",
        Value = "demon",
        Operator = "Contains"
    },
    new QueryEntity()
    {
        Key = "age",
        Value = "18",
        Operator = "GreaterEqual",
        LogicalOperator = "AND" //AND 表示是并且的关系 OR表示或者关系
    }
};

var expression2 = ExpressionExtension<User>.ExpressionSplice(list2);

//多表查询
var list3 = new List<QueryEntity>()
{
    new QueryEntity()
    {
        Key = "name",
        Value = "demon",
        Operator = "Contains"
    },
    new QueryEntity()
    {
        Key = "address.Province",
        Value = "广东省",
        Operator = "Equals",
        // 注意：这里得填入 "AND",代表两个条件是并且的关系，如果需要查询名称包含 "chen" 或者 年龄大于等于18，则填入 "OR"
        LogicalOperator = "AND"
    }
};
var expression3 = ExpressionExtension<User>.ExpressionSplice(list3);
// expression = {Param_0 => ((Param_0.Address.Province == Convert("广东省", String)) And Invoke(Param_1 => Param_1.Name.Contains("demon"), Param_0))}