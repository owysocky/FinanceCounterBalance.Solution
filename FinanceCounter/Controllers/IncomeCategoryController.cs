using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
  public class IncomeCategoryController : Controller
  {


    [HttpGet("/income/categories/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("accounts/{accountId}/income/categories")]
    public ActionResult Create( int accountId, string incomeCategoryName, double total)
    {
      IncomeCategory newIncomeCategory = new IncomeCategory(accountId, incomeCategoryName, total);
      newIncomeCategory.Save();
      List<IncomeCategory> allIncomeCategories = IncomeCategory.GetAll();
      return View("Index", allIncomeCategories);
    }

    [HttpGet("/income/categories/{id}")]
    public ActionResult Show(int id)
    {
      IncomeCategory newIncomeCategory = IncomeCategory.Find(id);
      return View(newIncomeCategory);
    }

    [HttpGet("/income/categories/{id}/edit")]
    public ActionResult Edit(int id)
    {
      IncomeCategory newIncomeCategory = IncomeCategory.Find(id);
      return View(newIncomeCategory);
    }

    [HttpPost("/income/categories/{id}/update")]
    public ActionResult Update(string newName, double newTotal, int id)
    {
      IncomeCategory newIncomeCategory = IncomeCategory.Find(id);
      newIncomeCategory.Edit(newName, newTotal);
      return View("Show", newIncomeCategory);
    }

  }
}
