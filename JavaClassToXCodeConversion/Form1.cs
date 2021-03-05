using JavaClassToXCodeConversion.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JavaClassToXCodeConversion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            var pathItem = new PathItem();
            string[] lines = txtInput.Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Array.ForEach(lines, l => l.Trim());

            string regexExpression = @"((super)\.)?{0}[ ]=[ ]+.(?<TEXT>\w+.*)";
            regexExpression = @"((super)\.)?{0}[ ]+(\=[ ])?(?<TEXT>[^\s]+)";
            //((super)\.)?public class[ ]+(\=[ ])?(?<TEXT>\w+)
            //((super)\.)?vyakhya\[\d\][ ]+(\=[ ].)?(?<TEXT>\w+.*)

            //until space found
            //((super)\.)?public class[ ]+(\=[ ].)?(?<TEXT>\w+?[ ])
            string[] searchItems = { "public class",@"vyakhya\[\d\]", "gurmukhiBani", "englishBani", "englishTranslation", "hindiBani" };
            foreach (var line in lines)
            {
                foreach (var searchItem in searchItems)
                {
                    string regEx = string.Format(regexExpression, searchItem);
                    if (Regex.IsMatch(line, regEx))
                    {
                        var match = Regex.Match(line, regEx);
                        var text = match.Groups[4].Value;
                        switch (searchItem)
                        {
                            case "public class":
                                pathItem.ClassName = text.RemoveGarbage();
                                break;
                            case @"vyakhya\[\d\]":
                                pathItem.VItems.Add(text.RemoveGarbage());
                                break;
                            case "gurmukhiBani":
                                pathItem.GText = text.RemoveGarbage();
                                break;
                            case "englishBani":
                                pathItem.EText = text.RemoveGarbage();
                                break;
                            case "englishTranslation":
                                pathItem.EVText = text.RemoveGarbage();
                                break;
                            case "hindiBani":
                                pathItem.HText = text.RemoveGarbage();
                                break;
                            }
                    }
                }
            }

            CreateFile(pathItem);
        }
    
    
        private void CreateFile(PathItem pathItem)
        {
            Console.WriteLine("Creating XCode files.");
        }
    }

    public static class UnwantedCharacters
    {
        public static string RemoveGarbage(this string input)
        {
            return input.Replace("\";", "").Replace("\"", "").Trim();
        }
    }
}
