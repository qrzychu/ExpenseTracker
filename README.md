# How to run

Simply run `docker compose up` in the root directory of the project.

Navigate to `https://localhost:5001` to view the app.

# Basic premise

Each user has their own list of expenses and expense types, accessible after login.

# Things I would improve

Frontend:

- styling
- error handling for http requests - expected errors are still logged in console
- component for validation message
- figure out why importing vuelidate breaks typescript compiler
- some kind of expense type management

Backend:

- maybe reorganize program.cs
- integration and unit tests
