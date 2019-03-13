using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
  public class ExpenseItemController : Controller
  {
    [HttpGet("/accounts/{accountId}/expense/{expenseId}/items/new")]
    public ActionResult New(int accountId, int expenseId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Account account = Account.Find(accountId);
      ExpenseCategory expenseCategory = ExpenseCategory.Find(expenseId);
      model.Add("account",account);
      model.Add("expenseCategory", expenseCategory);
      return View(model);
    }

    [HttpPost("accounts/{accountId}/expense/{expenseId}/items")]
    public ActionResult Create(int accountId, int expenseId, string name, double price)
    {
      ExpenseItem newExpenseItem = new ExpenseItem(name, price, expenseId);
      newExpenseItem.Save();
      return RedirectToAction("Show", "ExpenseCategory", new {id = expenseId});
    }

    [HttpGet("/accounts/{accountId}/expense/{expenseId}/items/{itemId}")]
    public ActionResult Show(int accountId, int expenseId, int itemId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Account account = Account.Find(accountId);
      ExpenseItem expenseItem = ExpenseItem.Find(itemId);
      ExpenseCategory expenseCategory = ExpenseCategory.Find(expenseId);
      model.Add("account",account);
      model.Add("expenseItem", expenseItem);
      model.Add("expenseCategory", expenseCategory);
      return View(model);
    }

    [HttpGet("/accounts/{accountId}/expense/{expenseId}/items/{itemId}/edit")]
    public ActionResult Edit(int accountId, int expenseId, int itemId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Account account = Account.Find(accountId);
      ExpenseItem expenseItem = ExpenseItem.Find(itemId);
      ExpenseCategory newExpenseCategory = ExpenseCategory.Find(expenseId);
      model.Add("expenseItem",expenseItem);
      model.Add("account",account);
      model.Add("newExpenseCategory",newExpenseCategory);
      return View(model);
    }

    [HttpPost("/accounts/{accountId}/expense/{expenseId}/items/{itemId}")]
    public ActionResult Update(int expenseId, int itemId, string newName, double newPrice)
    {
      ExpenseItem expenseItem = ExpenseItem.Find(itemId);
      expenseItem.Edit(newName, newPrice);
      return RedirectToAction("Show",expenseItem);
    }

    [HttpPost("/accounts/{accountId}/expense/{expenseId}/items/{itemId}/delete")]
    public ActionResult Delete(int expenseId, int itemId)
    {
      ExpenseItem expenseItem = ExpenseItem.Find(itemId);
      expenseItem.Delete();
      return RedirectToAction("Show", "ExpenseCategory", new {id = expenseId});
    }

    // [HttpPost("/accounts/{accountId}/expense/{expenseId}/items/delete")]
    // public ActionResult DeleteAll(int expenseId)
    // {
    //   ExpenseItem.DeleteAll(expenseId);
    //   return RedirectToAction("Show", "ExpenseCategory", new {id = expenseId});
    // }

  }
}
