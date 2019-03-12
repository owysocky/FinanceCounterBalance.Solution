
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
  public class ExpenseCategoryController : Controller
  {

    [HttpGet("/expense/categories/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpGet("/expense/categories/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      ExpenseCategory expenseCategory = ExpenseCategory.Find(id);
      List<ExpenseItem> categoryItems = expenseCategory.GetExpenseItems();
      model.Add("expenseCategory", expenseCategory);
      model.Add("categoryItems", categoryItems);
      return View(model);
    }

    [HttpPost("accounts/{accountId}/expense/categories")]
    public ActionResult Create(int accountId, string name)
    {
      ExpenseCategory newExpenseCategory = new ExpenseCategory(accountId, name);
      newExpenseCategory.Save();
      return RedirectToAction("Show", "Account", new {id = accountId});
    }



    // [HttpPost("/expense/categories/{expenseCategoryId}/items/new")]
    // public ActionResult AddExpenseItem(int expenseCategoryId, int exitemId)
    // {
    //   ExpenseCategory expenseCategory = ExpenseCategory.Find(expenseCategoryId);
    //   ExpenseItem exitem = ExpenseItem.Find(exitemId);
    //   expenseCategory.AddExpenseItem(exitem);
    //   return RedirectToAction("Show",  new { id = expenseCategoryId });
    // }


  }
}

