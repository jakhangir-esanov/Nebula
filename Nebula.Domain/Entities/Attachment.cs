using Nebula.Domain.Commons;

namespace Nebula.Domain.Entities;

public class Attachment : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
}
