document.querySelector("#menu-toggle").addEventListener("click", function (e) {
    e.preventDefault();
    const wrapper = document.querySelector("#wrapper");
    wrapper.classList.toggle('toggled');
})