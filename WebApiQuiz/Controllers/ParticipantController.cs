using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiQuiz.Models;

namespace WebApiQuiz.Controllers
{
    public class ParticipantController : ApiController
    {
        [HttpPost]
        [Route("api/InsertParticipant")]

        public Participant Insert(Participant model)
        {
            using (DBModel db = new DBModel())
            {
                db.Participants.Add(model);
                db.SaveChanges();
                return model;
            }
        }

        [HttpPost]
        [Route("api/UpdateOutput")]

        public void UpdateOutput(Participant model)
        {
            using (DBModel db = new DBModel())
            {
                db.Entry(model).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                
            }
        }

        [HttpGet]
        [Route("api/Participants")]

        public HttpResponseMessage GetParticipant()
        {
            using (DBModel db = new DBModel())
            {
                var Par = db.Participants.Select(x => new { Name = x.Name,ParticipantId=x.ParticipantId, Score = x.Score, TimeSpent = x.TimeSpent }).ToArray();
                var ParDetails = Par.AsEnumerable().Select(x => new
                {
                    Name = x.Name,
                    ParticipantId = x.ParticipantId,
                    Score = x.Score,
                    TimeSpent = x.TimeSpent
                }).ToList();
                return this.Request.CreateResponse(HttpStatusCode.OK, ParDetails);
            }
        }
    }
}
