using System;
using System.IO;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace DVLD_PresentationAccess.Helpers
{
    public static class clsImageHelper
    {
        public static string SaveImage(string sourceFilePath, string folderName, string fileName)
        {
            if (string.IsNullOrEmpty(sourceFilePath))
                return string.Empty;

            string basePath = Application.StartupPath;

            string imagesPath = Path.Combine(basePath, "Resources", "Images", folderName);

            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            string destinationPath = Path.Combine(imagesPath, fileName);

            File.Copy(sourceFilePath, destinationPath, true);

            return destinationPath;
        }
        
    }
}