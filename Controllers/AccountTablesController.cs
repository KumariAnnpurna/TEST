using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.XSSF.UserModel;
using Test.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Microsoft.AspNetCore.Hosting;

namespace Test.Controllers
{
    public class AccountTablesController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly Context _context;

        public AccountTablesController(Context context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: AccountTables
        public async Task<IActionResult> Index()
        {
            return View(await _context.AccountTables.ToListAsync());
        }

        // GET: AccountTables/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountTable = await _context.AccountTables
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (accountTable == null)
            {
                return NotFound();
            }

            return View(accountTable);
        }
        public async Task<IActionResult> OnPostExport()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"demo.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Account-Details");
                IRow row = excelSheet.CreateRow(0);



                row.CreateCell(0).SetCellValue("FirstName");
                row.CreateCell(1).SetCellValue("LastName");
                row.CreateCell(2).SetCellValue("Address");
                row.CreateCell(3).SetCellValue("Mobile");
                row.CreateCell(4).SetCellValue("Email");
                row.CreateCell(5).SetCellValue("Country");
                row.CreateCell(6).SetCellValue("State");
                row.CreateCell(7).SetCellValue("Amount");
                
                List<AccountTable> employees = _context.AccountTables.ToList<AccountTable>();
                int i = 1;
                foreach (var emp in employees)
                {

                    row = excelSheet.CreateRow(i);
                    row.CreateCell(0).SetCellValue(emp.FirstName);
                    row.CreateCell(1).SetCellValue(emp.LastName);
                    row.CreateCell(1).SetCellValue(emp.Address);
                    row.CreateCell(1).SetCellValue(emp. Mobile);
                    row.CreateCell(1).SetCellValue(emp. Email);
                    row.CreateCell(1).SetCellValue(emp. Country);
                    row.CreateCell(1).SetCellValue(emp.State);
                    row.CreateCell(2).SetCellValue(emp.Amount.ToString());
                    i++;

                }
                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }
      

        // GET: AccountTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,Address,Mobile,Email,Country,State,Amount,UserName,Password")] AccountTable accountTable)
        {
            if (ModelState.IsValid)
            {
                accountTable.UserId = Guid.NewGuid();
               

                _context.Add(accountTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountTable);
        }

        // GET: AccountTables/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountTable = await _context.AccountTables.FindAsync(id);
            if (accountTable == null)
            {
                return NotFound();
            }
            return View(accountTable);
        }

        // POST: AccountTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,FirstName,LastName,Address,Mobile,Email,Country,State,Amount,UserName,Password")] AccountTable accountTable)
        {
            if (id != accountTable.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountTableExists(accountTable.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(accountTable);
        }

        // GET: AccountTables/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountTable = await _context.AccountTables
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (accountTable == null)
            {
                return NotFound();
            }

            return View(accountTable);
        }

        // POST: AccountTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var accountTable = await _context.AccountTables.FindAsync(id);
            _context.AccountTables.Remove(accountTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountTableExists(Guid id)
        {
            return _context.AccountTables.Any(e => e.UserId == id);
        }
    }
}
