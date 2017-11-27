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
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace MissionAuthen.Controllers
{
    public class MissionsController : Controller
    {
        private MISSION_HQContext db = new MISSION_HQContext(); //creates database

        // GET: Missions
        public ActionResult Index(string returnUrl)
        {
            if (returnUrl == null)
            {
                returnUrl = "/Home/Index/";
            }

            ViewBag.ReturnUrl = returnUrl;

            ViewBag.MissionNames = db.Missions.ToList(); //creates list of mission objects
            return View(db.Missions.ToList()); //passes list to view
        }

        // GET: Missions/Details/5
        [Authorize]
        public ActionResult Details(int? id, int? authen)
        {

            string userEmail = User.Identity.GetUserName();

            int current = db.Database.SqlQuery<int>(
                  "SELECT TOP 1 UserId " +
                  "FROM [User] " +
                  "WHERE UserEmail = '" + userEmail + "'").First<int>();

            if (current == 0)
            {
                current = 2;
            }

            ViewBag.userID = current;
            ViewBag.Questions = db.Questions.ToList(); //creates list of question objects
                ViewBag.Responses = db.Responses.ToList(); //creates list of responses object
                ViewBag.Users = db.Users.ToList(); //creates list of users

            if (id == null) //makes sure a mission is select via a number
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Mission mission = db.Missions.Find(id);
                if (mission == null) //makes sure mission exists
                {
                    return HttpNotFound();
                }
                return View(mission); //passes mission object to strongly typed view;
        }

        // GET: Missions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Missions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MissionId,MissionName,MissionPresident,MissionAddress,MissionPrimaryLanguage,MissionClimate,MissionDominantReligion,MissionFlagURL")] Mission mission)
        {
            if (ModelState.IsValid) //ensures binding completed successfully and model is valid
            {
                db.Missions.Add(mission);
                db.SaveChanges(); //saves to database
                return RedirectToAction("Index"); //returns to index action method
            }

            return View(mission); //if model was invalid, returns to create view and shows validation errors
        }

        // GET: Missions/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.MissionNames = db.Missions.ToList(); //creates list of mission objects
            if (id == null) //ensures mission id was included
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Missions.Find(id);
            if (mission == null) //makes sure mission exists
            {
                return HttpNotFound();
            }
            return View(mission); //returns to edit view is mission does not exist
        }

        // POST: Missions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MissionId,MissionName,MissionPresident,MissionAddress,MissionPrimaryLanguage,MissionClimate,MissionDominantReligion,MissionFlagURL")] Mission mission)
        {
            if (ModelState.IsValid) //ensures model state is valid
            {
                db.Entry(mission).State = EntityState.Modified; //indicates object has been modified
                db.SaveChanges(); //saves/updates changes to database
                return RedirectToAction("Index"); //returns to index method
            }
            return View(mission);
        }

        // GET: Missions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) //ensures mission id was passed
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Missions.Find(id);
            if (mission == null) //ensures message exists
            {
                return HttpNotFound();
            }
            return View(mission); //passes mission to view
        }

        // POST: Missions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mission mission = db.Missions.Find(id); //finds mission via mission id
            db.Missions.Remove(mission); //removes from database
            db.SaveChanges(); //saves changes to database
            return RedirectToAction("Index"); //returns to index method
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
