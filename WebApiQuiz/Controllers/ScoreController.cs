using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiQuiz.Models;

namespace WebApiQuiz.Controllers
{
    public class ScoreController : ApiController
    {
        [HttpPost]
        [Route("api/AddScores")]
        public Score GetScores(Score model)
        {
            using (DBModel db = new DBModel())
            {
                db.Scores.Add(model);
                db.SaveChanges();
                return model;
            }
        }
    }
}
