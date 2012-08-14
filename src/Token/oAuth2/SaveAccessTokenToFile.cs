using System;
using System.IO;
using System.IO.IsolatedStorage;

public class SaveAccessTokenToFile
{
    public void WriteToFile(String path, byte[] jsonBlob, IsolatedStorageFile isolatedFile)
    {
        if (isolatedFile.FileExists(path))
        {
            isolatedFile.DeleteFile(path);
        }
        var isolatedStorage = new IsolatedStorageFileStream(path, System.IO.FileMode.Create, isolatedFile);
        using (var writer = new BinaryWriter(isolatedStorage))
        {
            writer.Write(jsonBlob);
            writer.Close();
        }
    }
}