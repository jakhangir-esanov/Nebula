using Nebula.Domain.Commons;

namespace Nebula.Domain.Entities;

public sealed class Office : Auditable
{
    public string Name { get; set; }

}
