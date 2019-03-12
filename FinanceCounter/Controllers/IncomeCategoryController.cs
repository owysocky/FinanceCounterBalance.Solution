
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
  public class IncomeCategoryController : Controller
  {
    [HttpGet("accounts/accountId/income/categories")]
    public ActionResult Index(int accountId)
    {

      List<IncomeCategory> allIncomeCategories = IncomeCategory.GetAll();
      return View(allIncomeCategories);
    }

    [HttpGet("/income/categories/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/income/categories")]
    public ActionResult Create(string incomeCategoryName, double total)
    {
      IncomeCategory newIncomeCategory = new IncomeCategory(incomeCategoryName, total);
      newIncomeCategory.Save();
      List<IncomeCategory> allIncomeCategories = IncomeCategory.GetAll();
      return View("Index", allIncomeCategories);
    }

    [HttpGet("/income/categories/{id}")]
    public ActionResult Show(int id)
    {
      IncomeCategory newIncomeCategory = IncomeCategory.Find(int id)
      return View(newIncomeCategory);
    }

    [HttpGet("/income/categories/{id}/edit")]
    public ActionResult Edit(int categpryId )
    {
      IncomeCategory newIncomeCategory = IncomeCategory.Find(int id)
      return View(newIncomeCategory);
    }

    [HttpPost("/income/categories/{id}/update")]
    public ActionResult Update(string newName, int id, double newIncome )
    {
      Ca
      client.Edit(newName, newPhone);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      model.Add("client", client);
      return View("Show", model);
    }

  }
}
