using System.Reflection;
using System.Reflection.Emit;
using EmitSample;

// hello world use emit
var method = new DynamicMethod("Main",null,Type.EmptyTypes);
var ilGenerator = method.GetILGenerator();
ilGenerator.Emit(OpCodes.Nop);
ilGenerator.Emit(OpCodes.Ldstr,"hello world");
ilGenerator.Emit(OpCodes.Call,typeof(Console).GetMethod("WriteLine",new Type[]{typeof(string)}));
ilGenerator.Emit(OpCodes.Nop);
ilGenerator.Emit(OpCodes.Ret);

var action =method.CreateDelegate(typeof(Action)) as Action;
action!.Invoke();

// Foo<T> class
var barType = typeof(Bar);
var interfaceType = typeof(IFoo<>);

var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("Demon.Emit"), AssemblyBuilderAccess.Run);
var moduleBuilder = assemblyBuilder.DefineDynamicModule("Demon.Emit");
var typeBuilder = moduleBuilder.DefineType("Foo",
    TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AnsiClass | TypeAttributes.AutoClass |
    TypeAttributes.BeforeFieldInit);
var genericTypeBuilder = typeBuilder.DefineGenericParameters("T")[0];
genericTypeBuilder.SetGenericParameterAttributes(GenericParameterAttributes.NotNullableValueTypeConstraint);
typeBuilder.SetParent(barType);
typeBuilder.AddInterfaceImplementation(interfaceType.MakeGenericType(genericTypeBuilder));

var fieldBuilder = typeBuilder.DefineField("_name", genericTypeBuilder, FieldAttributes.Private);

// 定义构造函数
var ctorBuilder = typeBuilder.DefineConstructor(
    MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName |
    MethodAttributes.RTSpecialName, CallingConventions.Standard, new Type[] { genericTypeBuilder });
var ctorIl = ctorBuilder.GetILGenerator();
ctorIl.Emit(OpCodes.Ldarg_0);
ctorIl.Emit(OpCodes.Ldarg_1);

ctorIl.Emit(OpCodes.Stfld, fieldBuilder);
ctorIl.Emit(OpCodes.Ret);


var propertyBuilder = typeBuilder.DefineProperty("Name", PropertyAttributes.None, genericTypeBuilder, Type.EmptyTypes);

//定义get方法
var getMethodBuilder = typeBuilder.DefineMethod("get_Name", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.SpecialName | MethodAttributes.Virtual, CallingConventions.Standard, genericTypeBuilder, Type.EmptyTypes);
var getIl = getMethodBuilder.GetILGenerator();
getIl.Emit(OpCodes.Ldarg_0);
getIl.Emit(OpCodes.Ldfld, fieldBuilder);
getIl.Emit(OpCodes.Ret);
typeBuilder.DefineMethodOverride(getMethodBuilder, interfaceType.GetProperty("Name").GetGetMethod()); //实现对接口方法的重载
propertyBuilder.SetGetMethod(getMethodBuilder); //设置为属性的get方法
//定义set方法
var setMethodBuilder = typeBuilder.DefineMethod("set_Name", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.SpecialName | MethodAttributes.Virtual, CallingConventions.Standard, null, new Type[] { genericTypeBuilder });
var setIl = setMethodBuilder.GetILGenerator();
setIl.Emit(OpCodes.Ldarg_0);
setIl.Emit(OpCodes.Ldarg_1);
setIl.Emit(OpCodes.Stfld, fieldBuilder);
setIl.Emit(OpCodes.Ret);
typeBuilder.DefineMethodOverride(setMethodBuilder, interfaceType.GetProperty("Name").GetSetMethod()); //实现对接口方法的重载propertyBuilder.SetSetMethod(setMethodBuilder); //设置为属性的set方法

//定义方法
var printMethodBuilder = typeBuilder.DefineMethod("PrintName", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual, CallingConventions.Standard, null, Type.EmptyTypes);
var printIl = printMethodBuilder.GetILGenerator();
printIl.Emit(OpCodes.Ldarg_0);
printIl.Emit(OpCodes.Ldflda, fieldBuilder);
printIl.Emit(OpCodes.Constrained, genericTypeBuilder);
printIl.Emit(OpCodes.Callvirt, typeof(object).GetMethod("ToString", Type.EmptyTypes));
printIl.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
printIl.Emit(OpCodes.Ret);
//实现对基类方法的重载
typeBuilder.DefineMethodOverride(printMethodBuilder, barType.GetMethod("PrintName", Type.EmptyTypes));

var type = typeBuilder.CreateType();
var obj = Activator.CreateInstance(type!.MakeGenericType(typeof(DateOnly)), DateOnly.MaxValue);
(obj as Bar)!.PrintName();
Console.WriteLine((obj as IFoo<DateOnly>)!.Name);
Console.ReadKey();