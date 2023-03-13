using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hangHoas = new List<HangHoa>();
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            //linQ
            try
            {
                var hangHoa = hangHoas.SingleOrDefault(hh => hh.MaHH == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(hangHoa);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(HangHoaVM hangHoaVM)
        {
            var hangHoa = new HangHoa
            {
                MaHH = Guid.NewGuid(),
                TenHangHoa = hangHoaVM.TenHangHoa,
                DonGia = hangHoaVM.DonGia,
            };
            hangHoas.Add(hangHoa);
            return Ok(new
            {
                Success = true, Data = hangHoa
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, HangHoa hangHoaEdit)
        {
            try
            {
                var hh = hangHoas.SingleOrDefault(hh => hh.MaHH == Guid.Parse(id));
                if (hh == null)
                {
                    return NotFound();
                }
                if (id != hh.MaHH.ToString())
                {
                    return BadRequest();
                }
                //update
                hh.TenHangHoa = hangHoaEdit.TenHangHoa;
                hh.DonGia = hangHoaEdit.DonGia;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var hh = hangHoas.SingleOrDefault(hh => hh.MaHH == Guid.Parse(id));
                if (hh == null)
                {
                    return NotFound();
                }
                hangHoas.Remove(hh);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
