using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.UI;
using System.Drawing;
using System.Reflection;

namespace Esperanto
{
    public static class DataDumpers
    {
        static string jsToggleFunction =
            "function toggle(showHide) {\n" +
            " var table = document.getElementById(showHide);\n" +
            " table.style.display = (table.style.display != \"table\" ? \"table\" : \"none\");\n" +
            "}\n";

        static string css =
            "body { font-family:sans-serif; }\n" +
            "a { text-decoration:none; }\n" +
            ".header { background-color:rgb(196,196,196); }\n" +
            ".header-text { text-align:left; float:left; }\n" +
            ".header-toggle { text-align:right; display:block; }\n" +
            "table { border:1px solid black; display:none; border-collapse:collapse; width:100%; }\n" +
            "tr { border-bottom:1px solid rgb(191,191,191); vertical-align:top; }\n" +
            "td { font-family: \"DejaVu Sans Mono\", \"Lucida Console\", Monaco, monospace; }\n" +
            ".header-id { border-right:1px solid rgb(191,191,191); width:5em; }\n" +
            ".header-desc-text { width:40em; }\n" +
            ".header-desc-sim { white-space:nowrap; }\n" +
            ".message-id { border-right:1px solid rgb(191,191,191); width:5em; }\n" +
            ".message-desc-text { width:40em; }\n" +
            ".message-desc-sim { white-space:nowrap; text-align:center; }\n" +
            ".message-desc-suspicious { width:40em; background-color:rgb(240,224,224); }\n" +
            "span.tooltip { border-bottom:2px black dotted; cursor:help; }\n" +
            "span.tooltip span { display:none; padding:2px 3px; margin-left:8px; max-width:30em; }\n" +
            "span.tooltip:hover span { display:inline; position:absolute; background-color:white; border:1px solid #cccccc; color:#6c6c6c; }\n";

        private static void WriteHeader(HtmlTextWriter html, string title)
        {
            html.RenderBeginTag(HtmlTextWriterTag.Head);
            {
                html.RenderBeginTag(HtmlTextWriterTag.Title);
                {
                    html.Write(title);
                }
                html.RenderEndTag();

                html.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
                html.RenderBeginTag(HtmlTextWriterTag.Script);
                {
                    html.Write(jsToggleFunction);
                }
                html.RenderEndTag();

                html.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
                html.RenderBeginTag(HtmlTextWriterTag.Style);
                {
                    html.Write(css);
                }
                html.RenderEndTag();
            }
            html.RenderEndTag();
        }

        public static void DumpComparison(string filename, RomHandler romPrimary, RomHandler romSecondary)
        {
            TextWriter writer = File.CreateText(filename);
            using (HtmlTextWriter html = new HtmlTextWriter(writer))
            {
                html.WriteLine("<!DOCTYPE html>");
                html.RenderBeginTag(HtmlTextWriterTag.Html);
                {
                    WriteHeader(html, string.Format("{0} Message Comparison", System.Windows.Forms.Application.ProductName));

                    html.RenderBeginTag(HtmlTextWriterTag.Body);
                    {
                        html.RenderBeginTag(HtmlTextWriterTag.H2);
                        {
                            if (romSecondary != null)
                                html.WriteEncodedText(string.Format("Comparison between {0} ({1}) and {2} ({3})", Path.GetFileName(romPrimary.FileName), romPrimary.Language, Path.GetFileName(romSecondary.FileName), romSecondary.Language));
                            else
                                html.WriteEncodedText(string.Format("Dump of {0} ({1})", Path.GetFileName(romPrimary.FileName), romPrimary.Language));
                        }
                        html.RenderEndTag();

                        html.RenderBeginTag(HtmlTextWriterTag.H4);
                        {
                            Version ver = new Version(System.Windows.Forms.Application.ProductVersion);
                            html.WriteEncodedText(string.Format("{0} v{1}.{2}", System.Windows.Forms.Application.ProductName, ver.Major, ver.Minor));
                            html.WriteEncodedText(" - ");
                            html.WriteEncodedText(string.Format("{0}", (Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute).Copyright));
                        }
                        html.RenderEndTag();

                        int numBanks = 0;

                        if (romSecondary != null)
                            numBanks = (romPrimary.MessageBanks.Length == romSecondary.MessageBanks.Length ? romPrimary.MessageBanks.Length : Math.Max(romPrimary.MessageBanks.Length, romSecondary.MessageBanks.Length));
                        else
                            numBanks = romPrimary.MessageBanks.Length;

                        for (int i = 0; i < numBanks; i++)
                        {
                            MessageBank[] langBanks = new MessageBank[]
                            {
                                (romPrimary != null && i < romPrimary.MessageBanks.Length ? romPrimary.MessageBanks[i] : null),
                                (romSecondary != null && i < romSecondary.MessageBanks.Length ? romSecondary.MessageBanks[i] : null)
                            };
                            string bankId = string.Format("bank-{0:D4}", i);

                            html.AddAttribute(HtmlTextWriterAttribute.Class, "header");
                            html.RenderBeginTag(HtmlTextWriterTag.Div);
                            {
                                html.AddAttribute(HtmlTextWriterAttribute.Class, "header-text");
                                html.RenderBeginTag(HtmlTextWriterTag.Span);
                                {
                                    if (romPrimary.MessageBanks[i].Number == -1)
                                        html.Write("General");
                                    else
                                        html.Write("Bank {0}", romPrimary.MessageBanks[i].Number);
                                }
                                html.RenderEndTag();
                                html.AddAttribute(HtmlTextWriterAttribute.Class, "header-toggle");
                                html.RenderBeginTag(HtmlTextWriterTag.Span);
                                {
                                    html.AddAttribute(HtmlTextWriterAttribute.Href, string.Format("javascript:toggle('{0}');", bankId), false);
                                    html.RenderBeginTag(HtmlTextWriterTag.A);
                                    {
                                        html.Write("+/-");
                                    }
                                    html.RenderEndTag();
                                }
                                html.RenderEndTag();
                            }
                            html.RenderEndTag();

                            html.AddAttribute(HtmlTextWriterAttribute.Id, bankId);
                            html.AddStyleAttribute(HtmlTextWriterStyle.Display, "table");
                            html.RenderBeginTag(HtmlTextWriterTag.Table);
                            {
                                html.RenderBeginTag(HtmlTextWriterTag.Tr);
                                {
                                    html.AddAttribute(HtmlTextWriterAttribute.Class, "header-id");
                                    html.RenderBeginTag(HtmlTextWriterTag.Th);
                                    {
                                        html.Write("ID");
                                    }
                                    html.RenderEndTag();

                                    html.AddAttribute(HtmlTextWriterAttribute.Class, "header-desc-text");
                                    html.RenderBeginTag(HtmlTextWriterTag.Th);
                                    {
                                        html.Write("Primary ({0})", romPrimary.Language);
                                    }
                                    html.RenderEndTag();

                                    if (romSecondary != null)
                                    {
                                        html.AddAttribute(HtmlTextWriterAttribute.Class, "header-desc-text");
                                        html.RenderBeginTag(HtmlTextWriterTag.Th);
                                        {
                                            html.Write("Secondary ({0})", romSecondary.Language);
                                        }
                                        html.RenderEndTag();
                                    }

                                    html.AddAttribute(HtmlTextWriterAttribute.Class, "header-desc-sim");
                                    html.RenderBeginTag(HtmlTextWriterTag.Th);
                                    {
                                        html.Write("Primary ({0}; Sim)", romPrimary.Language);
                                    }
                                    html.RenderEndTag();

                                    if (romSecondary != null)
                                    {
                                        html.AddAttribute(HtmlTextWriterAttribute.Class, "header-desc-sim");
                                        html.RenderBeginTag(HtmlTextWriterTag.Th);
                                        {
                                            html.Write("Secondary ({0}; Sim)", romSecondary.Language);
                                        }
                                        html.RenderEndTag();
                                    }
                                }
                                html.RenderEndTag();

                                int numMessages = 0;

                                if (langBanks[1] != null)
                                    numMessages = (langBanks[0].Messages.Count == langBanks[1].Messages.Count ? langBanks[0].Messages.Count : Math.Max(langBanks[0].Messages.Count, langBanks[1].Messages.Count));
                                else
                                    numMessages = langBanks[0].Messages.Count;

                                for (int j = 0; j < numMessages; j++)
                                {
                                    html.RenderBeginTag(HtmlTextWriterTag.Tr);
                                    {
                                        html.AddAttribute(HtmlTextWriterAttribute.Class, "message-id");
                                        html.RenderBeginTag(HtmlTextWriterTag.Th);
                                        {
                                            html.Write("#{0}", j + 1);
                                        }
                                        html.RenderEndTag();

                                        string jpnMessage = (j < langBanks[0].Messages.Count ? langBanks[0].Messages[j].Text : string.Empty);

                                        for (int k = 0; k < langBanks.Length; k++)
                                        {
                                            if (langBanks[k] == null) continue;

                                            string message = (j < langBanks[k].Messages.Count ? langBanks[k].Messages[j].Text : string.Empty);

                                            if (k != 0 && jpnMessage.Equals(message) || message.StartsWith("エラー" + Environment.NewLine))
                                                html.AddAttribute(HtmlTextWriterAttribute.Class, "message-desc-suspicious");
                                            else
                                                html.AddAttribute(HtmlTextWriterAttribute.Class, "message-desc-text");

                                            html.RenderBeginTag(HtmlTextWriterTag.Td);
                                            {
                                                message = message.Replace(" ", "&nbsp;");
                                                message = message.Replace("<", "&lt;");
                                                message = message.Replace(">", "&gt;");
                                                message = message.Replace(Environment.NewLine, "<br />");
                                                message = message.Replace("[Highlight:On]", "<span style=\"color:#ff0000;\">");
                                                message = message.Replace("[Highlight:Off]", "</span>");
                                                html.Write(message);
                                            }
                                            html.RenderEndTag();
                                        }

                                        for (int k = 0; k < langBanks.Length; k++)
                                        {
                                            if (langBanks[k] == null) continue;

                                            Message message = langBanks[k].Messages[j];

                                            html.AddAttribute(HtmlTextWriterAttribute.Class, "message-desc-sim");
                                            html.RenderBeginTag(HtmlTextWriterTag.Td);
                                            {
                                                string imgFile = string.Format("img\\{0}-{1}-{2}.png", i, j, k);
                                                string imgPath = Path.Combine(Path.GetDirectoryName(filename), imgFile);

                                                if (!Directory.Exists(Path.GetDirectoryName(imgPath)))
                                                    Directory.CreateDirectory(Path.GetDirectoryName(imgPath));

                                                Bitmap image = (k == 0 ? romPrimary.GetMessageImage(message) : romSecondary.GetMessageImage(message));
                                                if (image == null) continue;

                                                image.Save(imgPath);

                                                html.AddAttribute(HtmlTextWriterAttribute.Src, imgFile);
                                                html.RenderBeginTag(HtmlTextWriterTag.Img);
                                                html.RenderEndTag();
                                                html.WriteBreak();
                                            }
                                            html.RenderEndTag();
                                        }
                                    }
                                    html.RenderEndTag();
                                }
                            }
                            html.RenderEndTag();
                            html.WriteBreak();
                        }
                    }
                    html.RenderEndTag();
                }
                html.RenderEndTag();
            }
            writer.Close();
        }
    }
}
