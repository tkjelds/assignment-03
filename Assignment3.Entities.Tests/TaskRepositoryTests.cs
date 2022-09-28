namespace Assignment3.Entities.Tests;

public class TaskRepositoryTests
{
    [Fact]
    public void TaskCreateTest()
    {
        // Given
        KanbanContext _context = new KanbanContext();
        TaskRepository tr = new TaskRepository(_context);
        _context.Tasks.RemoveRange(_context.Tasks);
        var user = new User(){Name = "test",Email = "Test@test@Test",Id = 42};
        _context.Users.Add(user);
        _context.SaveChanges();
        // When
        TaskCreateDTO tcd = new TaskCreateDTO("test",42,"test",new [] {"test","test2"});
        var actual = tr.Create(tcd);
        _context.Users.Remove(user);
        _context.SaveChanges();
        // Then
        Assert.Equal(Response.Created,actual.Response);
    }

    [Fact]
    public void TaskCreateFailUserNotExist()
    {
        // Given
        KanbanContext _context = new KanbanContext();
        TaskRepository tr = new TaskRepository(_context);
        _context.Tasks.RemoveRange(_context.Tasks);
        // When
        TaskCreateDTO tcd = new TaskCreateDTO("test",42,"test",new [] {"test","test2"});
        var actual = tr.Create(tcd);
        _context.SaveChanges();
        // Then
        Assert.Equal(Response.BadRequest,actual.Response);
    }
}
