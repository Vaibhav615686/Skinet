using System;

namespace API.Models;

public class ServerResponse<T>
{
    public T Data { get; set; }
    public bool IsSuccess { get; set; }

    public string Message { get; set; } = "";
}
