using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bd2_msft_project.Models.DBModels;
using bd2_msft_project.Utils.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bd2_msft_project.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly DataGetter dbGet;
        private readonly DateEditor dbEdit;

        private List<Product> allProducts;

        public ProductsController(ILogger<ProductsController> logger)
        {
            dbGet = new DataGetter();
            dbEdit = new DateEditor();

            _logger = logger;
        }

        private async Task LoadProducts()
        {
            await Task.Run(() => allProducts = dbGet.GetAllProducts());
        }

        private async Task AddOrEditProductAsync(Product givenProduct)
        {
            await Task.Run(() =>
            {
                Product toRemove = allProducts.FirstOrDefault(x => x.ID == givenProduct.ID);
                if (toRemove != null)
                {
                    allProducts.Remove(toRemove);
                }

                allProducts.Add(givenProduct);
                dbEdit.AddOrEditProduct(givenProduct);
            });
        }

        private async Task DeleteProductAsync(int productID)
        {
            await Task.Run(() =>
            {
                Product toRemove = allProducts.FirstOrDefault(x => x.ID == productID);
                if (toRemove != null)
                {
                    allProducts.Remove(toRemove);
                    dbEdit.DeleteProductByID(productID);
                }
            });

        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            await LoadProducts();

            return View(allProducts);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await LoadProducts();
            var product = allProducts.FirstOrDefault(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProductName,ProductDescription,DateAdded,RemainingStock,UnitPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                await LoadProducts();

                product.DateAdded = DateTime.UtcNow.Date;
                product.ID = allProducts.Count + 1;

                await AddOrEditProductAsync(product);

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await LoadProducts();
            var product = allProducts.FirstOrDefault(x => x.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductName,ProductDescription,DateAdded,RemainingStock,UnitPrice")] Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await LoadProducts();
                    await AddOrEditProductAsync(product);
                }
                catch
                {
                    if (allProducts.FirstOrDefault(x => x.ID == id) == null)
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await LoadProducts();
            var product = allProducts.FirstOrDefault(x => x.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await DeleteProductAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
