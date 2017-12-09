using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HTK.Bank.Core;
using System.Collections.Generic;
using HTK.Bank.Core.Models;
using System.IO;
using HTK.Bank.Core.Services;

namespace HTK.Bank.UnitTest
{
    [TestClass]
    public class DistributionTest
    {
        private DistributionService _aiService = new DistributionService();
        MovementService _movementService = new MovementService(Path.Combine(Environment.CurrentDirectory, "App_Data/HTK.Bank.db"));
        
        [TestMethod]
        public void SHOULD_Generate_Distribution_based_on_list_of_movements()
        {
            var dawid01 = _movementService.GetMovements("Dawid01", null, Measure.AngleOfCurvature);
            var dawid02 = _movementService.GetMovements("Dawid02", null, Measure.AngleOfCurvature);
            var dawid03 = _movementService.GetMovements("Dawid03", null, Measure.AngleOfCurvature);
            var dawid04 = _movementService.GetMovements("Dawid04", null, Measure.AngleOfCurvature);
            var dawid05 = _movementService.GetMovements("Dawid05", null, Measure.AngleOfCurvature);

            var hari01 = _movementService.GetMovements("Hari", null, Measure.AngleOfCurvature);
            var hari02 = _movementService.GetMovements("jrab", null, Measure.AngleOfCurvature);

            var marek01 = _movementService.GetMovements(null, "012414fd-aba2-4df3-93b8-dced0308a07f", Measure.AngleOfCurvature);
            var marek02 = _movementService.GetMovements(null, "980279aa-6140-4e00-846f-6806a2c329bb", Measure.AngleOfCurvature);

            Console.WriteLine("=== Dawid01 ===");
            Verify(dawid01, dawid01, true);
            Verify(dawid01, dawid02, true);
            Verify(dawid01, dawid03, true);
            Verify(dawid01, dawid04, true);
            Verify(dawid01, dawid05, true);

            Console.WriteLine();
            Console.WriteLine("=== Dawid02 ===");
            Verify(dawid02, dawid01, true);
            Verify(dawid02, dawid02, true);
            Verify(dawid02, dawid03, true);
            Verify(dawid02, dawid04, true);
            Verify(dawid02, dawid05, true);

            Console.WriteLine();
            Console.WriteLine("=== Dawid03 ===");
            Verify(dawid03, dawid01, true);
            Verify(dawid03, dawid02, true);
            Verify(dawid03, dawid03, true);
            Verify(dawid03, dawid04, true);
            Verify(dawid03, dawid05, true);

            Console.WriteLine();
            Console.WriteLine("=== Dawid04 ===");
            Verify(dawid04, dawid01, true);
            Verify(dawid04, dawid02, true);
            Verify(dawid04, dawid03, true);
            Verify(dawid04, dawid04, true);
            Verify(dawid04, dawid05, true);

            Console.WriteLine();
            Console.WriteLine("=== Dawid04 Marek ===");
            Verify(dawid04, marek01, false);
            Verify(dawid04, marek02, false);

            Console.WriteLine();
            Console.WriteLine("=== Dawid04 Hari ===");
            Verify(dawid04, hari01, false);
            Verify(dawid04, hari02, false);
        }

        private void Verify(double[] person1, double[] person2, bool expectedResult)
        {
            Console.WriteLine("Shoule be {0}:", expectedResult ? "Same" : "Different");
            var resutl = _aiService.Distance(person1, person2, 190);
            Console.WriteLine(string.Format("\t\t - Distance 1 result: [{0}]", resutl));

            resutl = _aiService.Distance2(person1, person2, 190);
            Console.WriteLine(string.Format("\t\t - Distance 2 result: [{0}]", resutl));

            resutl = _aiService.Distance3(person1, person2, 190);
            Console.WriteLine(string.Format("\t\t - Distance 3 result: [{0}]", resutl));

            //foreach (var item in person2)
            //{
            //    Console.WriteLine(Math.Truncate(item));
            //}
        }
    }
}
