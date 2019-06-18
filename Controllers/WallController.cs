using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dojo_wall.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace dojo_wall.Controllers
{
    public class WallController : Controller
    {
        private Context dbContext;

        public WallController(Context context) 
        {
            dbContext = context;
        }

        [Route("wall")]
        [HttpGet]
        public IActionResult Wall()
        {
            int? currUserId = HttpContext.Session.GetInt32("UserId");
            if (currUserId == null) {
                return Redirect("/");
            }
            WallViewModel vm = new WallViewModel();
            vm.messages = dbContext.Messages
                .Include(m => m.user)
                .Include(c => c.Comments)
                .ThenInclude(cu => cu.user)
                .OrderByDescending(d => d.CreatedAt).ToList();
            vm.currUser = dbContext.Users.FirstOrDefault(u => u.UserId == currUserId);
            return View("Wall", vm);
        }

        [Route("post_message")]
        [HttpPost]
        public IActionResult postMessage(WallViewModel vm)
        {
            if (ModelState.IsValid) {
                int? currUserId = HttpContext.Session.GetInt32("UserId");
                if (currUserId == null) {
                    return Redirect("/");
                }
                Message msg = vm.newMsg;
                msg.UserId = (int)currUserId;
                msg.CreatedAt = DateTime.Now;
                dbContext.Messages.Add(msg);
                dbContext.SaveChanges();
                return Redirect("Wall");
            }
            return View("Wall", vm);
        }
        [Route("post_comment")]
        [HttpPost]
        public IActionResult postComment(WallViewModel vm)
        {
            if (ModelState.IsValid) {
                int? currUserId = HttpContext.Session.GetInt32("UserId");
                if (currUserId == null) {
                    return Redirect("/");
                }
                Comment cmt = vm.newCmt;
                Message msg = dbContext.Messages.FirstOrDefault(m => m.Id == cmt.MessageId);
                cmt.UserId = (int)currUserId;
                cmt.user = dbContext.Users.FirstOrDefault(u => u.UserId == cmt.UserId);
                cmt.CreatedAt = DateTime.Now;
                //msg.Comments.Add(cmt);
                dbContext.Comments.Add(cmt);
                dbContext.SaveChanges();
                return Redirect("Wall");
            }
            return View("Wall", vm);
        }

    }
}
