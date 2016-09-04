using FluentNHibernate.Mapping;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.Mapping
{
    public abstract class VersionedClassMap<T>: ClassMap<T> where T : IVersionedEntity
    {
        protected VersionedClassMap()
        {
            Version(x => x.Version)
            .Column("ts")
            .CustomSqlType("TIMESTAMP")
            .Generated.Always()
            .UnsavedValue("null");
        }
    }
}