using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWebService.Backend.LfsrGenerators;
using CryptoWebService.Models.Generators;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWebService.Controllers
{
    public class GeneratorsController : Controller
    {
        public IActionResult Cascade()
        {
            return View();
        }

        public IActionResult CascadeGenerate([FromBody]LfsrGeneratorsViewModel viewModel)
        {
            var numOfRegisters = viewModel.Registers.Length;
            Lfsr[] registers = new Lfsr[numOfRegisters];

            for (int i = 0; i < numOfRegisters; i++)
            {
                registers[i] = new Lfsr(viewModel.Registers[i]);
            }

            CascadeGenerator generator = new CascadeGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            switch (mode)
            {
                case SeriesMode.Binary:
                {
                    var data = generator.GenerateBitsAsChars(viewModel.Length);
                    return Json(new string(data));
                }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(BitConverter.ToString(data));
                }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(data);
                }

            }

            return Json("");
        }

        public IActionResult Geffe()
        {
            return View();
        }
        public IActionResult GeffeGenerate([FromBody]LfsrGeneratorsViewModel viewModel)
        {
            var numOfRegisters = viewModel.Registers.Length;
            Lfsr[] registers = new Lfsr[3];

            for (int i = 0; i < numOfRegisters; i++)
            {
                registers[i] = new Lfsr(viewModel.Registers[i]);
            }

            GeffesGenerator generator = new GeffesGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            switch (mode)
            {
                case SeriesMode.Binary:
                {
                    var data = generator.GenerateBitsAsChars(viewModel.Length);
                    return Json(new string(data));
                }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(BitConverter.ToString(data));
                }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(data);
                }

            }

            return Json("");
        }

        public IActionResult SelfDecimation()
        {
            return View();
        }

        public IActionResult SelfDecimationGenerate([FromBody]LfsrGeneratorsViewModel viewModel)
        {
            var numOfRegisters = viewModel.Registers.Length;
            Lfsr[] registers = new Lfsr[numOfRegisters];

            for (int i = 0; i < numOfRegisters; i++)
            {
                registers[i] = new Lfsr(viewModel.Registers[i]);
            }

            SelfDecimationGenerator generator = new SelfDecimationGenerator(registers);
            generator.K_value = (int)viewModel.K_value;
            generator.D_value = (int)viewModel.D_value;

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            switch (mode)
            {
                case SeriesMode.Binary:
                {
                    var data = generator.GenerateBitsAsChars(viewModel.Length);
                    return Json(new string(data));
                }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(BitConverter.ToString(data));
                }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(data);
                }

            }

            return Json("");
        }
        public IActionResult SelfShrinking()
        {
            return View();
        }

        public IActionResult SelfShrinkingGenerate([FromBody]LfsrGeneratorsViewModel viewModel)
        {
            var numOfRegisters = viewModel.Registers.Length;
            Lfsr[] registers = new Lfsr[numOfRegisters];

            for (int i = 0; i < numOfRegisters; i++)
            {
                registers[i] = new Lfsr(viewModel.Registers[i]);
            }

            SelfShrinkingGenerator generator = new SelfShrinkingGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            switch (mode)
            {
                case SeriesMode.Binary:
                {
                    var data = generator.GenerateBitsAsChars(viewModel.Length);
                    return Json(new string(data));
                }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(BitConverter.ToString(data));
                }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(data);
                }

            }

            return Json("");
        }

        public IActionResult Shrinking()
        {
            return View();
        }

        public IActionResult ShrinkingGenerate([FromBody]LfsrGeneratorsViewModel viewModel)
        {
            var numOfRegisters = viewModel.Registers.Length;
            Lfsr[] registers = new Lfsr[numOfRegisters];

            for (int i = 0; i < numOfRegisters; i++)
            {
                registers[i] = new Lfsr(viewModel.Registers[i]);
            }

            ShrinkingGenerator generator = new ShrinkingGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            switch (mode)
            {
                case SeriesMode.Binary:
                {
                    var data = generator.GenerateBitsAsChars(viewModel.Length);
                    return Json(new string(data));
                }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(BitConverter.ToString(data));
                }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(data);
                }

            }

            return Json("");
        }
        public IActionResult StopAndGo()
        {
            return View();
        }

        public IActionResult StopAndGoGenerate([FromBody]LfsrGeneratorsViewModel viewModel)
        {
            var numOfRegisters = viewModel.Registers.Length;
            Lfsr[] registers = new Lfsr[numOfRegisters];

            for (int i = 0; i < numOfRegisters; i++)
            {
                registers[i] = new Lfsr(viewModel.Registers[i]);
            }

            StopAndGoGenerator generator = new StopAndGoGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            switch (mode)
            {
                case SeriesMode.Binary:
                {
                    var data = generator.GenerateBitsAsChars(viewModel.Length);
                    return Json(new string(data));
                }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(BitConverter.ToString(data));
                }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(data);
                }

            }

            return Json("");
        }

        public IActionResult Threshold()
        {
            return View();
        }

        public IActionResult ThresholdGenerate([FromBody]LfsrGeneratorsViewModel viewModel)
        {
            var numOfRegisters = viewModel.Registers.Length;
            Lfsr[] registers = new Lfsr[numOfRegisters];

            for (int i = 0; i < numOfRegisters; i++)
            {
                registers[i] = new Lfsr(viewModel.Registers[i]);
            }

            ThresholdGenerator generator = new ThresholdGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            switch (mode)
            {
                case SeriesMode.Binary:
                {
                    var data = generator.GenerateBitsAsChars(viewModel.Length);
                    return Json(new string(data));
                }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(BitConverter.ToString(data));
                }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(data);
                }

            }

            return Json("");
        }

        public IActionResult Lfsr()
        {
            return View();
        }

        public IActionResult LfsrGenerate([FromBody]LfsrGeneratorsViewModel viewModel)
        {
            var numOfRegisters = viewModel.Registers.Length;
            Lfsr[] registers = new Lfsr[numOfRegisters];

            for (int i = 0; i < numOfRegisters; i++)
            {
                registers[i] = new Lfsr(viewModel.Registers[i]);
            }

            LfsrGenerator generator = new LfsrGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            switch (mode)
            {
                case SeriesMode.Binary:
                {
                    var data = generator.GenerateBitsAsChars(viewModel.Length);
                    return Json(new string(data));
                }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(BitConverter.ToString(data));
                }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    return Json(data);
                }

            }

            return Json("");
        }
    }
}