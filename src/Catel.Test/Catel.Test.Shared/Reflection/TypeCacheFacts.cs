﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeCacheFacts.cs" company="Catel development team">
//   Copyright (c) 2008 - 2015 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Catel.Test.Reflection
{
    using System;
    using System.Collections.Generic;
    using Catel.Reflection;
    using NUnit.Framework;

    public class TypeCacheFacts
    {
        [TestFixture]
        public class TheGetTypeMethod
        {
            [TestCase("System.String", false, typeof(string))]
            [TestCase("System.String", true, typeof(string))]
            [TestCase("sYstEm.StRing", false, null)]
            [TestCase("sYstEm.StRing", true, typeof(string))]
            public void ReturnsTypeFromMsCorlib(string typeName, bool ignoreCase, Type expectedType)
            {
                var type = TypeCache.GetType(typeName, ignoreCase);

                Assert.AreEqual(expectedType, type);
            }

            [TestCase("System.Uri", false, typeof(Uri))]
            [TestCase("System.Uri", true, typeof(Uri))]
            [TestCase("sYstEm.uRi", false, null)]
            [TestCase("sYstEm.uRi", true, typeof(Uri))]
            public void ReturnsTypeFromSystem(string typeName, bool ignoreCase, Type expectedType)
            {
                var type = TypeCache.GetType(typeName, ignoreCase);

                Assert.AreEqual(expectedType, type);
            }

            [TestCase("System.Lazy`1", false, typeof(Lazy<>))]
            [TestCase("System.Lazy`1", true, typeof(Lazy<>))]
            [TestCase("sYstEm.lAzY`1", false, null)]
            [TestCase("sYstEm.lAzY`1", true, typeof(Lazy<>))]
            public void ReturnsTypeFromSystemCore(string typeName, bool ignoreCase, Type expectedType)
            {
                var type = TypeCache.GetType(typeName, ignoreCase);

                Assert.AreEqual(expectedType, type);
            }

            [TestCase]
            public void ReturnsTypeForLateBoundGenericTypeMultipleTimes()
            {
                var type = TypeCache.GetType("System.Collections.Generic.List`1[[System.Int32]]");
                Assert.AreEqual(typeof(List<int>), type);

                var type2 = TypeCache.GetType("System.Collections.Generic.List`1[[System.Int32]]");
                Assert.AreEqual(typeof(List<int>), type2);
            }

            [TestCase("Catel.Test.Reflection.TypeCacheFacts[]", typeof(TypeCacheFacts[]))]
            [TestCase("System.Collections.Generic.List`1[[Catel.Test.Reflection.TypeCacheFacts]]", typeof(List<TypeCacheFacts>))]
            public void ReturnsRightType(string typeName, Type expectedType)
            {
                var type = TypeCache.GetType(typeName);

                Assert.AreEqual(expectedType, type);
            }
        }

        [TestFixture]
        public class TheGetTypesImplementingInterfaceMethod
        {
            public interface IMySpecialInterface
            {
                
            }

            public class MySpecialClass : IMySpecialInterface
            {
                
            }

            [TestCase]
            public void ReturnsRightTypes()
            {
                var interfaces = TypeCache.GetTypesImplementingInterface(typeof (IMySpecialInterface));

                Assert.AreEqual(1, interfaces.Length);
                Assert.AreEqual(typeof(MySpecialClass), interfaces[0]);
            }
        }
    }
}