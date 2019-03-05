﻿using CCP.UnitTests.Executors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CCP.UnitTests
{
    [TestClass]
    public class Exploratory
    {
        [TestMethod]
        public void HowToReadProperties()
        {
            TypeInfo type = typeof(OperationWithSimpleProps).GetTypeInfo();

            PropertyInfo[] props = type.DeclaredProperties.ToArray();

            var writable = props.Where(p => p.CanWrite && p.SetMethod?.IsPublic == true).ToArray();

            Assert.AreEqual(3, writable.Length);

            PropertyInfo arg1 = props.Where(p => p.Name == "Arg1").FirstOrDefault();
            Assert.IsNotNull(arg1);
        }

        [TestMethod]
        public void HowToSetPropertyOnDynamicallyCreatedObject()
        {
            Type type = typeof(OperationWithSimpleProps);          

            IOperation instance = (IOperation)Activator.CreateInstance(type);

            PropertyInfo arg1 = type.GetProperty("Arg1");
            arg1.SetValue(instance, "test value");

            OperationWithSimpleProps typed = (OperationWithSimpleProps)instance;
            Assert.AreEqual("test value", typed.Arg1);
        }
    }
}
