
//function to validate the password
function validatePassword() {

    var passwordInput = document.getElementById("password");
    var passwordError = document.getElementById("password-error");
    var password = passwordInput.value;

    var minLength = 8;
    var hasUpperCase = /[A-Z]/.test(password);
    var hasLowerCase = /[a-z]/.test(password);
    var hasDigit = /\d/.test(password);
    var hasSpecialChar = /[!@#$%^&*]/.test(password);


    if (password.trim() === "") {

        passwordError.textContent = "Empty password";
    }
    else if (password.length < minLength) {

        passwordError.textContent = "Password must be at least " + minLength + " characters long.";

    }
    else if (!(hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar)) {

        passwordError.textContent = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.";

    }
    else {
        passwordError.textContent = "";
    }
}

function CheckPassword() {
    var password = document.getElementById("password").value;
    var confirmPassword = document.getElementById("confirm-password").value;

    if (password !== confirmPassword) {
        document.getElementById("password-error2").innerText = "Passwords do not match.";
    } else {
        document.getElementById("password-error2").innerText = "";
    }
}





//function to calculate age
function calculateAge() {
    var dobInput = document.getElementById("dob").value;
    var dobError = document.getElementById("dob-error");

    var dobDate = new Date(dobInput);
    var currentDate = new Date();
    var age = currentDate.getFullYear() - dobDate.getFullYear();
    document.getElementById("age").value = age;

    if (age < 18) {
        dobError.textContent = "Age must be above 18";
    }
    else {
        dobError.textContent = "";
    }
}


//calender date validation
function setMaxDate() {
    var today = new Date();
    var day = today.getDate();
    var month = today.getMonth() + 1; // Months are 0-based
    var year = today.getFullYear();

    if (day < 10) {
        day = '0' + day;
    }
    if (month < 10) {
        month = '0' + month;
    }

    var maxDate = year + '-' + month + '-' + day;
    document.getElementById("dob").setAttribute("max", maxDate);
}


//function validate phone number
function validateNumber() {
    var phoneNumberInput = document.getElementById("phone");
    var phoneError = document.getElementById("number-error");

    var phoneNumber = phoneNumberInput.value;


    phoneNumber = phoneNumber.replace(/\D/g, "");


    if (phoneNumber.length !== 10) {

        phoneError.textContent = 'Phone number must be 10 digits';
    }
    else {

        phoneError.textContent = '';

    }
}

//function to validate email
function validateEmail() {
    const emailElement = document.getElementById('email');
    const validationMessageElement = document.getElementById('email-error');
    const emailValue = emailElement.value;


    const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

    if (emailRegex.test(emailValue)) {
        validationMessageElement.textContent = '';
    } else {
        validationMessageElement.textContent = 'Email is not valid. Please enter a valid email address.';
    }
}

//function to validate empty input
function validateEmptyInput(inputId) {
    const inputElement = document.getElementById(inputId);
    const validationMessageElement = document.getElementById(inputId + 'error');
    const inputValue = inputElement.value.trim();

    if (inputValue === '') {
        validationMessageElement.textContent = inputId + ' cannot be empty';
    } else {
        validationMessageElement.textContent = '';
    }
}