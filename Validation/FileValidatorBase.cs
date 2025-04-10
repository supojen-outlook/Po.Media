namespace Po.Media.Validation;

internal abstract class FileValidatorBase
{
    protected FileValidatorBase(
        string typeName,
        string[] extensions,
        byte[][] magicNumbers
    )
    {
        TypeName = typeName;
        Extensions.UnionWith(extensions);
        MagicNumbers.AddRange(magicNumbers);
        MaxMagicNumberLength = MagicNumbers.Max(m => m.Length);
    }
    
    public string TypeName { get; set; }
    
    protected HashSet<string> Extensions { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    protected List<byte[]> MagicNumbers { get; } = [];
    protected int MaxMagicNumberLength { get; }
    
    public bool IsIncludedExtensions(string extension) => Extensions.Contains(extension);

    public bool Validate(MemoryStream stream)
    {
        Span<byte> initialBytes = stackalloc byte[MaxMagicNumberLength];
        stream.Seek(0, SeekOrigin.Begin);
        _ = stream.Read(initialBytes);
        foreach (var magicNumber in MagicNumbers)
        {
            if (initialBytes[..magicNumber.Length].SequenceCompareTo(magicNumber) == 0)
            {
                return true;
            }
        }
        return false;
    }
    
}