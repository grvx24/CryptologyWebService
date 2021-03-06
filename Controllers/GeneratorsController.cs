﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWebService.Backend.LfsrGenerators;
using CryptoWebService.Helpers;
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
                registers[i].SetFeedbackFunction(viewModel.FeedbackFunctions[i]);
            }

            CascadeGenerator generator = new CascadeGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            switch (mode)
            {
                case SeriesMode.Binary:
                {
                    var data = generator.GenerateBitsAsChars(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                        return Json(new {data = new string(data), registers=r});
                }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                        return Json(new{ data = BitConverter.ToString(data), registers = r });
                }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new{ data = data,registers =r });
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
                registers[i].SetFeedbackFunction(viewModel.FeedbackFunctions[i]);
            }

            GeffesGenerator generator = new GeffesGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            switch (mode)
            {
                case SeriesMode.Binary:
                {
                    var data = generator.GenerateBitsAsChars(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = new string(data), registers = r });
                    }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = BitConverter.ToString(data), registers = r });
                    }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = data, registers = r });
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
                registers[i].SetFeedbackFunction(viewModel.FeedbackFunctions[i]);
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
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = new string(data), registers = r });
                    }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = BitConverter.ToString(data), registers = r });
                    }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = data, registers = r });
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
                registers[i].SetFeedbackFunction(viewModel.FeedbackFunctions[i]);
            }

            SelfShrinkingGenerator generator = new SelfShrinkingGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            try
            {
                switch (mode)
                {
                    case SeriesMode.Binary:
                        {
                            var data = generator.GenerateBitsAsChars(viewModel.Length);
                            var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                            return Json(new { data = new string(data), registers = r });
                        }
                    case SeriesMode.Hex:
                        {
                            var data = generator.GenerateBytes(viewModel.Length);
                            var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                            return Json(new { data = BitConverter.ToString(data), registers = r });
                        }
                    case SeriesMode.Base64:
                        {
                            var data = generator.GenerateBytes(viewModel.Length);
                            var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                            return Json(new { data = data, registers = r });
                        }
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
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
                registers[i].SetFeedbackFunction(viewModel.FeedbackFunctions[i]);
            }

            ShrinkingGenerator generator = new ShrinkingGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;
            try
            {
                switch (mode)
                {
                    case SeriesMode.Binary:
                        {
                            var data = generator.GenerateBitsAsChars(viewModel.Length);
                            var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                            return Json(new { data = new string(data), registers = r });
                        }
                    case SeriesMode.Hex:
                        {
                            var data = generator.GenerateBytes(viewModel.Length);
                            var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                            return Json(new { data = BitConverter.ToString(data), registers = r });
                        }
                    case SeriesMode.Base64:
                        {
                            var data = generator.GenerateBytes(viewModel.Length);
                            var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                            return Json(new { data = data, registers = r });
                        }

                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Result = false, Message = e.Message });
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
                registers[i].SetFeedbackFunction(viewModel.FeedbackFunctions[i]);
            }

            StopAndGoGenerator generator = new StopAndGoGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            switch (mode)
            {
                case SeriesMode.Binary:
                {
                    var data = generator.GenerateBitsAsChars(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = new string(data), registers = r });
                    }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = BitConverter.ToString(data), registers = r });
                    }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = data, registers = r });
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
                registers[i].SetFeedbackFunction(viewModel.FeedbackFunctions[i]);
            }

            ThresholdGenerator generator = new ThresholdGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            switch (mode)
            {
                case SeriesMode.Binary:
                {
                    var data = generator.GenerateBitsAsChars(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = new string(data), registers = r });
                    }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = BitConverter.ToString(data), registers = r });
                    }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = data, registers = r });
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
                registers[i].SetFeedbackFunction(viewModel.FeedbackFunctions[i]);
            }

            LfsrGenerator generator = new LfsrGenerator(registers);

            SeriesMode mode = (SeriesMode)viewModel.Mode;

            switch (mode)
            {
                case SeriesMode.Binary:
                {
                    var data = generator.GenerateBitsAsChars(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = new string(data), registers = r });
                    }
                case SeriesMode.Hex:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = BitConverter.ToString(data), registers = r });
                    }
                case SeriesMode.Base64:
                {
                    var data = generator.GenerateBytes(viewModel.Length);
                    var r = LfsrHelper.GetRegisterAsStringArray(generator.Registers);
                    return Json(new { data = data, registers = r });
                }

            }

            return Json("");
        }
    }
}