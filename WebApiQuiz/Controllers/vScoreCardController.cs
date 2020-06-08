using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiQuiz.Models;

namespace WebApiQuiz.Controllers
{
    public class vScoreCardController : ApiController
    {
        [HttpGet]
        [Route("api/vScoreCard/{id}")]
        public HttpResponseMessage GetScore(int id)
        {
            using (DBModel db = new DBModel())
            {
                var Scr = db.vScoreCards.Select(x => new { Name = x.Name, Qn = x.Qn, Score=x.Score, x.Option1, x.Option2, x.Option3, x.Option4, Answer = x.Answer, Expr1 = x.Expr1,x.ParticipantId }).Where(x=> x.ParticipantId == id).ToArray();
                var Updated = Scr.AsEnumerable().Select(x => new
                {
                    Name = x.Name,
                    Qn=x.Qn,
                    Score=x.Score,
                    Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 },
                    Answer=x.Answer,
                    Expr1=x.Expr1
                }).ToList();
                return this.Request.CreateResponse(HttpStatusCode.OK, Updated);
            }
        }
    }
}
