
using System.Collections.Generic;
public class TodosVm
{
	public IList<PriorityLevelDto> PriorityLevels { get; set; }

	public IList<TodoListDto> Lists { get; set; }
}