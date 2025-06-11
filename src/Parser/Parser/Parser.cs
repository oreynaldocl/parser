using Microsoft.Extensions.Primitives;
using Parser.Interfaces;
using System.Xml;

namespace Parser.Parser;

public class Parser : IParser
{
    private readonly ILogger<Parser> _logger;
    public Parser(ILogger<Parser> logger)
    {
        _logger = logger;
    }
    public async Task ParseAsync(string[] files)
    {
        foreach (var file in files)
        {
            _logger.LogInformation("Parsing file: {file}", file);
            string content = await GetContentAsync(file);
            await ParserProcessAsync(content);
        }
    }

    internal void ShowNodeInformation(XmlNode node, string type)
    {
        if (node != null)
        {
            _logger.LogInformation($"{type}: {node.InnerText.Trim()}");
        }
        else
        {
            _logger.LogInformation($"{type}: Not found (XPath: //hl7:code/hl7:originalText/hl7:paragraph)");
        }
    }

    internal async Task ParserProcessAsync(string splXmlData)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(splXmlData);

        // Initialize Namespace Manager
        var _nsManager = new XmlNamespaceManager(doc.NameTable);
        // The most common namespace is "urn:hl7-org:v3"
        _nsManager.AddNamespace("hl7", "urn:hl7-org:v3");
        // You might find other namespaces, add them here as needed
        // _nsManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

        _logger.LogInformation("--- Extracted Dupixent Information ---");

        // 1. Extract Drug Name (Product Type)
        // This path might vary slightly based on the specific SPL version.
        // Look for the <code originalText="..."> or <title> within the initial sections.
        string drugNameXPath = "//hl7:code/hl7:originalText/hl7:paragraph";
        XmlNode drugNameNode = doc.SelectSingleNode(drugNameXPath, _nsManager);
        XmlNode titleNode = doc.SelectSingleNode("/hl7:document/hl7:title", _nsManager);
        ShowNodeInformation(titleNode, "Title");
       

        // 2. Extract Indications and Usage
        // LOINC code for "Indications and Usage" is 34067-9
        string indicationsXPath = "//hl7:section[hl7:code/@code='34067-9']/hl7:text";
        XmlNode indicationsNode = doc.SelectSingleNode(indicationsXPath, _nsManager);
        if (indicationsNode != null)
        {
            _logger.LogInformation("Indications and Usage:");
            // InnerXml will give you the HTML-like content including <p> tags
            // You might need to strip HTML tags if you want plain text.
            _logger.LogInformation(StripHtmlTags(indicationsNode.InnerXml).Trim());
        }
        else
        {
            _logger.LogInformation("Indications and Usage: Not found (LOINC code 34067-9)");
        }

        // 3. Extract Dosage and Administration
        // LOINC code for "Dosage and Administration" is 34068-7
        string dosageXPath = "//hl7:section[hl7:code/@code='34068-7']/hl7:text";
        XmlNode dosageNode = doc.SelectSingleNode(dosageXPath, _nsManager);
        if (dosageNode != null)
        {
            _logger.LogInformation("Dosage and Administration:");
            _logger.LogInformation(StripHtmlTags(dosageNode.InnerXml).Trim());
        }
        else
        {
            _logger.LogInformation("Dosage and Administration: Not found (LOINC code 34068-7)");
        }

        // 4. Extract Contraindications
        // LOINC code for "Contraindications" is 34070-3
        string contraindicationsXPath = "//hl7:section[hl7:code/@code='34070-3']/hl7:text";
        XmlNode contraindicationsNode = doc.SelectSingleNode(contraindicationsXPath, _nsManager);
        if (contraindicationsNode != null)
        {
            _logger.LogInformation("Contraindications:");
            _logger.LogInformation(StripHtmlTags(contraindicationsNode.InnerXml).Trim());
        }
        else
        {
            _logger.LogInformation("Contraindications: Not found (LOINC code 34070-3)");
        }

        // Add more sections as needed (e.g., Warnings and Precautions, Adverse Reactions)
        // You'll need to find their corresponding LOINC codes or unique identifiers within the SPL.
        // Example for Adverse Reactions (LOINC code 34084-4):
        string adverseReactionsXPath = "//hl7:section[hl7:code/@code='34084-4']/hl7:text";
        XmlNode adverseReactionsNode = doc.SelectSingleNode(adverseReactionsXPath, _nsManager);
        if (adverseReactionsNode != null)
        {
            _logger.LogInformation("Adverse Reactions:");
            _logger.LogInformation(StripHtmlTags(adverseReactionsNode.InnerXml).Trim());
        }
        else
        {
            _logger.LogInformation("Adverse Reactions: Not found (LOINC code 34084-4)");
        }
    }
    /// <summary>
    /// Simple utility to strip basic HTML tags from a string.
    /// You might need a more robust HTML parser for complex scenarios.
    /// </summary>
    private static string StripHtmlTags(string html)
    {
        if (string.IsNullOrEmpty(html))
            return string.Empty;

        // Use a simple regex to remove HTML tags. This is not robust for all HTML.
        return System.Text.RegularExpressions.Regex.Replace(html, "<.*?>", string.Empty);
    }

    internal async Task<string> GetContentAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            _logger.LogWarning("File not found: {filePath}", filePath);
            return string.Empty;
        }

        try
        {
            return await File.ReadAllTextAsync(filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading file: {filePath}", filePath);
            return string.Empty;
        }
    }
}
