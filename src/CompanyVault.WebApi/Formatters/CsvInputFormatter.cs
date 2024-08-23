using Microsoft.AspNetCore.Mvc.Formatters;

namespace CompanyVault.WebApi.Formatters;

/// <summary>
/// Implements CSV input formatter for text/csv media type.
/// </summary>
public class CsvInputFormatter : InputFormatter
{
    public CsvInputFormatter()
    {
        SupportedMediaTypes.Add("text/csv");
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
    {
        var streamReader = new StreamReader(context.HttpContext.Request.Body);
        var result = await streamReader.ReadToEndAsync();
        return await InputFormatterResult.SuccessAsync(result);
    }

    public override bool CanRead(InputFormatterContext context)
    {
        var contentType = context.HttpContext.Request.ContentType;
        return contentType != null && SupportedMediaTypes.Contains(contentType);
    }
}
