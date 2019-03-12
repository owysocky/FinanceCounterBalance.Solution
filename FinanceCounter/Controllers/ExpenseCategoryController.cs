
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
  public class ExpenseCategoryController : Controller
  {
    [HttpGet("/excategories")]
    public ActionResult Index()
    {
      List<ExpenseCategory> allExpenseCategories = ExpenseCategory.GetAll();
      return View(allExpenseCategories);
    }

    [HttpGet("/excategories/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/excategories")]
    public ActionResult Create(string expenseCategoryName, double total)
    {
      ExpenseCategory newExpenseCategory = new ExpenseCategory(expenseCategoryName, total);
      newExpenseCategory.Save();
      List<ExpenseCategory> allExpenseCategories = ExpenseCategory.GetAll();
      return View("Index", allExpenseCategories);
    }

    [HttpGet("/excategories/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      ExpenseCategory selectedExpenseCategory = ExpenseCategory.Find(id);
      List<ExpenseItem> expenseCategoryItems = selectedExpenseCategory.GetExpenseItems();
      List<ExpenseItem> allExpenseItems = ExpenseItem.GetAll();
      model.Add("expenseCategory", selectedExpenseCategory);
      model.Add("expenseCategoryItems", expenseCategoryItems);
      model.Add("allExpenseItems", allExpenseItems);
      return View(model);
    }

    // [HttpPost("/excategories/{expenseCategoryId}/exitems")]
    // public ActionResult Create(string name, double price, int expenseCategoryId,)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   ExpenseCategory selectedExpenseCategory = ExpenseCategory.Find(expenseCategoryId);
    //   ExpenseItem newExpenseItem = new ExpenseItem(name, price, expenseCategoryId);
    //   newExpenseItem.Save();
    //   List<ExpenseItem> expenseCategoryItems = selectedExpenseCategory.GetExpenseItems();
    //   List<ExpenseItem> allExpenseItems = ExpenseItem.GetAll();
    //   List<ExpenseItem> expenseCategoryItems = selectedExpenseCategory.GetExpenseItems();
    //   model.Add("exitems", expenseCategoryItems);
    //   model.Add("expenseCategoryItems", expenseCategoryItems);
    //   model.Add("expenseCategory", selectedExpenseCategory);
    //   model.Add("allExpenseItems", allExpenseItems);
    //   return View("Show", model);
    // }
    //
    // [HttpPost("/excategories/{expenseCategoryId}/exitems/new")]
    // public ActionResult AddExpenseItem(int expenseCategoryId, int exitemId)
    // {
    //   ExpenseCategory expenseCategory = ExpenseCategory.Find(expenseCategoryId);
    //   ExpenseItem exitem = ExpenseItem.Find(exitemId);
    //   expenseCategory.AddExpenseItem(exitem);
    //   return RedirectToAction("Show",  new { id = expenseCategoryId });
    // }


  }
}
