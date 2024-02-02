using System.Reflection;
using System.Text;
using TodoApp.DAL.Entities;

namespace TodoApp.DAL.InitialData
{
    public static class ImagesInitialDataSeed
    {
        public static List<Image> Images => new()
        {
             new Image
             {
                 Id = 1,
                 Name = "Sunrise",
                 Description = "A beautiful sunrise over the mountains",
                 ImageBytes = File.ReadAllBytes($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\InitialData\\sunrise.PNG"),
                 TodoItemId = 1
             },
            new Image
            {
                Id = 2,
                Name = "Ocean",
                Description = "Waves crashing on the shore",
                ImageBytes = File.ReadAllBytes($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\InitialData\\ocean.PNG"),
                TodoItemId = 1
            },
            new Image
            {
                Id = 3,
                Name = "Doctor",
                Description = "A doctor with stethoscope",
                ImageBytes = File.ReadAllBytes($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\InitialData\\doctor.PNG"),
                TodoItemId = 2
            },
        };

        
    }
}
