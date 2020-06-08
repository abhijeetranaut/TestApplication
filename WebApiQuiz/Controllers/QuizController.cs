using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiQuiz.Models;


namespace WebApiQuiz.Controllers

{
   
    public class QuizController : ApiController
    {
       
        //public HttpResponseMessage GetQuestions()
        //{
        //    using (DBModel db = new DBModel())
        //    {
        //        var Qns = db.Questions.Select(x => new { QnId = x.QnId, Qn = x.Qn, x.Option1, x.Option2, x.Option3, x.Option4 }).OrderBy(y => Guid.NewGuid()).Take(10).ToArray();
        //        var Updated = Qns.AsEnumerable().Select(x => new
        //        {
        //            QnId = x.QnId,
        //            Qn = x.Qn,
        //            Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
        //        }).ToList();
        //        return this.Request.CreateResponse(HttpStatusCode.OK, Updated);
        //    }
        //}

        [HttpGet]
        [Route("api/Quiz/{id}")]
        public HttpResponseMessage GetQuestions(int id)
        {
            using (DBModel db = new DBModel())
            {
                var Qns = db.Questions.Select(x => new { QnId = x.QnId, Qn = x.Qn, x.Option1, x.Option2, x.Option3, x.Option4, x.QnType }).Where(x => x.QnType == id).OrderBy(y => Guid.NewGuid()).Take(10).ToArray(); 
                var Updated = Qns.AsEnumerable().Select(x => new
                {
                    QnId = x.QnId,
                    Qn = x.Qn,
                    Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                }).ToList();
                return this.Request.CreateResponse(HttpStatusCode.OK, Updated);
            }
        }







        [HttpPost]
        [Route("api/Answers")]
        public HttpResponseMessage GetAnswers(int[] qIds)
        {
            using(DBModel db = new DBModel())
            {
                var result = db.Questions.AsEnumerable().Where(y => qIds.Contains(y.QnId))
                    .OrderBy(x => { return Array.IndexOf(qIds, x.QnId); })
                    .Select(z => z.Answer)
                    .ToArray();
                return this.Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        [HttpPost]
        [Route("api/AddQuestions")]
        public Question GetQuestions(Question model)
        {
            using (DBModel db = new DBModel())
            {
                db.Questions.Add(model);
                db.SaveChanges();
                return model;
            }
        }
    }
}
