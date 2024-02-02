using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.DAL.Entities;

namespace TodoApp.DAL.InitialData
{
    public static class TodoItemsInitialDataSeed
    {
        public static List<TodoItem> TodoItems => new()
        {
            new TodoItem
            {
                Id = 1,
                Type = "Holiday",
                Title = "Holiday at Tenerife",
                Description = "",
                Place = "Tenerife",
                CreatedAt = new DateTime(2024,1,1),
                Due = new DateTime(2024,1,1),
                CompletedAt = null,
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new TodoItem
            {
                Id = 2,
                Type = "Work",
                Title = "Visit doctor",
                Description = "",
                Place = "Vilnius",
                CreatedAt = new DateTime(2024,1,1),
                Due = new DateTime(2024,1,1),
                CompletedAt = null,
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new TodoItem
            {
                Id = 3,
                Type = "Shopping",
                Title = "Buy groceries",
                Description = "",
                Place = "Vilnius",
                CreatedAt = new DateTime(2024,1,1),
                Due = new DateTime(2024,1,1),
                CompletedAt = null,
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new TodoItem
            {
                Id = 4,
                Type = "Other",
                Title = "Call plumber",
                Description = "",
                Place = "Ukmerge",
                CreatedAt = new DateTime(2024,1,1),
                Due = new DateTime(2024,1,2),
                CompletedAt = new DateTime(2024,1,2),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new TodoItem
            {
                Id = 5,
                Type = "Other",
                Title = "Call electrician",
                Description = "",
                Place = "Utena",
                CreatedAt = new DateTime(2024,1,1),
                Due = new DateTime(2024,1,3),
                CompletedAt = new DateTime(2024,1,4),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            },
            new TodoItem
            {
                Id = 6,
                Type = "Work",
                Title = "Complete assignment",
                Description = "",
                Place = "Online",
                CreatedAt = new DateTime(2024,1,1),
                Due = new DateTime(2024,1,1),
                CompletedAt = null,
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            },
        };
    }
}
