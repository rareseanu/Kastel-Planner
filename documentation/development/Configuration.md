If you have not yet completed the setup stage, please go to the [installation guide](Installation.md).

# Backend
The backend is configured through key-value pairs. 

These pairs can be set using a `.json` file that the application reads when it starts.

Depending on the runtime environment you're targeting, you should modify the `app.settings.Development.json`
or the `app.settings.json` file. Both .json files can be found inside the home directory of the backend project.

## Required

### User authentication & authorization (JwtConfig)

| Key    | Type     | Description                                                                                  |
|--------|----------|----------------------------------------------------------------------------------------------|
|`Secret`| `string` | Used to sign the JWT tokens so we can verify the integrity of the claims contained within it.<br/>Its value should be in the form of a long password.|

### Emails (SmtpConfig)

This application uses emails as a way of validating the user after performing certain actions.

You will need to set the following keys using credentials from your SMTP server provider.

| Key        | Type     | Description                                                  |
|------------|----------|--------------------------------------------------------------|
|`Host`      | `string` | Outgoing mail server name. Most often smtp.yourprovider.com  |     
|`Port`      | `string` | The port number your outgoing mail server uses.              |
|`Username`  | `string` | Your SMTP username.                                          |
|`Password`  | `string` | Your SMTP password.                                          |
|`From`      | `string` | The name you want your email recipients to see.              |

### Database connection (ConnectionStrings)

| Key               | Type     | Description                                                                               |
|-------------------|----------|-------------------------------------------------------------------------------------------|
|`DefaultConnection`| `string` | Ex: "Server=localhost;Username=user; Password=pass;Database=KastelPlanner"                |

# Frontend

The only configuration required to run the frontend Angular application is to modify the `BASE_API_URL` accordingly. 
It can be found in `src\environments\environment(.prod).ts`.

| Key           | Type     | Description                                                                               |
|---------------|----------|-------------------------------------------------------------------------------------------|
|`BASE_API_URL` | `string` | The URL on which the backend can be publicly reached. Ex. `https://localhost:44336/api`.<br/> The path following the port number should always be `/api`.|