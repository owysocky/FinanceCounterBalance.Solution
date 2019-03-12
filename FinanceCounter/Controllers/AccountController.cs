using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
    public class AccountController : Controller
    {

      [HttpGet("/accounts")]
     public ActionResult Index()
     {
       List<Account> allAccounts = Account.GetAll();
       return View(allAccounts);
     }

     [HttpGet("/accounts/new")]
     public ActionResult New()
     {
       return View();
     }

     [HttpPost("/accounts")]
     public ActionResult Create(string accountName)
     {
       Account newAccount = new Account(accountName);
       newAccount.Save();
       List<Account> allAccounts = Account.GetAll();
       return View("Index", allAccounts);
     }

     [HttpGet("/accounts/{id}")]
     public ActionResult Show(int id)
     {
       Dictionary<string, object> model = new Dictionary<string, object>();
       Account account = Account.Find(id);
       List<ExpenseCategory> expenseCategories = account.GetExpenseCategories();
       List<IncomeCategory> incomeCategories = account.GetIncomeCategories();
       model.Add("account", account);
       model.Add("expenseCategories", expenseCategories);
       model.Add("incomeCategories", incomeCategories);
       return View(model);
     }

     [HttpPost("/accounts/{id}/delete")]
     public ActionResult Delete(int id)
     {
       Account account = Account.Find(id);
       account.Delete();
       return RedirectToAction("Index");
     }

     [HttpPost("/accounts/deleteall")]
     public ActionResult DeleteAll()
     {
       Account.DeleteAll();
       return RedirectToAction("Index");
     }

     [HttpGet("/accounts/{id}/edit")]
     public ActionResult Edit(int id)
     {
       Dictionary<string, object> model = new Dictionary<string, object>();
       Account account = Account.Find(id);
       List<ExpenseCategory> expenseCategories = account.GetExpenseCategories();
       List<IncomeCategory> incomeCategories = account.GetIncomeCategories();
       model.Add("account", account);
       model.Add("expenseCategories", expenseCategories);
       model.Add("incomeCategories", incomeCategories);
       return View(model);
     }

     [HttpPost("/accounts/{id}/update")]
     public ActionResult Update(string newName, int id)
     {
       Account account = Account.Find(id);
       Dictionary<string, object> model = new Dictionary<string, object>();
       List<ExpenseCategory> expenseCategories = account.GetExpenseCategories();
       List<IncomeCategory> incomeCategories = account.GetIncomeCategories();
       account.Edit(newName);
       model.Add("account", account);
       model.Add("expenseCategories", expenseCategories);
       model.Add("incomeCategories", incomeCategories);
       return View("Show", model);
     }



    }
}
