// ============== Declare Values =====================

const baseUrl = '/api/Blog';
const sectionCard = document.querySelector("#card");
const searchBar = document.getElementById("searchBar");
const btnContainer = document.querySelector("#btn-container");
let blogsCharacters;
let index = 0;
let pages = [];

const setupUI = () => {
    displayPostItems(pages[index])
    displayButtons(btnContainer, pages, index)
}

const init = async () => {
    const blogs = await loadBlogs();
    pages = paginate(blogs);
    setupUI();
}

btnContainer.addEventListener('click', function (e) {
    if (e.target.classList.contains('btn-container')) return;
    if (e.target.classList.contains('page-btn')) {
        index = parseInt(e.target.dataset.index)
    }
    if (e.target.classList.contains('next-btn')) {
        index++
        if (index > pages.length - 1) {
            index = 0;
        }
    }
    if (e.target.classList.contains('prev-btn')) {
        index--
        if (index < 0) {
            index = pages.length - 1;
        }
    }
    setupUI();
});

//Search Function
searchBar.addEventListener('keyup', (e) => {
    const searchString = e.target.value.toLowerCase();
    const filterBlogs = blogsCharacters.filter(blog => {
        return blog.title.toLowerCase().includes(searchString) ||
            blog.category.toLowerCase().includes(searchString);
    });
    displayPostItems(filterBlogs);
});

// Fetch the items
const loadBlogs = async () => {
    try {
        const res = await fetch(baseUrl);
        blogsCharacters = await res.json();
        if (!res.ok) throw new Error(`${blogsCharacters.message} ${res.status}`);
        return blogsCharacters;
    } catch (err) {
        console.log(err);
    }
}
loadBlogs();

//=============== Display Items to screen 

const displayPostItems = (blogs) => {
    const htmlString = blogs.map((blog) => {
    //const htmlString = blogs.data.map((blog) => {
        return `
              <div class="card mb-4 posts">
                  <div class="item ${blog.category}">
                            <img class="card-img-top img-fluid" src="/content/blog/${blog.image}" alt="${blog.title}" />
                               
                            <div class="card-body">
                                <h2 class="card-title">${blog.title}</h2>
                                <a asp-action="Detail" href="/UI/Blog/Details/${blog.id}" class="btn btn-primary">Read More</a>
                            </div>
                            <div class="card-footer text-muted">
                                Posted on ${new Date(blog.created).toLocaleDateString()} by
                                <a href="#">Martin</a>
                            </div>
                    </div>
              </div>
              `;
    }).join('');
    if (htmlString.length < 1) {
        sectionCard.innerHTML = "<h2>Sorry, no blog matched your search</h2>";
    } else {
        sectionCard.innerHTML = htmlString;
    }

}
loadBlogs();

//Filter function
function displayMenuButtons() {
    const filterBtns = document.querySelectorAll('.filter-btn');
    //filter 
    filterBtns.forEach(function (btn) {
        btn.addEventListener('click', function (e) {
            const category = e.currentTarget.dataset.id;
            const menuType = blogsCharacters.filter(function (blog) {
                console.log(blog.category);
                if (blog.category === category) {
                    return blog;
                }
            });
            if (category === 'all') {
                displayPostItems(blogsCharacters);
            } else {
                displayPostItems(menuType);
            }
        })
    })
}

displayMenuButtons();

const paginate = (blogs) => {
    const itemsPerPage = 5;
    const numberOfPages = Math.ceil(blogs.length / itemsPerPage);
    const newProduct = Array.from({ length: numberOfPages }, (_, index) => {
        const start = index * itemsPerPage;
        return blogs.slice(start, start + itemsPerPage)
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
window.addEventListener('load', init);





