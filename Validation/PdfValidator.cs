namespace Po.Media.Validation;

internal class PdfValidator : FileValidatorBase
{
    public PdfValidator() : base(
        typeName:"PDF FILE",
        extensions:[".pdf"],
        magicNumbers:[
            [0x25, 0x50, 0x44, 0x46]
        ]) { }
}