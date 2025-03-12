namespace Domain.Abstractions
{
    public static class ProgramError
    {
        static public readonly Error PROGRAM_NOT_EXISTS = new("PROGRAM_NOT_EXISTS", "This program does not exist.");
    }
}
