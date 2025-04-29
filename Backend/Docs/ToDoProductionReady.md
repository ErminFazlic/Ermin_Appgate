### Not working

Currently the testing of the frontend components is not working. The tests give error message "Support for the experimental syntax 'jsx' isn't currently enabled".
I have not done testing of React components before and I am not sure how to fix this.

I have tried to add the babel preset for react but it does not work and
also tried to add the babel preset for typescript but it does not work either. Due to the time constraints of this assignment I will leave it like this.

### These are a few of the immediate improvements I would prioritize for a production-ready version of this MVP

- Setup prod/dev configurations for ports, secrets etc.
- Use password hashing, i.e. not storing the passwords in plain text
- Removing all secrets from the code and storing them in environment variables
- Implementing a proper logging system
- Implementing a proper error handling system
- Get as close as possible to full test coverage
- Implementing validity checks of JWT's and ability to refresh the tokens
- Implement a proper database for persistent storage
- Store JWT's as cookies or something better than local storage
- Implement a proper auth flow (Password strength, verify email, reset password, confirm email/password etc.)
- Ensure compliance with GDPR and other data protection regulations
- UI changes (Using js built in alert right now, disable buttons when forms are empty, etc.)
