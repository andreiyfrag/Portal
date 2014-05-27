using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;


namespace Portal.Helpers
{
    public static class Reflection
    {
        #region New Mapper
        private const string RuntimeExceptionMessageFormat = "A crapat incercand sa faca {0}, iar tipul proprietatii din result este {1}.Vezi inner Exception pentru detalii!";
        private const string ExpressionTreeBuildingException = "A crapat incercand sa descrie atribuirea proprietatii (din Destinatie) numita {0} de tipul {1}. Vezi inner Exception pentru detalii!";
        private readonly static ConstructorInfo NewExceptionConstructorInfo = typeof(Exception).GetConstructor(new Type[] { typeof(string), typeof(Exception) });
        private const string InvalidMemberInitExpression = "Parametrul optional baseMapper este incorect, el trebuie sa fie o expresie lambda al carei body sa fie un constructor cu proprietatile preinitializate. De exemplu: x => new TResult { Id = x.Id, Name = x.Name }";
        /// <summary>
        /// Creeaza o expresie lamda care copie dintr-un obiect Sursa in altul Destinatie toate proprietatile care au acelasi nume si acelasi tip.
        /// Daca crapa in exexcutia expresiei lambda, cel mai probabil crapa una dintre proprietati. Pentru a o identifica apelati cu parametrul userTryCatchForEachProperty = true, si folositi compilarea expresiei lambda cu metoda Compile().
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="baseMapper">este folosit pentru a putea folosi constructori cu parametri si pentru a avea atribuiri custom ale proprietatilor, de exemplu: x => new TResult(param1, param2) { Id = x.Id, Name = x.Name }</param>
        /// <param name="userTryCatchForEachProperty">este folosit pentru debug doar in varianta compilata a expresiei lambda</param>
        /// <returns></returns>
        public static Expression<Func<TSource, TResult>> ExpressionMapper<TSource, TResult>(Expression<Func<TSource, TResult>> baseMapper = null, bool userTryCatchForEachProperty = false, bool useOnlyBaseMapper = false, List<string> excludedProperties = null)
        {
            if (useOnlyBaseMapper)
                return baseMapper;

            excludedProperties = excludedProperties ?? new List<string>();

            ParameterExpression parameterX;
            NewExpression newExpression;
            List<MemberBinding> bindings;
            if (baseMapper != null)
            {
                parameterX = baseMapper.Parameters.FirstOrDefault();
                if (baseMapper.Body is MemberInitExpression)
                {
                    var initExpression = baseMapper.Body as MemberInitExpression;
                    newExpression = initExpression.NewExpression;
                    bindings = initExpression.Bindings.Where(x => !excludedProperties.Contains(x.Member.Name)).ToList();
                }
                else if (baseMapper.Body is NewExpression)
                {
                    newExpression = baseMapper.Body as NewExpression;
                    bindings = new List<MemberBinding>();
                }
                else
                {
                    throw new Exception(InvalidMemberInitExpression);
                }
            }
            else
            {
                parameterX = Expression.Parameter(typeof(TSource), "x");
                newExpression = Expression.New(typeof(TResult));
                bindings = new List<MemberBinding>();
            }

            var sourceProperties = typeof(TSource).GetProperties();
            var resultProperties = typeof(TResult).GetProperties();

            foreach (var resultProperty in resultProperties.Where(x => !excludedProperties.Contains(x.Name)))
            {
                try
                {
                    var existingBinding = bindings.FirstOrDefault(x => x.Member.Name == resultProperty.Name);
                    Expression sourceExpression;
                    PropertyInfo sourceProperty;
                    if (existingBinding != null)
                    {
                        var existingMemberAssignment = existingBinding as MemberAssignment;
                        sourceExpression = existingMemberAssignment.Expression;
                        bindings.Remove(existingBinding);
                    }
                    else if ((sourceProperty = sourceProperties.FirstOrDefault(p => p.Name == resultProperty.Name && p.GetType() == resultProperty.GetType())) != null)
                    {
                        sourceExpression = Expression.Property(parameterX, sourceProperty.Name);
                    }
                    else
                        continue;

                    Type t = resultProperty.GetType();
                    var convertedAssignment = t.IsValueType ? Expression.Convert(sourceExpression, resultProperty.PropertyType) : sourceExpression;
                    Expression resultExpression;
                    if (userTryCatchForEachProperty)
                    {
                        var exVariable = Expression.Variable(typeof(Exception), "ex");
                        var exMessageExpression = Expression.Constant(string.Format(RuntimeExceptionMessageFormat, convertedAssignment.ToString(), resultProperty.PropertyType), typeof(string));
                        var throwExpression = Expression.Throw(Expression.New(NewExceptionConstructorInfo, exMessageExpression, exVariable), typeof(Exception));
                        resultExpression = Expression.TryCatch(convertedAssignment, Expression.Catch(exVariable, convertedAssignment));
                    }
                    else
                    {
                        resultExpression = convertedAssignment;
                    }

                    bindings.Add(Expression.Bind(resultProperty, resultExpression));
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format(ExpressionTreeBuildingException, resultProperty.Name, resultProperty.PropertyType), ex);
                }
            }

            var expressionInit = Expression.MemberInit(newExpression, bindings.ToArray());

            return Expression.Lambda<Func<TSource, TResult>>(expressionInit, parameterX);
        }

        /// <summary>
        /// copie dintr-un obiect Sursa in altul Destinatie toate proprietatile care au acelasi nume si acelasi tip
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="baseMapper"></param>
        /// <returns></returns>
        public static TResult ExpressionMapperSingle<TSource, TResult>(this TSource source, Expression<Func<TSource, TResult>> baseMapper = null, bool useOnlyBaseMapper = false)
        {
            return ExpressionMapper<TSource, TResult>(baseMapper, true, useOnlyBaseMapper).Compile()(source);
        }

        /// <summary>
        /// copie dintr-un obiect Sursa in altul Destinatie toate proprietatile care au acelasi nume si acelasi tip
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="baseMapper"></param>
        /// <returns></returns>
        public static void ExpressionMapperSingle<TSource, TResult>(this TSource source, out TResult destination, Expression<Func<TSource, TResult>> baseMapper = null, bool useOnlyBaseMapper = false)
        {
            destination = ExpressionMapper<TSource, TResult>(baseMapper, true, useOnlyBaseMapper).Compile()(source);
        }

        public static IEnumerable<TResult> SelectExpressionMapper<TSource, TResult>(this IEnumerable<TSource> source, Expression<Func<TSource, TResult>> baseMapper = null)
        {
            var mapper = ExpressionMapper<TSource, TResult>(baseMapper, true).Compile();
            return source.Select(mapper);
        }

        public static IQueryable<TResult> SelectExpressionMapper<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> baseMapper = null, bool userTryCatchForEachProperty = false, bool useOnlyBaseMapper = false)
        {
            var mapper = ExpressionMapper<TSource, TResult>(baseMapper, userTryCatchForEachProperty, useOnlyBaseMapper);
            return source.Select(mapper);
        }
        #endregion

        public static string ToQueryString<T>(this T obj, int lvl = 1, params string[] skippedProperties)
        {
            if (lvl > 20)
            {
                return string.Empty;
            }

            if (obj != null)
            {
                try
                {
                    var type = typeof(T);
                    if (type.IsClass && !type.IsPrimitive && type.Namespace != null && type.Namespace.Contains("IndacoEMS"))
                    {
                        var sb = new System.Text.StringBuilder();
                        foreach (var prop in type.GetProperties().Where(x => x.Name != "Item" && !x.Name.ToLower().Contains("password") && !skippedProperties.Contains(x.Name)))
                        {
                            var propValue = prop.GetValue(obj, null);
                            sb.AppendFormat("&{0}={1}", prop.Name, ToQueryString(propValue, lvl + 1));
                        }

                        return "(" + (sb.Length > 0 ? sb.Remove(0, 1).ToString() : string.Empty) + ")";
                    }
                    else
                        return obj.ToString();
                }
                catch (Exception)
                {
                    return obj.ToString();
                }
            }

            return "null";
        }
    }
}