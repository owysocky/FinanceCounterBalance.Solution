using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
  public class ExpenseItemController : Controller
  {

    [HttpGet("/account/{accountId}/expense/{expenseId}/items/{itemId}")]
    public ActionResult Show(int itemId)
    {
      ExpenseItem newExpenseItem = ExpenseItem.Find(id);
      return Show(newExpenseItem);
    }

    [HttpGet("/account/{accountId}/expense/{expenseId}/items/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpGet("/account/{accountId}/expense/{expenseId}/items/create")]
    public ActionResult Create(string name, double price, int expenseId)
    {
      ExpenseItem newExpenseItem = new ExpenseItem(name, price, expenseId);
      newExpenseItem.Save();
      return ();
    }



  }
}
