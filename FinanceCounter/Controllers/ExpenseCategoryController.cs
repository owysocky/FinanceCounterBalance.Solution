using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
  public class ExpenseCategoryController : Controller
  {

    [HttpGet("/accounts/{accountId}/expense/new")]
    public ActionResult New(int accountId)
    {
      Account account = Account.Find(accountId);
      return View(account);
    }

    [HttpPost("accounts/{accountId}/expense")]
    public ActionResult Create(int accountId, string name)
    {
      ExpenseCategory newExpenseCategory = new ExpenseCategory(accountId, name);
      newExpenseCategory.Save();
      return RedirectToAction("Show", "Account", new {id = accountId});
    }

    [HttpGet("accounts/{accountId}/expense/{id}")]
    public ActionResult Show(int accountId, int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      ExpenseCategory expenseCategory = ExpenseCategory.Find(id);
      //List<ExpenseItem> categoryItems = expenseCategory.GetExpenseItems();
      model.Add("expenseCategory", expenseCategory);
      //model.Add("categoryItems", categoryItems);
      return View(model);
    }

    [HttpPost("accounts/{accountId}/expense/{id}/delete")]
    public ActionResult Delete(int accountId, int id)
    {
      ExpenseCategory expenseCategory = ExpenseCategory.Find(id);
      expenseCategory.Delete();
      return RedirectToAction("Show", "Account", new {id = accountId});
    }

    [HttpPost("accounts/{accountId}/expense/deleteall")]
    public ActionResult DeleteAll(int accountId)
    {
      ExpenseCategory.DeleteAllExpenseCategories();
      return RedirectToAction("Show", "Account", new {id = accountId});
    }

    // [HttpPost("/expense/{id}/update")]
    // public ActionResult Update(int id, string newName)
    // {
    //   ExpenseCategory expenseCategory = ExpenseCategory.Find(id);
    //   expenseCategory.Edit(newName);
    //   return RedirectToAction("Show", id);
    // }
    //
    // [HttpPost("/expense/{id}/edit")]
    // public ActionResult Edit(int id)
    // {
    //   ExpenseCategory expenseCategory = ExpenseCategory.Find(id);
    //   return View(expenseCategory);
    // }

  }
}
