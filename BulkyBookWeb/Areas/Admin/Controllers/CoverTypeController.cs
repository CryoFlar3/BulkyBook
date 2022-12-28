using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers;
[Area("Admin")]

public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CoverTypeController(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index() {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);
    }

    // GET 
    public IActionResult Create() {
        return View();
    }
    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj) {
        //if (obj.Name == obj.DisplayOrder.ToString()) {
        //    ModelState.AddModelError("CustomError", "The DisplayOrder cannot be the same as the Name");
        //}
        if (ModelState.IsValid) {
            _unitOfWork.CoverType.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    // GET 
    public IActionResult Edit(int id) {
        if (id == null || id == 0) {
            return NotFound();
        }
        var categoryFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (categoryFromDbFirst == null) {
            return NotFound();
        }
        return View(categoryFromDbFirst);
    }
    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj) {
        //if (obj.Name == obj.DisplayOrder.ToString()) {
        //    ModelState.AddModelError("CustomError", "The DisplayOrder cannot be the same as the Name");
        //}
        if (ModelState.IsValid) {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    // GET 
    public IActionResult Delete(int? id) {
        if (id == null || id == 0) {
            return NotFound();
        }
        var categoryFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

        if (categoryFromDbFirst == null) {
            return NotFound();
        }
        return View(categoryFromDbFirst);
    }
    // POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id) {
        var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        if (obj == null) {
            return NotFound();
        }
        _unitOfWork.CoverType.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}

