// ============== Declare Values =====================

const baseUrl = '/api/Blog';
const sectionCard = document.querySelector("#card");
let blogs;

const loadBlogs = async () => {
    try {
        const res = await fetch(baseUrl);
        blogs = await res.json();
        console.log(blogs);
        displayPostItems(blogs);
        if (!res.ok) throw new Error(`${blogs.message} ${res.status}`);
        return blogs;
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
    sectionCard.innerHTML = htmlString;
}


