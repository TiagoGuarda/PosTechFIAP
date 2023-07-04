namespace TC1WebApp.Interfaces
{
    public interface IAPIService
    {
        bool AddFileRecord(string fileName, string filePath);
        IEnumerable<FileRecordViewModel> GetAllFiles();
    }
}
