using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
  public class IncomeCategoryController : Controller
  {
    [HttpGet("/accounts/{accountId}/income")]
    public ActionResult Index(int accountId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Account account = Account.Find(accountId);
      List<IncomeCategory> incomeCategory = IncomeCategory.GetAll();
      model.Add("account",account);
      model.Add("incomeCategory", incomeCategory);
      return View(model);
    }

    [HttpGet("/accounts/{accountId}/income/new")]
    public ActionResult New(int accountId)
    {
      Account account = Account.Find(accountId);
      return View(account);
    }

    [HttpPost("/accounts/{accountId}")]
    public ActionResult Create(int accountId, string incomeCategoryName, double total, int id)

    {
      IncomeCategory newIncomeCategory = new IncomeCategory(accountId, incomeCategoryName, total);
      newIncomeCategory.Save();
      return RedirectToAction("Show", "Account", new {id = accountId});
    }

    [HttpGet("/accounts/{accountId}/income/{id}")]
    public ActionResult Show(int accountId, int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Account account = Account.Find(accountId);
      IncomeCategory newIncomeCategory = IncomeCategory.Find(id);
      model.Add("account",account);
      model.Add("newIncomeCategory",newIncomeCategory);
      return View(model);
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

    [HttpPost("/accounts/{accountId}/income/{id}/delete")]
    public ActionResult Delete(int accountId, int id)
    {
      IncomeCategory newIncomeCategory = IncomeCategory.Find(id);
      newIncomeCategory.Delete();
      return RedirectToAction("Show", "Account", new {id = accountId});
    }

  }
}
