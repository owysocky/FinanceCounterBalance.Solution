using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
  public class ExpenseCategoryController : Controller
  {

    [HttpGet("/expense/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpGet("/expense/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      ExpenseCategory expenseCategory = ExpenseCategory.Find(id);
      List<ExpenseItem> categoryItems = expenseCategory.GetExpenseItems();
      model.Add("expenseCategory", expenseCategory);
      model.Add("categoryItems", categoryItems);
      return View(model);
    }

    [HttpPost("accounts/{accountId}/expense")]
    public ActionResult Create(int accountId, string name)
    {
      ExpenseCategory newExpenseCategory = new ExpenseCategory(accountId, name);
      newExpenseCategory.Save();
      return RedirectToAction("Show", "Account", new {id = accountId});
    }

    [HttpPost("/expense/{id}/delete")]
    public ActionResult Delete(int id)
    {
      ExpenseCategory expenseCategory = ExpenseCategory.Find(id);
      expenseCategory.Delete();
      return View();
    }

    [HttpPost("/expense/delete")]
    public ActionResult DeleteAll(int id)
    {
      ExpenseCategory.Delete();
      return View();
    }

    [HttpPost("/expense/{id}/update")]
    public ActionResult Update(int id, string newName)
    {
      ExpenseCategory expenseCategory = ExpenseCategory.Find(id);
      expenseCategory.Edit(newName);
      return RedirectToAction("Show", id);
    }

    [HttpPost("/expense/{id}/edit")]
    public ActionResult Edit(int id)
    {
      ExpenseCategory expenseCategory = ExpenseCategory.Find(id);
      return View(expenseCategory);
    }

  }
}
