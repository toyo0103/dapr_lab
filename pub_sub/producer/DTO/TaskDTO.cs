using System;

namespace producer.Controllers
{
    public class TaskDTO
    {
        public Guid Id { get; set; }
        public string JobName{ get; set;}
        public string Data { get; set;}
    }
}