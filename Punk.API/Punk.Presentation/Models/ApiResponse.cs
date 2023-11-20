namespace Punk.Presentation.Models;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public bool Success { get; set; }
    public object Data { get; set; }
    public string Message { get; set; }
}