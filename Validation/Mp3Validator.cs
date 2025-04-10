namespace Po.Media.Validation;

internal class Mp3Validator : FileValidatorBase
{
    public Mp3Validator() : base(
        typeName:"AUDIO FILE",
        extensions:[".mp3"],
        magicNumbers:[
            [0x49, 0x44, 0x33],
        ]) { }
}