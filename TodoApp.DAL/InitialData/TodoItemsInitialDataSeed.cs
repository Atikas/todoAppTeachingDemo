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
                CreatedAt = new DateTime(2024,1,1),
                Due = new DateTime(2024,1,1),
                CompletedAt = null,
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new TodoItem
            {
                Id = 5,
                Type = "Other",
                Title = "Call electrician",
                Description = "",
                CreatedAt = new DateTime(2024,1,1),
                Due = new DateTime(2024,1,1),
                CompletedAt = null,
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            },
            new TodoItem
            {
                Id = 6,
                Type = "Work",
                Title = "Complete assignment",
                Description = "",
                CreatedAt = new DateTime(2024,1,1),
                Due = new DateTime(2024,1,1),
                CompletedAt = null,
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            },
        };
    }
}
