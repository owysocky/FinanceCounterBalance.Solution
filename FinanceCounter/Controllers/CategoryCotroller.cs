
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using FinanceCounter.Models;

namespace FinanceCounter.Controllers
{
  public class CategoryController : Controller
  {
    [HttpGet("/categories")]
    public ActionResult Index()
    {
      List<Category> allCategories = Category.GetAll();
      return View(allCategories);
    }

    [HttpGet("/categories/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/categories")]
    public ActionResult Create(string categoryName, double total)
    {
      Category newCategory = new Category(categoryName, total);
      newCategory.Save();
      List<Category> allCategories = Category.GetAll();
      return View("Index", allCategories);
    }

    [HttpGet("/categories/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Category selectedCategory = Category.Find(id);
      List<Item> categoryItems = selectedCategory.GetItems();
      List<Item> allItems = Item.GetAll();
      model.Add("category", selectedCategory);
      model.Add("categoryItems", categoryItems);
      model.Add("allItems", allItems);
      return View(model);
    }

    // [HttpPost("/categories/{categoryId}/items")]
    // public ActionResult Create(string name, double price, int categoryId,)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Category selectedCategory = Category.Find(categoryId);
    //   Item newItem = new Item(name, price, categoryId);
    //   newItem.Save();
    //   List<Item> categoryItems = selectedCategory.GetItems();
    //   List<Item> allItems = Item.GetAll();
    //   List<Item> categoryItems = selectedCategory.GetItems();
    //   model.Add("items", categoryItems);
    //   model.Add("categoryItems", categoryItems);
    //   model.Add("category", selectedCategory);
    //   model.Add("allItems", allItems);
    //   return View("Show", model);
    // }
    //
    // [HttpPost("/categories/{categoryId}/items/new")]
    // public ActionResult AddItem(int categoryId, int itemId)
    // {
    //   Category category = Category.Find(categoryId);
    //   Item item = Item.Find(itemId);
    //   category.AddItem(item);
    //   return RedirectToAction("Show",  new { id = categoryId });
    // }


  }
}
