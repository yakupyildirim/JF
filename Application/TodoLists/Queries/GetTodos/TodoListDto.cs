using System.Collections.Generic;

public class TodoListDto : IMapFrom<TodoList>
{
	public TodoListDto()
	{
		Items = new List<TodoItemDto>();
	}

	public int Id { get; set; }

	public string Title { get; set; }

	public IList<TodoItemDto> Items { get; set; }
}

