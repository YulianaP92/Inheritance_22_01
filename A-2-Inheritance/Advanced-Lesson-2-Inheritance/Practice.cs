using System;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Lesson_2_Inheritance
{
    public static partial class Practice
    {
        /// <summary>
        /// A.L2.P1/1. Создать консольное приложение, которое может выводить 
        /// на печатать введенный текст  одним из трех способов: 
        /// консоль, файл, картинка. 
        /// </summary>
        public static void A_L2_P1_1()
        {
            Console.WriteLine("Inter text for conclusion:");
            var text = Console.ReadLine();
            Console.WriteLine(value: "Choos print type:");
            Console.WriteLine(value: "1-Console");
            Console.WriteLine(value: "2-File");
            Console.WriteLine(value: "3-Image");
            var type = Console.ReadLine();
            IPrinter printer;
            switch (type)
            {
                case "1":
                    printer = new ConsolePrinter(ConsoleColor.Blue);
                    printer.Print(text);
                    break;
                case "2":
                    printer = new FilePrinter(fileName: "TEST");
                    printer.Print(text);
                    break;
                case "3":
                    printer = new ImagePrinter(Color.DarkRed, Color.AliceBlue);
                    printer.Print(text);
                    break;
                default:
                    break;
            }
        }
        public interface  IPrinter
        {
             void Print(string text);
        }

        public class ConsolePrinter : IPrinter
        {

            private ConsoleColor _color { get; set; }
            public ConsolePrinter(ConsoleColor color)
            {

                _color = color;
            }
            public void Print(string text)
            {
                Console.ForegroundColor = _color;
                Console.WriteLine(text);
                Console.ResetColor();
            }
        }
        public class ImagePrinter : IPrinter
        {
            public Font font = new Font("Arial", 50);
            public Color textColor { get; set; }
            public Color backColor { get; set; }
            public ImagePrinter(Color textColor, Color backColor)
            {
                this.textColor = textColor;
                this.backColor = backColor;
            }
            public  void Print(string text)
            {
                using (Image img = new Bitmap(1000, 1000))
                {
                    Graphics drawing = Graphics.FromImage(img);
                    //закрасить фон
                    drawing.Clear(backColor);
                    //залить текст цветом
                    Brush textBrush = new SolidBrush(textColor);
                    drawing.DrawString(text, font, textBrush, 30, 30);

                    img.Save("D:\\image1.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                Console.WriteLine("The text successfully saved to the picture");
            }
        }
        public class FilePrinter : IPrinter
        {
            public string FileName { get; set; }
            private string path;
            public FilePrinter(string fileName)
            {
                FileName = fileName;
                path = $@"D:\{FileName}.txt";
            }

            public void Print(string text)
            {
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                    Console.WriteLine("The text file was successfully recorded");
                }
            }
        }
    }
}
