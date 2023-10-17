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