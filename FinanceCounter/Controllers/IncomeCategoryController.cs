
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
  public class IncomeCategoryController : Controller
  {
    [HttpGet("/incategories")]
    public ActionResult Index()
    {
      List<IncomeCategory> allIncomeCategories = IncomeCategory.GetAll();
      return View(allIncomeCategories);
    }

    [HttpGet("/incategories/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/incategories")]
    public ActionResult Create(string incomeCategoryName, double total)
    {
      IncomeCategory newIncomeCategory = new IncomeCategory(incomeCategoryName, total);
      newIncomeCategory.Save();
      List<IncomeCategory> allIncomeCategories = IncomeCategory.GetAll();
      return View("Index", allIncomeCategories);
    }

    [HttpGet("/incategories/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      IncomeCategory selectedIncomeCategory = IncomeCategory.Find(id);
      List<IncomeItem> incomeCategoryItems = selectedIncomeCategory.GetIncomeItems();
      List<IncomeItem> allIncomeItems = IncomeItem.GetAll();
      model.Add("incomeCategory", selectedIncomeCategory);
      model.Add("incomeCategoryItems", incomeCategoryItems);
      model.Add("allIncomeItems", allIncomeItems);
      return View(model);
    }

    // [HttpPost("/incategories/{incomeCategoryId}/initems")]
    // public ActionResult Create(string name, double price, int incomeCategoryId,)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   IncomeCategory selectedIncomeCategory = IncomeCategory.Find(incomeCategoryId);
    //   IncomeItem newIncomeItem = new IncomeItem(name, price, incomeCategoryId);
    //   newIncomeItem.Save();
    //   List<IncomeItem> incomeCategoryItems = selectedIncomeCategory.GetIncomeItems();
    //   List<IncomeItem> allIncomeItems = IncomeItem.GetAll();
    //   List<IncomeItem> incomeCategoryItems = selectedIncomeCategory.GetIncomeItems();
    //   model.Add("initems", incomeCategoryItems);
    //   model.Add("incomeCategoryItems", incomeCategoryItems);
    //   model.Add("incomeCategory", selectedIncomeCategory);
    //   model.Add("allIncomeItems", allIncomeItems);
    //   return View("Show", model);
    // }
    //
    // [HttpPost("/incategories/{incomeCategoryId}/initems/new")]
    // public ActionResult AddIncomeItem(int incomeCategoryId, int exitemId)
    // {
    //   IncomeCategory incomeCategory = IncomeCategory.Find(incomeCategoryId);
    //   IncomeItem exitem = IncomeItem.Find(exitemId);
    //   incomeCategory.AddIncomeItem(exitem);
    //   return RedirectToAction("Show",  new { id = incomeCategoryId });
    // }


  }
}
