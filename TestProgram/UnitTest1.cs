using Fitnes.View.CheckClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography.X509Certificates;

namespace TestProgram
{
    [TestClass]
    public class UnitTest1
    {
        //Password_Check
        [TestMethod]
        public void Check_AllPassowrd_True()
        {
            //Arrange
            string password = "12345678As#";
            //Act
            bool actual = RegCheck.PasswordCheckTest(password);
            //Assert
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void Check_SpacesBeginningPassowrd_True()
        {
            //Arrange
            string password =  "   12345678As#";
            //Act
            bool actual = RegCheck.PasswordCheckTest(password);
            //Assert
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void Check_SpacesMiddlePassowrd_True()
        {
            //Arrange
            string password = "1234   5678As#";
            //Act
            bool actual = RegCheck.PasswordCheckTest(password);
            //Assert
            Assert.IsTrue(actual);
        }
        public void Check_SpacesEndPassowrd_True()
        {
            //Arrange
            string password = "12345678As#   ";
            //Act
            bool actual = RegCheck.PasswordCheckTest(password);
            //Assert
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void Check_EmptyPassowrd_False()
        {
            //Arrange
            string password = "";
            //Act
            bool actual = RegCheck.PasswordCheckTest(password);
            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void Check_EasyPassowrd_False()
        {
            //Arrange
            string password = "21343432";
            //Act
            bool actual = RegCheck.PasswordCheckTest(password);
            //Assert
            Assert.IsFalse(actual);
        }
        //Number_Check
        [TestMethod]
        public void Check_AllNumber_True()
        {
            //Arrange
            string telephone = "89932777250";
            //Act
            bool actual = RegCheck.TelephoneCheckTest(telephone);
            //Assert
            Assert.IsTrue(actual);
        }
       
        [TestMethod]
        public void Check_SpacesMiddleNumber_False()
        {
            //Arrange
            string telephone = "899327  77250";
            //Act
            bool actual = RegCheck.TelephoneCheckTest(telephone);
            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void Check_ShortNumber_False()
        {
            //Arrange
            string telephone = "8999288250";
            //Act
            bool actual = RegCheck.TelephoneCheckTest(telephone);
            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void Check_AnotherBeginningNumber_False()
        {
            //Arrange
            string telephone = "59932777250";
            //Act
            bool actual = RegCheck.TelephoneCheckTest(telephone);
            //Assert
            Assert.IsFalse(actual);
        }
        [TestMethod]
        public void Check_LettersNumber_False()
        {
            //Arrange
            string telephone = "5993d777250";
            //Act
            bool actual = RegCheck.TelephoneCheckTest(telephone);
            //Assert
            Assert.IsFalse(actual);
        }
    }
}
