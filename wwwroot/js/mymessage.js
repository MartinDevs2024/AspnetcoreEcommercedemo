//Get values
let name, email, message;
let submitForm = document.querySelector("#messageForm");
//Add Even Listener
submitForm.addEventListener("submit", submitData);
function getValues() {
    name = document.getElementById("MyMessage_Name").value;
    email = document.getElementById("MyMessage_Email").value;
    message = document.getElementById("MyMessage_Message").value;
}
// Validation
function validate() {
    if (name === "") {
        alert("Please enter your name !!!");
        return false;
    };
    if (email === "") {
        alert("Please enter your email !!!");
        return false;
    };
    if (message === "") {
        alert("Please enter your Message !!!");
        return false;
    };
}
// Submit Function
function submitData(e) {
    e.preventDefault();
    getValues();
    validate();
    fetch('/UI/Messages/Create', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name: name, email: email, message: message })
    })
        .then((res) => res.json())
        .then((data) => {
            submitForm.reset();
            toastr.success('Message Send');
        })
        .catch((error) => {
            console.error('Error', error);
        })
}