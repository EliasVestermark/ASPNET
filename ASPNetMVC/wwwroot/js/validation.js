const lengthValidator = (element, minlength = 2) => {

    if (element.length >= minlength) {
        return true;
    } else {
        return false;
    }
}

const checkedValidator = (element) => {
    if (element.checked) {
        return true;
    } else {
        return false;
    }
}

const formErrorHandler = (element, validationResult) => {
    let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`)

    if (validationResult) {
        element.classList.remove('input-validation-error');
        spanElement.classList.remove('field-validation-error');
        spanElement.classList.add('field-validation-valid');
        spanElement.innerHTML = '';
    } else {
        element.classList.add('input-validation-error');
        spanElement.classList.add('field-validation-error');
        spanElement.classList.remove('field-validation-valid');
        spanElement.innerHTML = element.dataset.valRequired;
    }
}

const textValidator = (element) => {
    formErrorHandler(element, lengthValidator(element.value));
}

const emailValidator = (element) => {
    const regEx = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/;
    formErrorHandler(element, regEx.test(element.value));
}

const passwordValidator = (element) => {
    if (element.dataset.valEqualtoOther !== undefined) {
        if (element.value === document.getElementsByName(element.dataset.valEqualtoOther.replace('*', 'Form'))[0].value) {
            formErrorHandler(element, true);
        } else {
            formErrorHandler(element, false);        }

        
    } else {
        const regEx = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
        formErrorHandler(element, regEx.test(element.value));
    }
}

const checkboxValidator = (element) => {
    formErrorHandler(element, checkedValidator(element));
}

let forms = document.querySelectorAll('form');

forms.forEach(form => {
    let inputs = form.querySelectorAll('input, textarea')

    inputs.forEach(input => {
        if (input.dataset.val === 'true') {

            if (input.type === 'checkbox') {

                input.addEventListener('change', (e) => {
                    checkboxValidator(e.target);
                })
            } else {

                if (input.tagName.toLowerCase() === 'textarea') {

                    input.addEventListener('keyup', (e) => {
                        textValidator(e.target);
                    })
                } else {
                    input.addEventListener('keyup', (e) => {

                        switch (e.target.type) {
                            case 'text':
                                textValidator(e.target);
                                break;
                            case 'email':
                                emailValidator(e.target);
                                break;
                            case 'password':
                                passwordValidator(e.target);
                                break;
                        }
                    })
                }
            }
        }
    })
})
