namespace ClientService.Application.Files.Models;

public class UploadFileResponse
{
    public UploadFileResponse(string fileName)
    {
        FileName = fileName;
    }

    public string FileName { get; private set; }
}