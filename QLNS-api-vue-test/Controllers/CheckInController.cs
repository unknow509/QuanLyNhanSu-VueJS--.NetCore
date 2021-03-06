﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLNS_api_vue_test.Models;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;

namespace QLNS_api_vue_test.Controllers
{
    [Route("api/[controller]")]
    public class CheckInController : Controller
    {
        private DACNQuanLyNhanSuContext db = new DACNQuanLyNhanSuContext();
        public static readonly TelegramBotClient bot = new TelegramBotClient("1251187799:AAFlWJPB64OAWH5aXg0FPaqJ_aZAfj3vlkI");
        //*************************************GET**************************************
        [HttpGet("GetAllPending")]
        public async Task<IActionResult> GetAllPending()
        {
            try { 
                var pending = from nv in db.Nhanvien
                              join ctcc in db.Chitietchamcong
                              on nv.MaNhanVien equals ctcc.MaNhanVien
                              where ctcc.Status == "Pending"
                              select new
                              {
                                  ctcc.Status,
                                  ctcc.MaNhanVien,
                                  ctcc.GioBatDau,
                                  ctcc.GioKetThuc,
                                  ctcc.Day,
                                  nv.HoTen
                              };
                return Ok(pending);
            }
            catch (Exception ex)
            {
                DateTime date = DateTime.Now;
                date.ToString("dddd, dd MMMM yyyy");             
                var t = await bot.SendTextMessageAsync(-388649962, "/CheckIn/getAllPending: " + date);
                return BadRequest(ex);
            }
        }
        //*************************************POST************************************** 
        [HttpPost("CheckIn")]/* checkin giờ vào*/
        public async Task<IActionResult> CheckIn([FromBody] Chitietchamcong TimeCheckIn)
        {
            try
            {
                db.Chitietchamcong.Add(TimeCheckIn);
                db.SaveChanges();
                return Ok(TimeCheckIn);
            }
            catch (Exception ex)
            {
                DateTime date = DateTime.Now;
                date.ToString("dddd, dd MMMM yyyy");
                var t = await bot.SendTextMessageAsync(-388649962, "/CheckIn: " + date);
                return BadRequest();
            }
        }
        //*************************************PUT**************************************
        [HttpPut("Update")] /*  chi tiết chấm công */
        public async Task<IActionResult> Update([FromBody] Chitietchamcong ctcc)
        {
            try
            {
                db.Entry(ctcc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return Ok(ctcc);
            }
            catch
            {
                DateTime date = DateTime.Now;
                date.ToString("dddd, dd MMMM yyyy");                
                var t = await bot.SendTextMessageAsync(-388649962, "/CheckIn/update: " + date);
                return BadRequest();
            }
        }
        [HttpPut("UpdateAll")]
        public async Task<IActionResult> UpdateAll([FromBody] List<Chitietchamcong> listCtcc)
        {
            try
            {
                foreach(var ctcc in listCtcc)
                {
                db.Entry(ctcc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                db.SaveChanges();
                return Ok(listCtcc);
            }
            catch
            {
                DateTime date = DateTime.Now;
                date.ToString("dddd, dd MMMM yyyy");
                var t = await bot.SendTextMessageAsync(-388649962, "/CheckIn/updateAll: " + date);
                return BadRequest();
            }
        }
        //*************************************DELETE**************************************
    }
}