namespace Assignment3.Entities;

public class TaskRepository : ITaskRepository
{
    private readonly KanbanContext _context;

    public TaskRepository(KanbanContext context)
    {
        _context = context;
    }
    public (Response Response, int TaskId) Create(TaskCreateDTO task)
    {
        // IEnumerable<Tag> _tags() {
        //     foreach (var tag in task.Tags)
        //     {
        //     yield return  _context.Tags.Where(t => t.Name == tag).First();
        //     }} 
        // Task newTask = new Task {
        //     Title = task.Title,
        //     AssignedTo = _context.Users.Where(u => task.AssignedToId == u.Id).First(),
        //     Description = task.Description,
        //     Tags = _tags()
        // };
        throw new NotImplementedException();
    }

    public Response Delete(int taskId)
    {
        var task = _context.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
        if (task == null) return Response.BadRequest;
        if(task.State == State.Active){
            task.State = State.Removed;
            _context.Update(task);
        }
        if (task.State != State.New) return Response.Conflict;
        _context.Remove(task);
        _context.SaveChanges();
        return Response.Deleted;
    }

    public TaskDetailsDTO Read(int taskId)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<TaskDTO> ReadAll()
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<TaskDTO> ReadAllByState(State state)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<TaskDTO> ReadAllRemoved()
    {
        throw new NotImplementedException();
    }

    public Response Update(TaskUpdateDTO task)
    {
        throw new NotImplementedException();
    }
}
