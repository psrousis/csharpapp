namespace CSharpApp.Core.Dtos;
public record AddTodoRecord(
    [property: JsonProperty("userId")] int UserId,
    [property: JsonProperty("body")] string Body,
    [property: JsonProperty("title")] string Title);
