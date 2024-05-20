using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.XObjects;

namespace PDFimageConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string pdfFilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\test1.pdf";
            string targetFolderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\target";


            try
            {
                using (PdfDocument pdfDocument = PdfDocument.Open(pdfFilePath))
                {
                    int imageCount = 1;
                    foreach ( Page page in pdfDocument.GetPages())
                    {
                        List<XObjectImage> images = page.GetImages().Cast<XObjectImage>().ToList();
                        foreach (XObjectImage image in images)
                        {
                            byte[] imageRawBytes = image.RawBytes.ToArray();

                            using (FileStream stream = new FileStream($"{targetFolderPath}\\{imageCount}.png", FileMode.Create, FileAccess.Write))
                            using (BinaryWriter writer = new BinaryWriter(stream)) 
                            {
                                writer.Write(imageRawBytes);
                                writer.Flush();
                            }

                            imageCount++;
                        }
                    }
                }
            }

            catch (Exception)
            {

            }
           

            Console.ReadKey(true);
        }
    }
}
