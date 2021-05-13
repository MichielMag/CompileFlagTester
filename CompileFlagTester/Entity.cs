using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileFlagTester
{
    class Identifier : IEquatable<Identifier>,
#if ID_GUID
        IEquatable<Guid>
#else
        IEquatable<long>
#endif
    {
#if ID_GUID
        public Identifier(Guid id) { Id = id; }
        Guid Id { get; set; }
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
        long Id { get; set; }
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
    }
    interface IEntity
    {
        Identifier Id { get; set; }
    }
    class Entity : IEntity, IEquatable<IEntity>
    {
        Identifier _id = Guid.Empty;
        public Identifier Id { get => _id; set { _id = value; } }

        public bool Equals(IEntity other)
        {
            return Id == other.Id;
        }
    }
}
