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





  }
}
