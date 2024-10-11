using System;

public interface IReportBuilder
{
    void SetHeader(string header);
    void SetContent(string content);
    void SetFooter(string footer);
    Report GetReport();
}

public class TextReportBuilder : IReportBuilder
{
    private readonly Report _report = new Report();

    public void SetHeader(string header)
    {
        _report.Header = header;
    }

    public void SetContent(string content)
    {
        _report.Content = content;
    }

    public void SetFooter(string footer)
    {
        _report.Footer = footer;
    }

    public Report GetReport()
    {
        return _report;
    }
}

public class HtmlReportBuilder : IReportBuilder
{
    private readonly Report _report = new Report();

    public void SetHeader(string header)
    {
        _report.Header = $"<h1>{header}</h1>";
    }

    public void SetContent(string content)
    {
        _report.Content = $"<p>{content}</p>";
    }

    public void SetFooter(string footer)
    {
        _report.Footer = $"<footer>{footer}</footer>";
    }

    public Report GetReport()
    {
        return _report;
    }
}

public class ReportDirector
{
    public void ConstructReport(IReportBuilder builder)
    {
        builder.SetHeader("Отчет");
        builder.SetContent("Содержимое отчета");
        builder.SetFooter("Подвал отчета");
    }
}

public class Report
{
    public string Header { get; set; }
    public string Content { get; set; }
    public string Footer { get; set; }

    public override string ToString()
    {
        return $"{Header}\n{Content}\n{Footer}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        ReportDirector director = new ReportDirector();

        // Создание текстового отчета
        IReportBuilder textBuilder = new TextReportBuilder();
        director.ConstructReport(textBuilder);
        Report textReport = textBuilder.GetReport();
        Console.WriteLine("Текстовый отчет:");
        Console.WriteLine(textReport);

        // Создание HTML отчета
        IReportBuilder htmlBuilder = new HtmlReportBuilder();
        director.ConstructReport(htmlBuilder);
        Report htmlReport = htmlBuilder.GetReport();
        Console.WriteLine("\nHTML отчет:");
        Console.WriteLine(htmlReport);
    }
}
