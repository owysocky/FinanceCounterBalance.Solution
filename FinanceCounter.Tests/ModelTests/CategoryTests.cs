using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceCounter.Models;
using System.Collections.Generic;
using System;

namespace FinanceCounter.Tests
{
    [TestClass]
    public class CategoryTest : IDisposable
    {

        public void Dispose()
        {
            Category.ClearAll();
            Item.ClearAll();
        }

        public CategoryTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=finance_test;";
        }

        [TestMethod]
        public void CategoryConstructor_CreatesInstanceOfCategory_Category()
        {
            Category newCategory = new Category("Home");
            Assert.AreEqual(typeof(Category), newCategory.GetType());
        }



        [TestMethod]
        public void AddAuthor_AddsAuthorToCategory_AuthorList()
        {
            Category newCategory = new Category("Mow the lawn");
            newCategory.Save();
            Item testItem = new Item("Test", 100.00,newCategory.GetId());
            testItem.Save();
            double result = newCategory.GetTotal();
            double answer = 100.00;
            Assert.AreEqual(answer, result);
        }

        // [TestMethod]
        // public void GetAuthorId_ReturnsCategorysParentAuthorId_Int()
        // {
        //   //Arrange
        //   Author newAuthor = new Author("Home Tasks");
        //   Category newCategory = new Category("Walk the dog.", 1, newAuthor.GetId());
        //
        //   //Act
        //   int result = newCategory.GetAuthorId();
        //
        //   //Assert
        //   Assert.AreEqual(newAuthor.GetId(), result);
        // }

    }
}
