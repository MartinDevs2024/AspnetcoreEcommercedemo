const baseURL = "https://fakestoreapi.com/products";
const newProduct = document.getElementById("newProducts");
const searchBar = document.getElementById("searchBar");
const btnContainer = document.querySelector("#btn-container");
let products = [];
let index = 0;
let pages = [];

const setupUI = () => {
    displayProducts(pages[index])
    displayButtons(btnContainer, pages, index)
}

const init = async () => {
    const products = await loadProducts();
    pages = paginate(products);
    setupUI();
}

btnContainer.addEventListener('click', function (e) {
    if (e.target.classList.contains('btn-container')) return
    if (e.target.classList.contains('page-btn')) {
        index = parseInt(e.target.dataset.index);
    }
    if (e.target.classList.contains('next-btn')) {
        index++
        if (index > pages.length - 1) {
            index = 0
        }
    }
    if (e.target.classList.contains('prev-btn')) {
        index--
        if (index < 0) {
            index = pages.length - 1
        }
    }
    setupUI();
})

//SearchBar
searchBar.addEventListener("keyup", function (e) {
    const searchString = e.target.value.toLowerCase();
    const filterProducts = products.filter(product => {
        return product.category.toLowerCase().includes(searchString) ||
            product.title.toLowerCase().includes(searchString);
    });
    displayProducts(filterProducts);
});

//load Products
const loadProducts = async () => {
    try {
        const res = await fetch(baseURL);
        products = await res.json();
        displayProducts(products);
        if (!res.ok) throw new Error(`${products.message} ${res.status}`);
        return products;
    }
    catch (err) {
        console.log(err);
    }
}

const displayProducts = (products) => {
    const htmlString = products.map((product) => {
        return `
                          <div class="col-md-4">
                               <div class="card">
                                   <img class="card-img-top" width="40" src="${product.image}" alt="card image cap">
                                   <div class="card-body">
                                        <h5 class="card-title">${product.category}</h5>
                                        <a href="#" class="btn btn-primary">Go to Products</a>
                                   </div>
                              </div>
                         </div>`;
    }).join('');
    newProduct.innerHTML = htmlString;
}
loadProducts();

const paginate = (product) => {
    const itemsPerPage = 10;
    const numberOfPages = Math.ceil(product.length / itemsPerPage);

    const newProduct = Array.from({ length: numberOfPages }, (_, index) => {
        const start = index * itemsPerPage;
        return product.slice(start, start + itemsPerPage)
    })
    return newProduct;
}

const displayButtons = (btnContainer, pages, activeIndex) => {
    let btns = pages.map((_, pageIndex) => {
        return `<button class="page-btn ${activeIndex === pageIndex
            ? 'active-btn' : 'null'}" data-index="${pageIndex}">
                    ${pageIndex + 1}</button>`
    });
    btns.push(`<button class="prev-btn">prev</button>`)
    btnContainer.innerHTML = btns.join('');

}
displayButtons(btnContainer, pages, index);
window.addEventListener('load', init)