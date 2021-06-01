namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWriter
    {
        void PrintLine();
        void PrintLine(string input);
        string ReadLine(string message);
    }
}