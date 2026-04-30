using Application.Interfaces.Services;
using Application.Responses.Planning;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace Infrastructure.Services
{
    public class SchedulePdfService : ISchedulePdfService
    {
        public byte[] GenerateSchedulePdf(List<MonthlyPlanningResponse> data, string departmentName, string? departmentShortName, int month, int year)
        {
            var culture = new CultureInfo("fr-FR");
            var monthName = culture.DateTimeFormat.GetMonthName(month);
            var deptLabel = !string.IsNullOrWhiteSpace(departmentShortName) ? departmentShortName.ToUpper() : departmentName.ToUpper();
            var title = $"PLANIFICATION ({deptLabel})";
            var generatedAt = DateTime.Now.ToString("dd/MM/yyyy HH:mm", culture);

            // Flatten data
            var rows = new List<FlatRow>();
            foreach (var prg in data)
            {
                foreach (var d in prg.Dates)
                {
                    foreach (var svc in d.Services)
                    {
                        foreach (var m in svc.Members)
                        {
                            rows.Add(new FlatRow
                            {
                                Date = d.Date,
                                ProgramName = prg.ProgramName,
                                ProgramShortName = prg.ProgramShortName,
                                ServiceName = svc.ServiceName,
                                PosteName = m.PosteName ?? "-",
                                PosteDisplayOrder = m.PosteDisplayOrder,
                                MemberName = m.MemberName,
                                IndTraining = m.IndTraining
                            });
                        }
                    }
                }
            }

            // Group by week
            var weeks = rows
                .GroupBy(r =>
                {
                    var d = r.Date.ToDateTime(TimeOnly.MinValue);
                    var day = (int)d.DayOfWeek;
                    var diff = d.AddDays(-(day == 0 ? 6 : day - 1));
                    return DateOnly.FromDateTime(diff);
                })
                .OrderBy(g => g.Key)
                .Select((g, idx) => BuildWeek(g.ToList(), idx + 1, culture))
                .ToList();

            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(20);

                    page.Header().Element(h =>
                    {
                        h.Text(title).FontSize(16).Bold().AlignCenter();
                    });

                    page.Footer().Element(f =>
                    {
                        f.Row(row =>
                        {
                            row.RelativeItem().Text($"{char.ToUpper(monthName[0])}{monthName[1..]} {year}").FontSize(8);
                            row.RelativeItem().Text(text =>
                            {
                                text.AlignCenter();
                                text.CurrentPageNumber().FontSize(8);
                                text.Span(" / ").FontSize(8);
                                text.TotalPages().FontSize(8);
                            });
                            row.RelativeItem().Text($"Généré le {generatedAt}").FontSize(8).AlignRight();
                        });
                    });

                    page.Content().Element(content =>
                    {
                        content.Column(col =>
                        {
                            foreach (var week in weeks)
                            {
                                // Adapter la police selon le nombre de colonnes
                                var colCount = week.Columns.Count;
                                var headerFont = colCount > 6 ? 7f : colCount > 4 ? 8f : 9f;
                                var subHeaderFont = colCount > 6 ? 6f : colCount > 4 ? 7f : 8f;
                                var posteFont = colCount > 6 ? 7f : colCount > 4 ? 8f : 9f;
                                var memberFont = colCount > 6 ? 7f : colCount > 4 ? 8f : 9f;
                                var trainingFont = colCount > 6 ? 5f : colCount > 4 ? 6f : 7f;

                                col.Item().Table(table =>
                                {
                                    // Columns: poste label + one per date-service column
                                    table.ColumnsDefinition(cd =>
                                    {
                                        cd.ConstantColumn(80);
                                        foreach (var _ in week.Columns)
                                        {
                                            cd.RelativeColumn();
                                        }
                                    });

                                    uint currentRow = 1;

                                    // === Row 1: Semaine N + dates (merged if same day) ===
                                    table.Cell().Row(currentRow).Column(1)
                                        .Border(0.5f).Background(Colors.Grey.Lighten3)
                                        .Padding(4).Text($"Semaine {week.WeekNum}").FontSize(posteFont).Bold();

                                    // Group columns by date+program for merging
                                    var dateGroups = week.Columns
                                        .Select((c, i) => new { Col = c, Idx = i })
                                        .GroupBy(x => new { x.Col.Date, x.Col.ProgramName })
                                        .ToList();

                                    foreach (var dg in dateGroups)
                                    {
                                        var firstIdx = dg.First().Idx;
                                        var span = (uint)dg.Count();
                                        var dateStr = dg.Key.Date.ToString("dddd dd MMMM", culture);
                                        var prgLabel = !string.IsNullOrWhiteSpace(dg.First().Col.ProgramShortName) ? dg.First().Col.ProgramShortName : dg.Key.ProgramName;

                                        table.Cell().Row(currentRow).Column((uint)(firstIdx + 2)).ColumnSpan(span)
                                            .Border(0.5f).Background(Colors.Grey.Lighten3)
                                            .Padding(3).Column(cell =>
                                            {
                                                cell.Item().Text($"{char.ToUpper(dateStr[0])}{dateStr[1..]}").FontSize(headerFont).Bold().AlignCenter();
                                                cell.Item().Text(prgLabel).FontSize(trainingFont).AlignCenter();
                                            });
                                    }

                                    currentRow++;

                                    // === Row 2: Programme / Service per column ===
                                    table.Cell().Row(currentRow).Column(1)
                                        .Border(0.5f).Background(Colors.Grey.Lighten4)
                                        .Padding(3);

                                    for (int i = 0; i < week.Columns.Count; i++)
                                    {
                                        var c = week.Columns[i];

                                        table.Cell().Row(currentRow).Column((uint)(i + 2))
                                            .Border(0.5f).Background(Colors.Grey.Lighten4)
                                            .Padding(3).Text(c.ServiceName).FontSize(subHeaderFont).Bold().AlignCenter();
                                    }

                                    currentRow++;

                                    // === Poste rows (all members, training marked with "(Formation)") ===
                                    foreach (var poste in week.Postes)
                                    {
                                        table.Cell().Row(currentRow).Column(1)
                                            .Border(0.5f).Background(Colors.Grey.Lighten5)
                                            .Padding(4).Text(poste).FontSize(posteFont).Bold();

                                        for (int i = 0; i < week.Columns.Count; i++)
                                        {
                                            var c = week.Columns[i];
                                            var members = week.Entries
                                                .Where(e => e.Date == c.Date && e.ServiceName == c.ServiceName
                                                    && e.ProgramName == c.ProgramName && e.PosteName == poste)
                                                .ToList();

                                            table.Cell().Row(currentRow).Column((uint)(i + 2))
                                                .Border(0.5f)
                                                .Padding(3).Column(cell =>
                                                {
                                                    foreach (var m in members)
                                                    {
                                                        if (m.IndTraining)
                                                        {
                                                            cell.Item().Text(text =>
                                                            {
                                                                text.AlignCenter();
                                                                text.Span(m.MemberName).FontSize(memberFont).FontColor(Colors.Red.Medium);
                                                                text.Span(" (Formation)").FontSize(trainingFont).FontColor(Colors.Red.Medium).Italic();
                                                            });
                                                        }
                                                        else
                                                        {
                                                            cell.Item().Text(m.MemberName).FontSize(memberFont).AlignCenter();
                                                        }
                                                    }
                                                });
                                        }
                                        currentRow++;
                                    }
                                });
                            }
                        });
                    });
                });
            });

            using var stream = new MemoryStream();
            doc.GeneratePdf(stream);
            return stream.ToArray();
        }

        public byte[] GenerateDailyPdf(List<MonthlyPlanningResponse> data, string departmentName, string? departmentShortName, DateOnly date)
        {
            var culture = new CultureInfo("fr-FR");
            var deptLabel = !string.IsNullOrWhiteSpace(departmentShortName) ? departmentShortName.ToUpper() : departmentName.ToUpper();
            var dateStr = date.ToString("dddd dd MMMM yyyy", culture);
            var title = $"PLANIFICATION ({deptLabel}) — {char.ToUpper(dateStr[0])}{dateStr[1..]}";
            var generatedAt = DateTime.Now.ToString("dd/MM/yyyy HH:mm", culture);

            // Filter data for this date only
            var filteredData = data.Select(prg => new MonthlyPlanningResponse
            {
                ProgramName = prg.ProgramName,
                ProgramShortName = prg.ProgramShortName,
                Dates = prg.Dates.Where(d => d.Date == date).ToList()
            }).Where(prg => prg.Dates.Any()).ToList();

            // Flatten
            var rows = new List<FlatRow>();
            foreach (var prg in filteredData)
            {
                foreach (var d in prg.Dates)
                {
                    foreach (var svc in d.Services)
                    {
                        foreach (var m in svc.Members)
                        {
                            rows.Add(new FlatRow
                            {
                                Date = d.Date,
                                ProgramName = prg.ProgramName,
                                ProgramShortName = prg.ProgramShortName,
                                ServiceName = svc.ServiceName,
                                PosteName = m.PosteName ?? "-",
                                PosteDisplayOrder = m.PosteDisplayOrder,
                                MemberName = m.MemberName,
                                IndTraining = m.IndTraining
                            });
                        }
                    }
                }
            }

            var columns = rows
                .Select(r => new { r.ProgramName, r.ProgramShortName, r.ServiceName })
                .Distinct()
                .OrderBy(c => c.ProgramName)
                .ThenBy(c => c.ServiceName)
                .ToList();

            var postes = rows
                .GroupBy(r => r.PosteName)
                .Select(g => new { Name = g.Key, Order = g.Min(r => r.PosteDisplayOrder) ?? int.MaxValue })
                .OrderBy(p => p.Order)
                .ThenBy(p => p.Name)
                .Select(p => p.Name)
                .ToList();

            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(25);

                    page.Header().Element(h =>
                    {
                        h.Text(title).FontSize(14).Bold().AlignCenter();
                    });

                    page.Footer().Element(f =>
                    {
                        f.Row(row =>
                        {
                            row.RelativeItem().Text($"Généré le {generatedAt}").FontSize(8);
                            row.RelativeItem().Text(text =>
                            {
                                text.AlignRight();
                                text.CurrentPageNumber().FontSize(8);
                                text.Span(" / ").FontSize(8);
                                text.TotalPages().FontSize(8);
                            });
                        });
                    });

                    page.Content().Element(content =>
                    {
                        if (!rows.Any())
                        {
                            content.Text("Aucune assignation pour cette date.").FontSize(12).AlignCenter();
                            return;
                        }

                        content.Table(table =>
                        {
                            table.ColumnsDefinition(cd =>
                            {
                                cd.ConstantColumn(100);
                                foreach (var _ in columns)
                                {
                                    cd.RelativeColumn();
                                }
                            });

                            // Header: Programme / Service
                            table.Cell().Row(1).Column(1)
                                .Border(0.5f).Background(Colors.Grey.Lighten3)
                                .Padding(5);

                            for (int i = 0; i < columns.Count; i++)
                            {
                                var c = columns[i];
                                var prgLabel = !string.IsNullOrWhiteSpace(c.ProgramShortName) ? c.ProgramShortName : c.ProgramName;

                                table.Cell().Row(1).Column((uint)(i + 2))
                                    .Border(0.5f).Background(Colors.Grey.Lighten3)
                                    .Padding(5).Column(cell =>
                                    {
                                        cell.Item().Text(prgLabel).FontSize(9).Bold().AlignCenter();
                                        cell.Item().Text(c.ServiceName).FontSize(8).AlignCenter();
                                    });
                            }

                            // Poste rows
                            uint rowIdx = 2;
                            foreach (var poste in postes)
                            {
                                table.Cell().Row(rowIdx).Column(1)
                                    .Border(0.5f).Background(Colors.Grey.Lighten5)
                                    .Padding(5).Text(poste).FontSize(10).Bold();

                                for (int i = 0; i < columns.Count; i++)
                                {
                                    var c = columns[i];
                                    var members = rows
                                        .Where(r => r.PosteName == poste && r.ProgramName == c.ProgramName && r.ServiceName == c.ServiceName)
                                        .ToList();

                                    table.Cell().Row(rowIdx).Column((uint)(i + 2))
                                        .Border(0.5f)
                                        .Padding(4).Column(cell =>
                                        {
                                            foreach (var m in members)
                                            {
                                                if (m.IndTraining)
                                                {
                                                    cell.Item().Text(text =>
                                                    {
                                                        text.AlignCenter();
                                                        text.Span(m.MemberName).FontSize(10).FontColor(Colors.Red.Medium);
                                                        text.Span(" (Formation)").FontSize(8).FontColor(Colors.Red.Medium).Italic();
                                                    });
                                                }
                                                else
                                                {
                                                    cell.Item().Text(m.MemberName).FontSize(10).AlignCenter();
                                                }
                                            }
                                        });
                                }
                                rowIdx++;
                            }
                        });
                    });
                });
            });

            using var stream = new MemoryStream();
            doc.GeneratePdf(stream);
            return stream.ToArray();
        }

        private static WeekData BuildWeek(List<FlatRow> entries, int weekNum, CultureInfo culture)
        {
            var columns = entries
                .Select(r => new ColumnInfo
                {
                    Date = r.Date,
                    ServiceName = r.ServiceName,
                    ProgramName = r.ProgramName,
                    ProgramShortName = r.ProgramShortName
                })
                .Distinct(new ColumnInfoComparer())
                .OrderBy(c => c.Date)
                .ThenBy(c => c.ServiceName)
                .ToList();

            var postes = entries
                .GroupBy(r => r.PosteName)
                .Select(g => new { Name = g.Key, Order = g.Min(r => r.PosteDisplayOrder) ?? int.MaxValue })
                .OrderBy(p => p.Order)
                .ThenBy(p => p.Name)
                .Select(p => p.Name)
                .ToList();

            return new WeekData { WeekNum = weekNum, Entries = entries, Columns = columns, Postes = postes };
        }

        private class WeekData
        {
            public int WeekNum { get; set; }
            public List<FlatRow> Entries { get; set; } = [];
            public List<ColumnInfo> Columns { get; set; } = [];
            public List<string> Postes { get; set; } = [];
        }

        private class ColumnInfo
        {
            public DateOnly Date { get; set; }
            public string ServiceName { get; set; } = string.Empty;
            public string ProgramName { get; set; } = string.Empty;
            public string? ProgramShortName { get; set; }
        }

        private class ColumnInfoComparer : IEqualityComparer<ColumnInfo>
        {
            public bool Equals(ColumnInfo? x, ColumnInfo? y)
            {
                if (x == null || y == null) { return false; }
                return x.Date == y.Date && x.ServiceName == y.ServiceName && x.ProgramName == y.ProgramName;
            }

            public int GetHashCode(ColumnInfo obj)
            {
                return HashCode.Combine(obj.Date, obj.ServiceName, obj.ProgramName);
            }
        }

        private class FlatRow
        {
            public DateOnly Date { get; set; }
            public string ProgramName { get; set; } = string.Empty;
            public string? ProgramShortName { get; set; }
            public string ServiceName { get; set; } = string.Empty;
            public string PosteName { get; set; } = string.Empty;
            public int? PosteDisplayOrder { get; set; }
            public string MemberName { get; set; } = string.Empty;
            public bool IndTraining { get; set; }
        }
    }
}
