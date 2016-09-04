using System;

namespace WebApi2Book.Data.Entities
{
    public interface IVersionedEntity
    {
        DateTime Version { get; set; }
    }
}