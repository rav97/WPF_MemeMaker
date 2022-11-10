using MemeMakerWPF.Properties;
using MvvmLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeMakerWPF.Models
{
    public class FileUploadModel
    {
        private string _fileName;
        private string _filePath;
        private string _contentType;
        private byte[] _fileData;

        public FileUploadModel(string path)
        {
            FilePath = path;
            FileName = Path.GetFileName(path);

            var extension = Path.GetExtension(path);

            if (Settings.Default.ALLOWED_EXTENSIONS.Contains(extension))
            {
                if (extension.ToLower() == "png")
                    ContentType = "image/png";
                else
                    ContentType = "image/jpeg";
            }
            else
                throw new ArgumentException("Unsupported file extension");

            FileData = File.ReadAllBytes(path);
        }

        public string FileName { get => _fileName; set => _fileName = value; }
        public string FilePath { get => _filePath; set => _filePath = value; }
        public string ContentType { get => _contentType; set => _contentType = value; }
        public byte[] FileData { get => _fileData; set => _fileData = value; }
    }
}
