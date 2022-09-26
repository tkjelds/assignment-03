// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
using var KanbanDB = new KanbanContext();

Console.WriteLine($"Database path: {KanbanDB.DbPath}");
KanbanDB.Add(new User{Id = 12, Name = "Tore", Email = "tore"});
KanbanDB.SaveChanges();