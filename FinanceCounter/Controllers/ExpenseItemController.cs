using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
  public class ExpenseItemController : Controller
  {

    [HttpGet("/account/{accountId}/expense/{expenseId}/items/{itemId}")]
    public ActionResult Show(int expenseId, int itemId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      ExpenseItem expenseItem = ExpenseItem.Find(itemId);
      ExpenseCategory expenseCategory = ExpenseCategory.Find(expenseId);
      model.Add("expenseItem", expenseItem);
      model.Add("expenseCategory", expenseCategory);
      return Show(model);
    }

    [HttpGet("/account/{accountId}/expense/{expenseId}/items/{itemId}/edit")]
    public ActionResult Edit(int expenseId, int itemId)
    {
      ExpenseItem expenseItem = ExpenseItem.Find(itemId);
      ExpenseCategory expenseCategory = ExpenseCategory.Find(expenseId);
      return Show();
    }

    [HttpPost("/account/{accountId}/expense/{expenseId}/items/{itemId}/update")]
    public ActionResult Update(int expenseId, int itemId, string newName, double newPrice)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      ExpenseItem expenseItem = ExpenseItem.Find(itemId);
      expenseItem.Edit(newName, newPrice);
      ExpenseCategory expenseCategory = ExpenseCategory.Find(expenseId);
      model.Add("expenseItem", expenseItem);
      model.Add("expenseCategory", expenseCategory);
      return RedierectToAction("Show", model);
    }



  }
}
