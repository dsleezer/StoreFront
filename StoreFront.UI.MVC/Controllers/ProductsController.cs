﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;
using System.Drawing;
using StoreFront.UI.MVC.Utilities;
using X.PagedList;

namespace StoreFront.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly StoreFrontContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(StoreFrontContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var storeFrontContext = _context.Products.Include(p => p.ProductType).Include(p => p.StockStatus).Include(p => p.Supplier).Include(p => p.OrderProducts).Include(p => p.Games);
            return View(await storeFrontContext.ToListAsync());
        }

        public async Task<IActionResult> TileView(string searchTerm, int productTypeId = 0, int page = 1)
        {
            int pageSize = 12;

            //TODO figure out how to pull all product type names to populate into a list
            //ViewBag.TypeName = _context.ProductTypes.Include(p => p.TypeName).ToList();


            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "TypeName");
            ViewBag.ProductType = 0;
            

            //ALL Products
            var products = _context.Products.Include(p => p.ProductType).Include(p => p.StockStatus).Include(p => p.Supplier).Include(p => p.OrderProducts)
                .Include(p => p.Games).ThenInclude(p => p.Genre).Include(p => p.Games).ThenInclude(p => p.GameType).ToList();
                        
            //Sort By Products DDL
            if (productTypeId != 0)
            {
                products = products.Where(p => p.ProductTypeId == productTypeId).ToList();

                ViewData["ProductyTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "ProductyTypeName", productTypeId);
                ViewBag.ProductType = productTypeId;
            }

            #region Optional Search Filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                int searchInt = 0;

                Int32.TryParse(searchTerm, out searchInt);

                products = products.Where(p => p.ProductName.ToLower().Contains(searchTerm.ToLower())
                                    || p.Description.ToLower().Contains(searchTerm)
                                    || p.Games.Any(g => g.MinPlayers <= searchInt && g.MaxPlayers >= searchInt)
                                    || p.Games.Any(g => g.GameType.Name.ToLower().Contains(searchTerm.ToLower()))
                                    || p.Games.Any(g => g.Genre.Genre1.ToLower().Contains(searchTerm.ToLower()))
                ).ToList();

                ViewBag.NbrResults = products.Count;
                ViewBag.SearchTerm = searchTerm;
            }
            else
            {
                ViewBag.NbrResults = null;
                ViewBag.NbrResults = null;
            }


            #endregion


            return View(products.ToPagedList(page, pageSize));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.StockStatus)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public async Task<IActionResult> StyledDetails(int? id)
        {

            ViewBag.Genres = _context.Genres.Where(x => x.Games.Select(y => y.ProductId).SingleOrDefault() == id);

            ViewBag.GameType = _context.GameTypes.Where(x => x.Games.Select(y => y.ProductId).SingleOrDefault() == id);
                        
            //ViewBag.Games = _context.Games.Select(y => y.ProductId).SingleOrDefault() == id;
          

            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.StockStatus)
                .Include(p => p.Supplier)
                .Include(p => p.Games)
                .ThenInclude(p => p.Genre)
                .Include(p => p.Games)
                .ThenInclude(p => p.GameType)
                
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "TypeName");
            ViewData["StockStatusId"] = new SelectList(_context.StockStatuses, "StockStatusId", "StatusName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Address");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Price,Description,UnitsInStock,UnitsOnOrder,IsActive,SupplierId,PhotoUrl,StockStatusId,ProductTypeId,Image")] Product product)
        {
            if (ModelState.IsValid)
            {
                #region File Upload - CREATE
                if (product.Image !=null)
                {
                    string ext = Path.GetExtension(product.Image.FileName);

                    string[] validExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    if (validExts.Contains(ext.ToLower()) && product.Image.Length < 4_194_303)
                    {

                        product.PhotoUrl = Guid.NewGuid() + ext;

                        string webRootPath = _webHostEnvironment.WebRootPath;

                        string fullImagePath = webRootPath + "/img/";

                        using (var memoryStream = new MemoryStream())
                        {
                            await product.Image.CopyToAsync(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                int maxImageSize = 500;
                                int maxThumbSize = 100;

                                ImageUtility.ResizeImage(fullImagePath, product.PhotoUrl, img, maxImageSize, maxThumbSize);
                            }
                        }
                    }
                }
                else
                {
                    product.PhotoUrl = "noimage.png";
                }


                #endregion

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "TypeName", product.ProductTypeId);
            ViewData["StockStatusId"] = new SelectList(_context.StockStatuses, "StockStatusId", "StatusName", product.StockStatusId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Address", product.SupplierId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "TypeName", product.ProductTypeId);
            ViewData["StockStatusId"] = new SelectList(_context.StockStatuses, "StockStatusId", "StatusName", product.StockStatusId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Address", product.SupplierId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Price,Description,UnitsInStock,UnitsOnOrder,IsActive,SupplierId,PhotoUrl,StockStatusId,ProductTypeId,Image")] Product product)
        {
            string oldImageName = product.PhotoUrl;

            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(product.Image != null)
                {
                    string ext = Path.GetExtension(product.Image.FileName); 
                    string[] validExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    if (validExts.Contains(ext.ToLower()) && product.Image.Length < 4_194_303)
                    {
                        product.PhotoUrl = Guid.NewGuid() + ext;

                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string fullPath = webRootPath + "/img/";

                        if (oldImageName != "noImage.png")
                        {
                            ImageUtility.Delete(fullPath, oldImageName);
                        }

                        using (var memoryStream = new MemoryStream())
                        {
                            await product.Image.CopyToAsync(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                int maxImageSize = 500;
                                int maxThumbSize = 100;
                                ImageUtility.ResizeImage(fullPath, product.PhotoUrl, img, maxImageSize, maxThumbSize);
                            }
                        }
                    }
                }
                
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["ProductTypeId"] = new SelectList(_context.ProductTypes, "ProductTypeId", "TypeName", product.ProductTypeId);
            ViewData["StockStatusId"] = new SelectList(_context.StockStatuses, "StockStatusId", "StatusName", product.StockStatusId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Address", product.SupplierId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.StockStatus)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'StoreFrontContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
