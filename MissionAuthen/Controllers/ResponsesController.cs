using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MissionAuthen.DAL;
using MissionAuthen.Models;
using MissionAuthen.Controllers;
using Microsoft.AspNet.Identity;

namespace MissionAuthen.Controllers
{
    public class ResponsesController : Controller
    {
        private MISSION_HQContext db = new MISSION_HQContext(); //creates new database object

        // GET: Responses
        public ActionResult Index()
        {
            return View(db.Responses.ToList()); //passes list of missions to view
        }

        // GET: Responses/Details/5
        public ActionResult Details(int? id) //gives details on specific mission
        {
            if (id == null) //makes mission number was passed
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null) //makes sure mission exists
            {
                return HttpNotFound();
            }
            return View(response); //passes reponse to strongly typed model
        }

        // GET: Responses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Responses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResponseId, QuestionId,ResponseDescription,UserId")] Response response)
        {

            if (ModelState.IsValid) //ensures model state is valid
            {
                db.Responses.Add(response);
                db.SaveChanges();

                Question question = db.Questions.Find(response.QuestionId); // finds question from database
                return RedirectToAction("Details", "Missions", new { id = question.MissionId });
            }

            return View(response);
        }

        // GET: Responses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id); 
            if (response == null) //ensures the mission exists
            {
                return HttpNotFound();
            }
            return View(response); //passes model to view
        }

        // POST: Responses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResponseId,QuestionId,ResponseDescription,ResponseAuthor,ResponseDate")] Response response) //binds form fields to objects
        {
            if (ModelState.IsValid)
            {
                db.Entry(response).State = EntityState.Modified; //indicates model has been modified
                db.SaveChanges(); //saves to database
                return RedirectToAction("Index");
            }
            return View(response);
        }

        // GET: Responses/Delete/5
        public ActionResult Delete(int? id) //deletes mission by passing mission id
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // POST: Responses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Response response = db.Responses.Find(id); //finds reponse object using response id
            db.Responses.Remove(response); //removes from table
            db.SaveChanges(); //updates database
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
