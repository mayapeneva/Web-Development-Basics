namespace Torshia.App
{
    using SIS.Framework;

    public class Launcher
    {
        public static void Main()
        {
            //var db = new TorshiaDbContext();
            //for (var i = 0; i < 6; i++)
            //{
            //    var task = new Task
            //    {
            //        Title = "Cleaning" + i + i,
            //        AffectedSectors = new List<TaskSector>
            //        {
            //            new TaskSector {Sector = Sector.Marketing},
            //            new TaskSector {Sector = Sector.Internal},
            //        },
            //        Description = "to clean",
            //        DueDate = new DateTime(2018, 11, 5 + i),
            //        Participants = new List<UserTask> { new UserTask { UserId = 2 } }
            //    };
            //    db.Tasks.Add(task);
            //}

            //db.SaveChanges();

            WebHost.Start(new StartUp());
        }
    }
}