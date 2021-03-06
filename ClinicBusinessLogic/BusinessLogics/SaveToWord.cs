using ClinicBusinessLogic.HelperModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ClinicBusinessLogic.BusinessLogics
{
    static class SaveToWord
    {
        public static void CreateDoc(WordInfoDoctor info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());

                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));

                var list = info.Receipts.GroupBy(disease => disease.DiseaseName, (name, cost) => new { Key = name, Price = cost, Count = cost.Count() });
                var j = 0;
                foreach (var disease in list)
                {
                    docBody.AppendChild(CreateParagraph(new WordParagraph
                    {
                        Texts = new List<(string, WordTextProperties)> { ("Наименование: " + disease.Key, new WordTextProperties { Size = "24" }) },
                        TextProperties = new WordTextProperties
                        {
                            Size = "24",
                            JustificationValues = JustificationValues.Both
                        }
                    }));
                    Table table = new Table();
                    TableProperties props = new TableProperties(
                    new TableBorders(
                    new TopBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 6
                    },
                    new BottomBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 6
                    },
                    new LeftBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 6
                    },
                    new RightBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 6
                    },
                    new InsideHorizontalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 6
                    },
                    new InsideVerticalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 6
                    }));

                    table.AppendChild<TableProperties>(props);
                    TableRow tr = new TableRow();
                    TableCell tc = new TableCell();
                    tc.Append(new Paragraph(new Run(new Text("Название лекарства"))));
                    tr.Append(tc);
                    tc = new TableCell();
                    tc.Append(new Paragraph(new Run(new Text("Общая доза"))));
                    tr.Append(tc);
                    tc = new TableCell();
                    tc.Append(new Paragraph(new Run(new Text("Приемов в день"))));
                    tr.Append(tc);
                    tc = new TableCell();
                    tc.Append(new Paragraph(new Run(new Text("Доктор"))));
                    tr.Append(tc);
                    table.Append(tr);

                    for (var i = 0; i < disease.Count; i++)
                    {
                        tr = new TableRow();
                        tc = new TableCell();
                        tc.Append(new Paragraph(new Run(new Text(info.Receipts[j].MedicineName.ToString()))));
                        tr.Append(tc);
                        tc = new TableCell();
                        tc.Append(new Paragraph(new Run(new Text(info.Receipts[j].Dose.ToString()))));
                        tr.Append(tc);
                        tc = new TableCell();
                        tc.Append(new Paragraph(new Run(new Text(info.Receipts[j].PerDose.ToString()))));
                        tr.Append(tc);
                        tc = new TableCell();
                        tc.Append(new Paragraph(new Run(new Text(info.Receipts[j].DoctorId.ToString()))));
                        tr.Append(tc);
                        table.Append(tr);
                        j++;
                    }
                    docBody.Append(table);
                }

                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }
        /// <summary>
        /// Настройки страницы
        /// </summary>
        /// <returns></returns>
        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }
        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();
                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = run.Item2.Size });
                    if (run.Item2.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text { Text = run.Item1, Space = SpaceProcessingModeValues.Preserve });
                    docParagraph.AppendChild(docRun);
                }
                return docParagraph;
            }
            return null;
        }
        /// <summary>
        /// Задание форматирования для абзаца
        /// </summary>
        /// <param name="paragraphProperties"></param>
        /// <returns></returns>
        private static ParagraphProperties CreateParagraphProperties(WordTextProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize { Val = paragraphProperties.Size });
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
    }
}
