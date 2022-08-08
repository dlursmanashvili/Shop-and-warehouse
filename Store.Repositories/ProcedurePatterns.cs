using Pluralize.NET;
using Store.Models;

namespace Store.Repositories
{
    internal static class ProcedurePatterns<T>
    {
        public static string GetProcedure = $"Get{typeof(T).Name}_SP";
        public static string SelectProcedure = $"Select{new Pluralizer().Pluralize(typeof(T).Name)}_SP";
        public static string InsertProcedure = $"Insert{typeof(T).Name}_SP";
        public static string UpdateProcedure = $"Update{typeof(T).Name}_SP";
        public static string DeleteProcedure = $"Delete{typeof(T).Name}_SP";
    }
}