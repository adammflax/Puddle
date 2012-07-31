using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace IsolatedStorage.oAuth2
{
    public class ReadAccessTokenFromFile
    {

        public byte[] ReadFile(String path, IsolatedStorageFile isolatedFile)
        {
            if (!isolatedFile.FileExists(path))
            {
                return null;
            }

            byte[] data;
            using (var stream = new IsolatedStorageFileStream(path, FileMode.Open, isolatedFile))
            using (var reader = new BinaryReader(stream))
            {
                data = reader.ReadBytes((int) stream.Length);
            }

            return data;
        }
    }
}