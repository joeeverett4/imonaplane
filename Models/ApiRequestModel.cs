
public class ApiRequestModel
{
    public ApiSlice[] Slices { get; set; }
}

public class ApiSlice
{
    public string Origin { get; set; }
    public string Destination { get; set; }
}
