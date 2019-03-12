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

    [HttpPost("/expense/categories/{id}/items")]
    public ActionResult Create(string name, double price, int id,)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      ExpenseCategory selectedExpenseCategory = ExpenseCategory.Find(expenseCategoryId);
      ExpenseItem newExpenseItem = new ExpenseItem(name, price, expenseCategoryId);
      newExpenseItem.Save();
      List<ExpenseItem> expenseCategoryItems = selectedExpenseCategory.GetExpenseItems();
      List<ExpenseItem> allExpenseItems = ExpenseItem.GetAll();
      List<ExpenseItem> expenseCategoryItems = selectedExpenseCategory.GetExpenseItems();
      model.Add("exitems", expenseCategoryItems);
      model.Add("expenseCategoryItems", expenseCategoryItems);
      model.Add("expenseCategory", selectedExpenseCategory);
      model.Add("allExpenseItems", allExpenseItems);
      return View("Show", model);
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
