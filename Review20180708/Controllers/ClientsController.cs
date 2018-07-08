using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Review20180708.Models;

namespace Review20180708.Controllers
{
    [RoutePrefix("client")]
    public class ClientsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        ClientRepository repo;
        OccupationRepository occuRepo;

        public ClientsController()
        {
            repo = RepositoryHelper.GetClientRepository(unitOfWork);
            occuRepo = RepositoryHelper.GetOccupationRepository(unitOfWork); //改成使用同一條連線字串連線而不是二條
        }

        [Route("Default")]
        public ActionResult Index()
        {
            //var client = db.Client.Include(c => c.Occupation).Take(50);
            var client = repo.All().Include(c => c.Occupation);
            return View(client.ToList());
        }

        [Route("Search")]
        public ActionResult Search(string filterword)
        {
            //var data = db.Client.Take(50).Where(c => c.FirstName.Contains(filterword)).ToList();
            var data = repo.All().Where(c => c.FirstName.Contains(filterword)).ToList();
            return View("Index", data);
        }

        public ActionResult ErrorPage()
        {
            return PartialView();
        }

        [Route("Detail/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Client client = db.Client.Find(id);
            Client client = repo.All().FirstOrDefault(c => c.ClientId == id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [Route("Create")]
        public ActionResult Create()
        {
            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes,IdNumber,IsDelete")] Client client)
        {
            if (ModelState.IsValid)
            {
                //db.Client.Add(client);
                //db.SaveChanges();
                repo.Add(client);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        [Route("Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.All().FirstOrDefault(c => c.ClientId == id); //db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes,IdNumber,IsDelete")] Client client)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(client).State = EntityState.Modified;
                //db.SaveChanges();
                var db = repo.UnitOfWork.Context;
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        [Route("Destroy/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.All().FirstOrDefault(c => c.ClientId == id); //db.Client.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = repo.All().FirstOrDefault(c => c.ClientId == id); //db.Client.Find(id);
            repo.Delete(client); //db.Client.Remove(client);
            repo.UnitOfWork.Commit(); //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
