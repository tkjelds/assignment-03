namespace Assignment3.Entities.Tests;

public class TagRepositoryTests
{
    [Fact]
    public void TagRepoCreateTest()
    {
        // Given
        KanbanContext _context = new KanbanContext();
        TagRepository tr = new TagRepository(_context);
        _context.Tags.RemoveRange(_context.Tags);
        var _tagDTO = new TagCreateDTO("test");
        // When 
        var response = tr.Create(_tagDTO);
        var actual = _context.Tags.Where(t => t.Name == "test").First();
        // Then
        Assert.Equal((Response.Created),response.Response);
    }
    [Fact]
    public void TagRepoDeleteTestWhenForce()
    {
        // Given
        KanbanContext _context = new KanbanContext();
        TagRepository tr = new TagRepository(_context);
        _context.Tags.RemoveRange(_context.Tags);
        var _tagDTO = new TagCreateDTO("test");
        // When
        var create = tr.Create(_tagDTO);
        var actual = tr.Delete(create.TagId, true);
        // Then
        Assert.Equal(Response.Deleted,actual);
    }
    [Fact]
    public void TagRepoDeleteTestWhenNoForce()
    {
        // Given
        KanbanContext _context = new KanbanContext();
        TagRepository tr = new TagRepository(_context);
        _context.Tags.RemoveRange(_context.Tags);
        var _tagDTO = new TagCreateDTO("test");
        // When
        var create = tr.Create(_tagDTO);
        var actual = tr.Delete(create.TagId, false);
        // Then
        Assert.Equal(Response.Conflict,actual);
    }
    [Fact]
    public void TagRepoRead()
    {
        // Given
        KanbanContext _context = new KanbanContext();
        TagRepository tr = new TagRepository(_context);
        _context.Tags.RemoveRange(_context.Tags);
        var _tagDTO = new TagCreateDTO("test");
        tr.Create(_tagDTO);
        // When
        var tagId = _context.Tags.Where(t => t.Name == "test").First();
        var actual = tr.Read(tagId.Id);
        // Then
        Assert.Equal("test",actual.Name);
    }
    [Fact]
    public void TagRepoUpdate()
    {
        // Given
        KanbanContext _context = new KanbanContext();
        TagRepository tr = new TagRepository(_context);
        _context.Tags.RemoveRange(_context.Tags);
        var _tagDTO = new TagCreateDTO("update me");
        var created = tr.Create(_tagDTO);
        // When
        var actual = tr.Update(new TagUpdateDTO(created.TagId,"You have been updated"));
        var newTitle = tr.Read(created.TagId).Name;
        // Then
        Assert.Equal((actual,"You have been updated"),(Response.Updated,newTitle));
    }
}
