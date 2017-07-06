using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ProjectManagementSystem.Models;
using ProjectManagementSystem.ViewModel.Project;

namespace ProjectManagementSystem.Controllers
{
    public class ProjectController : Controller
    {
        private readonly Models.ProjectManagementSystem _pms = new Models.ProjectManagementSystem();
        public ActionResult Index()
        {
            var projectListViewModel = new ProjectListViewModel {Projects = new List<ProjectViewModel>()};

            projectListViewModel.Projects.AddRange(_pms.Projects.Select(project =>
                new ProjectViewModel
                {
                    ID = project.ID,
                    Name = project.Name,
                    InternalCode = project.InternalCode,
                    Activities = project.Activities,
                    Active = project.Active == 1
                }));

            return View(projectListViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel projectViewModel)
        {
            if (!ModelState.IsValid) return View(projectViewModel);
            var project = new Project()
            {
                Name = projectViewModel.Name,
                InternalCode = projectViewModel.InternalCode,
                Active = 1
            };
            _pms.Projects.Add(project);
            _pms.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _pms.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var projectViewModel = new ProjectViewModel()
            {
                ID = project.ID,
                Name = project.Name,
                InternalCode = project.InternalCode,
                Activities = project.Activities,
                Active = Convert.ToBoolean(project.Active)
            };
            return View(projectViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = _pms.Projects.Find(id);
            if (project == null)
                return HttpNotFound();
            project.Active = 0;
            foreach (var activity in project.Activities)
            {
                activity.Active = default(int);
            }
            _pms.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _pms.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var projectViewModel = new ProjectViewModel
            {
                ID = project.ID,
                Name = project.Name,
                InternalCode = project.InternalCode,
                Activities = project.Activities,
                Active = Convert.ToBoolean(project.Active)
            };
            return View(projectViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectViewModel projectViewModel)
        {
            if (!ModelState.IsValid) return View(projectViewModel);
            var project = _pms.Projects.Find(projectViewModel.ID);
            if (project != null)
            {
                project.Name = projectViewModel.Name;
                project.InternalCode = projectViewModel.InternalCode;
            }
            _pms.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = _pms.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var projectViewModel = new ProjectViewModel()
            {
                ID = project.ID,
                Name = project.Name,
                InternalCode = project.InternalCode,
                Activities = project.Activities,
                Active = Convert.ToBoolean(project.Active)
            };
            return View(projectViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pms.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}