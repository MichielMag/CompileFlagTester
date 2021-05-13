using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTester;

namespace CompileFlagTester
{
    public interface IIdentifier<T>
    {
        bool IsEmpty();
        static T Empty { get; }

        T Id { get; set; }
    }
    [JsonConverter(typeof(IdentifierConverter))]
    [TypeConverter(typeof(IdentifierTypeConverter))]
    public class Identifier : IEquatable<Identifier>,
#if ID_GUID
        IEquatable<Guid>,
        IIdentifier<Guid>
#else
        IEquatable<long>,
        IIdentifier<long>
#endif
    {
#if ID_GUID
        public Identifier(Guid id) { Id = id; }
        public Guid Id { get; set; }
        public Guid Empty => Guid.Empty;
        public bool Equals(Guid other)
        {
            return Id == other;
        }
        public static implicit operator Identifier(Guid id)
        {
            return new Identifier(id);
        }
#else

        public Identifier(long id) { Id = id; }
        public long Id { get; set; }

        public static long Empty => -1;

        public bool Equals(long other)
        {
            return Id == other;
        }
        public static implicit operator Identifier(long id)
        {
            return new Identifier(id);
        }
#endif
        public Identifier() { }
        public bool Equals(Identifier other)
        {
            return Id == other.Id;
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }
    }
    public interface IEntity
    {
        Identifier Id { get; set; }
    }
    public class Entity : IEntity, IEquatable<IEntity>
    {
        Identifier _id = Identifier.Empty;
        public Identifier Id { get => _id; set { _id = value; } }

        public bool Equals(IEntity other)
        {
            return Id == other.Id;
        }
    }
}
